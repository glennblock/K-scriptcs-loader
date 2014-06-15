using Microsoft.Framework.Runtime;
using ScriptCs;
using ScriptCs.Contracts;
using ScriptCs.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ScriptCsLoader
{
    /// <summary>
    /// Summary description for Program
    /// </summary>
    public class Program
    {
        private readonly IApplicationEnvironment _environment;
        private readonly ILibraryManager _libraryManager;
        private readonly IAssemblyLoaderEngine _loaderEngine;
        private readonly IAssemblyResolver _assemblyResolver;

        public Program(IApplicationEnvironment environment,
                       ILibraryManager libraryManager,
                       IAssemblyLoaderEngine loaderEngine)
        {
            _environment = environment;
            _libraryManager = libraryManager;
            _loaderEngine = loaderEngine;

            var references = new List<string>();

            foreach (var lib in libraryManager.GetLibraries())
            {
                //skip project.json
                if (lib.Path.EndsWith(".dll"))
                    references.Add(lib.Path);
            }

            _assemblyResolver = new KAssemblyResolver(references);
        }


        public void Main(string[] args)
        {
            var programArgs = Environment.GetCommandLineArgs();
            var script = programArgs[programArgs.Length - 1];
           
            var services = CreateScriptServices(script);
            var scriptExecutor = services.Executor;
            var scriptPackResolver = services.ScriptPackResolver;

            scriptExecutor.Initialize(_assemblyResolver.GetAssemblyPaths(""), scriptPackResolver.GetPacks());
            var scriptResult = scriptExecutor.Execute(script, "");
            scriptExecutor.Terminate();
        }

        private ScriptServices CreateScriptServices(string script)
        {
            var console = new ScriptConsole();
            var configurator = new LoggerConfigurator(LogLevel.Info);
            configurator.Configure(console);
            var logger = configurator.GetLogger();
            ScriptServicesBuilder builder = (ScriptServicesBuilder) new ScriptServicesBuilder(console, logger)
                .ScriptName(script);
            builder.Overrides[typeof(IAssemblyResolver)] = _assemblyResolver;
            builder.ScriptEngine<ScriptCs.Engine.Roslyn.RoslynScriptEngine>();
            //builder.LoadModules(Path.GetExtension(script));
            return builder.Build();
        }
    }
}