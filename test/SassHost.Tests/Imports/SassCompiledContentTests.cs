using System;

using NUnit.Framework;

namespace SassHost.Tests.Imports
{
	[TestFixture]
	public sealed class SassCompiledContentTests : CompiledContentTestsBase
	{
		public SassCompiledContentTests()
			: base(SyntaxType.Sass)
		{ }
	}
}