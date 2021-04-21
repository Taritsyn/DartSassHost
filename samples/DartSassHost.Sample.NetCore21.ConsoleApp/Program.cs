using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.ChakraCore;

using DartSassHost.Sample.Logic;

namespace DartSassHost.Sample.NetCore21.ConsoleApp
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