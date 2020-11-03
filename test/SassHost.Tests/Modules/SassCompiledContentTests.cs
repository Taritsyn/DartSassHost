using System;

using NUnit.Framework;

namespace SassHost.Tests.Modules
{
	[TestFixture]
	public sealed class SassCompiledContentTests : CompiledContentTestsBase
	{
		public SassCompiledContentTests()
			: base(SyntaxType.Sass)
		{ }
	}
}