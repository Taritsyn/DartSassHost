using System;

using NUnit.Framework;

namespace SassHost.Tests.Imports
{
	[TestFixture]
	public sealed class ScssCompiledContentTests : CompiledContentTestsBase
	{
		public ScssCompiledContentTests()
			: base(SyntaxType.Scss)
		{ }
	}
}