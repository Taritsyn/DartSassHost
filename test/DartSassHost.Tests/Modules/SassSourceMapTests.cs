using NUnit.Framework;

namespace DartSassHost.Tests.Modules
{
	[TestFixture]
	public sealed class SassSourceMapTests : SourceMapTestsBase
	{
		public SassSourceMapTests()
			: base(SyntaxType.Sass)
		{ }
	}
}