// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseSupport.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the AutofacBootstrapper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;

    using Microsoft.SqlServer.Management.Common;
    using Microsoft.SqlServer.Management.Smo;

    /// <summary>
    /// Class provides functionality to help developer perform unit testing on data access code
    /// </summary>
    public class DatabaseSupport : IDisposable
    {
        private bool disposed = false;
        private SqlConnection sqlConnection;
        private Server targetDatabaseServer;
        private Server targetServer;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseSupport" /> class.
        /// </summary>
        /// <param name="connectionString">standard connection string for database to interact with</param>
        public DatabaseSupport(string connectionString)
        {
            this.sqlConnection = new SqlConnection(connectionString);
            this.targetServer = new Server(this.sqlConnection.DataSource);
            this.targetDatabaseServer = new Server(new ServerConnection(this.sqlConnection));
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="DatabaseSupport" /> class.
        /// </summary>
        ~DatabaseSupport()
        {
            Dispose(false);
        }

        /// <summary>
        /// Creates a new empty database on the server if one of that name doesn't already exist
        /// </summary>
        /// <param name="name">Name of the database to create</param>
        public void CreateDB(string name)
        {
            if (!this.targetServer.Databases.Contains(name))
            {
                var toCreate = new Microsoft.SqlServer.Management.Smo.Database(this.targetServer, name);
                toCreate.Create();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Drops a database on the server
        /// </summary>
        /// <param name="name">name of the database to drop</param>
        public void DropDB(string name)
        {
            if (this.targetServer.Databases.Contains(name))
            {
                this.targetServer.KillAllProcesses(this.sqlConnection.Database);
                this.targetServer.KillDatabase(name);
            }
        }

        /// <summary>
        /// Method creates\recreates database
        /// </summary>
        /// <param name="createScriptPath">Path to the <b>directory</b> that contains the script(s) for creating database</param>
        /// <remarks>The database is only created\recreated if the script file(s) or database object have changed since last created.
        /// <para>If the a series of scripts is being used then a file Order.txt needs to be provided that simply holds 
        /// a list of the files in the order they are to be run</para></remarks>
        public void RecreateDbSchema(string createScriptPath)
        {
            // 1 - DB does not exist --> recreate, run file(s)
            // 2 - DB exists, but file(s) date  > schema --> file(s) more recent, therefore run it
            // otherwise the DB exists, but file(s) date < schema --> schema has not changed, do nothing.
            DateTime dateLastSchemaFileUpdate = GetLastFileUpdate(createScriptPath);
            DateTime dateLastDbUpdate = GetLastSchemaChange();

            if (dateLastDbUpdate >= DateTime.MaxValue)
            {
                RecreateDbWithData(createScriptPath);
            }
            else if (dateLastSchemaFileUpdate > dateLastDbUpdate)
            {
                RecreateDbWithData(createScriptPath);
            }
        }

        /// <summary>
        /// Runs a specified script against a target database
        /// </summary>
        /// <param name="scriptPath">Path to the script file to execute</param>
        public void RunScript(string scriptPath)
        {
            RunScript(scriptPath, false, null);
        }

        /// <summary>
        /// Runs a specified script against a target database
        /// </summary>
        /// <param name="scriptPath">Path to the script file to execute</param>
        /// <param name="parameters">Parameters to use within the script</param>
        public void RunScript(string scriptPath, Dictionary<string, string> parameters)
        {
            RunScript(scriptPath, false, parameters);
        }

        /// <summary>
        /// Runs a specified script against a target database 
        /// </summary>
        /// <param name="scriptPath">Path to the script file to execute</param>
        /// <param name="returnResults">Flag indicating we expect to be return results from executing the script</param>
        /// <returns>Dataset containing data returned from executing the script</returns>
        public DataSet RunScript(string scriptPath, bool returnResults)
        {
            return RunScript(scriptPath, returnResults, null);
        }

        /// <summary>
        /// Runs a specified script against a target database
        /// </summary>
        /// <param name="scriptPath">Path to the script file to execute</param>
        /// <param name="returnResults">Flag indicating we expect to be return results from executing the script</param>
        /// <param name="parameters">Parameters to use within the script</param>
        /// <returns>Dataset containing data returned from executing the script</returns>
        public DataSet RunScript(string scriptPath, bool returnResults, Dictionary<string, string> parameters)
        {
            DataSet results = null;
            string script = LoadScript(scriptPath);

            // If parameters provided then alter the script accordingly
            if (parameters != null && parameters.Count > 0)
            {
                foreach (KeyValuePair<string, string> parameter in parameters)
                {
                    script = script.Replace(parameter.Key, parameter.Value);
                }
            }

            if (returnResults)
            {
                results = this.targetDatabaseServer.ConnectionContext.ExecuteWithResults(script);
            }
            else
            {
                this.targetDatabaseServer.ConnectionContext.ExecuteNonQuery(script);
            }

            return results;
        }

        /// <summary>
        /// Method to dispose of objects used 
        /// </summary>
        /// <param name="disposing">Flag to indicate we are currently disposing the object</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (this.sqlConnection.State != ConnectionState.Closed)
                    {
                        this.sqlConnection.Close();
                    }

                    this.disposed = true;
                }
            }
        }

        /// <summary>
        /// Get the last write time of specified file
        /// </summary>
        /// <param name="createScriptPath">Path to directory where create script(s) are held.</param>
        /// <returns>DateTime newest file in directory was written to.</returns>
        private static DateTime GetLastFileUpdate(string createScriptPath)
        {
            DirectoryInfo di = new DirectoryInfo(createScriptPath);

            FileInfo[] files = di.GetFiles();

            DateTime lastWriteTime = DateTime.MinValue;

            // Go through all files in the directory and find the latest time a file
            // has been written to
            foreach (FileInfo fi in files)
            {
                if (fi.LastWriteTime > lastWriteTime)
                {
                    lastWriteTime = fi.LastWriteTime;
                }
            }

            return lastWriteTime;
        }

        /// <summary>
        /// Loads the path with the data provided
        /// </summary>
        /// <param name="path">The absolute path where the script is loaded from</param>
        /// <returns>The contents of the file passed in</returns>
        private static string LoadScript(string path)
        {
            string script;

            if (string.IsNullOrEmpty(path))
            {
                throw new InvalidOperationException("No path has been provided for the path. Unable to load.");
            }

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    script = sr.ReadToEnd();
                }
            }
            catch (IOException)
            {
                throw new IOException("Error occurred whilst trying to Load Script");
            }

            return script;
        }

        /// <summary>
        /// Get the data of the last schema change
        /// </summary>
        /// <returns>DateTime the database schema was last changed.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Required for Unit Testing")]
        private DateTime GetLastSchemaChange()
        {
            string sql = "select max(crDate) from sysobjects";
            DateTime lastUpdate = DateTime.MaxValue;
            using (SqlCommand cmd = new SqlCommand(sql, this.sqlConnection))
            {
                try
                {
                    this.sqlConnection.Open();
                    lastUpdate = (DateTime)cmd.ExecuteScalar();
                }
                catch
                {
                    // We need catch here so that when used in conjunction with unit testing the 
                    // unit test doesn't fail at this point.
                }
                finally
                {
                    this.sqlConnection.Close();
                    SqlConnection.ClearPool(this.sqlConnection);
                }
            }

            return lastUpdate;
        }

        /// <summary>
        /// Recreates the database with base data
        /// </summary>
        /// <param name="createScriptPath">The path of the file that contains what should be created</param>
        private void RecreateDbWithData(string createScriptPath)
        {
            if (Directory.GetFiles(createScriptPath).Length > 1)
            {
                // ensure that any processes using the db are killed so that the db can be dropped
                // if the creation script wants to
                this.targetServer.KillAllProcesses(this.sqlConnection.Database);

                // running multiple files to create db
                // open Order.txt to find files to run and the order
                using (StreamReader sr = new StreamReader(Path.Combine(createScriptPath, "Order.txt")))
                {
                    while (sr.Peek() >= 0)
                    {
                        RunScript(Path.Combine(createScriptPath, sr.ReadLine()));
                    }
                }
            }
            else
            {
                // running single file to create db
                string[] file = Directory.GetFiles(createScriptPath);
                RunScript(file[0]);
            }

            // hold off until path has been updated:
            DateTime dateLastDBUpdate = GetLastSchemaChange();
            while (dateLastDBUpdate == DateTime.MaxValue)
            {
                Console.WriteLine("Waiting for new DB schema change to take affect");
                System.Threading.Thread.Sleep(1000);
                dateLastDBUpdate = GetLastSchemaChange();
            }
        }
    }
}