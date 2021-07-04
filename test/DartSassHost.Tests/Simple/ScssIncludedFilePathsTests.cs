using NUnit.Framework;

namespace DartSassHost.Tests.Simple
{
	[TestFixture]
	public sealed class ScssIncludedFilePathsTests : IncludedFilePathsTestsBase
	{
		public ScssIncludedFilePathsTests()
			: base(SyntaxType.Scss)
		{ }
	}
}