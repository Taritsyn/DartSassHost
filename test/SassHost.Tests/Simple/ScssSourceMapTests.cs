using System;

using NUnit.Framework;

namespace SassHost.Tests.Simple
{
	[TestFixture]
	public sealed class ScssSourceMapTests : SourceMapTestsBase
	{
		public ScssSourceMapTests()
			: base(SyntaxType.Scss)
		{ }
	}
}