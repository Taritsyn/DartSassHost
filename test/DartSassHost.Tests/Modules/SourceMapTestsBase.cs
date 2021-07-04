using NUnit.Framework;

namespace DartSassHost.Tests.Modules
{
	public abstract class SourceMapTestsBase : PhysicalFileSystemTestsBase
	{
		public override string BaseDirectoryPath => "modules/source-map";


		protected SourceMapTestsBase(SyntaxType syntaxType)
			: base(syntaxType)
		{ }


		#region Code

		[Test]
		public void UsageOfSourceMapPropertyDuringCompilationOfCode()
		{
			// Arrange
			var sourceMapDisabledOptions = new CompilationOptions { SourceMap = false };
			var sourceMapEnabledOptions = new CompilationOptions { SourceMap = true };

			string inputPath = GenerateSassFilePath("ordinary", "style");
			string input = GetFileContent(inputPath);

			string targetOutputPath1 = GenerateCssFilePath("ordinary", "style");
			string targetOutput1 = GetFileContent(targetOutputPath1);

			string targetOutputPath2 = GenerateCssFilePath("ordinary", "style-with-source-map-url");
			string targetOutput2 = GetFileContent(targetOutputPath2);

			string targetSourceMapPath2 = GenerateSourceMapFilePath("ordinary", "style");
			string targetSourceMap2 = GetFileContent(targetSourceMapPath2);

			// Act
			CompilationResult result1;
			CompilationResult result2;

			using (var compiler = CreateSassCompiler())
			{
				result1 = compiler.Compile(input, inputPath, options: sourceMapDisabledOptions);
				result2 = compiler.Compile(input, inputPath, options: sourceMapEnabledOptions);
			}

			// Assert
			Assert.AreEqual(targetOutput1, result1.CompiledContent);
			Assert.IsEmpty(result1.SourceMap);

			Assert.AreEqual(targetOutput2, result2.CompiledContent);
			Assert.AreEqual(targetSourceMap2, result2.SourceMap);
		}

		[Test]
		public void UsageOfSourceMapIncludeContentsPropertyDuringCompilationOfCode()
		{
			// Arrange
			var sourceMapWithoutContentsOptions = new CompilationOptions
			{
				SourceMap = true,
				SourceMapIncludeContents = false
			};
			var sourceMapWithContentsOptions = new CompilationOptions
			{
				SourceMap = true,
				SourceMapIncludeContents = true
			};

			string inputPath = GenerateSassFilePath("ordinary", "style");
			string input = GetFileContent(inputPath);

			string targetOutputPath1 = GenerateCssFilePath("ordinary", "style-with-source-map-url");
			string targetOutput1 = GetFileContent(targetOutputPath1);

			string targetSourceMapPath1 = GenerateSourceMapFilePath("ordinary", "style");
			string targetSourceMap1 = GetFileContent(targetSourceMapPath1);

			string targetOutputPath2 = targetOutputPath1;
			string targetOutput2 = targetOutput1;

			string targetSourceMapPath2 = GenerateSourceMapFilePath("ordinary", "style-with-contents");
			string targetSourceMap2 = GetFileContent(targetSourceMapPath2);

			// Act
			CompilationResult result1;
			CompilationResult result2;

			using (var compiler = CreateSassCompiler())
			{
				result1 = compiler.Compile(input, inputPath, options: sourceMapWithoutContentsOptions);
				result2 = compiler.Compile(input, inputPath, options: sourceMapWithContentsOptions);
			}

			// Assert
			Assert.AreEqual(targetOutput1, result1.CompiledContent);
			Assert.AreEqual(targetSourceMap1, result1.SourceMap);

			Assert.AreEqual(targetOutput2, result2.CompiledContent);
			Assert.AreEqual(targetSourceMap2, result2.SourceMap);
		}

