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
			_siteOutputFileContent = "body {\n" +
				"  background-image: url(\"data:image/svg+xml," +
				"<svg xmlns='http://www.w3.org/2000/svg' width='20' height='20' viewBox='0 0 20 20'>" +
				"<g stroke-width='1' fill='none' stroke='gray' stroke-miterlimit='8'>" +
				"<polygon points='15.5,13.5 8,1.5 0.5,13.5'/>" +
				"</g>" +
				"</svg>" +
				"\");\n" +
				"}\n\n" +
				".icons {\n" +
				"  display: inline-block;\n" +
				"  background-repeat: no-repeat;\n" +
				"  width: 16px;\n" +
				"  height: 16px;\n" +
				"  line-height: 0;\n" +
				"  vertical-align: bottom;\n" +
				"}\n\n" +
				".icon-computer {\n" +
				"  background-image: url(../images/computer.gif);\n" +
				"}\n\n" +
				".icon-database {\n" +
				"  background-image: url(\"../images/database.png\");\n" +
				"}\n\n" +
				".icon-google-plus {\n" +
				"  background-image: url(\"../images/google-plus.svg\");\n" +
				"}\n\n" +
				".icon-headphone {\n" +
				"  background-image: url(/images/icons/headphone[1].gif);\n" +
				"}\n\n" +
				".icon-joystick {\n" +
				"  background-image: Url(\"/images/icons/joystick(2).png\");\n" +
				"}\n\n" +
				".icon-keyboard {\n" +
				"  background-image: URL(\"/images/icons/keyboard{3}.png\");\n" +
				"}\n\n" +
				".icon-license {\n" +
				"  background-image: url(/images/icons/license.png);\n" +
				"}\n\n" +
				".icon-monitor {\n" +
				"  background-image: url(\"/images/icons/@monitor.png\");\n" +
				"}\n\n" +
				".icon-network {\n" +
				"  background-image: url(\"/images/icons/$network.png\");\n" +
				"}\n\n" +
				".icon-open-source {\n" +
				"  background-image: url(\"/images/icons/open_source.png\");\n" +
				"}\n\n" +
				".icon-printer {\n" +
				"  background-image: url(\"/images/icons/printer.png\");\n" +
				"}\n\n" +
				".icon-qr-code {\n" +
				"  background-image: url(/images/icons/qr-code.png);\n" +
				"}\n\n" +
				".icon-radio {\n" +
				"  background-image: url(\"/images/icons/radio.png\");\n" +
				"}\n\n" +
				".icon-scanner {\n" +
				"  background-image: Url(\"Data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAACGUlEQ" +
				"VR42qWRA4wYURCG/31n27Zt2zai2rZthW1jNDxHtW2FtW1zb6fTVxv3Jd8zZ/Ar6urqytiBbCx+g1JZWYlfMLizs3NVTU3NYABHp" +
				"D9B4Be8efNGvH37FitWrFitqmo8t+PxL+Tk5Ix48uQxnThxnE6fPkWFhYXDCgoKEvC3pKSkjLlz5zYdOLBfevLkCcrOzh6ZlZWVi" +
				"L8hMTExJSYmZvy1a1do164d0kOHDlBqauoYPjwZn8jNzfVle7Ejvjc9PX10SEjI5AsXLtCWLVuke/fuobi4uPGxsbEpYJTMzMzeK" +
				"1YsH29nZxfEgcPXvn2r4v79+zh58iSamprABwGMkZERhg4dOomI9uL9k27cuCEDFTuxjaKmb6SwyWspcGwX+Y5qJ7/RHZQ6tZ2ah" +
				"0+j8+cv0ubNW6V79uyjwMDAqYJTpBARwJChJaKiohAaGorAgAD4+vjAw9MTpq4BeB1UjFkr18DbOwCAHl6+VPH69Wuh8F/GR7dEL" +
				"xmrjsGQ9ivQMbOFEAJCUWSto6sLfQMD6POzdZ/fgXq8HXNGDJXf6dWrbrYSGRk5ce3atYuePHnCJ77ik1/J+kMM3qK7W4OmaSAir" +
				"gkPHjzAkSOHMWHCfM5U0DwlKChoCg/M19PTA3/n+yDKzYDCClbno7qyjo72XghHR8fZN2/epK6uLmptbf0njY2NF8HMzKy/iYnJw" +
				"v/RwMBgIHrKOw2CR46YSAM+AAAAAElFTkSuQmCC\");\n" +
				"}\n\n" +
				".icon-server {\n" +
				"  background-image: url(\"/images/icons/server.png\");\n" +
				"}\n\n" +
				"/*# sourceMappingURL=site.css.map */"
				;

			_appAbsolutePath = "/app01";
			_appOutputFileContent = "body {\n" +
				"  background-image: url(\"data:image/svg+xml," +
				"<svg xmlns='http://www.w3.org/2000/svg' width='20' height='20' viewBox='0 0 20 20'>" +
				"<g stroke-width='1' fill='none' stroke='gray' stroke-miterlimit='8'>" +
				"<polygon points='15.5,13.5 8,1.5 0.5,13.5'/>" +
				"</g>" +
				"</svg>" +
				"\");\n" +
				"}\n\n" +
				".icons {\n" +
				"  display: inline-block;\n" +
				"  background-repeat: no-repeat;\n" +
				"  width: 16px;\n" +
				"  height: 16px;\n" +
				"  line-height: 0;\n" +
				"  vertical-align: bottom;\n" +
				"}\n\n" +
				".icon-computer {\n" +
				"  background-image: url(../images/computer.gif);\n" +
				"}\n\n" +
				".icon-database {\n" +
				"  background-image: url(\"../images/database.png\");\n" +
				"}\n\n" +
				".icon-google-plus {\n" +
				"  background-image: url(\"../images/google-plus.svg\");\n" +
				"}\n\n" +
				".icon-headphone {\n" +
				"  background-image: url(/app01/images/icons/headphone[1].gif);\n" +
				"}\n\n" +
				".icon-joystick {\n" +
				"  background-image: Url(\"/app01/images/icons/joystick(2).png\");\n" +
				"}\n\n" +
				".icon-keyboard {\n" +
				"  background-image: URL(\"/app01/images/icons/keyboard{3}.png\");\n" +
				"}\n\n" +
				".icon-license {\n" +
				"  background-image: url(~/images/icons/license.png);\n"+
				"}\n\n" +
				".icon-monitor {\n" +
				"  background-image: url(\"/app01/images/icons/@monitor.png\");\n" +
				"}\n\n" +
				".icon-network {\n" +
				"  background-image: url(\"/app01/images/icons/$network.png\");\n" +
				"}\n\n" +
				".icon-open-source {\n" +
				"  background-image: url(\"/app01/images/icons/open_source.png\");\n" +
				"}\n\n" +
				".icon-printer {\n" +
				"  background-image: url(\"/app01/images/icons/printer.png\");\n" +
				"}\n\n" +
				".icon-qr-code {\n" +
				"  background-image: url(/app01/images/icons/qr-code.png);\n" +
				"}\n\n" +
				".icon-radio {\n" +
				"  background-image: url(\"/app01/images/icons/radio.png\");\n" +
				"}\n\n" +
				".icon-scanner {\n" +
				"  background-image: Url(\"Data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAACGUlEQ" +
				"VR42qWRA4wYURCG/31n27Zt2zai2rZthW1jNDxHtW2FtW1zb6fTVxv3Jd8zZ/Ar6urqytiBbCx+g1JZWYlfMLizs3NVTU3NYABHp" +
				"D9B4Be8efNGvH37FitWrFitqmo8t+PxL+Tk5Ix48uQxnThxnE6fPkWFhYXDCgoKEvC3pKSkjLlz5zYdOLBfevLkCcrOzh6ZlZWVi" +
				"L8hMTExJSYmZvy1a1do164d0kOHDlBqauoYPjwZn8jNzfVle7Ejvjc9PX10SEjI5AsXLtCWLVuke/fuobi4uPGxsbEpYJTMzMzeK" +
				"1YsH29nZxfEgcPXvn2r4v79+zh58iSamprABwGMkZERhg4dOomI9uL9k27cuCEDFTuxjaKmb6SwyWspcGwX+Y5qJ7/RHZQ6tZ2ah" +
				"0+j8+cv0ubNW6V79uyjwMDAqYJTpBARwJChJaKiohAaGorAgAD4+vjAw9MTpq4BeB1UjFkr18DbOwCAHl6+VPH69Wuh8F/GR7dEL" +
				"xmrjsGQ9ivQMbOFEAJCUWSto6sLfQMD6POzdZ/fgXq8HXNGDJXf6dWrbrYSGRk5ce3atYuePHnCJ77ik1/J+kMM3qK7W4OmaSAir" +
				"gkPHjzAkSOHMWHCfM5U0DwlKChoCg/M19PTA3/n+yDKzYDCClbno7qyjo72XghHR8fZN2/epK6uLmptbf0njY2NF8HMzKy/iYnJw" +
				"v/RwMBgIHrKOw2CR46YSAM+AAAAAElFTkSuQmCC\");\n" +
				"}\n\n" +
				".icon-server {\n" +
				"  background-image: url(\"/app01/images/icons/server.png\");\n" +
				"}\n\n" +
				"/*# sourceMappingURL=app.css.map */"
				;
		}


		[Test]
		public void CompilationWithAppAbsolutePaths([Values]bool fromFile)
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
			if (fromFile)
			{
				virtualFileManagerMock
					.Setup(fm => fm.FileExists(_siteInputFileAbsolutePath))
					.Returns(true)
					;
				virtualFileManagerMock
					.Setup(fm => fm.ReadFile(_siteInputFileAbsolutePath))
					.Returns(_siteInputFileContent)
					;
			}

			IFileManager virtualFileManager = new VirtualFileManager(virtualFileManagerMock);
			var options = new CompilationOptions { SourceMap = true };

			// Act
			CompilationResult result;

			using (var compiler = new SassCompiler(virtualFileManager))
			{
				if (fromFile)
				{
					result = compiler.CompileFile(_siteInputFileAbsolutePath, options: options);
				}
				else
				{
					result = compiler.Compile(_siteInputFileContent, _siteInputFileAbsolutePath, options: options);
				}
			}

			// Assert
			Assert.AreEqual(_siteOutputFileContent, result.CompiledContent);
			Assert.AreEqual(1, result.IncludedFilePaths.Count);
			Assert.AreEqual(_siteInputFileAbsolutePath, result.IncludedFilePaths[0]);
			Assert.AreEqual(_siteSourceMapFileContent, result.SourceMap);
		}

		[Test]
		public void CompilationWithAppRelativePaths([Values]bool fromFile)
		{
			var virtualFileManagerMock = new Mock<IFileManager>();
			virtualFileManagerMock
				.SetupGet(fm => fm.SupportsVirtualPaths)
				.Returns(true)
				;
			virtualFileManagerMock
				.Setup(fm => fm.GetCurrentDirectory())
				.Returns(_appAbsolutePath)
				;
			if (fromFile)
			{
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
			}

			IFileManager virtualFileManager = new VirtualFileManager(virtualFileManagerMock, _appAbsolutePath);
			var options = new CompilationOptions { SourceMap = true };

			// Act
			CompilationResult result;

			using (var compiler = new SassCompiler(virtualFileManager))
			{
				if (fromFile)
				{
					result = compiler.CompileFile(_appInputFileRelativePath, options: options);
				}
				else
				{
					result = compiler.Compile(_appInputFileContent, _appInputFileRelativePath, options: options);
				}
			}

			// Assert
			Assert.AreEqual(_appOutputFileContent, result.CompiledContent);
			Assert.AreEqual(1, result.IncludedFilePaths.Count);
			Assert.AreEqual(_appInputFileAbsolutePath, result.IncludedFilePaths[0]);
			Assert.AreEqual(_appSourceMapFileContent, result.SourceMap);
		}
	}
}