using NUnit.Framework;

namespace DartSassHost.Tests.Modules
{
	[TestFixture]
	public sealed class SassCompiledContentTests : CompiledContentTestsBase
	{
		public SassCompiledContentTests()
			: base(SyntaxType.Sass)
		{ }
	}
}