		[Test]
		public void UsageOfOmitSourceMapUrlPropertyDuringCompilationOfCode()
		{
			// Arrange
			var sourceMapUrlIncludedOptions = new CompilationOptions
			{
				SourceMap = true,
				OmitSourceMapUrl = false
			};
			var sourceMapUrlOmittedOptions = new CompilationOptions
			{
				SourceMap = true,
				OmitSourceMapUrl = true
			};

			string inputPath = GenerateSassFilePath("ordinary", "style");
			string input = GetFileContent(inputPath);

			string targetOutputPath1 = GenerateCssFilePath("ordinary", "style-with-source-map-url");
			string targetOutput1 = GetFileContent(targetOutputPath1);

			string targetSourceMapPath1 = GenerateSourceMapFilePath("ordinary", "style");
			string targetSourceMap1 = GetFileContent(targetSourceMapPath1);

			string targetOutputPath2 = GenerateCssFilePath("ordinary", "style");
			string targetOutput2 = GetFileContent(targetOutputPath2);

			string targetSourceMapPath2 = targetSourceMapPath1;
			string targetSourceMap2 = targetSourceMap1;

			// Act
			CompilationResult result1;
			CompilationResult result2;

			using (var compiler = CreateSassCompiler())
			{
				result1 = compiler.Compile(input, inputPath, options: sourceMapUrlIncludedOptions);
				result2 = compiler.Compile(input, inputPath, options: sourceMapUrlOmittedOptions);
			}

			// Assert
			Assert.AreEqual(targetOutput1, result1.CompiledContent);
			Assert.AreEqual(targetSourceMap1, result1.SourceMap);

			Assert.AreEqual(targetOutput2, result2.CompiledContent);
			Assert.AreEqual(targetSourceMap2, result2.SourceMap);
		}

		[Test]
		public void UsageOfSourceMapRootPathPropertyDuringCompilationOfCode()
		{
			// Arrange
			var sourceMapOptions = new CompilationOptions
			{
				SourceMap = true,
				SourceMapRootPath = "/"
			};

			string inputPath = GenerateSassFilePath("ordinary", "style");
			string input = GetFileContent(inputPath);

			string targetOutputPath = GenerateCssFilePath("ordinary", "style-with-source-map-url");
			string targetOutput = GetFileContent(targetOutputPath);

			string targetSourceMapPath = GenerateSourceMapFilePath("ordinary", "style-with-source-root");
			string targetSourceMap = GetFileContent(targetSourceMapPath);

			// Act
			CompilationResult result;

			using (var compiler = CreateSassCompiler())
			{
				result = compiler.Compile(input, inputPath, options: sourceMapOptions);
			}

			// Assert
			Assert.AreEqual(targetOutput, result.CompiledContent);
			Assert.AreEqual(targetSourceMap, result.SourceMap);
		}

		[Test]
		public void UsageOfInlineSourceMapPropertyDuringCompilationOfCode()
		{
			// Arrange
			var inlineSourceMapDisabledOptions = new CompilationOptions
			{
				SourceMap = true,
				InlineSourceMap = false
			};
			var inlineSourceMapEnabledOptions = new CompilationOptions
			{
				SourceMap = true,
				InlineSourceMap = true
			};

			string inputPath = GenerateSassFilePath("ordinary", "style");
			string input = GetFileContent(inputPath);

			string targetOutputPath1 = GenerateCssFilePath("ordinary", "style-with-source-map-url");
			string targetOutput1 = GetFileContent(targetOutputPath1);

			string targetSourceMapPath1 = GenerateSourceMapFilePath("ordinary", "style");
			string targetSourceMap1 = GetFileContent(targetSourceMapPath1);

			string targetOutputPath2 = GenerateCssFileWithInlineSourceMapFilePath("ordinary",
				"style-with-inline-source-map");
			string targetOutput2 = GetFileContent(targetOutputPath2);

			string targetSourceMapPath2 = targetSourceMapPath1;
			string targetSourceMap2 = targetSourceMap1;

			// Act
			CompilationResult result1;
			CompilationResult result2;

			using (var compiler = CreateSassCompiler())
			{
				result1 = compiler.Compile(input, inputPath, options: inlineSourceMapDisabledOptions);
				result2 = compiler.Compile(input, inputPath, options: inlineSourceMapEnabledOptions);
			}

			// Assert
			Assert.AreEqual(targetOutput1, result1.CompiledContent);
			Assert.AreEqual(targetSourceMap1, result1.SourceMap);

			Assert.AreEqual(targetOutput2, result2.CompiledContent);
			Assert.AreEqual(targetSourceMap2, result2.SourceMap);
		}

		[Test]
		public void UsageOfSourceMapPathParameterDuringCompilationOfCode()
		{
			// Arrange
			var sourceMapOptions = new CompilationOptions { SourceMap = true };

			string inputPath = GenerateSassFilePath("ordinary", "style");
			string input = GetFileContent(inputPath);

			string sourceMapPath = GenerateSourceMapFilePath("ordinary", "maps/style-custom");

			string targetOutputPath = GenerateCssFilePath("ordinary", "style-with-custom-source-map-url");
			string targetOutput = GetFileContent(targetOutputPath);

			string targetSourceMap = GetFileContent(sourceMapPath);

			// Act
			CompilationResult result;

			using (var compiler = CreateSassCompiler())
			{
				result = compiler.Compile(input, inputPath, sourceMapPath: sourceMapPath, options: sourceMapOptions);
			}

			// Assert
			Assert.AreEqual(targetOutput, result.CompiledContent);
			Assert.AreEqual(targetSourceMap, result.SourceMap);
		}

