using System.Threading;

namespace DartSassHost.Utilities
{
	internal struct InterlockedStatedFlag
	{
		private int _counter;


		internal bool IsSet()
		{
			return _counter != 0;
		}

		internal bool Set()
		{
			return Interlocked.Exchange(ref _counter, 1) == 0;
		}
	}
}