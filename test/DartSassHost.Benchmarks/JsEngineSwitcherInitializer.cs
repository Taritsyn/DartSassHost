#if NET10_0_OR_GREATER
using Lock = System.Threading.Lock;
#else
using Lock = System.Object;
#endif

using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Core;

namespace DartSassHost.Benchmarks
{
	internal static class JsEngineSwitcherInitializer
	{
		private static readonly Lock _synchronizer = new Lock();
		private static bool _initialized;


		public static void Initialize()
		{
			if (_initialized)
			{
				return;
			}

			lock (_synchronizer)
			{
				if (_initialized)
				{
					return;
				}

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