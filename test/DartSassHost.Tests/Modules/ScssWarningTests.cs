using System;
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


		[Test]
		public void UsageOfFatalDeprecationsPropertyDuringCompilation([Values] bool fromFile)
		{
			// Arrange
			var alternativePaths = new List<string> { GenerateSassDirectoryPath("all", "alternative") };

			var withoutFatalDeprecationsOptions = new CompilationOptions
			{
				FatalDeprecations = new List<string>(),
				IncludePaths = alternativePaths
			};
			var withFatalDeprecationIdOptions = new CompilationOptions
			{
				FatalDeprecations = new List<string> { DeprecationId.FunctionUnits },
				IncludePaths = alternativePaths
			};
			var withFatalDeprecationVersionOptions = new CompilationOptions
			{
				FatalDeprecations = new List<string> { "1.23.0" },
				IncludePaths = alternativePaths
			};
			var withFatalDeprecationIdAndVersionOptions = new CompilationOptions
			{
				FatalDeprecations = new List<string> { DeprecationId.SlashDiv, "1.79.0" },
				IncludePaths = alternativePaths
			};
			var withFatalDeprecationVersionAndIdOptions = new CompilationOptions
			{
				FatalDeprecations = new List<string> { "1.55.0", DeprecationId.ColorModuleCompat },
				IncludePaths = alternativePaths
			};

			string inputPath = GenerateSassFilePath("all", "base");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;

			// Act and Assert
			using (var sassCompiler = CreateSassCompiler())
			{
				Assert.DoesNotThrow(() => sassCompiler.AdvancedCompile(fromFile, input, inputPath,
					options: withoutFatalDeprecationsOptions));

				var exception1 = Assert.Throws<SassCompilationException>(() => sassCompiler.AdvancedCompile(fromFile,
					input, inputPath, options: withFatalDeprecationIdOptions));
				string description1 = string.Format(
						WarningConstants.NumberValueWithoutPercentagesDeprecated,
						"saturation", 98
					) +
					"\n\n" +
					string.Format(WarningConstants.DeprecationWarningAsErrorExplanation, DeprecationId.FunctionUnits)
					;
				Assert.AreEqual(
					"Error: " + description1 + Environment.NewLine +
					"   at @use (Files/modules/warnings/all/scss/_variables.scss:8:8) -> " +
					"  red: hsl(9, 98, 52%)," + Environment.NewLine +
					"   at root stylesheet (Files/modules/warnings/all/scss/base.scss:3:1)",
					exception1.Message
				);
				Assert.AreEqual(description1, exception1.Description);
				Assert.AreEqual(1, exception1.Status);
				Assert.AreEqual(GenerateSassFilePath("all", "_variables"), exception1.File);
				Assert.AreEqual(8, exception1.LineNumber);
				Assert.AreEqual(8, exception1.ColumnNumber);
				Assert.AreEqual(
					"Line 7: $colors: (" + Environment.NewLine +
					"Line 8:   red: hsl(9, 98, 52%)," + Environment.NewLine +
					"---------------^" + Environment.NewLine +
					"Line 9:   blue: #0099cc,",
					exception1.SourceFragment
				);
				Assert.AreEqual(
					"   at @use (Files/modules/warnings/all/scss/_variables.scss:8:8)" + Environment.NewLine +
					"   at root stylesheet (Files/modules/warnings/all/scss/base.scss:3:1)",
					exception1.CallStack
				);

				var exception2 = Assert.Throws<SassCompilationException>(() => sassCompiler.AdvancedCompile(fromFile,
					input, inputPath, options: withFatalDeprecationVersionOptions));
				string description2 = string.Format(
						WarningConstants.ColorInversionWithNumberArgumentsDeprecated,
						221716
					) +
					"\n\n" +
					string.Format(WarningConstants.DeprecationWarningAsErrorExplanation, DeprecationId.ColorModuleCompat)
					;
				Assert.AreEqual(
					"Error: " + description2 + Environment.NewLine +
					"   at root stylesheet (Files/modules/warnings/all/scss/base.scss:15:10) -> " +
					"  color: color.invert($text-color);",
					exception2.Message
				);
				Assert.AreEqual(description2, exception2.Description);
				Assert.AreEqual(1, exception2.Status);
				Assert.AreEqual(inputPath, exception2.File);
				Assert.AreEqual(15, exception2.LineNumber);
				Assert.AreEqual(10, exception2.ColumnNumber);
				Assert.AreEqual(
					"Line 14:   background-color: color.invert($body-bg);" + Environment.NewLine +
					"Line 15:   color: color.invert($text-color);" + Environment.NewLine +
					"------------------^" + Environment.NewLine +
					"Line 16: }",
					exception2.SourceFragment
				);
				Assert.AreEqual(
					"   at root stylesheet (Files/modules/warnings/all/scss/base.scss:15:10)",
					exception2.CallStack
				);

				var exception3 = Assert.Throws<SassCompilationException>(() => sassCompiler.AdvancedCompile(fromFile,
					input, inputPath, options: withFatalDeprecationIdAndVersionOptions));
				string description3 = string.Format(
						WarningConstants.NumberValueWithoutPercentagesDeprecated,
						"saturation", 98
					) +
					"\n\n" +
					string.Format(WarningConstants.DeprecationWarningAsErrorExplanation, DeprecationId.FunctionUnits)
					;
				Assert.AreEqual(
					"Error: " + description3 + Environment.NewLine +
					"   at @use (Files/modules/warnings/all/scss/_variables.scss:8:8) -> " +
					"  red: hsl(9, 98, 52%)," + Environment.NewLine +
					"   at root stylesheet (Files/modules/warnings/all/scss/base.scss:3:1)",
					exception3.Message
				);
				Assert.AreEqual(description3, exception3.Description);
				Assert.AreEqual(1, exception3.Status);
				Assert.AreEqual(GenerateSassFilePath("all", "_variables"), exception3.File);
				Assert.AreEqual(8, exception3.LineNumber);
				Assert.AreEqual(8, exception3.ColumnNumber);
				Assert.AreEqual(
					"Line 7: $colors: (" + Environment.NewLine +
					"Line 8:   red: hsl(9, 98, 52%)," + Environment.NewLine +
					"---------------^" + Environment.NewLine +
					"Line 9:   blue: #0099cc,",
					exception3.SourceFragment
				);
				Assert.AreEqual(
					"   at @use (Files/modules/warnings/all/scss/_variables.scss:8:8)" + Environment.NewLine +
					"   at root stylesheet (Files/modules/warnings/all/scss/base.scss:3:1)",
					exception3.CallStack
				);

				var exception4 = Assert.Throws<SassCompilationException>(() => sassCompiler.AdvancedCompile(fromFile,
					input, inputPath, options: withFatalDeprecationVersionAndIdOptions));
				string description4 = string.Format(
						WarningConstants.DeprecatedDivisionWithSimpleRecommendation,
						"$grid-gutter-width", 2
					) +
					"\n\n" +
					string.Format(WarningConstants.DeprecationWarningAsErrorExplanation, DeprecationId.SlashDiv)
					;
				Assert.AreEqual(
					"Error: " + description4 + Environment.NewLine +
					"   at @use (Files/modules/warnings/all/scss/_grid.scss:17:16) -> " +
					"  margin-left: $grid-gutter-width / 2;" + Environment.NewLine +
					"   at root stylesheet (Files/modules/warnings/all/scss/base.scss:5:1)",
					exception4.Message
				);
				Assert.AreEqual(description4, exception4.Description);
				Assert.AreEqual(1, exception4.Status);
				Assert.AreEqual(GenerateSassFilePath("all", "_grid"), exception4.File);
				Assert.AreEqual(17, exception4.LineNumber);
				Assert.AreEqual(16, exception4.ColumnNumber);
				Assert.AreEqual(
					"Line 16:   margin-right: math.div(\"#{$grid-gutter-width}\", 2);" + Environment.NewLine +
					"Line 17:   margin-left: $grid-gutter-width / 2;" + Environment.NewLine +
					"------------------------^",
					exception4.SourceFragment
				);
				Assert.AreEqual(
					"   at @use (Files/modules/warnings/all/scss/_grid.scss:17:16)" + Environment.NewLine +
					"   at root stylesheet (Files/modules/warnings/all/scss/base.scss:5:1)",
					exception4.CallStack
				);
			}
		}

		[Test]
		public void MappingSassWarningDuringCompilation([Values]bool fromFile)
		{
			// Arrange
			string inputPath = GenerateSassFilePath("division-with-non-numeric-args", "base");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;
			string gridMixinFilePath = GenerateSassFilePath("division-with-non-numeric-args", "mixins/_grid");

			// Act
			IList<ProblemInfo> warnings;

			using (var sassCompiler = CreateSassCompiler())
			{
				if (fromFile)
				{
					warnings = sassCompiler.CompileFile(inputPath).Warnings;
				}
				else
				{
					warnings = sassCompiler.Compile(input, inputPath).Warnings;
				}
			}

			// Assert
			Assert.AreEqual(3, warnings.Count);

			Assert.AreEqual(
				"Warning: " + WarningConstants.MathDivOnlySupportNumberArguments + Environment.NewLine +
				"   at make-column (Files/modules/warnings/division-with-non-numeric-args/scss/mixins/_grid.scss:17:10) -> " +
				"  width: math.div(100% * $columns - $grid-gutter-width * ($grid-columns - $columns), \"#{$grid-column" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/division-with-non-numeric-args/scss/base.scss:7:5)",
				warnings[0].Message
			);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings[0].Description);
			Assert.AreEqual(false, warnings[0].IsDeprecation);
			Assert.Null(warnings[0].DeprecationId);
			Assert.AreEqual(gridMixinFilePath, warnings[0].File);
			Assert.AreEqual(17, warnings[0].LineNumber);
			Assert.AreEqual(10, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 16:   float: left;" + Environment.NewLine +
				"Line 17:   width: math.div(100% * $columns - $grid-gutter-width * ($grid-columns - $columns), \"#{$grid…" + Environment.NewLine +
				"------------------^" + Environment.NewLine +
				"Line 18:   margin-right: math.div(\"#{$grid-gutter-width}\", 2);",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at make-column (Files/modules/warnings/division-with-non-numeric-args/scss/mixins/_grid.scss:17:10)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/division-with-non-numeric-args/scss/base.scss:7:5)",
				warnings[0].CallStack
			);

			Assert.AreEqual(
				"Warning: " + WarningConstants.MathDivOnlySupportNumberArguments + Environment.NewLine +
				"   at make-column (Files/modules/warnings/division-with-non-numeric-args/scss/mixins/_grid.scss:18:17) -> " +
				"  margin-right: math.div(\"#{$grid-gutter-width}\", 2);" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/division-with-non-numeric-args/scss/base.scss:7:5)",
				warnings[1].Message
			);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings[1].Description);
			Assert.AreEqual(false, warnings[1].IsDeprecation);
			Assert.Null(warnings[1].DeprecationId);
			Assert.AreEqual(gridMixinFilePath, warnings[1].File);
			Assert.AreEqual(18, warnings[1].LineNumber);
			Assert.AreEqual(17, warnings[1].ColumnNumber);
			Assert.AreEqual(
				"Line 17:   width: math.div(100% * $columns - $grid-gutter-width * ($grid-columns - $columns), \"#{$grid-c…" + Environment.NewLine +
				"Line 18:   margin-right: math.div(\"#{$grid-gutter-width}\", 2);" + Environment.NewLine +
				"-------------------------^" + Environment.NewLine +
				"Line 19:   margin-left: math.div(\"\" + $grid-gutter-width, 2);",
				warnings[1].SourceFragment
			);
			Assert.AreEqual(
				"   at make-column (Files/modules/warnings/division-with-non-numeric-args/scss/mixins/_grid.scss:18:17)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/division-with-non-numeric-args/scss/base.scss:7:5)",
				warnings[1].CallStack
			);

			Assert.AreEqual(
				"Warning: " + WarningConstants.MathDivOnlySupportNumberArguments + Environment.NewLine +
				"   at make-column (Files/modules/warnings/division-with-non-numeric-args/scss/mixins/_grid.scss:19:16) -> " +
				"  margin-left: math.div(\"\" + $grid-gutter-width, 2);" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/division-with-non-numeric-args/scss/base.scss:7:5)",
				warnings[2].Message
			);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings[2].Description);
			Assert.AreEqual(false, warnings[2].IsDeprecation);
			Assert.Null(warnings[2].DeprecationId);
			Assert.AreEqual(gridMixinFilePath, warnings[2].File);
			Assert.AreEqual(19, warnings[2].LineNumber);
			Assert.AreEqual(16, warnings[2].ColumnNumber);
			Assert.AreEqual(
				"Line 18:   margin-right: math.div(\"#{$grid-gutter-width}\", 2);" + Environment.NewLine +
				"Line 19:   margin-left: math.div(\"\" + $grid-gutter-width, 2);" + Environment.NewLine +
				"------------------------^",
				warnings[2].SourceFragment
			);
			Assert.AreEqual(
				"   at make-column (Files/modules/warnings/division-with-non-numeric-args/scss/mixins/_grid.scss:19:16)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/division-with-non-numeric-args/scss/base.scss:7:5)",
				warnings[2].CallStack
			);
		}

		[Test]
		public void MappingSassDeprecationWarningDuringCompilation([Values]bool fromFile)
		{
			// Arrange
			string inputPath = GenerateSassFilePath("deprecated-division", "base");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;
			string importedFilePath = GenerateSassFilePath("deprecated-division", "_mixins");

			// Act
			IList<ProblemInfo> warnings;

			using (var sassCompiler = CreateSassCompiler())
			{
				if (fromFile)
				{
					warnings = sassCompiler.CompileFile(inputPath).Warnings;
				}
				else
				{
					warnings = sassCompiler.Compile(input, inputPath).Warnings;
				}
			}

			// Assert
			Assert.AreEqual(1, warnings.Count);

			string description = string.Format(WarningConstants.DeprecatedDivisionWithSimpleRecommendation,
				"$y", "$x");

			Assert.AreEqual(
				"Deprecation Warning [" + DeprecationId.SlashDiv + "]: " + description + Environment.NewLine +
				"   at responsive-ratio (Files/modules/warnings/deprecated-division/scss/_mixins.scss:10:29) -> " +
				"  $padding: string.unquote(($y / $x) * 100 + \"%\");" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/deprecated-division/scss/base.scss:12:3)",
				warnings[0].Message
			);
			Assert.AreEqual(description, warnings[0].Description);
			Assert.AreEqual(true, warnings[0].IsDeprecation);
			Assert.AreEqual(DeprecationId.SlashDiv, warnings[0].DeprecationId);
			Assert.AreEqual(importedFilePath, warnings[0].File);
			Assert.AreEqual(10, warnings[0].LineNumber);
			Assert.AreEqual(29, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line  9: @mixin responsive-ratio($x, $y, $pseudo: false) {" + Environment.NewLine +
				"Line 10:   $padding: string.unquote(($y / $x) * 100 + \"%\");" + Environment.NewLine +
				"-------------------------------------^" + Environment.NewLine +
				"Line 11:   @if $pseudo {",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at responsive-ratio (Files/modules/warnings/deprecated-division/scss/_mixins.scss:10:29)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/deprecated-division/scss/base.scss:12:3)",
				warnings[0].CallStack
			);
		}

		[Test]
		public void MappingSassCustomWarningDuringCompilation([Values]bool fromFile)
		{
			// Arrange
			string inputPath = GenerateSassFilePath("custom-warning", "base");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;
			string importedFilePath = GenerateSassFilePath("custom-warning", "_mixins");

			// Act
			IList<ProblemInfo> warnings;

			using (var sassCompiler = CreateSassCompiler())
			{
				if (fromFile)
				{
					warnings = sassCompiler.CompileFile(inputPath).Warnings;
				}
				else
				{
					warnings = sassCompiler.Compile(input, inputPath).Warnings;
				}
			}

			// Assert
			Assert.AreEqual(1, warnings.Count);

			string description = string.Format(WarningConstants.UnknownVendorPrefix, "wekbit");

			Assert.AreEqual(
				"Warning: " + description + Environment.NewLine +
				"   at prefix (Files/modules/warnings/custom-warning/scss/_mixins.scss:8:7) -> " +
				"      @warn 'Unknown prefix #{$prefix}.';" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/custom-warning/scss/base.scss:4:3)",
				warnings[0].Message
			);
			Assert.AreEqual(description, warnings[0].Description);
			Assert.AreEqual(false, warnings[0].IsDeprecation);
			Assert.Null(warnings[0].DeprecationId);
			Assert.AreEqual(importedFilePath, warnings[0].File);
			Assert.AreEqual(8, warnings[0].LineNumber);
			Assert.AreEqual(7, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 7:     @if not list.index($known-prefixes, $prefix) {" + Environment.NewLine +
				"Line 8:       @warn 'Unknown prefix #{$prefix}.';" + Environment.NewLine +
				"--------------^" + Environment.NewLine +
				"Line 9:     }",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at prefix (Files/modules/warnings/custom-warning/scss/_mixins.scss:8:7)" + Environment.NewLine +
				"   at root stylesheet (Files/modules/warnings/custom-warning/scss/base.scss:4:3)",
				warnings[0].CallStack
			);
		}
	}
}