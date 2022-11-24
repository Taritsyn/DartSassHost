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
				".icon-computer\n" +
				"  background-image: url(../images/computer.gif)\n" +
				".icon-database\n" +
				"  background-image: url('../images/database.png')\n" +
				".icon-google-plus\n" +
				"  background-image: url(\"../images/google-plus.svg\")\n" +
				".icon-headphone\n" +
				"  background-image: url( /images/icons/headphone[1].gif )\n" +
				".icon-joystick\n" +
				"  background-image: Url(  '/images/icons/joystick(2).png'  )\n" +
				".icon-keyboard\n" +
				"  background-image: URL(	\"/images/icons/keyboard{3}.png\"	)\n" +
				".icon-license\n" +
				"  background-image: url(#{$icons-path}/license.png)\n" +
				".icon-monitor\n" +
				"  background-image: url('#{$icons-path}/@monitor.png')\n" +
				".icon-network\n" +
				"  background-image: url(\"#{$icons-path}/$network.png\")\n" +
				".icon-open-source\n" +
				"  background-image: url($icons-path + '/open_source.png')\n" +
				".icon-printer\n" +
				"  background-image: url($icons-path + \"/printer.png\")\n" +
				".icon-qr-code\n" +
				"  background-image: url(/images/icons/qr-code.png)\n" +
				".icon-radio\n" +
				"  background-image: url('/images/icons/radio.png')\n" +
				".icon-server\n" +
				"  background-image: url(\"/images/icons/server.png\")"
				;
			_siteSourceMapFileContent = "{\"version\":3,\"sourceRoot\":\"\"," +
				"\"sources\":[\"site.sass\"],\"names\":[]," +
				"\"mappings\":\"AAEA;EACE;EACA;EACA;EACA;EACA;EACA;;;AAEF;EACE;;;AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;" +
				"AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;" +
				"AACF;EACE;;;AACF;EACE\"," +
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
				".icon-computer\n" +
				"  background-image: url(../images/computer.gif)\n" +
				".icon-database\n" +
				"  background-image: url('../images/database.png')\n" +
				".icon-google-plus\n" +
				"  background-image: url(\"../images/google-plus.svg\")\n" +
				".icon-headphone\n" +
				"  background-image: url( ~/images/icons/headphone[1].gif )\n" +
				".icon-joystick\n" +
				"  background-image: Url(  '~/images/icons/joystick(2).png'  )\n" +
				".icon-keyboard\n" +
				"  background-image: URL(	\"~/images/icons/keyboard{3}.png\"	)\n" +
				".icon-license\n" +
				"  background-image: url(#{$icons-path}/license.png)\n" +
				".icon-monitor\n" +
				"  background-image: url('#{$icons-path}/@monitor.png')\n" +
				".icon-network\n" +
				"  background-image: url(\"#{$icons-path}/$network.png\")\n" +
				".icon-open-source\n" +
				"  background-image: url($icons-path + '/open_source.png')\n" +
				".icon-printer\n" +
				"  background-image: url($icons-path + \"/printer.png\")\n" +
				".icon-qr-code\n" +
				"  background-image: url(/app01/images/icons/qr-code.png)\n" +
				".icon-radio\n" +
				"  background-image: url('/app01/images/icons/radio.png')\n" +
				".icon-server\n" +
				"  background-image: url(\"/app01/images/icons/server.png\")"
				;
			_appSourceMapFileContent = "{\"version\":3,\"sourceRoot\":\"\"," +
				"\"sources\":[\"app.sass\"],\"names\":[]," +
				"\"mappings\":\"AAEA;EACE;EACA;EACA;EACA;EACA;EACA;;;AAEF;EACE;;;AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;" +
				"AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;" +
				"AACF;EACE;;;AACF;EACE\"," +
				"\"file\":\"app.css\"}"
				;
		}
	}
}