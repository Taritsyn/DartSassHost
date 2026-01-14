using System.Collections.Generic;

using NUnit.Framework;

namespace DartSassHost.Tests.Imports
{
	[TestFixture]
	public class ScssVirtualFileManagerTests : VirtualFileManagerTestsBase
	{
		public ScssVirtualFileManagerTests()
		{
			_siteInputFileAbsolutePath = "/styles/site.scss";
			_siteInputFileContent = "@import \"variables\";\n" +
				"@import 'foundation';\n" +
				"@import \"/styles/icons\", '/styles/fonts';\n" +
				"@import \"/styles/layout.scss\";\n" +
				"@import 'print' print and (min-resolution: 300dpi);"
				;
			_siteImportedFiles["/styles/_variables.scss"] = "$bg-color: #fef;\n" +
				"$bg-image: URL(DATA:image/svg+xml," +
				"%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20width%3D%2220%22%20height%3D%2220%22%20" +
				"viewBox%3D%220%200%2020%2020%22%3E%3Cg%20stroke-width%3D%221%22%20fill%3D%22none%22%20" +
				"stroke%3D%22gray%22%20stroke-miterlimit%3D%228%22%3E%3Cpolygon%20" +
				"points%3D%2215.5%2C13.5%208%2C1.5%200.5%2C13.5%22%2F%3E%3C%2Fg%3E%3C%2Fsvg%3E" +
				");\n" +
				"$icons-path: \"/styles/icons\";"
				;
			_siteImportedFiles["/styles/foundation/_index.scss"] = "@import 'mixins', 'reset';";
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
			_siteImportedFiles["/styles/_fonts.scss"] = "@import \"fonts/bebas-neue\";\n" +
				"@import \"http://fonts.googleapis.com/css?family=Droid+Sans\";\n" +
				"@import '/styles/fonts/bitstream-vera-sans-bold.css', \"/styles/fonts/league-gothic.css\";"
				;
			_siteImportedFiles["/styles/_icons.scss"] = ".icons {\n"+
				"  display: inline-block;\n" +
				"  background-repeat: no-repeat;\n" +
				"  width: 16px;\n" +
				"  height: 16px;\n" +
				"  line-height: 0;\n" +
				"  vertical-align: bottom;\n" +
				"}\n\n" +
				"@import url(./icons/computer.css);\n" +
				"@import url('./icons/database.css');\n" +
				"@import url(\"./icons/google-plus.css\");\n" +
				"@import url( /styles/icons/headphone[1].css );\n" +
				"@import Url(  '/styles/icons/joystick(2).css'  );\n" +
				"@import URL(	\"/styles/icons/keyboard{3}.css\"	);\n" +
				"@import url(#{$icons-path}/license.css);\n" +
				"@import url('#{$icons-path}/@monitor.css');\n" +
				"@import url(\"#{$icons-path}/$network.css\");\n" +
				"@import url($icons-path + '/open_source.css');\n" +
				"@import url($icons-path + \"/printer.css\");\n" +
				"@import url(/styles/icons/qr-code.css), url('/styles/icons/radio.css'), " +
				"url(\"/styles/icons/server.css\");\n\n" +
				".icon-scanner {\n" +
				"  background-image: UrL(DaTa:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAACGUlEQ" +
				"VR42qWRA4wYURCG/31n27Zt2zai2rZthW1jNDxHtW2FtW1zb6fTVxv3Jd8zZ/Ar6urqytiBbCx+g1JZWYlfMLizs3NVTU3NYABHp" +
				"D9B4Be8efNGvH37FitWrFitqmo8t+PxL+Tk5Ix48uQxnThxnE6fPkWFhYXDCgoKEvC3pKSkjLlz5zYdOLBfevLkCcrOzh6ZlZWVi" +
				"L8hMTExJSYmZvy1a1do164d0kOHDlBqauoYPjwZn8jNzfVle7Ejvjc9PX10SEjI5AsXLtCWLVuke/fuobi4uPGxsbEpYJTMzMzeK" +
				"1YsH29nZxfEgcPXvn2r4v79+zh58iSamprABwGMkZERhg4dOomI9uL9k27cuCEDFTuxjaKmb6SwyWspcGwX+Y5qJ7/RHZQ6tZ2ah" +
				"0+j8+cv0ubNW6V79uyjwMDAqYJTpBARwJChJaKiohAaGorAgAD4+vjAw9MTpq4BeB1UjFkr18DbOwCAHl6+VPH69Wuh8F/GR7dEL" +
				"xmrjsGQ9ivQMbOFEAJCUWSto6sLfQMD6POzdZ/fgXq8HXNGDJXf6dWrbrYSGRk5ce3atYuePHnCJ77ik1/J+kMM3qK7W4OmaSAir" +
				"gkPHjzAkSOHMWHCfM5U0DwlKChoCg/M19PTA3/n+yDKzYDCClbno7qyjo72XghHR8fZN2/epK6uLmptbf0njY2NF8HMzKy/iYnJw" +
				"v/RwMBgIHrKOw2CR46YSAM+AAAAAElFTkSuQmCC);\n" +
				"}"
				;
			_siteImportedFiles["/styles/layout.scss"] = "body {\n" +
				"  background-color: $bg-color;\n" +
				"  background-image: $bg-image;\n" +
				"  color: #699;\n" +
				"  font-size: .85em;\n" +
				"  font-family: \"League Gothic\", Helvetica, Sans-Serif;\n" +
				"}\n\n" +
				".sidebar {\n" +
				"  background-color: #f8f9fa;\n" +
				"  color: #6b717f;\n" +
				"  font-family: \"Droid Sans\", Arial, Sans-Serif;\n" +
				"  border: 1px solid #f5f6f8;\n" +
				"  @include border-radius(5px);\n" +
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
				"/styles/layout.scss"
			};
			_siteSourceMapFileContent = "{\"version\":3,\"sourceRoot\":\"\"," +
				"\"sources\":[\"_icons.scss\",\"_fonts.scss\",\"site.scss\",\"foundation/_reset.scss\"," +
				"\"fonts/bebas-neue.css\",\"layout.scss\",\"_variables.scss\",\"foundation/_mixins.scss\"]," +
				"\"names\":[]," +
				"\"mappings\":\"AASQ;AACA;AACA;AACA;AACA;AACA;AACA;AACA;AACA;AACA;AACA;AACA;AAAgC;AAAgC;ACnBhE;AACA;AAA8C;ACE9C;" +
				"ACJR;AAAA;AAAA;AAAA;EAIE;EACA;;;AHLF;EACE;EACA;EACA;EACA;EACA;EACA;;;AAgBF;EACE;;;AIvBF;EACE;EACA;EACA;EAIA;EACA;;" +
				"ACRF;EACE,kBCDS;EDET,kBCDS;EDET;EACA;EACA;;;AAGF;EACE;EACA;EACA;EACA;EEXA,eFYuB;EEXvB,uBFWuB;EEVvB,oBFUuB\"," +
				"\"file\":\"site.css\"}"
				;

