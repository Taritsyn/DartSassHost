using System;

using NUnit.Framework;

namespace SassHost.Tests.Modules
{
	[TestFixture]
	public sealed class ScssIncludedFilePathsTests : IncludedFilePathsTestsBase
	{
		public ScssIncludedFilePathsTests()
			: base(SyntaxType.Scss)
		{ }
	}
}