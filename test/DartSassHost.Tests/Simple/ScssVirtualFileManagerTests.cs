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
				".icon-computer {\n" +
				"  background-image: url(../images/computer.gif);\n" +
				"}\n\n" +
				".icon-database {\n" +
				"  background-image: url('../images/database.png');\n" +
				"}\n\n" +
				".icon-google-plus {\n" +
				"  background-image: url(\"../images/google-plus.svg\");\n" +
				"}\n\n" +
				".icon-headphone {\n" +
				"  background-image: url( /images/icons/headphone[1].gif );\n" +
				"}\n\n" +
				".icon-joystick {\n" +
				"  background-image: Url(  '/images/icons/joystick(2).png'  );\n" +
				"}\n\n" +
				".icon-keyboard {\n" +
				"  background-image: URL(	\"/images/icons/keyboard{3}.png\"	);\n" +
				"}\n\n" +
				".icon-license {\n" +
				"  background-image: url(#{$icons-path}/license.png);\n" +
				"}\n\n" +
				".icon-monitor {\n" +
				"  background-image: url('#{$icons-path}/@monitor.png');\n" +
				"}\n\n" +
				".icon-network {\n" +
				"  background-image: url(\"#{$icons-path}/$network.png\");\n" +
				"}\n\n" +
				".icon-open-source {\n" +
				"  background-image: url($icons-path + '/open_source.png');\n" +
				"}\n\n" +
				".icon-printer {\n" +
				"  background-image: url($icons-path + \"/printer.png\");\n" +
				"}\n\n" +
				".icon-qr-code {\n" +
				"  background-image: url(/images/icons/qr-code.png);\n" +
				"}\n\n" +
				".icon-radio {\n" +
				"  background-image: url('/images/icons/radio.png');\n" +
				"}\n\n" +
				".icon-server {\n" +
				"  background-image: url(\"/images/icons/server.png\");\n" +
				"}"
				;
			_siteSourceMapFileContent = "{\"version\":3,\"sourceRoot\":\"\"," +
				"\"sources\":[\"site.scss\"],\"names\":[]," +
				"\"mappings\":\"AAEA;EACE;EACA;EACA;EACA;EACA;EACA;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;" +
				"AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;" +
				"AAGF;EACE;;;AAGF;EACE\"," +
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
				".icon-computer {\n" +
				"  background-image: url(../images/computer.gif);\n" +
				"}\n\n" +
				".icon-database {\n" +
				"  background-image: url('../images/database.png');\n" +
				"}\n\n" +
				".icon-google-plus {\n" +
				"  background-image: url(\"../images/google-plus.svg\");\n" +
				"}\n\n" +
				".icon-headphone {\n" +
				"  background-image: url( ~/images/icons/headphone[1].gif );\n" +
				"}\n\n" +
				".icon-joystick {\n" +
				"  background-image: Url(  '~/images/icons/joystick(2).png'  );\n" +
				"}\n\n" +
				".icon-keyboard {\n" +
				"  background-image: URL(	\"~/images/icons/keyboard{3}.png\"	);\n" +
				"}\n\n" +
				".icon-license {\n" +
				"  background-image: url(#{$icons-path}/license.png);\n" +
				"}\n\n" +
				".icon-monitor {\n" +
				"  background-image: url('#{$icons-path}/@monitor.png');\n" +
				"}\n\n" +
				".icon-network {\n" +
				"  background-image: url(\"#{$icons-path}/$network.png\");\n" +
				"}\n\n" +
				".icon-open-source {\n" +
				"  background-image: url($icons-path + '/open_source.png');\n" +
				"}\n\n" +
				".icon-printer {\n" +
				"  background-image: url($icons-path + \"/printer.png\");\n" +
				"}\n\n" +
				".icon-qr-code {\n" +
				"  background-image: url(/app01/images/icons/qr-code.png);\n" +
				"}\n\n" +
				".icon-radio {\n" +
				"  background-image: url('/app01/images/icons/radio.png');\n" +
				"}\n\n" +
				".icon-server {\n" +
				"  background-image: url(\"/app01/images/icons/server.png\");\n" +
				"}"
				;
			_appSourceMapFileContent = "{\"version\":3,\"sourceRoot\":\"\"," +
				"\"sources\":[\"app.scss\"],\"names\":[]," +
				"\"mappings\":\"AAEA;EACE;EACA;EACA;EACA;EACA;EACA;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;" +
				"AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;" +
				"AAGF;EACE;;;AAGF;EACE\"," +
				"\"file\":\"app.css\"}"
				;
		}
	}
}