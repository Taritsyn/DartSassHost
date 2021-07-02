using System;
#if !NET40
using System.Runtime.InteropServices;
#endif

using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.Msie;

using NUnit.Framework;
#if NET40
using PolyfillsForOldDotNet.System.Runtime.InteropServices;
#endif

namespace DartSassHost.Tests
{
	[TestFixture]
	public class OldJsEngineTests
	{
		[Test]
		public void LatestMsieChakraIsSupported()
		{
			// Arrange
			IJsEngineFactory jsEngineFactory = new MsieJsEngineFactory(new MsieSettings
			{
				EngineMode = JsEngineMode.ChakraIeJsRt
			});
			const string input = @".icon-bug {
  background-image: url('http://www.codeplex.com/Download?ProjectName=bundletransformer&DownloadId=358407');
}";

			// Act
			string output;
			SassCompilationException exception = null;

			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				try
				{
					using (var sassCompiler = new SassCompiler(jsEngineFactory))
					{
						output = sassCompiler.Compile(input, false).CompiledContent;
					}
				}
				catch (SassCompilerLoadException)
				{
					// Ignore this error
				}
				catch (SassCompilationException e)
				{
					exception = e;
				}
			}

			// Assert
			Assert.Null(exception);
		}
	}
}