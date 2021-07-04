using NUnit.Framework;

namespace DartSassHost.Tests.Modules
{
	[TestFixture]
	public sealed class ScssCompiledContentTests : CompiledContentTestsBase
	{
		public ScssCompiledContentTests()
			: base(SyntaxType.Scss)
		{ }
	}
}