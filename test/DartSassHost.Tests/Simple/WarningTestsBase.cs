using System.Collections.Generic;

using NUnit.Framework;

namespace DartSassHost.Tests.Simple
{
	public abstract class WarningTestsBase : PhysicalFileSystemTestsBase
	{
		public override string BaseDirectoryPath => "simple/warnings";


		protected WarningTestsBase(SyntaxType syntaxType)
			: base(syntaxType)
		{ }


		[Test]
		public void UsageOfWarningLevelPropertyDuringCompilation([Values]bool fromFile)
		{
			// Arrange
			var quietWarningLevelOptions = new CompilationOptions { WarningLevel = WarningLevel.Quiet };
			var defaultWarningLevelOptions = new CompilationOptions { WarningLevel = WarningLevel.Default };
			var verboseWarningLevelOptions = new CompilationOptions { WarningLevel = WarningLevel.Verbose };

			string inputPath = GenerateSassFilePath("all", "style");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;

			// Act
			IList<ProblemInfo> warnings1;
			IList<ProblemInfo> warnings2;
			IList<ProblemInfo> warnings3;

			using (var sassCompiler = CreateSassCompiler())
			{
				if (fromFile)
				{
					warnings1 = sassCompiler.CompileFile(inputPath, options: quietWarningLevelOptions).Warnings;
					warnings2 = sassCompiler.CompileFile(inputPath, options: defaultWarningLevelOptions).Warnings;
					warnings3 = sassCompiler.CompileFile(inputPath, options: verboseWarningLevelOptions).Warnings;
				}
				else
				{
					warnings1 = sassCompiler.Compile(input, inputPath, options: quietWarningLevelOptions).Warnings;
					warnings2 = sassCompiler.Compile(input, inputPath, options: defaultWarningLevelOptions).Warnings;
					warnings3 = sassCompiler.Compile(input, inputPath, options: verboseWarningLevelOptions).Warnings;
				}
			}

			// Assert
			Assert.AreEqual(0, warnings1.Count);

			Assert.AreEqual(16, warnings2.Count);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings2[0].Description);
			Assert.IsFalse(warnings2[0].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.DeprecatedDivisionWithSimpleRecommendation, "$grid-gutter-width", 2),
				warnings2[1].Description
			);
			Assert.IsTrue(warnings2[1].IsDeprecation);
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
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 7 - $grid-gutter-width * ($grid-columns - 7)", "$grid-columns"
				),
				warnings2[8].Description
			);
			Assert.IsTrue(warnings2[8].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 8 - $grid-gutter-width * ($grid-columns - 8)", "$grid-columns"
				),
				warnings2[9].Description
			);
			Assert.IsTrue(warnings2[9].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 9 - $grid-gutter-width * ($grid-columns - 9)", "$grid-columns"
				),
				warnings2[10].Description
			);
			Assert.IsTrue(warnings2[10].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 10 - $grid-gutter-width * ($grid-columns - 10)", "$grid-columns"
				),
				warnings2[11].Description
			);
			Assert.IsTrue(warnings2[11].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.ColorInversionWithNumberArgumentsDeprecated, 221716),
				warnings2[12].Description
			);
			Assert.IsTrue(warnings2[12].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.GlobalBuiltinFunctionDeprecated, "list.index"),
				warnings2[13].Description
			);
			Assert.IsTrue(warnings2[13].IsDeprecation);
			Assert.AreEqual(string.Format(WarningConstants.UnknownVendorPrefix, "wekbit"), warnings2[14].Description);
			Assert.IsFalse(warnings2[14].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.RepetitiveDeprecationWarningsOmitted, 2),
				warnings2[15].Description
			);
			Assert.IsFalse(warnings2[15].IsDeprecation);

			Assert.AreEqual(17, warnings3.Count);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings3[0].Description);
			Assert.IsFalse(warnings3[0].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.DeprecatedDivisionWithSimpleRecommendation, "$grid-gutter-width", 2),
				warnings3[1].Description
			);
			Assert.IsTrue(warnings3[1].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings3[2].Description);
			Assert.IsFalse(warnings3[2].IsDeprecation);
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
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 7 - $grid-gutter-width * ($grid-columns - 7)", "$grid-columns"
				),
				warnings3[8].Description
			);
			Assert.IsTrue(warnings3[8].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 8 - $grid-gutter-width * ($grid-columns - 8)", "$grid-columns"
				),
				warnings3[9].Description
			);
			Assert.IsTrue(warnings3[9].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 9 - $grid-gutter-width * ($grid-columns - 9)", "$grid-columns"
				),
				warnings3[10].Description
			);
			Assert.IsTrue(warnings3[10].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 10 - $grid-gutter-width * ($grid-columns - 10)", "$grid-columns"
				),
				warnings3[11].Description
			);
			Assert.IsTrue(warnings3[11].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 11 - $grid-gutter-width * ($grid-columns - 11)", "$grid-columns"
				),
				warnings3[12].Description
			);
			Assert.IsTrue(warnings3[12].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 12 - $grid-gutter-width * ($grid-columns - 12)", "$grid-columns"
				),
				warnings3[13].Description
			);
			Assert.IsTrue(warnings3[13].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.ColorInversionWithNumberArgumentsDeprecated, 221716),
				warnings3[14].Description
			);
			Assert.IsTrue(warnings3[14].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.GlobalBuiltinFunctionDeprecated, "list.index"),
				warnings3[15].Description
			);
			Assert.IsTrue(warnings3[15].IsDeprecation);
			Assert.AreEqual(string.Format(WarningConstants.UnknownVendorPrefix, "wekbit"), warnings3[16].Description);
			Assert.IsFalse(warnings3[16].IsDeprecation);
		}

		[Test]
		public void UsageOfSilenceDeprecationsPropertyDuringCompilation([Values] bool fromFile)
		{
			// Arrange
			var withoutSilenceDeprecationsOptions = new CompilationOptions
			{
				SilenceDeprecations = new List<string>()
			};
			var withSilenceDeprecationsOptions = new CompilationOptions
			{
				SilenceDeprecations = new List<string> { "global-builtin", "import", "slash-div" }
			};

			string inputPath = GenerateSassFilePath("all", "style");
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
			Assert.AreEqual(16, warnings1.Count);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings1[0].Description);
			Assert.IsFalse(warnings1[0].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.DeprecatedDivisionWithSimpleRecommendation, "$grid-gutter-width", 2),
				warnings1[1].Description
			);
			Assert.IsTrue(warnings1[1].IsDeprecation);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings1[2].Description);
			Assert.IsFalse(warnings1[2].IsDeprecation);
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
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 7 - $grid-gutter-width * ($grid-columns - 7)", "$grid-columns"
				),
				warnings1[8].Description
			);
			Assert.IsTrue(warnings1[8].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 8 - $grid-gutter-width * ($grid-columns - 8)", "$grid-columns"
				),
				warnings1[9].Description
			);
			Assert.IsTrue(warnings1[9].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 9 - $grid-gutter-width * ($grid-columns - 9)", "$grid-columns"
				),
				warnings1[10].Description
			);
			Assert.IsTrue(warnings1[10].IsDeprecation);
			Assert.AreEqual(
				string.Format(
					WarningConstants.DeprecatedDivisionWithComplexRecommendation,
					"100% * 10 - $grid-gutter-width * ($grid-columns - 10)", "$grid-columns"
				),
				warnings1[11].Description
			);
			Assert.IsTrue(warnings1[11].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.ColorInversionWithNumberArgumentsDeprecated, 221716),
				warnings1[12].Description
			);
			Assert.IsTrue(warnings1[12].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.GlobalBuiltinFunctionDeprecated, "list.index"),
				warnings1[13].Description
			);
			Assert.IsTrue(warnings1[13].IsDeprecation);
			Assert.AreEqual(string.Format(WarningConstants.UnknownVendorPrefix, "wekbit"), warnings1[14].Description);
			Assert.IsFalse(warnings1[14].IsDeprecation);
			Assert.AreEqual(
				string.Format(WarningConstants.RepetitiveDeprecationWarningsOmitted, 2),
				warnings1[15].Description
			);
			Assert.IsFalse(warnings1[15].IsDeprecation);

			Assert.AreEqual(9, warnings2.Count);
			Assert.AreEqual(WarningConstants.MathDivOnlySupportNumberArguments, warnings2[0].Description);
			Assert.IsFalse(warnings2[0].IsDeprecation);
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
			Assert.AreEqual(
				string.Format(WarningConstants.ColorInversionWithNumberArgumentsDeprecated, 221716),
				warnings2[7].Description
			);
			Assert.IsTrue(warnings2[7].IsDeprecation);
			Assert.AreEqual(string.Format(WarningConstants.UnknownVendorPrefix, "wekbit"), warnings2[8].Description);
			Assert.IsFalse(warnings2[8].IsDeprecation);
		}

		[Test]
		public void UsageOfFutureDeprecationsPropertyDuringCompilation([Values] bool fromFile)
		{
			// Arrange
			var withoutFutureDeprecationsOptions = new CompilationOptions
			{
				FutureDeprecations = new List<string>()
			};
			var withFutureDeprecationsOptions = new CompilationOptions
			{
				FutureDeprecations = new List<string> { "calc-interp" }
			};

			string inputPath = GenerateSassFilePath("calc-interpolation", "style");
			string input = !fromFile ? GetFileContent(inputPath) : string.Empty;

			// Act
			IList<ProblemInfo> warnings1;
			IList<ProblemInfo> warnings2;

			using (var sassCompiler = CreateSassCompiler())
			{
				if (fromFile)
				{
					warnings1 = sassCompiler.CompileFile(inputPath, options: withoutFutureDeprecationsOptions).Warnings;
					warnings2 = sassCompiler.CompileFile(inputPath, options: withFutureDeprecationsOptions).Warnings;
				}
				else
				{
					warnings1 = sassCompiler.Compile(input, inputPath, options: withoutFutureDeprecationsOptions).Warnings;
					warnings2 = sassCompiler.Compile(input, inputPath, options: withFutureDeprecationsOptions).Warnings;
				}
			}

			// Assert
			Assert.AreEqual(0, warnings1.Count);

			Assert.AreEqual(1, warnings2.Count);
			Assert.AreEqual(string.Format(WarningConstants.DeprecationIsNotFuture, "calc-interp"), warnings2[0].Description);
			Assert.IsFalse(warnings2[0].IsDeprecation);
		}
	}
}