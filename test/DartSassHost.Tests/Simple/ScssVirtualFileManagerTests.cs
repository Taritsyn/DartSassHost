using System;

using NUnit.Framework;

namespace DartSassHost.Tests.Simple
{
	[TestFixture]
	public class ScssVirtualFileManagerTests : VirtualFileManagerTestsBase
	{
		public ScssVirtualFileManagerTests()
		{
			_siteInputFileAbsolutePath = "/styles/site.scss";
			_siteInputFileContent = "$icons-path: \"/images/icons\";\n\n" +
				".icon-google-plus {\n" +
				"  display: inline;\n" +
				"  background-image: url(../images/google-plus.svg);\n" +
				"}\n\n" +
				".icon-headphone {\n" +
				"  display: inline;\n" +
				"  background-image: URL(  '/images/icons/headphone.gif'  );\n" +
				"}\n\n" +
				".icon-monitor {\n" +
				"  display: inline;\n" +
				"  background-image: url(\"#{$icons-path}/monitor.png\");\n" +
				"}\n\n" +
				".icon-robot {\n" +
				"  display: inline;\n" +
				"  background-image: url($icons-path + \"/robot.png\");\n" +
				"}\n\n" +
				".icon-usb-flash-drive {\n" +
				"  display: inline;\n" +
				"  background-image: Url(  /images/icons/usb-flash-drive.png  );\n" +
				"}"
				;
			_siteSourceMapFileContent = "{\"version\":3,\"sourceRoot\":\"\"," +
				"\"sources\":[\"site.scss\"],\"names\":[]," +
				"\"mappings\":\"AAEA;EACE;EACA;;;AAGF;EACE;EACA;;;AAGF;EACE;EACA;;;AAGF;EACE;EACA;;;AAGF;EACE;EACA\"," +
				"\"file\":\"site.css\"}"
				;

			_appInputFileRelativePath = "~/styles/app.scss";
			_appInputFileAbsolutePath = _appAbsolutePath + "/styles/app.scss";
			_appInputFileContent = "$icons-path: \"~/images/icons\";\n\n" +
				".icon-google-plus {\n" +
				"  display: inline;\n" +
				"  background-image: url(../images/google-plus.svg);\n" +
				"}\n\n" +
				".icon-headphone {\n" +
				"  display: inline;\n" +
				"  background-image: URL(  '~/images/icons/headphone.gif'  );\n" +
				"}\n\n" +
				".icon-monitor {\n" +
				"  display: inline;\n" +
				"  background-image: url(\"#{$icons-path}/monitor.png\");\n" +
				"}\n\n" +
				".icon-robot {\n" +
				"  display: inline;\n" +
				"  background-image: url($icons-path + \"/robot.png\");\n" +
				"}\n\n" +
				".icon-usb-flash-drive {\n" +
				"  display: inline;\n" +
				"  background-image: Url(  ~/images/icons/usb-flash-drive.png  );\n" +
				"}"
				;
			_appSourceMapFileContent = "{\"version\":3,\"sourceRoot\":\"\"," +
				"\"sources\":[\"file:///app01/styles/app.scss\"],\"names\":[]," +
				"\"mappings\":\"AAEA;EACE;EACA;;;AAGF;EACE;EACA;;;AAGF;EACE;EACA;;;AAGF;EACE;EACA;;;AAGF;EACE;EACA\"," +
				"\"file\":\"app.css\"}"
				;
		}
	}
}