			_appInputFileRelativePath = "~/styles/app.scss";
			_appInputFileAbsolutePath = "/app01/styles/app.scss";
			_appInputFileContent = "@import \"variables\";\n" +
				"@import 'foundation';\n" +
				"@import \"~/styles/icons\", '~/styles/fonts';\n" +
				"@import \"~/styles/layout.scss\";\n" +
				"@import 'print' print and (min-resolution: 300dpi);"
				;
			_appImportedFiles["/app01/styles/_variables.scss"] = "$bg-color: #fef;\n" +
				"$bg-image: URL(DATA:image/svg+xml," +
				"%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20width%3D%2220%22%20height%3D%2220%22%20" +
				"viewBox%3D%220%200%2020%2020%22%3E%3Cg%20stroke-width%3D%221%22%20fill%3D%22none%22%20" +
				"stroke%3D%22gray%22%20stroke-miterlimit%3D%228%22%3E%3Cpolygon%20" +
				"points%3D%2215.5%2C13.5%208%2C1.5%200.5%2C13.5%22%2F%3E%3C%2Fg%3E%3C%2Fsvg%3E" +
				");\n" +
				"$icons-path: \"~/styles/icons\";"
				;
			_appImportedFiles["/app01/styles/foundation/_index.scss"] = "@import 'mixins', 'reset';";
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
			_appImportedFiles["/app01/styles/_fonts.scss"] = "@import \"fonts/bebas-neue\";\n" +
				"@import \"http://fonts.googleapis.com/css?family=Droid+Sans\";\n" +
				"@import '~/styles/fonts/bitstream-vera-sans-bold.css', \"~/styles/fonts/league-gothic.css\";"
				;
			_appImportedFiles["/app01/styles/_icons.scss"] = ".icons {\n"+
				"  display: inline-block;\n" +
				"  background-repeat: no-repeat;\n" +
				"  width: 16px;\n" +
				"  height: 16px;\n" +
				"  line-height: 0;\n" +
				"  vertical-align: bottom;\n" +
				"}\n\n" +
				"@import url(./icons/computer.css);\n" +
				"@import url('./icons/database.css');\n" +
				"@import url(\"./icons/google-plus.css\");\n" +
				"@import url( ~/styles/icons/headphone[1].css );\n" +
				"@import Url(  '~/styles/icons/joystick(2).css'  );\n" +
				"@import URL(	\"~/styles/icons/keyboard{3}.css\"	);\n" +
				"@import url(#{$icons-path}/license.css);\n" +
				"@import url('#{$icons-path}/@monitor.css');\n" +
				"@import url(\"#{$icons-path}/$network.css\");\n" +
				"@import url($icons-path + '/open_source.css');\n" +
				"@import url($icons-path + \"/printer.css\");\n" +
				"@import url(/app01/styles/icons/qr-code.css), url('/app01/styles/icons/radio.css'), " +
				"url(\"/app01/styles/icons/server.css\");\n\n" +
				".icon-scanner {\n" +
				"  background-image: UrL(DaTa:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAACGUlEQ" +
				"VR42qWRA4wYURCG/31n27Zt2zai2rZthW1jNDxHtW2FtW1zb6fTVxv3Jd8zZ/Ar6urqytiBbCx+g1JZWYlfMLizs3NVTU3NYABHp" +
				"D9B4Be8efNGvH37FitWrFitqmo8t+PxL+Tk5Ix48uQxnThxnE6fPkWFhYXDCgoKEvC3pKSkjLlz5zYdOLBfevLkCcrOzh6ZlZWVi" +
				"L8hMTExJSYmZvy1a1do164d0kOHDlBqauoYPjwZn8jNzfVle7Ejvjc9PX10SEjI5AsXLtCWLVuke/fuobi4uPGxsbEpYJTMzMzeK" +
				"1YsH29nZxfEgcPXvn2r4v79+zh58iSamprABwGMkZERhg4dOomI9uL9k27cuCEDFTuxjaKmb6SwyWspcGwX+Y5qJ7/RHZQ6tZ2ah" +
				"0+j8+cv0ubNW6V79uyjwMDAqYJTpBARwJChJaKiohAaGorAgAD4+vjAw9MTpq4BeB1UjFkr18DbOwCAHl6+VPH69Wuh8F/GR7dEL" +
				"xmrjsGQ9ivQMbOFEAJCUWSto6sLfQMD6POzdZ/fgXq8HXNGDJXf6dWrbrYSGRk5ce3atYuePHnCJ77ik1/J+kMM3qK7W4OmaSAir" +
				"gkPHjzAkSOHMWHCfM5U0DwlKChoCg/M19PTA3/n+yDKzYDCClbno7qyjo72XghHR8fZN2/epK6uLmptbf0njY2NF8HMzKy/iYnJw" +
				"v/RwMBgIHrKOw2CR46YSAM+AAAAAElFTkSuQmCC);\n" +
				"}"
				;
			_appImportedFiles["/app01/styles/layout.scss"] = "body {\n" +
				"  background-color: $bg-color;\n" +
				"  background-image: $bg-image;\n" +
				"  color: #699;\n" +
				"  font-size: .85em;\n" +
				"  font-family: \"League Gothic\", Helvetica, Sans-Serif;\n" +
				"}\n\n" +
				".sidebar {\n" +
				"  background-color: #f8f9fa;\n" +
				"  color: #6b717f;\n" +
				"  font-family: \"Droid Sans\", Arial, Sans-Serif;\n" +
				"  border: 1px solid #f5f6f8;\n" +
				"  @include border-radius(5px);\n" +
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
				"/app01/styles/layout.scss"
			};
			_appSourceMapFileContent = "{\"version\":3,\"sourceRoot\":\"\"," +
				"\"sources\":[\"_icons.scss\",\"_fonts.scss\",\"app.scss\",\"foundation/_reset.scss\"," +
				"\"fonts/bebas-neue.css\",\"layout.scss\",\"_variables.scss\",\"foundation/_mixins.scss\"]," +
				"\"names\":[]," +
				"\"mappings\":\"AASQ;AACA;AACA;AACA;AACA;AACA;AACA;AACA;AACA;AACA;AACA;AACA;AAAsC;AAAsC;ACnB5E;AACA;" +
				"AAA+C;ACE/C;ACJR;AAAA;AAAA;AAAA;EAIE;EACA;;;AHLF;EACE;EACA;EACA;EACA;EACA;EACA;;;AAgBF;EACE;;;" +
				"AIvBF;EACE;EACA;EACA;EAIA;EACA;;ACRF;EACE,kBCDS;EDET,kBCDS;EDET;EACA;EACA;;;" +
				"AAGF;EACE;EACA;EACA;EACA;EEXA,eFYuB;EEXvB,uBFWuB;EEVvB,oBFUuB\"," +
				"\"file\":\"app.css\"}"
				;
		}
	}
}