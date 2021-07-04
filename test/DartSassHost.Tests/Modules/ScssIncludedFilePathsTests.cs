using NUnit.Framework;

namespace DartSassHost.Tests.Modules
{
	[TestFixture]
	public sealed class ScssIncludedFilePathsTests : IncludedFilePathsTestsBase
	{
		public ScssIncludedFilePathsTests()
			: base(SyntaxType.Scss)
		{ }
	}
}