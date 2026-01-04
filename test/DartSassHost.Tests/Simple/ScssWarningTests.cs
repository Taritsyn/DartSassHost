using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace DartSassHost.Tests.Simple
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
			var withoutFatalDeprecationsOptions = new CompilationOptions
			{
				FatalDeprecations = new List<string>()
			};
			var withFatalDeprecationIdOptions = new CompilationOptions
			{
				FatalDeprecations = new List<string> { DeprecationId.GlobalBuiltin }
			};
			var withFatalDeprecationVersionOptions = new CompilationOptions
			{
				FatalDeprecations = new List<string> { "1.23.0" }
			};
			var withFatalDeprecationIdAndVersionOptions = new CompilationOptions
			{
				FatalDeprecations = new List<string> { DeprecationId.SlashDiv, "1.80.0" }
			};
			var withFatalDeprecationVersionAndIdOptions = new CompilationOptions
			{
				FatalDeprecations = new List<string> { "1.17.2", DeprecationId.ColorModuleCompat }
			};

			string inputPath = GenerateSassFilePath("all", "style");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;

			// Act and Assert
			using (var sassCompiler = CreateSassCompiler())
			{
				Assert.DoesNotThrow(() => sassCompiler.AdvancedCompile(fromFile, input, inputPath,
					options: withoutFatalDeprecationsOptions));

				var exception1 = Assert.Throws<SassCompilationException>(() => sassCompiler.AdvancedCompile(fromFile,
					input, inputPath, options: withFatalDeprecationIdOptions));
				string description1 = string.Format(
						WarningConstants.GlobalBuiltinFunctionDeprecated,
						"list.index"
					) +
					"\n\n" +
					string.Format(WarningConstants.DeprecationWarningAsErrorExplanation, DeprecationId.GlobalBuiltin)
					;
				Assert.AreEqual(
					"Error: " + description1 + Environment.NewLine +
					"   at prefix (Files/simple/warnings/all/scss/style.scss:12:13) -> " +
					"    @if not index($known-prefixes, $prefix) {" + Environment.NewLine +
					"   at root stylesheet (Files/simple/warnings/all/scss/style.scss:111:3)",
					exception1.Message
				);
				Assert.AreEqual(description1, exception1.Description);
				Assert.AreEqual(1, exception1.Status);
				Assert.AreEqual(inputPath, exception1.File);
				Assert.AreEqual(12, exception1.LineNumber);
				Assert.AreEqual(13, exception1.ColumnNumber);
				Assert.AreEqual(
					"Line 11:   @each $prefix in $prefixes {" + Environment.NewLine +
					"Line 12:     @if not index($known-prefixes, $prefix) {" + Environment.NewLine +
					"---------------------^" + Environment.NewLine +
					"Line 13:       @warn \"Unknown prefix #{$prefix}.\";",
					exception1.SourceFragment
				);
				Assert.AreEqual(
					"   at prefix (Files/simple/warnings/all/scss/style.scss:12:13)" + Environment.NewLine +
					"   at root stylesheet (Files/simple/warnings/all/scss/style.scss:111:3)",
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
					"   at root stylesheet (Files/simple/warnings/all/scss/style.scss:107:10) -> " +
					"  color: color.invert($text-color);",
					exception2.Message
				);
				Assert.AreEqual(description2, exception2.Description);
				Assert.AreEqual(1, exception2.Status);
				Assert.AreEqual(inputPath, exception2.File);
				Assert.AreEqual(107, exception2.LineNumber);
				Assert.AreEqual(10, exception2.ColumnNumber);
				Assert.AreEqual(
					"Line 106:   background-color: color.invert($body-bg);" + Environment.NewLine +
					"Line 107:   color: color.invert($text-color);" + Environment.NewLine +
					"-------------------^" + Environment.NewLine +
					"Line 108: }",
					exception2.SourceFragment
				);
				Assert.AreEqual(
					"   at root stylesheet (Files/simple/warnings/all/scss/style.scss:107:10)",
					exception2.CallStack
				);

				var exception3 = Assert.Throws<SassCompilationException>(() => sassCompiler.AdvancedCompile(fromFile,
					input, inputPath, options: withFatalDeprecationIdAndVersionOptions));
				string description3 = string.Format(
						WarningConstants.DeprecatedDivisionWithSimpleRecommendation,
						"$grid-gutter-width", 2
					) +
					"\n\n" +
					string.Format(WarningConstants.DeprecationWarningAsErrorExplanation, DeprecationId.SlashDiv)
					;
				Assert.AreEqual(
					"Error: " + description3 + Environment.NewLine +
					"   at root stylesheet (Files/simple/warnings/all/scss/style.scss:38:16) -> " +
					"  margin-left: $grid-gutter-width / 2;",
					exception3.Message
				);
				Assert.AreEqual(description3, exception3.Description);
				Assert.AreEqual(1, exception3.Status);
				Assert.AreEqual(inputPath, exception3.File);
				Assert.AreEqual(38, exception3.LineNumber);
				Assert.AreEqual(16, exception3.ColumnNumber);
				Assert.AreEqual(
					"Line 37:   margin-right: math.div(\"#{$grid-gutter-width}\", 2);" + Environment.NewLine +
					"Line 38:   margin-left: $grid-gutter-width / 2;" + Environment.NewLine +
					"------------------------^" + Environment.NewLine +
					"Line 39:   -webkit-box-sizing: border-box;",
					exception3.SourceFragment
				);
				Assert.AreEqual(
					"   at root stylesheet (Files/simple/warnings/all/scss/style.scss:38:16)",
					exception3.CallStack
				);

				var exception4 = Assert.Throws<SassCompilationException>(() => sassCompiler.AdvancedCompile(fromFile,
					input, inputPath, options: withFatalDeprecationVersionAndIdOptions));
				string description4 = string.Format(
						WarningConstants.ColorInversionWithNumberArgumentsDeprecated,
						221716
					) +
					"\n\n" +
					string.Format(WarningConstants.DeprecationWarningAsErrorExplanation, DeprecationId.ColorModuleCompat)
					;
				Assert.AreEqual(
					"Error: " + description4 + Environment.NewLine +
					"   at root stylesheet (Files/simple/warnings/all/scss/style.scss:107:10) -> " +
					"  color: color.invert($text-color);",
					exception4.Message
				);
				Assert.AreEqual(description4, exception4.Description);
				Assert.AreEqual(1, exception4.Status);
				Assert.AreEqual(inputPath, exception4.File);
				Assert.AreEqual(107, exception4.LineNumber);
				Assert.AreEqual(10, exception4.ColumnNumber);
				Assert.AreEqual(
					"Line 106:   background-color: color.invert($body-bg);" + Environment.NewLine +
					"Line 107:   color: color.invert($text-color);" + Environment.NewLine +
					"-------------------^" + Environment.NewLine +
					"Line 108: }",
					exception4.SourceFragment
				);
				Assert.AreEqual(
					"   at root stylesheet (Files/simple/warnings/all/scss/style.scss:107:10)",
					exception4.CallStack
				);
			}
		}

		[Test]
		public void MappingSassWarningDuringCompilation([Values]bool fromFile)
		{
			// Arrange
			string inputPath = GenerateSassFilePath("division-with-non-numeric-args", "style");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;

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
				"   at root stylesheet (Files/simple/warnings/division-with-non-numeric-args/scss/style.scss:22:17) -> " +
				"  margin-right: math.div(\"#{$grid-gutter-width}\", 2);",
				warnings[0].Message
			);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings[0].Description);
			Assert.AreEqual(false, warnings[0].IsDeprecation);
			Assert.Null(warnings[0].DeprecationId);
			Assert.AreEqual(inputPath, warnings[0].File);
			Assert.AreEqual(22, warnings[0].LineNumber);
			Assert.AreEqual(17, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 21:   float: left;" + Environment.NewLine +
				"Line 22:   margin-right: math.div(\"#{$grid-gutter-width}\", 2);" + Environment.NewLine +
				"-------------------------^" + Environment.NewLine +
				"Line 23:   margin-left: math.div(\"\" + $grid-gutter-width, 2);",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/simple/warnings/division-with-non-numeric-args/scss/style.scss:22:17)",
				warnings[0].CallStack
			);

			Assert.AreEqual(
				"Warning: " + WarningConstants.MathDivOnlySupportNumberArguments + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/division-with-non-numeric-args/scss/style.scss:23:16) -> " +
				"  margin-left: math.div(\"\" + $grid-gutter-width, 2);",
				warnings[1].Message
			);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings[1].Description);
			Assert.AreEqual(false, warnings[1].IsDeprecation);
			Assert.Null(warnings[1].DeprecationId);
			Assert.AreEqual(inputPath, warnings[1].File);
			Assert.AreEqual(23, warnings[1].LineNumber);
			Assert.AreEqual(16, warnings[1].ColumnNumber);
			Assert.AreEqual(
				"Line 22:   margin-right: math.div(\"#{$grid-gutter-width}\", 2);" + Environment.NewLine +
				"Line 23:   margin-left: math.div(\"\" + $grid-gutter-width, 2);" + Environment.NewLine +
				"------------------------^" + Environment.NewLine +
				"Line 24:   -webkit-box-sizing: border-box;",
				warnings[1].SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/simple/warnings/division-with-non-numeric-args/scss/style.scss:23:16)",
				warnings[1].CallStack
			);

			Assert.AreEqual(
				"Warning: " + WarningConstants.MathDivOnlySupportNumberArguments + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/division-with-non-numeric-args/scss/style.scss:39:12) -> " +
				"    width: math.div(100% * $num - $grid-gutter-width * ($grid-columns - $num), \"#{$grid-columns}\");",
				warnings[2].Message
			);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings[2].Description);
			Assert.AreEqual(false, warnings[2].IsDeprecation);
			Assert.Null(warnings[2].DeprecationId);
			Assert.AreEqual(inputPath, warnings[2].File);
			Assert.AreEqual(39, warnings[2].LineNumber);
			Assert.AreEqual(12, warnings[2].ColumnNumber);
			Assert.AreEqual(
				"Line 38:   .col-#{$num} {" + Environment.NewLine +
				"Line 39:     width: math.div(100% * $num - $grid-gutter-width * ($grid-columns - $num), \"#{$grid-colum…" + Environment.NewLine +
				"--------------------^" + Environment.NewLine +
				"Line 40:   }",
				warnings[2].SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/simple/warnings/division-with-non-numeric-args/scss/style.scss:39:12)",
				warnings[2].CallStack
			);
		}

		[Test]
		public void MappingSassDeprecationWarningDuringCompilation([Values]bool fromFile)
		{
			// Arrange
			string inputPath = GenerateSassFilePath("deprecated-division", "style");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;

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

			string description1 = string.Format(WarningConstants.GlobalBuiltinFunctionDeprecated, "map.get");

			Assert.AreEqual(
				"Deprecation Warning [" + DeprecationId.GlobalBuiltin + "]: " + description1 + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/deprecated-division/scss/style.scss:3:19) -> " +
				"$col-padding-xs:  map-get($grid-gutter-widths, xs) / 2;",
				warnings[0].Message
			);
			Assert.AreEqual(description1, warnings[0].Description);
			Assert.AreEqual(true, warnings[0].IsDeprecation);
			Assert.AreEqual(DeprecationId.GlobalBuiltin, warnings[0].DeprecationId);
			Assert.AreEqual(inputPath, warnings[0].File);
			Assert.AreEqual(3, warnings[0].LineNumber);
			Assert.AreEqual(19, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 3: $col-padding-xs:  map-get($grid-gutter-widths, xs) / 2;" + Environment.NewLine +
				"--------------------------^",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/simple/warnings/deprecated-division/scss/style.scss:3:19)",
				warnings[0].CallStack
			);

			string description2 = string.Format(WarningConstants.DeprecatedDivisionWithSimpleRecommendation,
				"map-get($grid-gutter-widths, xs)", 2);

			Assert.AreEqual(
				"Deprecation Warning [" + DeprecationId.SlashDiv + "]: " + description2 + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/deprecated-division/scss/style.scss:3:19) -> " +
				"$col-padding-xs:  map-get($grid-gutter-widths, xs) / 2;",
				warnings[1].Message
			);
			Assert.AreEqual(description2, warnings[1].Description);
			Assert.AreEqual(true, warnings[1].IsDeprecation);
			Assert.AreEqual(DeprecationId.SlashDiv, warnings[1].DeprecationId);
			Assert.AreEqual(inputPath, warnings[1].File);
			Assert.AreEqual(3, warnings[1].LineNumber);
			Assert.AreEqual(19, warnings[1].ColumnNumber);
			Assert.AreEqual(
				"Line 3: $col-padding-xs:  map-get($grid-gutter-widths, xs) / 2;" + Environment.NewLine +
				"--------------------------^",
				warnings[1].SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/simple/warnings/deprecated-division/scss/style.scss:3:19)",
				warnings[1].CallStack
			);

			string description3 = string.Format(WarningConstants.DeprecatedDivisionWithSimpleRecommendation,
				"$col-padding-xs", 2);

			Assert.AreEqual(
				"Deprecation Warning [" + DeprecationId.SlashDiv + "]: " + description3 + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/deprecated-division/scss/style.scss:6:18) -> " +
				"  padding-right: $col-padding-xs / 2;",
				warnings[2].Message
			);
			Assert.AreEqual(description3, warnings[2].Description);
			Assert.AreEqual(true, warnings[2].IsDeprecation);
			Assert.AreEqual(DeprecationId.SlashDiv, warnings[2].DeprecationId);
			Assert.AreEqual(inputPath, warnings[2].File);
			Assert.AreEqual(6, warnings[2].LineNumber);
			Assert.AreEqual(18, warnings[2].ColumnNumber);
			Assert.AreEqual(
				"Line 5: div {" + Environment.NewLine +
				"Line 6:   padding-right: $col-padding-xs / 2;" + Environment.NewLine +
				"-------------------------^" + Environment.NewLine +
				"Line 7: }",
				warnings[2].SourceFragment
			);
			Assert.AreEqual(
				"   at root stylesheet (Files/simple/warnings/deprecated-division/scss/style.scss:6:18)",
				warnings[2].CallStack
			);
		}

		[Test]
		public void MappingSassCustomWarningDuringCompilation([Values]bool fromFile)
		{
			// Arrange
			var options = new CompilationOptions { SilenceDeprecations = new List<string> { DeprecationId.GlobalBuiltin } };
			string inputPath = GenerateSassFilePath("custom-warning", "style");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;

			// Act
			IList<ProblemInfo> warnings;

			using (var sassCompiler = CreateSassCompiler(options))
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
				"   at prefix (Files/simple/warnings/custom-warning/scss/style.scss:6:7) -> " +
				"      @warn \"Unknown prefix #{$prefix}.\";" + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/custom-warning/scss/style.scss:15:3)",
				warnings[0].Message
			);
			Assert.AreEqual(description, warnings[0].Description);
			Assert.AreEqual(false, warnings[0].IsDeprecation);
			Assert.Null(warnings[0].DeprecationId);
			Assert.AreEqual(inputPath, warnings[0].File);
			Assert.AreEqual(6, warnings[0].LineNumber);
			Assert.AreEqual(7, warnings[0].ColumnNumber);
			Assert.AreEqual(
				"Line 5:     @if not index($known-prefixes, $prefix) {" + Environment.NewLine +
				"Line 6:       @warn \"Unknown prefix #{$prefix}.\";" + Environment.NewLine +
				"--------------^" + Environment.NewLine +
				"Line 7:     }",
				warnings[0].SourceFragment
			);
			Assert.AreEqual(
				"   at prefix (Files/simple/warnings/custom-warning/scss/style.scss:6:7)" + Environment.NewLine +
				"   at root stylesheet (Files/simple/warnings/custom-warning/scss/style.scss:15:3)",
				warnings[0].CallStack
			);
		}
	}
}