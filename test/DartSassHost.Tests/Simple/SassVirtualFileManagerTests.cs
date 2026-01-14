using NUnit.Framework;

namespace DartSassHost.Tests.Simple
{
	[TestFixture]
	public class SassVirtualFileManagerTests : VirtualFileManagerTestsBase
	{
		public SassVirtualFileManagerTests()
		{
			_siteInputFileAbsolutePath = "/styles/site.sass";
			_siteInputFileContent = "$bg-image: url(\"data:image/svg+xml," +
				"<svg xmlns='http://www.w3.org/2000/svg' width='20' height='20' viewBox='0 0 20 20'>" +
				"<g stroke-width='1' fill='none' stroke='gray' stroke-miterlimit='8'>" +
				"<polygon points='15.5,13.5 8,1.5 0.5,13.5'/>" +
				"</g>" +
				"</svg>" +
				"\")\n" +
				"$icons-path: \"/images/icons\"\n\n" +
				"body\n" +
				"  background-image: $bg-image;\n" +
				"\n" +
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
				".icon-scanner\n" +
				"  background-image: Url('Data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAACGUlEQ" +
				"VR42qWRA4wYURCG/31n27Zt2zai2rZthW1jNDxHtW2FtW1zb6fTVxv3Jd8zZ/Ar6urqytiBbCx+g1JZWYlfMLizs3NVTU3NYABHp" +
				"D9B4Be8efNGvH37FitWrFitqmo8t+PxL+Tk5Ix48uQxnThxnE6fPkWFhYXDCgoKEvC3pKSkjLlz5zYdOLBfevLkCcrOzh6ZlZWVi" +
				"L8hMTExJSYmZvy1a1do164d0kOHDlBqauoYPjwZn8jNzfVle7Ejvjc9PX10SEjI5AsXLtCWLVuke/fuobi4uPGxsbEpYJTMzMzeK" +
				"1YsH29nZxfEgcPXvn2r4v79+zh58iSamprABwGMkZERhg4dOomI9uL9k27cuCEDFTuxjaKmb6SwyWspcGwX+Y5qJ7/RHZQ6tZ2ah" +
				"0+j8+cv0ubNW6V79uyjwMDAqYJTpBARwJChJaKiohAaGorAgAD4+vjAw9MTpq4BeB1UjFkr18DbOwCAHl6+VPH69Wuh8F/GR7dEL" +
				"xmrjsGQ9ivQMbOFEAJCUWSto6sLfQMD6POzdZ/fgXq8HXNGDJXf6dWrbrYSGRk5ce3atYuePHnCJ77ik1/J+kMM3qK7W4OmaSAir" +
				"gkPHjzAkSOHMWHCfM5U0DwlKChoCg/M19PTA3/n+yDKzYDCClbno7qyjo72XghHR8fZN2/epK6uLmptbf0njY2NF8HMzKy/iYnJw" +
				"v/RwMBgIHrKOw2CR46YSAM+AAAAAElFTkSuQmCC');\n" +
				"\n" +
				".icon-server\n" +
				"  background-image: url(\"/images/icons/server.png\")"
				;
			_siteSourceMapFileContent = "{\"version\":3,\"sourceRoot\":\"\"," +
				"\"sources\":[\"site.sass\"],\"names\":[]," +
				"\"mappings\":\"AAGA;EACE,kBAJS;;;AAMX;EACE;EACA;EACA;EACA;EACA;EACA;;;AAEF;EACE;;;AACF;EACE;;;" +
				"AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;" +
				"AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;AAEF;EACE\"," +
				"\"file\":\"site.css\"}"
				;

			_appInputFileRelativePath = "~/styles/app.sass";
			_appInputFileAbsolutePath = "/app01/styles/app.sass";
			_appInputFileContent = "$bg-image: url(\"data:image/svg+xml," +
				"<svg xmlns='http://www.w3.org/2000/svg' width='20' height='20' viewBox='0 0 20 20'>" +
				"<g stroke-width='1' fill='none' stroke='gray' stroke-miterlimit='8'>" +
				"<polygon points='15.5,13.5 8,1.5 0.5,13.5'/>" +
				"</g>" +
				"</svg>" +
				"\")\n" +
				"$icons-path: \"~/images/icons\"\n\n" +
				"body\n" +
				"  background-image: $bg-image;\n" +
				"\n" +
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
				".icon-scanner\n" +
				"  background-image: Url('Data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAACGUlEQ" +
				"VR42qWRA4wYURCG/31n27Zt2zai2rZthW1jNDxHtW2FtW1zb6fTVxv3Jd8zZ/Ar6urqytiBbCx+g1JZWYlfMLizs3NVTU3NYABHp" +
				"D9B4Be8efNGvH37FitWrFitqmo8t+PxL+Tk5Ix48uQxnThxnE6fPkWFhYXDCgoKEvC3pKSkjLlz5zYdOLBfevLkCcrOzh6ZlZWVi" +
				"L8hMTExJSYmZvy1a1do164d0kOHDlBqauoYPjwZn8jNzfVle7Ejvjc9PX10SEjI5AsXLtCWLVuke/fuobi4uPGxsbEpYJTMzMzeK" +
				"1YsH29nZxfEgcPXvn2r4v79+zh58iSamprABwGMkZERhg4dOomI9uL9k27cuCEDFTuxjaKmb6SwyWspcGwX+Y5qJ7/RHZQ6tZ2ah" +
				"0+j8+cv0ubNW6V79uyjwMDAqYJTpBARwJChJaKiohAaGorAgAD4+vjAw9MTpq4BeB1UjFkr18DbOwCAHl6+VPH69Wuh8F/GR7dEL" +
				"xmrjsGQ9ivQMbOFEAJCUWSto6sLfQMD6POzdZ/fgXq8HXNGDJXf6dWrbrYSGRk5ce3atYuePHnCJ77ik1/J+kMM3qK7W4OmaSAir" +
				"gkPHjzAkSOHMWHCfM5U0DwlKChoCg/M19PTA3/n+yDKzYDCClbno7qyjo72XghHR8fZN2/epK6uLmptbf0njY2NF8HMzKy/iYnJw" +
				"v/RwMBgIHrKOw2CR46YSAM+AAAAAElFTkSuQmCC');\n" +
				"\n" +
				".icon-server\n" +
				"  background-image: url(\"/app01/images/icons/server.png\")"
				;
			_appSourceMapFileContent = "{\"version\":3,\"sourceRoot\":\"\"," +
				"\"sources\":[\"app.sass\"],\"names\":[]," +
				"\"mappings\":\"AAGA;EACE,kBAJS;;;AAMX;EACE;EACA;EACA;EACA;EACA;EACA;;;AAEF;EACE;;;AACF;EACE;;;" +
				"AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;" +
				"AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;AACF;EACE;;;AAEF;EACE\"," +
				"\"file\":\"app.css\"}"
				;
		}
	}
}