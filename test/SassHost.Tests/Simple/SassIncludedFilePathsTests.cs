using System;

using NUnit.Framework;

namespace SassHost.Tests.Simple
{
	[TestFixture]
	public sealed class SassIncludedFilePathsTests : IncludedFilePathsTestsBase
	{
		public SassIncludedFilePathsTests()
			: base(SyntaxType.Sass)
		{ }
	}
}