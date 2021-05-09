using System;

using NUnit.Framework;

namespace DartSassHost.Tests
{
	public abstract class TestsBase
	{
		[SetUp]
		public void Init()
		{
			JsEngineSwitcherInitializer.Initialize();
		}
	}
}