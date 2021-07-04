using NUnit.Framework;

namespace DartSassHost.Tests.Imports
{
	[TestFixture]
	public sealed class SassSourceMapTests : SourceMapTestsBase
	{
		public SassSourceMapTests()
			: base(SyntaxType.Sass)
		{ }
	}
}