		#endregion

		#region Files

		[Test]
		public void UsageOfSourceMapPropertyDuringCompilationOfFile()
		{
			// Arrange
			var sourceMapDisabledOptions = new CompilationOptions { SourceMap = false };
			var sourceMapEnabledOptions = new CompilationOptions { SourceMap = true };

			string inputPath = GenerateSassFilePath("ordinary", "style");

			string targetOutputPath1 = GenerateCssFilePath("ordinary", "style");
			string targetOutput1 = GetFileContent(targetOutputPath1);

			string targetOutputPath2 = GenerateCssFilePath("ordinary", "style-with-source-map-url");
			string targetOutput2 = GetFileContent(targetOutputPath2);

			string targetSourceMapPath2 = GenerateSourceMapFilePath("ordinary", "style");
			string targetSourceMap2 = GetFileContent(targetSourceMapPath2);

			// Act
			CompilationResult result1;
			CompilationResult result2;

			using (var compiler = CreateSassCompiler())
			{
				result1 = compiler.CompileFile(inputPath, options: sourceMapDisabledOptions);
				result2 = compiler.CompileFile(inputPath, options: sourceMapEnabledOptions);
			}

			// Assert
			Assert.AreEqual(targetOutput1, result1.CompiledContent);
			Assert.IsEmpty(result1.SourceMap);

			Assert.AreEqual(targetOutput2, result2.CompiledContent);
			Assert.AreEqual(targetSourceMap2, result2.SourceMap);
		}

		[Test]
		public void UsageOfSourceMapIncludeContentsPropertyDuringCompilationOfFile()
		{
			// Arrange
			var sourceMapWithoutContentsOptions = new CompilationOptions
			{
				SourceMap = true,
				SourceMapIncludeContents = false
			};
			var sourceMapWithContentsOptions = new CompilationOptions
			{
				SourceMap = true,
				SourceMapIncludeContents = true
			};

			string inputPath = GenerateSassFilePath("ordinary", "style");

			string targetOutputPath1 = GenerateCssFilePath("ordinary", "style-with-source-map-url");
			string targetOutput1 = GetFileContent(targetOutputPath1);

			string targetSourceMapPath1 = GenerateSourceMapFilePath("ordinary", "style");
			string targetSourceMap1 = GetFileContent(targetSourceMapPath1);

			string targetOutputPath2 = targetOutputPath1;
			string targetOutput2 = targetOutput1;

			string targetSourceMapPath2 = GenerateSourceMapFilePath("ordinary", "style-with-contents");
			string targetSourceMap2 = GetFileContent(targetSourceMapPath2);

			// Act
			CompilationResult result1;
			CompilationResult result2;

			using (var compiler = CreateSassCompiler())
			{
				result1 = compiler.CompileFile(inputPath, options: sourceMapWithoutContentsOptions);
				result2 = compiler.CompileFile(inputPath, options: sourceMapWithContentsOptions);
			}

			// Assert
			Assert.AreEqual(targetOutput1, result1.CompiledContent);
			Assert.AreEqual(targetSourceMap1, result1.SourceMap);

			Assert.AreEqual(targetOutput2, result2.CompiledContent);
			Assert.AreEqual(targetSourceMap2, result2.SourceMap);
		}

		[Test]
		public void UsageOfOmitSourceMapUrlPropertyDuringCompilationOfFile()
		{
			// Arrange
			var sourceMapUrlIncludedOptions = new CompilationOptions
			{
				SourceMap = true,
				OmitSourceMapUrl = false
			};
			var sourceMapUrlOmittedOptions = new CompilationOptions
			{
				SourceMap = true,
				OmitSourceMapUrl = true
			};

			string inputPath = GenerateSassFilePath("ordinary", "style");

			string targetOutputPath1 = GenerateCssFilePath("ordinary", "style-with-source-map-url");
			string targetOutput1 = GetFileContent(targetOutputPath1);

			string targetSourceMapPath1 = GenerateSourceMapFilePath("ordinary", "style");
			string targetSourceMap1 = GetFileContent(targetSourceMapPath1);

			string targetOutputPath2 = GenerateCssFilePath("ordinary", "style");
			string targetOutput2 = GetFileContent(targetOutputPath2);

			string targetSourceMapPath2 = targetSourceMapPath1;
			string targetSourceMap2 = targetSourceMap1;

			// Act
			CompilationResult result1;
			CompilationResult result2;

			using (var compiler = CreateSassCompiler())
			{
				result1 = compiler.CompileFile(inputPath, options: sourceMapUrlIncludedOptions);
				result2 = compiler.CompileFile(inputPath, options: sourceMapUrlOmittedOptions);
			}

			// Assert
			Assert.AreEqual(targetOutput1, result1.CompiledContent);
			Assert.AreEqual(targetSourceMap1, result1.SourceMap);

			Assert.AreEqual(targetOutput2, result2.CompiledContent);
			Assert.AreEqual(targetSourceMap2, result2.SourceMap);
		}

