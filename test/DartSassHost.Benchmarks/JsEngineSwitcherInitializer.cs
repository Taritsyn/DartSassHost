using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Core;

namespace DartSassHost.Benchmarks
{
	internal static class JsEngineSwitcherInitializer
	{
		private static readonly object _synchronizer = new object();
		private static bool _initialized;


		public static void Initialize()
		{
			if (!_initialized)
			{
				lock (_synchronizer)
				{
					if (!_initialized)
					{
						IJsEngineSwitcher engineSwitcher = JsEngineSwitcher.Current;
						engineSwitcher.EngineFactories
							.AddChakraCore()
							;
						engineSwitcher.DefaultEngineName = ChakraCoreJsEngine.EngineName;

						_initialized = true;
					}
				}
			}
		}
	}
}