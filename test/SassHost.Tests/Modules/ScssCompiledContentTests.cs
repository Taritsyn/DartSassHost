using System;

using NUnit.Framework;

namespace SassHost.Tests.Modules
{
	[TestFixture]
	public sealed class ScssCompiledContentTests : CompiledContentTestsBase
	{
		public ScssCompiledContentTests()
			: base(SyntaxType.Scss)
		{ }
	}
}