using ScriptCs.Contracts;
using System;
using System.Collections.Generic;

namespace ScriptCsLoader
{
    /// <summary>
    /// Summary description for KAssemblyResolver
    /// </summary>
    public class KAssemblyResolver : IAssemblyResolver
    {
        private IList<string> _assemblies;

        public KAssemblyResolver(IList<string> assemblies)
        {
            _assemblies = assemblies;
        }

        public IEnumerable<string> GetAssemblyPaths(string path)
        {
            return _assemblies;
        }
    }
}