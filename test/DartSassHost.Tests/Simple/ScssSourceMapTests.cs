using NUnit.Framework;

namespace DartSassHost.Tests.Simple
{
	[TestFixture]
	public sealed class ScssSourceMapTests : SourceMapTestsBase
	{
		public ScssSourceMapTests()
			: base(SyntaxType.Scss)
		{ }
	}
}