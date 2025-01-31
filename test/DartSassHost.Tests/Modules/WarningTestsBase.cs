using System.Collections.Generic;

using NUnit.Framework;

namespace DartSassHost.Tests.Modules
{
	public abstract class WarningTestsBase : PhysicalFileSystemTestsBase
	{
		public override string BaseDirectoryPath => "modules/warnings";


		protected WarningTestsBase(SyntaxType syntaxType)
			: base(syntaxType)
		{ }


		[Test]
		public void UsageOfWarningLevelAndQuietDependenciesPropertiesDuringCompilation([Values]bool fromFile)
		{
			// Arrange
			var alternativePaths = new List<string> { GenerateSassDirectoryPath("all", "alternative") };

			var quietWarningLevelAndQuietDepsOffOptions = new CompilationOptions
			{
				WarningLevel = WarningLevel.Quiet,
				QuietDependencies = false,
				IncludePaths = alternativePaths
			};
			var quietWarningLevelAndQuietDepsOnOptions = new CompilationOptions
			{
				WarningLevel = WarningLevel.Quiet,
				QuietDependencies = true,
				IncludePaths = alternativePaths
			};
			var defaultWarningLevelAndQuietDepsOffOptions = new CompilationOptions
			{
				WarningLevel = WarningLevel.Default,
				QuietDependencies = false,
				IncludePaths = alternativePaths
			};
			var defaultWarningLevelAndQuietDepsOnOptions = new CompilationOptions
			{
				WarningLevel = WarningLevel.Default,
				QuietDependencies = true,
				IncludePaths = alternativePaths
			};
			var verboseWarningLevelAndQuietDepsOffOptions = new CompilationOptions
			{
				WarningLevel = WarningLevel.Verbose,
				QuietDependencies = false,
				IncludePaths = alternativePaths
			};
			var verboseWarningLevelAndQuietDepsOnOptions = new CompilationOptions
			{
				WarningLevel = WarningLevel.Verbose,
				QuietDependencies = true,
				IncludePaths = alternativePaths
			};

			string inputPath = GenerateSassFilePath("all", "base");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;

			// Act
			IList<ProblemInfo> warnings1;
			IList<ProblemInfo> warnings2;
			IList<ProblemInfo> warnings3;
			IList<ProblemInfo> warnings4;
			IList<ProblemInfo> warnings5;
			IList<ProblemInfo> warnings6;

			using (var sassCompiler = CreateSassCompiler())
			{
				if (fromFile)
				{
					warnings1 = sassCompiler.CompileFile(inputPath, options: quietWarningLevelAndQuietDepsOffOptions)
						.Warnings
						;
					warnings2 = sassCompiler.CompileFile(inputPath, options: quietWarningLevelAndQuietDepsOnOptions)
						.Warnings
						;
					warnings3 = sassCompiler.CompileFile(inputPath, options: defaultWarningLevelAndQuietDepsOffOptions)
						.Warnings
						;
					warnings4 = sassCompiler.CompileFile(inputPath, options: defaultWarningLevelAndQuietDepsOnOptions)
						.Warnings
						;
					warnings5 = sassCompiler.CompileFile(inputPath, options: verboseWarningLevelAndQuietDepsOffOptions)
						.Warnings
						;
					warnings6 = sassCompiler.CompileFile(inputPath, options: verboseWarningLevelAndQuietDepsOnOptions)
						.Warnings
						;
				}
				else
				{
					warnings1 = sassCompiler.Compile(input, inputPath, options: quietWarningLevelAndQuietDepsOffOptions)
						.Warnings
						;
					warnings2 = sassCompiler.Compile(input, inputPath, options: quietWarningLevelAndQuietDepsOnOptions)
						.Warnings
						;
					warnings3 = sassCompiler.Compile(input, inputPath, options: defaultWarningLevelAndQuietDepsOffOptions)
						.Warnings
						;
					warnings4 = sassCompiler.Compile(input, inputPath, options: defaultWarningLevelAndQuietDepsOnOptions)
						.Warnings
						;
					warnings5 = sassCompiler.Compile(input, inputPath, options: verboseWarningLevelAndQuietDepsOffOptions)
						.Warnings
						;
					warnings6 = sassCompiler.Compile(input, inputPath, options: verboseWarningLevelAndQuietDepsOnOptions)
						.Warnings
						;
				}
			}

			// Assert
			Assert.AreEqual(0, warnings1.Count);

			Assert.AreEqual(0, warnings2.Count);

			Assert.AreEqual(22, warnings3.Count);
			Assert.AreEqual(
				string.Format(WarningConstants.NumberValueWithoutPercentagesDeprecated, "saturation", 98),
				warnings3[0].Description
			);
			Assert.IsTrue(warnings3[0].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings3[1].Description);
			Assert.IsFalse(warnings3[1].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.DeprecatedDivisionWithSimpleRecommendation, "$grid-gutter-width", 2),
				warnings3[2].Description
			);
			Assert.IsTrue(warnings3[2].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings3[3].Description);
			Assert.IsFalse(warnings3[3].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings3[4].Description);
			Assert.IsFalse(warnings3[4].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings3[5].Description);
			Assert.IsFalse(warnings3[5].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings3[6].Description);
			Assert.IsFalse(warnings3[6].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings3[7].Description);
			Assert.IsFalse(warnings3[7].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings3[8].Description);
			Assert.IsFalse(warnings3[8].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 7 - $grid-gutter-width * ($grid-columns - 7)", "$grid-columns"
				),
				warnings3[9].Description
			);
			Assert.IsTrue(warnings3[9].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 8 - $grid-gutter-width * ($grid-columns - 8)", "$grid-columns"
				),
				warnings3[10].Description
			);
			Assert.IsTrue(warnings3[10].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 9 - $grid-gutter-width * ($grid-columns - 9)", "$grid-columns"
				),
				warnings3[11].Description
			);
			Assert.IsTrue(warnings3[11].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 10 - $grid-gutter-width * ($grid-columns - 10)", "$grid-columns"
				),
				warnings3[12].Description
			);
			Assert.IsTrue(warnings3[12].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.AlwaysQuoteColorNamesInInterpolation, "red", "$color-name"),
				warnings3[13].Description
			);
			Assert.IsFalse(warnings3[13].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.AlwaysQuoteColorNamesInInterpolation, "red", "$color-name"),
				warnings3[14].Description
			);
			Assert.IsFalse(warnings3[14].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.AlwaysQuoteColorNamesInInterpolation, "blue", "$color-name"),
				warnings3[15].Description
			);
			Assert.IsFalse(warnings3[15].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.AlwaysQuoteColorNamesInInterpolation, "blue", "$color-name"),
				warnings3[16].Description
			);
			Assert.IsFalse(warnings3[16].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.AlwaysQuoteColorNamesInInterpolation, "green", "$color-name"),
				warnings3[17].Description
			);
			Assert.IsFalse(warnings3[17].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.AlwaysQuoteColorNamesInInterpolation, "green", "$color-name"),
				warnings3[18].Description
			);
			Assert.IsFalse(warnings3[18].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.ColorInversionWithNumberArgumentsDeprecated, 221716),
				warnings3[19].Description
			);
			Assert.IsTrue(warnings3[19].IsDeprecation);
			Assert.AreEqual(string.Format(WarningConstants.UnknownVendorPrefix, "wekbit"), warnings3[20].Description);
			Assert.IsFalse(warnings3[20].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.RepetitiveDeprecationWarningsOmitted, 2),
				warnings3[21].Description
			);
			Assert.IsFalse(warnings3[21].IsDeprecation);

			Assert.AreEqual(16, warnings4.Count);
			Assert.AreEqual(
				string.Format(WarningConstants.NumberValueWithoutPercentagesDeprecated, "saturation", 98),
				warnings4[0].Description
			);
			Assert.IsTrue(warnings4[0].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings4[1].Description);
			Assert.IsFalse(warnings4[1].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.DeprecatedDivisionWithSimpleRecommendation, "$grid-gutter-width", 2),
				warnings4[2].Description
			);
			Assert.IsTrue(warnings4[2].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings4[3].Description);
			Assert.IsFalse(warnings4[3].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings4[4].Description);
			Assert.IsFalse(warnings4[4].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings4[5].Description);
			Assert.IsFalse(warnings4[5].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings4[6].Description);
			Assert.IsFalse(warnings4[6].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings4[7].Description);
			Assert.IsFalse(warnings4[7].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings4[8].Description);
			Assert.IsFalse(warnings4[8].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 7 - $grid-gutter-width * ($grid-columns - 7)", "$grid-columns"
				),
				warnings4[9].Description
			);
			Assert.IsTrue(warnings4[9].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 8 - $grid-gutter-width * ($grid-columns - 8)", "$grid-columns"
				),
				warnings4[10].Description
			);
			Assert.IsTrue(warnings4[10].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 9 - $grid-gutter-width * ($grid-columns - 9)", "$grid-columns"
				),
				warnings4[11].Description
			);
			Assert.IsTrue(warnings4[11].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 10 - $grid-gutter-width * ($grid-columns - 10)", "$grid-columns"
				),
				warnings4[12].Description
			);
			Assert.IsTrue(warnings4[12].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.ColorInversionWithNumberArgumentsDeprecated, 221716),
				warnings4[13].Description
			);
			Assert.IsTrue(warnings4[13].IsDeprecation);
			Assert.AreEqual(string.Format(WarningConstants.UnknownVendorPrefix, "wekbit"), warnings4[14].Description);
			Assert.IsFalse(warnings4[14].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.RepetitiveDeprecationWarningsOmitted, 2),
				warnings4[15].Description
			);
			Assert.IsFalse(warnings4[15].IsDeprecation);

			Assert.AreEqual(23, warnings5.Count);
			Assert.AreEqual(
				string.Format(WarningConstants.NumberValueWithoutPercentagesDeprecated, "saturation", 98),
				warnings5[0].Description
			);
			Assert.IsTrue(warnings5[0].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings5[1].Description);
			Assert.IsFalse(warnings5[1].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.DeprecatedDivisionWithSimpleRecommendation, "$grid-gutter-width", 2),
				warnings5[2].Description
			);
			Assert.IsTrue(warnings5[2].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings5[3].Description);
			Assert.IsFalse(warnings5[3].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings5[4].Description);
			Assert.IsFalse(warnings5[4].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings5[5].Description);
			Assert.IsFalse(warnings5[5].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings5[6].Description);
			Assert.IsFalse(warnings5[6].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings5[7].Description);
			Assert.IsFalse(warnings5[7].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings5[8].Description);
			Assert.IsFalse(warnings5[8].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 7 - $grid-gutter-width * ($grid-columns - 7)", "$grid-columns"
				),
				warnings5[9].Description
			);
			Assert.IsTrue(warnings5[9].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 8 - $grid-gutter-width * ($grid-columns - 8)", "$grid-columns"
				),
				warnings5[10].Description
			);
			Assert.IsTrue(warnings5[10].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 9 - $grid-gutter-width * ($grid-columns - 9)", "$grid-columns"
				),
				warnings5[11].Description
			);
			Assert.IsTrue(warnings5[11].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 10 - $grid-gutter-width * ($grid-columns - 10)", "$grid-columns"
				),
				warnings5[12].Description
			);
			Assert.IsTrue(warnings5[12].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 11 - $grid-gutter-width * ($grid-columns - 11)", "$grid-columns"
				),
				warnings5[13].Description
			);
			Assert.IsTrue(warnings5[13].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 12 - $grid-gutter-width * ($grid-columns - 12)", "$grid-columns"
				),
				warnings5[14].Description
			);
			Assert.IsTrue(warnings5[14].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.AlwaysQuoteColorNamesInInterpolation, "red", "$color-name"),
				warnings5[15].Description
			);
			Assert.IsFalse(warnings5[15].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.AlwaysQuoteColorNamesInInterpolation, "red", "$color-name"),
				warnings5[16].Description
			);
			Assert.IsFalse(warnings5[16].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.AlwaysQuoteColorNamesInInterpolation, "blue", "$color-name"),
				warnings5[17].Description
			);
			Assert.IsFalse(warnings5[17].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.AlwaysQuoteColorNamesInInterpolation, "blue", "$color-name"),
				warnings5[18].Description
			);
			Assert.IsFalse(warnings5[18].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.AlwaysQuoteColorNamesInInterpolation, "green", "$color-name"),
				warnings5[19].Description
			);
			Assert.IsFalse(warnings5[19].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.AlwaysQuoteColorNamesInInterpolation, "green", "$color-name"),
				warnings5[20].Description
			);
			Assert.IsFalse(warnings5[20].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.ColorInversionWithNumberArgumentsDeprecated, 221716),
				warnings5[21].Description
			);
			Assert.IsTrue(warnings5[21].IsDeprecation);
			Assert.AreEqual(string.Format(WarningConstants.UnknownVendorPrefix, "wekbit"), warnings5[22].Description);
			Assert.IsFalse(warnings5[22].IsDeprecation);

			Assert.AreEqual(17, warnings6.Count);
			Assert.AreEqual(
				string.Format(WarningConstants.NumberValueWithoutPercentagesDeprecated, "saturation", 98),
				warnings6[0].Description
			);
			Assert.IsTrue(warnings6[0].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings6[1].Description);
			Assert.IsFalse(warnings6[1].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.DeprecatedDivisionWithSimpleRecommendation, "$grid-gutter-width", 2),
				warnings6[2].Description
			);
			Assert.IsTrue(warnings6[2].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings6[3].Description);
			Assert.IsFalse(warnings6[3].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings6[4].Description);
			Assert.IsFalse(warnings6[4].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings6[5].Description);
			Assert.IsFalse(warnings6[5].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings6[6].Description);
			Assert.IsFalse(warnings6[6].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings6[7].Description);
			Assert.IsFalse(warnings6[7].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings6[8].Description);
			Assert.IsFalse(warnings6[8].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 7 - $grid-gutter-width * ($grid-columns - 7)", "$grid-columns"
				),
				warnings6[9].Description
			);
			Assert.IsTrue(warnings6[9].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 8 - $grid-gutter-width * ($grid-columns - 8)", "$grid-columns"
				),
				warnings6[10].Description
			);
			Assert.IsTrue(warnings6[10].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 9 - $grid-gutter-width * ($grid-columns - 9)", "$grid-columns"
				),
				warnings6[11].Description
			);
			Assert.IsTrue(warnings6[11].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 10 - $grid-gutter-width * ($grid-columns - 10)", "$grid-columns"
				),
				warnings6[12].Description
			);
			Assert.IsTrue(warnings6[12].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 11 - $grid-gutter-width * ($grid-columns - 11)", "$grid-columns"
				),
				warnings6[13].Description
			);
			Assert.IsTrue(warnings6[13].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 12 - $grid-gutter-width * ($grid-columns - 12)", "$grid-columns"
				),
				warnings6[14].Description
			);
			Assert.IsTrue(warnings6[14].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.ColorInversionWithNumberArgumentsDeprecated, 221716),
				warnings6[15].Description
			);
			Assert.IsTrue(warnings6[15].IsDeprecation);
			Assert.AreEqual(string.Format(WarningConstants.UnknownVendorPrefix, "wekbit"), warnings6[16].Description);
			Assert.IsFalse(warnings6[16].IsDeprecation);
		}

		[Test]
		public void UsageOfSilenceDeprecationsDuringCompilation([Values] bool fromFile)
		{
			// Arrange
			var alternativePaths = new List<string> { GenerateSassDirectoryPath("all", "alternative") };

			var withoutSilenceDeprecationsOptions = new CompilationOptions
			{
				SilenceDeprecations = new List<string>(),
				IncludePaths = alternativePaths
			};
			var withSilenceDeprecationsOptions = new CompilationOptions
			{
				SilenceDeprecations = new List<string> { "global-builtin", "import", "slash-div" },
				IncludePaths = alternativePaths
			};

			string inputPath = GenerateSassFilePath("all", "base");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;

			// Act
			IList<ProblemInfo> warnings1;
			IList<ProblemInfo> warnings2;

			using (var sassCompiler = CreateSassCompiler())
			{
				if (fromFile)
				{
					warnings1 = sassCompiler.CompileFile(inputPath, options: withoutSilenceDeprecationsOptions).Warnings;
					warnings2 = sassCompiler.CompileFile(inputPath, options: withSilenceDeprecationsOptions).Warnings;
				}
				else
				{
					warnings1 = sassCompiler.Compile(input, inputPath, options: withoutSilenceDeprecationsOptions).Warnings;
					warnings2 = sassCompiler.Compile(input, inputPath, options: withSilenceDeprecationsOptions).Warnings;
				}
			}

			// Assert
			Assert.AreEqual(22, warnings1.Count);
			Assert.AreEqual(
				string.Format(WarningConstants.NumberValueWithoutPercentagesDeprecated, "saturation", 98),
				warnings1[0].Description
			);
			Assert.IsTrue(warnings1[0].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings1[1].Description);
			Assert.IsFalse(warnings1[1].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.DeprecatedDivisionWithSimpleRecommendation, "$grid-gutter-width", 2),
				warnings1[2].Description
			);
			Assert.IsTrue(warnings1[2].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings1[3].Description);
			Assert.IsFalse(warnings1[3].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings1[4].Description);
			Assert.IsFalse(warnings1[4].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings1[5].Description);
			Assert.IsFalse(warnings1[5].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings1[6].Description);
			Assert.IsFalse(warnings1[6].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings1[7].Description);
			Assert.IsFalse(warnings1[7].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings1[8].Description);
			Assert.IsFalse(warnings1[8].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 7 - $grid-gutter-width * ($grid-columns - 7)", "$grid-columns"
				),
				warnings1[9].Description
			);
			Assert.IsTrue(warnings1[9].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 8 - $grid-gutter-width * ($grid-columns - 8)", "$grid-columns"
				),
				warnings1[10].Description
			);
			Assert.IsTrue(warnings1[10].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 9 - $grid-gutter-width * ($grid-columns - 9)", "$grid-columns"
				),
				warnings1[11].Description
			);
			Assert.IsTrue(warnings1[11].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 10 - $grid-gutter-width * ($grid-columns - 10)", "$grid-columns"
				),
				warnings1[12].Description
			);
			Assert.IsTrue(warnings1[12].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.AlwaysQuoteColorNamesInInterpolation, "red", "$color-name"),
				warnings1[13].Description
			);
			Assert.IsFalse(warnings1[13].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.AlwaysQuoteColorNamesInInterpolation, "red", "$color-name"),
				warnings1[14].Description
			);
			Assert.IsFalse(warnings1[14].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.AlwaysQuoteColorNamesInInterpolation, "blue", "$color-name"),
				warnings1[15].Description
			);
			Assert.IsFalse(warnings1[15].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.AlwaysQuoteColorNamesInInterpolation, "blue", "$color-name"),
				warnings1[16].Description
			);
			Assert.IsFalse(warnings1[16].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.AlwaysQuoteColorNamesInInterpolation, "green", "$color-name"),
				warnings1[17].Description
			);
			Assert.IsFalse(warnings1[17].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.AlwaysQuoteColorNamesInInterpolation, "green", "$color-name"),
				warnings1[18].Description
			);
			Assert.IsFalse(warnings1[18].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.ColorInversionWithNumberArgumentsDeprecated, 221716),
				warnings1[19].Description
			);
			Assert.IsTrue(warnings1[19].IsDeprecation);
			Assert.AreEqual(string.Format(WarningConstants.UnknownVendorPrefix, "wekbit"), warnings1[20].Description);
			Assert.IsFalse(warnings1[20].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.RepetitiveDeprecationWarningsOmitted, 2),
				warnings1[21].Description
			);
			Assert.IsFalse(warnings1[21].IsDeprecation);

			Assert.AreEqual(16, warnings2.Count);
			Assert.AreEqual(
				string.Format(WarningConstants.NumberValueWithoutPercentagesDeprecated, "saturation", 98),
				warnings2[0].Description
			);
			Assert.IsTrue(warnings2[0].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings2[1].Description);
			Assert.IsFalse(warnings2[1].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings2[2].Description);
			Assert.IsFalse(warnings2[2].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings2[3].Description);
			Assert.IsFalse(warnings2[3].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings2[4].Description);
			Assert.IsFalse(warnings2[4].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings2[5].Description);
			Assert.IsFalse(warnings2[5].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings2[6].Description);
			Assert.IsFalse(warnings2[6].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings2[7].Description);
			Assert.IsFalse(warnings2[7].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.AlwaysQuoteColorNamesInInterpolation, "red", "$color-name"),
				warnings2[8].Description
			);
			Assert.IsFalse(warnings2[8].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.AlwaysQuoteColorNamesInInterpolation, "red", "$color-name"),
				warnings2[9].Description
			);
			Assert.IsFalse(warnings2[9].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.AlwaysQuoteColorNamesInInterpolation, "blue", "$color-name"),
				warnings2[10].Description
			);
			Assert.IsFalse(warnings2[10].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.AlwaysQuoteColorNamesInInterpolation, "blue", "$color-name"),
				warnings2[11].Description
			);
			Assert.IsFalse(warnings2[11].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.AlwaysQuoteColorNamesInInterpolation, "green", "$color-name"),
				warnings2[12].Description
			);
			Assert.IsFalse(warnings2[12].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.AlwaysQuoteColorNamesInInterpolation, "green", "$color-name"),
				warnings2[13].Description
			);
			Assert.IsFalse(warnings2[13].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.ColorInversionWithNumberArgumentsDeprecated, 221716),
				warnings2[14].Description
			);
			Assert.IsTrue(warnings2[14].IsDeprecation);
			Assert.AreEqual(string.Format(WarningConstants.UnknownVendorPrefix, "wekbit"), warnings2[15].Description);
			Assert.IsFalse(warnings2[15].IsDeprecation);
		}
	}
}