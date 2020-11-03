using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.ChakraCore;

namespace SassHost.Tests
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