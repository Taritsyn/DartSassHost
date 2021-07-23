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
				"  background-image: URL(  '/images/icons/headphone.gif'  );\n" +
				"}\n\n" +
				".icon-monitor {\n" +
				"  background-image: url(\"#{$icons-path}/monitor.png\");\n" +
				"}\n\n" +
				".icon-robot {\n" +
				"  background-image: url($icons-path + \"/robot.png\");\n" +
				"}\n\n" +
				".icon-usb-flash-drive {\n" +
				"  background-image: Url(  /images/icons/usb-flash-drive.png  );\n" +
				"}"
				;
			_siteSourceMapFileContent = "{\"version\":3,\"sourceRoot\":\"\"," +
				"\"sources\":[\"site.scss\"],\"names\":[]," +
				"\"mappings\":\"AAEA;EACE;EACA;EACA;EACA;EACA;EACA;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE\"," +
				"\"file\":\"site.css\"}"
				;

			_appInputFileRelativePath = "~/styles/app.scss";
			_appInputFileAbsolutePath = "/app01/styles/app.scss";
			_appInputFileContent = "$icons-path: \"~/images/icons\";\n\n" +
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
				"  background-image: URL(  '~/images/icons/headphone.gif'  );\n" +
				"}\n\n" +
				".icon-monitor {\n" +
				"  background-image: url(\"#{$icons-path}/monitor.png\");\n" +
				"}\n\n" +
				".icon-robot {\n" +
				"  background-image: url($icons-path + \"/robot.png\");\n" +
				"}\n\n" +
				".icon-usb-flash-drive {\n" +
				"  background-image: Url(  ~/images/icons/usb-flash-drive.png  );\n" +
				"}"
				;
			_appSourceMapFileContent = "{\"version\":3,\"sourceRoot\":\"\"," +
				"\"sources\":[\"app.scss\"],\"names\":[]," +
				"\"mappings\":\"AAEA;EACE;EACA;EACA;EACA;EACA;EACA;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE\"," +
				"\"file\":\"app.css\"}"
				;
		}
	}
}