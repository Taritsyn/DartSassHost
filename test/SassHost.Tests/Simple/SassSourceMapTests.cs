using System;

using NUnit.Framework;

namespace SassHost.Tests.Simple
{
	[TestFixture]
	public sealed class SassSourceMapTests : SourceMapTestsBase
	{
		public SassSourceMapTests()
			: base(SyntaxType.Sass)
		{ }
	}
}