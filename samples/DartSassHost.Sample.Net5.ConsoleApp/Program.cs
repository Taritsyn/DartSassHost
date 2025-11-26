using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Core;

using DartSassHost.Sample.Logic;

namespace DartSassHost.Sample.Net5.ConsoleApp
{
	class Program : CompilationExampleBase
	{
		static Program()
		{
			IJsEngineSwitcher engineSwitcher = JsEngineSwitcher.Current;
			engineSwitcher.EngineFactories
				.AddChakraCore()
				;
			engineSwitcher.DefaultEngineName = ChakraCoreJsEngine.EngineName;

		}

		static void Main()
		{
			CompileContent();
			CompileFile();
		}
	}
}