using System;

using NUnit.Framework;

namespace DartSassHost.Tests.Simple
{
	[TestFixture]
	public sealed class SassCompiledContentTests : CompiledContentTestsBase
	{
		public SassCompiledContentTests()
			: base(SyntaxType.Sass)
		{ }
	}
}