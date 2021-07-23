using NUnit.Framework;

namespace DartSassHost.Tests.Simple
{
	[TestFixture]
	public class SassVirtualFileManagerTests : VirtualFileManagerTestsBase
	{
		public SassVirtualFileManagerTests()
		{
			_siteInputFileAbsolutePath = "/styles/site.sass";
			_siteInputFileContent = "$icons-path: \"/images/icons\"\n\n" +
				".icons\n" +
				"  display: inline-block\n" +
				"  background-repeat: no-repeat\n" +
				"  width: 16px\n" +
				"  height: 16px\n" +
				"  line-height: 0\n" +
				"  vertical-align: bottom\n" +
				"\n" +
				".icon-google-plus\n" +
				"  background-image: url(../images/google-plus.svg)\n" +
				"\n" +
				".icon-headphone\n" +
				"  background-image: URL(  '/images/icons/headphone.gif'  )\n" +
				"\n" +
				".icon-monitor\n" +
				"  background-image: url(\"#{$icons-path}/monitor.png\")\n" +
				"\n" +
				".icon-robot\n" +
				"  background-image: url($icons-path + \"/robot.png\")\n" +
				"\n" +
				".icon-usb-flash-drive\n" +
				"  background-image: Url(  /images/icons/usb-flash-drive.png  )"
				;
			_siteSourceMapFileContent = "{\"version\":3,\"sourceRoot\":\"\"," +
				"\"sources\":[\"site.sass\"],\"names\":[]," +
				"\"mappings\":\"AAEA;EACE;EACA;EACA;EACA;EACA;EACA;;;AAEF;EACE;;;AAEF;EACE;;;AAEF;EACE;;;AAEF;EACE;;;AAEF;EACE\"," +
				"\"file\":\"site.css\"}"
				;

			_appInputFileRelativePath = "~/styles/app.sass";
			_appInputFileAbsolutePath = "/app01/styles/app.sass";
			_appInputFileContent = "$icons-path: \"~/images/icons\"\n\n" +
				".icons\n" +
				"  display: inline-block\n" +
				"  background-repeat: no-repeat\n" +
				"  width: 16px\n" +
				"  height: 16px\n" +
				"  line-height: 0\n" +
				"  vertical-align: bottom\n" +
				"\n" +
				".icon-google-plus\n" +
				"  background-image: url(../images/google-plus.svg)\n" +
				"\n" +
				".icon-headphone\n" +
				"  background-image: URL(  '~/images/icons/headphone.gif'  )\n" +
				"\n" +
				".icon-monitor\n" +
				"  background-image: url(\"#{$icons-path}/monitor.png\")\n" +
				"\n" +
				".icon-robot\n" +
				"  background-image: url($icons-path + \"/robot.png\")\n" +
				"\n" +
				".icon-usb-flash-drive\n" +
				"  background-image: Url(  ~/images/icons/usb-flash-drive.png  )"
				;
			_appSourceMapFileContent = "{\"version\":3,\"sourceRoot\":\"\"," +
				"\"sources\":[\"app.sass\"],\"names\":[]," +
				"\"mappings\":\"AAEA;EACE;EACA;EACA;EACA;EACA;EACA;;;AAEF;EACE;;;AAEF;EACE;;;AAEF;EACE;;;AAEF;EACE;;;AAEF;EACE\"," +
				"\"file\":\"app.css\"}"
				;
		}
	}
}