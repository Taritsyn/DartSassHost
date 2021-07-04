using System.Text;

using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Core;

using DartSassHost.Sample.Logic;

namespace DartSassHost.Sample.NetCore1.ConsoleApp
{
	class Program : CompilationExampleBase
	{
		static Program()
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

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