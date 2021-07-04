using NUnit.Framework;

namespace DartSassHost.Tests.Imports
{
	[TestFixture]
	public sealed class ScssIncludedFilePathsTests : IncludedFilePathsTestsBase
	{
		public ScssIncludedFilePathsTests()
			: base(SyntaxType.Scss)
		{ }
	}
}