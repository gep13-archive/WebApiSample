// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScriptRunner.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the AutofacBootstrapper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Common
{
    using System.Configuration;

    public class ScriptRunner
    {
        private const string ConnectionName = "Simple.Data.Properties.Settings.DefaultConnectionString";

        private readonly DatabaseSupport databaseSupport;

        public ScriptRunner(string connectionStringName)
        {
            var conn = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            this.databaseSupport = new DatabaseSupport(conn);
        }

        public static ScriptRunner Get(string connectionStringName)
        {
            return new ScriptRunner(connectionStringName);
        }

        public void CreateDb(string name)
        {
            this.databaseSupport.CreateDB(name);
        }

        public void DropDb(string name)
        {
            this.databaseSupport.DropDB(name);
        }

        public void Run(string script)
        {
            this.databaseSupport.RunScript(script);
        }
    }
}