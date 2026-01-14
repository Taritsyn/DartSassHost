using System.Collections.Generic;

using NUnit.Framework;

namespace DartSassHost.Tests.Modules
{
	[TestFixture]
	public class ScssVirtualFileManagerTests : VirtualFileManagerTestsBase
	{
		public ScssVirtualFileManagerTests()
		{
			_siteInputFileAbsolutePath = "/styles/site.scss";
			_siteInputFileContent = "@use \"variables\" as *;\n" +
				"@use 'foundation' as f;\n" +
				"@use \"/styles/icons\";\n" +
				"@use '/styles/fonts';\n\n" +
				"body {\n" +
				"  background-color: $bg-color;\n" +
				"  background-image: $bg-image;\n" +
				"  color: #699;\n" +
				"  font-size: .85em;\n" +
				"  font-family: \"League Gothic\", Helvetica, Sans-Serif;\n" +
				"}\n\n" +
				".sidebar {\n" +
				"  background-color: #f8f9fa;\n" +
				"  color: #6b717f;\n" +
				"  font-family: \"Bebas Neue\", Arial, Sans-Serif;\n" +
				"  border: 1px solid #f5f6f8;\n" +
				"  @include f.border-radius(5px);\n" +
				"}"
				;
			_siteImportedFiles["/styles/_variables.scss"] = "$bg-color: #fef;\n" +
				"$bg-image: uRl('dAtA:image/svg+xml," +
				"<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"20\" height=\"20\" viewBox=\"0 0 20 20\">" +
				"<g stroke-width=\"1\" fill=\"none\" stroke=\"gray\" stroke-miterlimit=\"8\">" +
				"<polygon points=\"15.5,13.5 8,1.5 0.5,13.5\"/>" +
				"</g>" +
				"</svg>" +
				"');\n" +
				"$icons-path: \"/images/icons\";"
				;
			_siteImportedFiles["/styles/foundation/_index.scss"] = "@forward 'mixins';\n" +
				"@forward 'reset';"
				;
			_siteImportedFiles["/styles/foundation/_mixins.scss"] = "@mixin border-radius($radius: 5px) {\n" +
				"  border-radius: $radius;\n" +
				"  -webkit-border-radius: $radius;\n" +
				"  -moz-border-radius: $radius;\n" +
				"}\n\n" +
				"@mixin visible {\n" +
				"  display: block;\n" +
				"}"
				;
			_siteImportedFiles["/styles/foundation/_reset.scss"] = "html,\n" +
				"body,\n" +
				"ul,\n" +
				"ol {\n" +
				"  margin: 0;\n" +
				"  padding: 0;\n" +
				"}"
				;
			_siteImportedFiles["/styles/_fonts.scss"] = "@use \"fonts/bebas-neue\";\n" +
				"@use \"fonts/league-gothic.scss\";"
				;
			_siteImportedFiles["/styles/fonts/league-gothic.scss"] = "$fonts-path: \"/fonts\";\n\n" +
				"@font-face {\n" +
				"  font-family: \"League Gothic\";\n" +
				"  src: url(\"#{$fonts-path}/league-gothic/league-gothic-webfont.eot\");\n" +
				"  src: url(\"#{$fonts-path}/league-gothic/league-gothic-webfont.eot?#iefix\") format(\"embedded-opentype\"),\n" +
				"    url(\"#{$fonts-path}/league-gothic/league-gothic-webfont.woff\") format(\"woff\"),\n" +
				"    url(\"#{$fonts-path}/league-gothic/league-gothic-webfont.ttf\") format(\"truetype\"),\n" +
				"    url(\"#{$fonts-path}/league-gothic/league-gothic-webfont.svg#league-gothicregular\") format(\"svg\");\n" +
				"  font-weight: normal;\n" +
				"  font-style: normal;\n" +
				"}"
				;
			_siteImportedFiles["/styles/_icons.scss"] = "@use \"variables\" as *;\n\n" +
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
				".icon-scanner {\n" +
				"  background-image: url(\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAACGUlEQ" +
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
				"}"
				;
			_siteIncludedFilePaths = new List<string>
			{
				_siteInputFileAbsolutePath,
				"/styles/_variables.scss",
				"/styles/foundation/_index.scss",
				"/styles/foundation/_mixins.scss",
				"/styles/foundation/_reset.scss",
				"/styles/_icons.scss",
				"/styles/_fonts.scss",
				"/styles/fonts/bebas-neue.css",
				"/styles/fonts/league-gothic.scss"
			};
			_siteSourceMapFileContent = "{\"version\":3,\"sourceRoot\":\"\"," +
				"\"sources\":[\"foundation/_reset.scss\",\"_icons.scss\",\"fonts/bebas-neue.css\"," +
				"\"fonts/league-gothic.scss\",\"site.scss\",\"_variables.scss\",\"foundation/_mixins.scss\"]," +
				"\"names\":[],\"mappings\":\"AAAA;AAAA;AAAA;AAAA;EAIE;EACA;;;ACHF;EACE;EACA;EACA;EACA;EACA;EACA;;;" +
				"AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;" +
				"AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;ACpEF;EACE;" +
				"EACA;EACA;EAIA;EACA;;ACNF;EACE;EACA;EACA;EAIA;EACA;;ACLF;EACE,kBCNS;EDOT,kBCNS;EDOT;EACA;EACA;;;" +
				"AAGF;EACE;EACA;EACA;EACA;EEhBA,eFiByB;EEhBzB,uBFgByB;EEfzB,oBFeyB\"," +
				"\"file\":\"site.css\"}"
				;

			_appInputFileRelativePath = "~/styles/app.scss";
			_appInputFileAbsolutePath = "/app01/styles/app.scss";
			_appInputFileContent = "@use \"variables\" as *;\n" +
				"@use 'foundation' as f;\n" +
				"@use \"~/styles/icons\";\n" +
				"@use '~/styles/fonts';\n\n" +
				"body {\n" +
				"  background-color: $bg-color;\n" +
				"  background-image: $bg-image;\n" +
				"  color: #699;\n" +
				"  font-size: .85em;\n" +
				"  font-family: \"League Gothic\", Helvetica, Sans-Serif;\n" +
				"}\n\n" +
				".sidebar {\n" +
				"  background-color: #f8f9fa;\n" +
				"  color: #6b717f;\n" +
				"  font-family: \"Bebas Neue\", Arial, Sans-Serif;\n" +
				"  border: 1px solid #f5f6f8;\n" +
				"  @include f.border-radius(5px);\n" +
				"}"
				;
			_appImportedFiles["/app01/styles/_variables.scss"] = "$bg-color: #fef;\n" +
				"$bg-image: uRl('dAtA:image/svg+xml," +
				"<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"20\" height=\"20\" viewBox=\"0 0 20 20\">" +
				"<g stroke-width=\"1\" fill=\"none\" stroke=\"gray\" stroke-miterlimit=\"8\">" +
				"<polygon points=\"15.5,13.5 8,1.5 0.5,13.5\"/>" +
				"</g>" +
				"</svg>" +
				"');\n" +
				"$icons-path: \"~/images/icons\";"
				;
			_appImportedFiles["/app01/styles/foundation/_index.scss"] = "@forward 'mixins';\n" +
				"@forward 'reset';"
				;
			_appImportedFiles["/app01/styles/foundation/_mixins.scss"] = "@mixin border-radius($radius: 5px) {\n" +
				"  border-radius: $radius;\n" +
				"  -webkit-border-radius: $radius;\n" +
				"  -moz-border-radius: $radius;\n" +
				"}\n\n" +
				"@mixin visible {\n" +
				"  display: block;\n" +
				"}"
				;
			_appImportedFiles["/app01/styles/foundation/_reset.scss"] = "html,\n" +
				"body,\n" +
				"ul,\n" +
				"ol {\n" +
				"  margin: 0;\n" +
				"  padding: 0;\n" +
				"}"
				;
			_appImportedFiles["/app01/styles/_fonts.scss"] = "@use \"fonts/bebas-neue\";\n" +
				"@use \"fonts/league-gothic.scss\";"
				;
			_appImportedFiles["/app01/styles/fonts/league-gothic.scss"] = "$fonts-path: \"~/fonts\";\n\n" +
				"@font-face {\n" +
				"  font-family: \"League Gothic\";\n" +
				"  src: url(\"#{$fonts-path}/league-gothic/league-gothic-webfont.eot\");\n" +
				"  src: url(\"#{$fonts-path}/league-gothic/league-gothic-webfont.eot?#iefix\") format(\"embedded-opentype\"),\n" +
				"    url(\"#{$fonts-path}/league-gothic/league-gothic-webfont.woff\") format(\"woff\"),\n" +
				"    url(\"#{$fonts-path}/league-gothic/league-gothic-webfont.ttf\") format(\"truetype\"),\n" +
				"    url(\"#{$fonts-path}/league-gothic/league-gothic-webfont.svg#league-gothicregular\") format(\"svg\");\n" +
				"  font-weight: normal;\n" +
				"  font-style: normal;\n" +
				"}"
				;
			_appImportedFiles["/app01/styles/_icons.scss"] = "@use \"variables\" as *;\n\n" +
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
				".icon-scanner {\n" +
				"  background-image: url(\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAACGUlEQ" +
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
				"}"
				;
			_appIncludedFilePaths = new List<string>
			{
				_appInputFileAbsolutePath,
				"/app01/styles/_variables.scss",
				"/app01/styles/foundation/_index.scss",
				"/app01/styles/foundation/_mixins.scss",
				"/app01/styles/foundation/_reset.scss",
				"/app01/styles/_icons.scss",
				"/app01/styles/_fonts.scss",
				"/app01/styles/fonts/bebas-neue.css",
				"/app01/styles/fonts/league-gothic.scss"
			};
			_appSourceMapFileContent = "{\"version\":3,\"sourceRoot\":\"\"," +
				"\"sources\":[\"foundation/_reset.scss\",\"_icons.scss\",\"fonts/bebas-neue.css\"," +
				"\"fonts/league-gothic.scss\",\"app.scss\",\"_variables.scss\",\"foundation/_mixins.scss\"]," +
				"\"names\":[],\"mappings\":\"AAAA;AAAA;AAAA;AAAA;EAIE;EACA;;;ACHF;EACE;EACA;EACA;EACA;EACA;EACA;;;" +
				"AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;" +
				"AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;ACpEF;EACE;EACA;" +
				"EACA;EAIA;EACA;;ACNF;EACE;EACA;EACA;EAIA;EACA;;ACLF;EACE,kBCNS;EDOT,kBCNS;EDOT;EACA;EACA;;;" +
				"AAGF;EACE;EACA;EACA;EACA;EEhBA,eFiByB;EEhBzB,uBFgByB;EEfzB,oBFeyB\"," +
				"\"file\":\"app.css\"}"
				;
		}
	}
}