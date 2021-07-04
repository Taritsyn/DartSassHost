using NUnit.Framework;

namespace DartSassHost.Tests.Modules
{
	[TestFixture]
	public sealed class SassIncludedFilePathsTests : IncludedFilePathsTestsBase
	{
		public SassIncludedFilePathsTests()
			: base(SyntaxType.Sass)
		{ }
	}
}