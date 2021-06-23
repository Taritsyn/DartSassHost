using System;

using Moq;
using NUnit.Framework;

namespace DartSassHost.Tests.Simple
{
	public abstract class VirtualFileManagerTestsBase : TestsBase
	{
		protected string _siteInputFileAbsolutePath;
		protected string _siteInputFileContent;
		private readonly string _siteOutputFileContent;
		protected string _siteSourceMapFileContent;

		private readonly string _appAbsolutePath;
		protected string _appInputFileRelativePath;
		protected string _appInputFileAbsolutePath;
		protected string _appInputFileContent;
		private readonly string _appOutputFileContent;
		protected string _appSourceMapFileContent;


		protected VirtualFileManagerTestsBase()
		{
			_siteOutputFileContent = ".icons {\n" +
				"  display: inline-block;\n" +
				"  background-repeat: no-repeat;\n" +
				"  width: 16px;\n" +
				"  height: 16px;\n" +
				"  line-height: 0;\n" +
				"  vertical-align: bottom;\n" +
				"}\n\n" +
				".icon-google-plus {\n" +
				"  background-image: url(../images/google-plus.svg);\n" +
				"}\n\n" +
				".icon-headphone {\n" +
				"  background-image: URL(\"/images/icons/headphone.gif\");\n" +
				"}\n\n" +
				".icon-monitor {\n" +
				"  background-image: url(\"/images/icons/monitor.png\");\n" +
				"}\n\n" +
				".icon-robot {\n" +
				"  background-image: url(\"/images/icons/robot.png\");\n" +
				"}\n\n" +
				".icon-usb-flash-drive {\n" +
				"  background-image: url(/images/icons/usb-flash-drive.png);\n" +
				"}\n\n" +
				"/*# sourceMappingURL=site.css.map */"
				;

			_appAbsolutePath = "/app01";
			_appOutputFileContent = ".icons {\n" +
				"  display: inline-block;\n" +
				"  background-repeat: no-repeat;\n" +
				"  width: 16px;\n" +
				"  height: 16px;\n" +
				"  line-height: 0;\n" +
				"  vertical-align: bottom;\n" +
				"}\n\n" +
				".icon-google-plus {\n" +
				"  background-image: url(../images/google-plus.svg);\n" +
				"}\n\n" +
				".icon-headphone {\n" +
				"  background-image: URL(\"/app01/images/icons/headphone.gif\");\n" +
				"}\n\n" +
				".icon-monitor {\n" +
				"  background-image: url(\"/app01/images/icons/monitor.png\");\n" +
				"}\n\n" +
				".icon-robot {\n" +
				"  background-image: url(\"/app01/images/icons/robot.png\");\n" +
				"}\n\n" +
				".icon-usb-flash-drive {\n" +
				"  background-image: url(/app01/images/icons/usb-flash-drive.png);\n" +
				"}\n\n" +
				"/*# sourceMappingURL=app.css.map */"
				;
		}


		#region Code

		[Test]
		public void CompilationOfCodeWithAppAbsolutePaths()
		{
			// Arrange
			var virtualFileManagerMock = new Mock<IFileManager>();
			virtualFileManagerMock
				.SetupGet(fm => fm.SupportsVirtualPaths)
				.Returns(true)
				;
			virtualFileManagerMock
				.Setup(fm => fm.GetCurrentDirectory())
				.Returns("/")
				;

			IFileManager virtualFileManager = new VirtualFileManager(virtualFileManagerMock);
			var options = new CompilationOptions { SourceMap = true };

			// Act
			CompilationResult result;

			using (var compiler = new SassCompiler(virtualFileManager))
			{
				result = compiler.Compile(_siteInputFileContent, _siteInputFileAbsolutePath, options: options);
			}

			// Assert
			Assert.AreEqual(_siteOutputFileContent, result.CompiledContent);
			Assert.AreEqual(1, result.IncludedFilePaths.Count);
			Assert.AreEqual(_siteInputFileAbsolutePath, result.IncludedFilePaths[0]);
			Assert.AreEqual(_siteSourceMapFileContent, result.SourceMap);
		}

