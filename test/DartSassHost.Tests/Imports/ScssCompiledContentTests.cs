using System;

using NUnit.Framework;

namespace DartSassHost.Tests.Imports
{
	[TestFixture]
	public sealed class ScssCompiledContentTests : CompiledContentTestsBase
	{
		public ScssCompiledContentTests()
			: base(SyntaxType.Scss)
		{ }
	}
}