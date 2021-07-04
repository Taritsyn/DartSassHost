using NUnit.Framework;

namespace DartSassHost.Tests.Imports
{
	[TestFixture]
	public sealed class SassIncludedFilePathsTests : IncludedFilePathsTestsBase
	{
		public SassIncludedFilePathsTests()
			: base(SyntaxType.Sass)
		{ }
	}
}