		[Test]
		public void CompilationOfCodeWithAppRelativePaths()
		{
			var virtualFileManagerMock = new Mock<IFileManager>();
			virtualFileManagerMock
				.SetupGet(fm => fm.SupportsVirtualPaths)
				.Returns(true)
				;
			virtualFileManagerMock
				.Setup(fm => fm.GetCurrentDirectory())
				.Returns(_appAbsolutePath + "/")
				;

			IFileManager virtualFileManager = new VirtualFileManager(virtualFileManagerMock, _appAbsolutePath);
			var options = new CompilationOptions { SourceMap = true };

			// Act
			CompilationResult result;

			using (var compiler = new SassCompiler(virtualFileManager))
			{
				result = compiler.Compile(_appInputFileContent, _appInputFileRelativePath, options: options);
			}

			// Assert
			Assert.AreEqual(_appOutputFileContent, result.CompiledContent);
			Assert.AreEqual(1, result.IncludedFilePaths.Count);
			Assert.AreEqual(_appInputFileAbsolutePath, result.IncludedFilePaths[0]);
			Assert.AreEqual(_appSourceMapFileContent, result.SourceMap);
		}

		#endregion

		#region Files

		[Test]
		public void CompilationOfFileWithAppAbsolutePaths()
		{
			// Arrange
			var virtualFileManagerMock = new Mock<IFileManager>();
			virtualFileManagerMock
				.SetupGet(fm => fm.SupportsVirtualPaths)
				.Returns(true)
				;
			virtualFileManagerMock
				.Setup(fm => fm.GetCurrentDirectory())
				.Returns("/")
				;
			virtualFileManagerMock
				.Setup(fm => fm.FileExists(_siteInputFileAbsolutePath))
				.Returns(true)
				;
			virtualFileManagerMock
				.Setup(fm => fm.ReadFile(_siteInputFileAbsolutePath))
				.Returns(_siteInputFileContent)
				;

			IFileManager virtualFileManager = new VirtualFileManager(virtualFileManagerMock);
			var options = new CompilationOptions { SourceMap = true };

			// Act
			CompilationResult result;

			using (var compiler = new SassCompiler(virtualFileManager))
			{
				result = compiler.CompileFile(_siteInputFileAbsolutePath, options: options);
			}

			// Assert
			Assert.AreEqual(_siteOutputFileContent, result.CompiledContent);
			Assert.AreEqual(1, result.IncludedFilePaths.Count);
			Assert.AreEqual(_siteInputFileAbsolutePath, result.IncludedFilePaths[0]);
			Assert.AreEqual(_siteSourceMapFileContent, result.SourceMap);
		}

		[Test]
		public void CompilationOfFileWithAppRelativePaths()
		{
			var virtualFileManagerMock = new Mock<IFileManager>();
			virtualFileManagerMock
				.SetupGet(fm => fm.SupportsVirtualPaths)
				.Returns(true)
				;
			virtualFileManagerMock
				.Setup(fm => fm.GetCurrentDirectory())
				.Returns(_appAbsolutePath + "/")
				;
			virtualFileManagerMock
				.Setup(fm => fm.FileExists(_appInputFileRelativePath))
				.Returns(true)
				;
			virtualFileManagerMock
				.Setup(fm => fm.FileExists(_appInputFileAbsolutePath))
				.Returns(true)
				;
			virtualFileManagerMock
				.Setup(fm => fm.ReadFile(_appInputFileAbsolutePath))
				.Returns(_appInputFileContent)
				;

			IFileManager virtualFileManager = new VirtualFileManager(virtualFileManagerMock, _appAbsolutePath);
			var options = new CompilationOptions { SourceMap = true };

			// Act
			CompilationResult result;

			using (var compiler = new SassCompiler(virtualFileManager))
			{
				result = compiler.CompileFile(_appInputFileRelativePath, options: options);
			}

			// Assert
			Assert.AreEqual(_appOutputFileContent, result.CompiledContent);
			Assert.AreEqual(1, result.IncludedFilePaths.Count);
			Assert.AreEqual(_appInputFileAbsolutePath, result.IncludedFilePaths[0]);
			Assert.AreEqual(_appSourceMapFileContent, result.SourceMap);
		}

		#endregion
	}
}