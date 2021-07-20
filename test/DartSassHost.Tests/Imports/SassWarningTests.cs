using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace DartSassHost.Tests.Imports
{
	[TestFixture]
	public sealed class SassWarningTests : WarningTestsBase
	{
		public SassWarningTests()
			: base(SyntaxType.Sass)
		{ }


		#region Code

		[Test]
		public void MappingSassDeprecationWarningDuringCompilationOfCode()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("deprecated-division", "base");
			string input = GetFileContent(inputPath);
			string importedFilePath = GenerateSassFilePath("deprecated-division", "_mixins");

			// Act
			IList<ProblemInfo> warnings;

			using (var sassCompiler = CreateSassCompiler())
			{
				warnings = sassCompiler.Compile(input, inputPath).Warnings;
			}

			// Assert
			Assert.AreEqual(1, warnings.Count);

			Assert.AreEqual(
				"Deprecation Warning: Using / for division is deprecated and will be removed in Dart Sass 2.0.0.\n\n" +
				"Recommendation: math.div($y, $x)\n\n" +
				"More info and automated migrator: https://sass-lang.com/d/slash-div" + Environment.NewLine +
				"   at responsive-ratio (Files/imports/warnings/deprecated-division/sass/_mixins.sass:7:22)" + Environment.NewLine +
				"   at root stylesheet (Files/imports/warnings/deprecated-division/sass/base.sass:11:3)",
				warnings[0].Message
			);
			Assert.AreEqual(
				"Using / for division is deprecated and will be removed in Dart Sass 2.0.0.\n\n" +
				"Recommendation: math.div($y, $x)\n\n" +
				"More info and automated migrator: https://sass-lang.com/d/slash-div",
				warnings[0].Description
			);
			Assert.AreEqual(true, warnings[0].IsDeprecation);
			Assert.AreEqual(importedFilePath, warnings[0].File);
			Assert.AreEqual(7, warnings[0].LineNumber);
			Assert.AreEqual(22, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 6: @mixin responsive-ratio($x, $y, $pseudo: false)" + Environment.NewLine +
				"Line 7:   $padding: unquote(($y / $x) * 100 + '%')" + Environment.NewLine +
				"-----------------------------^" + Environment.NewLine +
				"Line 8:   @if $pseudo",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at responsive-ratio (Files/imports/warnings/deprecated-division/sass/_mixins.sass:7:22)" + Environment.NewLine +
				"   at root stylesheet (Files/imports/warnings/deprecated-division/sass/base.sass:11:3)",
				warnings[0].CallStack
			);
		}

		[Test]
		public void MappingSassCustomWarningDuringCompilationOfCode()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("custom-warning", "base");
			string input = GetFileContent(inputPath);
			string importedFilePath = GenerateSassFilePath("custom-warning", "_mixins");

			// Act
			IList<ProblemInfo> warnings;

			using (var sassCompiler = CreateSassCompiler())
			{
				warnings = sassCompiler.Compile(input, inputPath).Warnings;
			}

			// Assert
			Assert.AreEqual(1, warnings.Count);

			Assert.AreEqual(
				"Warning: Unknown prefix wekbit." + Environment.NewLine +
				"   at prefix (Files/imports/warnings/custom-warning/sass/_mixins.sass:6:7)" + Environment.NewLine +
				"   at root stylesheet (Files/imports/warnings/custom-warning/sass/base.sass:4:3)",
				warnings[0].Message
			);
			Assert.AreEqual(
				"Unknown prefix wekbit.",
				warnings[0].Description
			);
			Assert.AreEqual(false, warnings[0].IsDeprecation);
			Assert.AreEqual(importedFilePath, warnings[0].File);
			Assert.AreEqual(6, warnings[0].LineNumber);
			Assert.AreEqual(7, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 5:     @if not index($known-prefixes, $prefix)" + Environment.NewLine +
				"Line 6:       @warn \"Unknown prefix #{$prefix}.\"" + Environment.NewLine +
				"--------------^",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at prefix (Files/imports/warnings/custom-warning/sass/_mixins.sass:6:7)" + Environment.NewLine +
				"   at root stylesheet (Files/imports/warnings/custom-warning/sass/base.sass:4:3)",
				warnings[0].CallStack
			);
		}

		#endregion

		#region Files

		[Test]
		public void MappingSassDeprecationWarningDuringCompilationOfFile()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("deprecated-division", "base");
			string importedFilePath = GenerateSassFilePath("deprecated-division", "_mixins");

			// Act
			IList<ProblemInfo> warnings;

			using (var sassCompiler = CreateSassCompiler())
			{
				warnings = sassCompiler.CompileFile(inputPath).Warnings;
			}

			// Assert
			Assert.AreEqual(1, warnings.Count);

			Assert.AreEqual(
				"Deprecation Warning: Using / for division is deprecated and will be removed in Dart Sass 2.0.0.\n\n" +
				"Recommendation: math.div($y, $x)\n\n" +
				"More info and automated migrator: https://sass-lang.com/d/slash-div" + Environment.NewLine +
				"   at responsive-ratio (Files/imports/warnings/deprecated-division/sass/_mixins.sass:7:22)" + Environment.NewLine +
				"   at root stylesheet (Files/imports/warnings/deprecated-division/sass/base.sass:11:3)",
				warnings[0].Message
			);
			Assert.AreEqual(
				"Using / for division is deprecated and will be removed in Dart Sass 2.0.0.\n\n" +
				"Recommendation: math.div($y, $x)\n\n" +
				"More info and automated migrator: https://sass-lang.com/d/slash-div",
				warnings[0].Description
			);
			Assert.AreEqual(true, warnings[0].IsDeprecation);
			Assert.AreEqual(importedFilePath, warnings[0].File);
			Assert.AreEqual(7, warnings[0].LineNumber);
			Assert.AreEqual(22, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 6: @mixin responsive-ratio($x, $y, $pseudo: false)" + Environment.NewLine +
				"Line 7:   $padding: unquote(($y / $x) * 100 + '%')" + Environment.NewLine +
				"-----------------------------^" + Environment.NewLine +
				"Line 8:   @if $pseudo",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at responsive-ratio (Files/imports/warnings/deprecated-division/sass/_mixins.sass:7:22)" + Environment.NewLine +
				"   at root stylesheet (Files/imports/warnings/deprecated-division/sass/base.sass:11:3)",
				warnings[0].CallStack
			);
		}

		[Test]
		public void MappingSassCustomWarningDuringCompilationOfFile()
		{
			// Arrange
			string inputPath = GenerateSassFilePath("custom-warning", "base");
			string importedFilePath = GenerateSassFilePath("custom-warning", "_mixins");

			// Act
			IList<ProblemInfo> warnings;

			using (var sassCompiler = CreateSassCompiler())
			{
				warnings = sassCompiler.CompileFile(inputPath).Warnings;
			}

			// Assert
			Assert.AreEqual(1, warnings.Count);

			Assert.AreEqual(
				"Warning: Unknown prefix wekbit." + Environment.NewLine +
				"   at prefix (Files/imports/warnings/custom-warning/sass/_mixins.sass:6:7)" + Environment.NewLine +
				"   at root stylesheet (Files/imports/warnings/custom-warning/sass/base.sass:4:3)",
				warnings[0].Message
			);
			Assert.AreEqual(
				"Unknown prefix wekbit.",
				warnings[0].Description
			);
			Assert.AreEqual(false, warnings[0].IsDeprecation);
			Assert.AreEqual(importedFilePath, warnings[0].File);
			Assert.AreEqual(6, warnings[0].LineNumber);
			Assert.AreEqual(7, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 5:     @if not index($known-prefixes, $prefix)" + Environment.NewLine +
				"Line 6:       @warn \"Unknown prefix #{$prefix}.\"" + Environment.NewLine +
				"--------------^",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at prefix (Files/imports/warnings/custom-warning/sass/_mixins.sass:6:7)" + Environment.NewLine +
				"   at root stylesheet (Files/imports/warnings/custom-warning/sass/base.sass:4:3)",
				warnings[0].CallStack
			);
		}

		#endregion
	}
}