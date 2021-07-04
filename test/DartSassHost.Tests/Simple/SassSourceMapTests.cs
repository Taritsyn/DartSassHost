using NUnit.Framework;

namespace DartSassHost.Tests.Simple
{
	[TestFixture]
	public sealed class SassSourceMapTests : SourceMapTestsBase
	{
		public SassSourceMapTests()
			: base(SyntaxType.Sass)
		{ }
	}
}