		[Test]
		public void UsageOfSourceMapRootPathPropertyDuringCompilationOfFile()
		{
			// Arrange
			var sourceMapOptions = new CompilationOptions
			{
				SourceMap = true,
				SourceMapRootPath = "/"
			};

			string inputPath = GenerateSassFilePath("ordinary", "style");

			string targetOutputPath = GenerateCssFilePath("ordinary", "style-with-source-map-url");
			string targetOutput = GetFileContent(targetOutputPath);

			string targetSourceMapPath = GenerateSourceMapFilePath("ordinary", "style-with-source-root");
			string targetSourceMap = GetFileContent(targetSourceMapPath);

			// Act
			CompilationResult result;

			using (var compiler = CreateSassCompiler())
			{
				result = compiler.CompileFile(inputPath, options: sourceMapOptions);
			}

			// Assert
			Assert.AreEqual(targetOutput, result.CompiledContent);
			Assert.AreEqual(targetSourceMap, result.SourceMap);
		}

		[Test]
		public void UsageOfInlineSourceMapPropertyDuringCompilationOfFile()
		{
			// Arrange
			var inlineSourceMapDisabledOptions = new CompilationOptions
			{
				SourceMap = true,
				InlineSourceMap = false
			};
			var inlineSourceMapEnabledOptions = new CompilationOptions
			{
				SourceMap = true,
				InlineSourceMap = true
			};

			string inputPath = GenerateSassFilePath("ordinary", "style");

			string targetOutputPath1 = GenerateCssFilePath("ordinary", "style-with-source-map-url");
			string targetOutput1 = GetFileContent(targetOutputPath1);

			string targetSourceMapPath1 = GenerateSourceMapFilePath("ordinary", "style");
			string targetSourceMap1 = GetFileContent(targetSourceMapPath1);

			string targetOutputPath2 = GenerateCssFileWithInlineSourceMapFilePath("ordinary",
				"style-with-inline-source-map");
			string targetOutput2 = GetFileContent(targetOutputPath2);

			string targetSourceMapPath2 = targetSourceMapPath1;
			string targetSourceMap2 = targetSourceMap1;

			// Act
			CompilationResult result1;
			CompilationResult result2;

			using (var compiler = CreateSassCompiler())
			{
				result1 = compiler.CompileFile(inputPath, options: inlineSourceMapDisabledOptions);
				result2 = compiler.CompileFile(inputPath, options: inlineSourceMapEnabledOptions);
			}

			// Assert
			Assert.AreEqual(targetOutput1, result1.CompiledContent);
			Assert.AreEqual(targetSourceMap1, result1.SourceMap);

			Assert.AreEqual(targetOutput2, result2.CompiledContent);
			Assert.AreEqual(targetSourceMap2, result2.SourceMap);
		}

		[Test]
		public void UsageOfSourceMapPathParameterDuringCompilationOfFile()
		{
			// Arrange
			var sourceMapOptions = new CompilationOptions { SourceMap = true };

			string inputPath = GenerateSassFilePath("ordinary", "style");
			string sourceMapPath = GenerateSourceMapFilePath("ordinary", "maps/style-custom");

			string targetOutputPath = GenerateCssFilePath("ordinary", "style-with-custom-source-map-url");
			string targetOutput = GetFileContent(targetOutputPath);

			string targetSourceMap = GetFileContent(sourceMapPath);

			// Act
			CompilationResult result;

			using (var compiler = CreateSassCompiler())
			{
				result = compiler.CompileFile(inputPath, sourceMapPath: sourceMapPath, options: sourceMapOptions);
			}

			// Assert
			Assert.AreEqual(targetOutput, result.CompiledContent);
			Assert.AreEqual(targetSourceMap, result.SourceMap);
		}

		#endregion
	}
}