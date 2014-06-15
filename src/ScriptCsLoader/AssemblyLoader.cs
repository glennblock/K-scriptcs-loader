using System;
using ScriptCs;
using ScriptCs.Contracts;
using System.Reflection;
using System.Runtime.Versioning;
using Microsoft.Framework.Runtime;
using NuGet;
using ScriptCs.Hosting;

namespace ScriptCsLoader
{
    public class AssemblyLoader : IAssemblyLoader
    {
        public Assembly Load(string assemblyName)
        {
            return typeof(Program).Assembly;
        }
    }
}
