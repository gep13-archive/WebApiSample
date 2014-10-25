// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommonAssemblyInfo.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   CommonAssemblyInfo.cs
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
[assembly: AssemblyCompany("N/A")]
[assembly: AssemblyCopyright("Copyright (c) gep13 and nathangloyn 2014")]
[assembly: AssemblyProduct("Web API Sample Project")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguage("en-GB")]

[assembly: ComVisible(false)]

[assembly: CLSCompliant(true)]