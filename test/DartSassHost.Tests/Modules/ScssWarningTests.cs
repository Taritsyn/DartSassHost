﻿using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace DartSassHost.Tests.Modules
{
	[TestFixture]
	public sealed class ScssWarningTests : WarningTestsBase
	{
		public ScssWarningTests()
			: base(SyntaxType.Scss)
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
				"   at responsive-ratio (Files/modules/warnings/deprecated-division/scss/_mixins.scss:8:22)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/deprecated-division/scss/base.scss:12:3)",
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
			Assert.AreEqual(8, warnings[0].LineNumber);
			Assert.AreEqual(22, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 7: @mixin responsive-ratio($x, $y, $pseudo: false) {" + Environment.NewLine +
				"Line 8:   $padding: unquote(($y / $x) * 100 + \"%\");" + Environment.NewLine +
				"-----------------------------^" + Environment.NewLine +
				"Line 9:   @if $pseudo {",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at responsive-ratio (Files/modules/warnings/deprecated-division/scss/_mixins.scss:8:22)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/deprecated-division/scss/base.scss:12:3)",
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
				"   at prefix (Files/modules/warnings/custom-warning/scss/_mixins.scss:6:7)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/custom-warning/scss/base.scss:4:3)",
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
				"Line 5:     @if not index($known-prefixes, $prefix) {" + Environment.NewLine +
				"Line 6:       @warn 'Unknown prefix #{$prefix}.';" + Environment.NewLine +
				"--------------^" + Environment.NewLine +
				"Line 7:     }",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at prefix (Files/modules/warnings/custom-warning/scss/_mixins.scss:6:7)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/custom-warning/scss/base.scss:4:3)",
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
				"   at responsive-ratio (Files/modules/warnings/deprecated-division/scss/_mixins.scss:8:22)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/deprecated-division/scss/base.scss:12:3)",
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
			Assert.AreEqual(8, warnings[0].LineNumber);
			Assert.AreEqual(22, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 7: @mixin responsive-ratio($x, $y, $pseudo: false) {" + Environment.NewLine +
				"Line 8:   $padding: unquote(($y / $x) * 100 + \"%\");" + Environment.NewLine +
				"-----------------------------^" + Environment.NewLine +
				"Line 9:   @if $pseudo {",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at responsive-ratio (Files/modules/warnings/deprecated-division/scss/_mixins.scss:8:22)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/deprecated-division/scss/base.scss:12:3)",
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
				"   at prefix (Files/modules/warnings/custom-warning/scss/_mixins.scss:6:7)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/custom-warning/scss/base.scss:4:3)",
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
				"Line 5:     @if not index($known-prefixes, $prefix) {" + Environment.NewLine +
				"Line 6:       @warn 'Unknown prefix #{$prefix}.';" + Environment.NewLine +
				"--------------^" + Environment.NewLine +
				"Line 7:     }",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at prefix (Files/modules/warnings/custom-warning/scss/_mixins.scss:6:7)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/custom-warning/scss/base.scss:4:3)",
				warnings[0].CallStack
			);
		}

		#endregion
	}
}