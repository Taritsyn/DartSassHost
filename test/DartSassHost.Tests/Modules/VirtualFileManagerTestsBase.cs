using System.Collections.Generic;

using Moq;
using NUnit.Framework;

namespace DartSassHost.Tests.Modules
{
	public abstract class VirtualFileManagerTestsBase : TestsBase
	{
		protected string _siteInputFileAbsolutePath;
		protected string _siteInputFileContent;
		protected Dictionary<string, string> _siteImportedFiles;
		private readonly string _siteOutputFileContent;
		protected List<string> _siteIncludedFilePaths;
		protected string _siteSourceMapFileContent;

		private readonly string _appAbsolutePath;
		protected string _appInputFileRelativePath;
		protected string _appInputFileAbsolutePath;
		protected string _appInputFileContent;
		protected Dictionary<string, string> _appImportedFiles;
		private readonly string _appOutputFileContent;
		protected List<string> _appIncludedFilePaths;
		protected string _appSourceMapFileContent;


		protected VirtualFileManagerTestsBase()
		{
			_siteImportedFiles = new Dictionary<string, string>();
			_siteImportedFiles["/styles/fonts/bebas-neue.css"] = "@font-face {\n" +
				"  font-family: \"Bebas Neue\";\n" +
				"  src: url(\"/fonts/bebas-neue/bebas-neue-webfont.eot\");\n" +
				"  src: url(\"/fonts/bebas-neue/bebas-neue-webfont.eot?#iefix\") format(\"embedded-opentype\"),\n" +
				"    url(\"/fonts/bebas-neue/bebas-neue-webfont.woff\") format(\"woff\"),\n" +
				"    url(\"/fonts/bebas-neue/bebas-neue-webfont.ttf\") format(\"truetype\"),\n" +
				"    url(\"/fonts/bebas-neue/bebas-neue-webfont.svg#bebas_neueregular\") format(\"svg\");\n" +
				"  font-weight: normal;\n" +
				"  font-style: normal;\n" +
				"}"
				;
			_siteOutputFileContent = "html,\n" +
				"body,\n" +
				"ul,\n" +
				"ol {\n" +
				"  margin: 0;\n" +
				"  padding: 0;\n" +
				"}\n\n" +
				".icons {\n" +
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
				"@font-face {\n" +
				"  font-family: \"Bebas Neue\";\n" +
				"  src: url(\"/fonts/bebas-neue/bebas-neue-webfont.eot\");\n" +
				"  src: url(\"/fonts/bebas-neue/bebas-neue-webfont.eot?#iefix\") format(\"embedded-opentype\"), " +
				"url(\"/fonts/bebas-neue/bebas-neue-webfont.woff\") format(\"woff\"), " +
				"url(\"/fonts/bebas-neue/bebas-neue-webfont.ttf\") format(\"truetype\"), " +
				"url(\"/fonts/bebas-neue/bebas-neue-webfont.svg#bebas_neueregular\") format(\"svg\");\n" +
				"  font-weight: normal;\n" +
				"  font-style: normal;\n" +
				"}\n" +
				"@font-face {\n" +
				"  font-family: \"League Gothic\";\n" +
				"  src: url(\"/fonts/league-gothic/league-gothic-webfont.eot\");\n" +
				"  src: url(\"/fonts/league-gothic/league-gothic-webfont.eot?#iefix\") format(\"embedded-opentype\"), " +
				"url(\"/fonts/league-gothic/league-gothic-webfont.woff\") format(\"woff\"), " +
				"url(\"/fonts/league-gothic/league-gothic-webfont.ttf\") format(\"truetype\"), " +
				"url(\"/fonts/league-gothic/league-gothic-webfont.svg#league-gothicregular\") format(\"svg\");\n" +
				"  font-weight: normal;\n" +
				"  font-style: normal;\n" +
				"}\n" +
				"body {\n" +
				"  background-color: #fef;\n" +
				"  color: #699;\n" +
				"  font-size: 0.85em;\n" +
				"  font-family: \"League Gothic\", Helvetica, Sans-Serif;\n" +
				"}\n\n" +
				".sidebar {\n" +
				"  background-color: #f8f9fa;\n" +
				"  color: #6b717f;\n" +
				"  font-family: \"Bebas Neue\", Arial, Sans-Serif;\n" +
				"  border: 1px solid #f5f6f8;\n" +
				"  border-radius: 5px;\n" +
				"  -webkit-border-radius: 5px;\n" +
				"  -moz-border-radius: 5px;\n" +
				"}\n\n" +
				"/*# sourceMappingURL=site.css.map */"
				;

			_appAbsolutePath = "/app01";
			_appImportedFiles = new Dictionary<string, string>();
			_appImportedFiles["/app01/styles/fonts/bebas-neue.css"] = "@font-face {\n" +
				"  font-family: \"Bebas Neue\";\n" +
				"  src: url(\"~/fonts/bebas-neue/bebas-neue-webfont.eot\");\n" +
				"  src: url(\"~/fonts/bebas-neue/bebas-neue-webfont.eot?#iefix\") format(\"embedded-opentype\"),\n" +
				"    url(\"~/fonts/bebas-neue/bebas-neue-webfont.woff\") format(\"woff\"),\n" +
				"    url(\"~/fonts/bebas-neue/bebas-neue-webfont.ttf\") format(\"truetype\"),\n" +
				"    url(\"~/fonts/bebas-neue/bebas-neue-webfont.svg#bebas_neueregular\") format(\"svg\");\n" +
				"  font-weight: normal;\n" +
				"  font-style: normal;\n" +
				"}"
				;
			_appOutputFileContent = "html,\n" +
				"body,\n" +
				"ul,\n" +
				"ol {\n" +
				"  margin: 0;\n" +
				"  padding: 0;\n" +
				"}\n\n" +
				".icons {\n" +
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
				"@font-face {\n" +
				"  font-family: \"Bebas Neue\";\n" +
				"  src: url(\"/app01/fonts/bebas-neue/bebas-neue-webfont.eot\");\n" +
				"  src: url(\"/app01/fonts/bebas-neue/bebas-neue-webfont.eot?#iefix\") format(\"embedded-opentype\"), " +
				"url(\"/app01/fonts/bebas-neue/bebas-neue-webfont.woff\") format(\"woff\"), " +
				"url(\"/app01/fonts/bebas-neue/bebas-neue-webfont.ttf\") format(\"truetype\"), " +
				"url(\"/app01/fonts/bebas-neue/bebas-neue-webfont.svg#bebas_neueregular\") format(\"svg\");\n" +
				"  font-weight: normal;\n" +
				"  font-style: normal;\n" +
				"}\n" +
				"@font-face {\n" +
				"  font-family: \"League Gothic\";\n" +
				"  src: url(\"/app01/fonts/league-gothic/league-gothic-webfont.eot\");\n" +
				"  src: url(\"/app01/fonts/league-gothic/league-gothic-webfont.eot?#iefix\") format(\"embedded-opentype\"), " +
				"url(\"/app01/fonts/league-gothic/league-gothic-webfont.woff\") format(\"woff\"), " +
				"url(\"/app01/fonts/league-gothic/league-gothic-webfont.ttf\") format(\"truetype\"), " +
				"url(\"/app01/fonts/league-gothic/league-gothic-webfont.svg#league-gothicregular\") format(\"svg\");\n" +
				"  font-weight: normal;\n" +
				"  font-style: normal;\n" +
				"}\n" +
				"body {\n" +
				"  background-color: #fef;\n" +
				"  color: #699;\n" +
				"  font-size: 0.85em;\n" +
				"  font-family: \"League Gothic\", Helvetica, Sans-Serif;\n" +
				"}\n\n" +
				".sidebar {\n" +
				"  background-color: #f8f9fa;\n" +
				"  color: #6b717f;\n" +
				"  font-family: \"Bebas Neue\", Arial, Sans-Serif;\n" +
				"  border: 1px solid #f5f6f8;\n" +
				"  border-radius: 5px;\n" +
				"  -webkit-border-radius: 5px;\n" +
				"  -moz-border-radius: 5px;\n" +
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
			virtualFileManagerMock
				.Setup(fm => fm.FileExists(It.IsAny<string>()))
				.Returns((string p) => {
					return _siteImportedFiles.ContainsKey(p);
				})
				;
			virtualFileManagerMock
				.Setup(fm => fm.ReadFile(It.IsAny<string>()))
				.Returns((string p) => {
					return _siteImportedFiles[p];
				})
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
			Assert.AreEqual(_siteIncludedFilePaths, result.IncludedFilePaths);
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
			virtualFileManagerMock
				.Setup(fm => fm.FileExists(It.IsAny<string>()))
				.Returns((string p) => {
					return _appImportedFiles.ContainsKey(p);
				})
				;
			virtualFileManagerMock
				.Setup(fm => fm.ReadFile(It.IsAny<string>()))
				.Returns((string p) => {
					return _appImportedFiles[p];
				})
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
			Assert.AreEqual(_appIncludedFilePaths, result.IncludedFilePaths);
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
				.Setup(fm => fm.FileExists(It.IsAny<string>()))
				.Returns((string p) => {
					if (p == _siteInputFileAbsolutePath)
					{
						return true;
					}

					return _siteImportedFiles.ContainsKey(p);
				})
				;
			virtualFileManagerMock
				.Setup(fm => fm.ReadFile(It.IsAny<string>()))
				.Returns((string p) => {
					if (p == _siteInputFileAbsolutePath)
					{
						return _siteInputFileContent;
					}

					return _siteImportedFiles[p];
				})
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
			Assert.AreEqual(_siteIncludedFilePaths, result.IncludedFilePaths);
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
				.Setup(fm => fm.FileExists(It.IsAny<string>()))
				.Returns((string p) => {
					if (p == _appInputFileRelativePath || p == _appInputFileAbsolutePath)
					{
						return true;
					}

					return _appImportedFiles.ContainsKey(p);
				})
				;
			virtualFileManagerMock
				.Setup(fm => fm.ReadFile(It.IsAny<string>()))
				.Returns((string p) => {
					if (p == _appInputFileAbsolutePath)
					{
						return _appInputFileContent;
					}

					return _appImportedFiles[p];
				})
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
			Assert.AreEqual(_appIncludedFilePaths, result.IncludedFilePaths);
			Assert.AreEqual(_appSourceMapFileContent, result.SourceMap);
		}

		#endregion
	}
}