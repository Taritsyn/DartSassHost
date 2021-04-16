using System;

using NUnit.Framework;

namespace DartSassHost.Tests.Imports
{
	[TestFixture]
	public sealed class ScssSourceMapTests : SourceMapTestsBase
	{
		public ScssSourceMapTests()
			: base(SyntaxType.Scss)
		{ }
	}
}