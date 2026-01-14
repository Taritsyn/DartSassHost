using System.Collections.Generic;

using Moq;
using NUnit.Framework;

namespace DartSassHost.Tests.Imports
{
	public abstract class VirtualFileManagerTestsBase : TestsBase
	{
		protected string _siteInputFileAbsolutePath;
		protected string _siteInputFileContent;
		protected Dictionary<string, string> _siteImportedFiles;
		private readonly string _siteOutputFileContent;
		protected List<string> _siteIncludedFilePaths;
		protected string _siteSourceMapFileContent;

		private readonly string _appAbsolutePath;
		protected string _appInputFileRelativePath;
		protected string _appInputFileAbsolutePath;
		protected string _appInputFileContent;
		protected Dictionary<string, string> _appImportedFiles;
		private readonly string _appOutputFileContent;
		protected List<string> _appIncludedFilePaths;
		protected string _appSourceMapFileContent;


		protected VirtualFileManagerTestsBase()
		{
			_siteImportedFiles = new Dictionary<string, string>();
			_siteImportedFiles["/styles/fonts/bebas-neue.css"] = "@font-face {\n" +
				"  font-family: \"Bebas Neue\";\n" +
				"  src: url(\"/fonts/bebas-neue/bebas-neue-webfont.eot\");\n" +
				"  src: url(\"/fonts/bebas-neue/bebas-neue-webfont.eot?#iefix\") format(\"embedded-opentype\"),\n" +
				"    url(\"/fonts/bebas-neue/bebas-neue-webfont.woff\") format(\"woff\"),\n" +
				"    url(\"/fonts/bebas-neue/bebas-neue-webfont.ttf\") format(\"truetype\"),\n" +
				"    url(\"/fonts/bebas-neue/bebas-neue-webfont.svg#bebas_neueregular\") format(\"svg\");\n" +
				"  font-weight: normal;\n" +
				"  font-style: normal;\n" +
				"}"
				;
			_siteImportedFiles["/styles/fonts/bitstream-vera-sans-bold.css"] = "@font-face {\n" +
				"  font-family: \"Bitstream Vera Sans Bold\";\n" +
				"  src: url(\"../../fonts/bitstream-vera-sans-bold/vera-bold-webfont.eot\");\n" +
				"  src: url(\"../../fonts/bitstream-vera-sans-bold/vera-bold-webfont.eot?#iefix\" ) format(\"embedded-opentype\"),\n" +
				"    url(\"../../fonts/bitstream-vera-sans-bold/vera-bold-webfont.woff\") format(\"woff\"),\n" +
				"    url(\"../../fonts/bitstream-vera-sans-bold/vera-bold-webfont.ttf\") format(\"truetype\"),\n" +
				"    url(\"../../fonts/bitstream-vera-sans-bold/vera-bold-webfont.svg#bitstream_vera_sansbold\") format(\"svg\");\n" +
				"  font-weight: normal;\n" +
				"  font-style: normal;\n" +
				"}"
				;
			_siteImportedFiles["/styles/fonts/league-gothic.css"] = "@font-face {\n" +
				"  font-family: \"League Gothic\";\n" +
				"  src: url(\"../../fonts/league-gothic/league-gothic-webfont.eot\");\n" +
				"  src: url(\"../../fonts/league-gothic/league-gothic-webfont.eot?#iefix\") format(\"embedded-opentype\"),\n" +
				"    url(\"../../fonts/league-gothic/league-gothic-webfont.woff\") format(\"woff\"),\n" +
				"    url(\"../../fonts/league-gothic/league-gothic-webfont.ttf\") format(\"truetype\"),\n" +
				"    url(\"../../fonts/league-gothic/league-gothic-webfont.svg#league-gothicregular\") format(\"svg\");\n" +
				"  font-weight: normal;\n" +
				"  font-style: normal;\n" +
				"}"
				;
			_siteImportedFiles["/styles/icons/computer.css"] = ".icon-computer {\n" +
				"  background-image: url(/images/icons/computer.gif);\n" +
				"}"
				;
			_siteImportedFiles["/styles/icons/database.css"] = ".icon-database {\n" +
				"  background-image: url(/images/icons/database.png);\n" +
				"}"
				;
			_siteImportedFiles["/styles/icons/google-plus.css"] = ".icon-google-plus {\n" +
				"  background-image: url(/images/icons/google-plus.svg);\n" +
				"}"
				;
			_siteImportedFiles["/styles/icons/headphone[1].css"] = ".icon-headphone {\n" +
				"  background-image: url(/images/icons/headphone[1].gif);\n" +
				"}"
				;
			_siteImportedFiles["/styles/icons/joystick(2).css"] = ".icon-joystick {\n" +
				"  background-image: url(/images/icons/joystick(2).png);\n" +
				"}"
				;
			_siteImportedFiles["/styles/icons/keyboard{3}.css"] = ".icon-keyboard {\n" +
				"  background-image: url(/images/icons/keyboard{3}.png);\n" +
				"}"
				;
			_siteImportedFiles["/styles/icons/license.css"] = ".icon-license {\n" +
				"  background-image: url(/images/icons/license.png);\n" +
				"}"
				;
			_siteImportedFiles["/styles/icons/@monitor.css"] = ".icon-monitor {\n" +
				"  background-image: url(/images/icons/@monitor.png);\n" +
				"}"
				;
			_siteImportedFiles["/styles/icons/$network.css"] = ".icon-network {\n" +
				"  background-image: url(/images/icons/$network.png);\n" +
				"}"
				;
			_siteImportedFiles["/styles/icons/open_source.css"] = ".icon-open-source {\n" +
				"  background-image: url(/images/icons/open_source.png);\n" +
				"}"
				;
			_siteImportedFiles["/styles/icons/printer.css"] = ".icon-printer {\n" +
				"  background-image: url(/images/icons/printer.png);\n" +
				"}"
				;
			_siteImportedFiles["/styles/icons/qr-code.css"] = ".icon-qr-code {\n" +
				"  background-image: url(/images/icons/qr-code.png);\n" +
				"}"
				;
			_siteImportedFiles["/styles/icons/radio.css"] = ".icon-radio {\n" +
				"  background-image: url(/images/icons/radio.png);\n" +
				"}"
				;
			_siteImportedFiles["/styles/icons/server.css"] = ".icon-server {\n" +
				"  background-image: url(/images/icons/server.png);\n" +
				"}"
				;
			_siteImportedFiles["/styles/print.css"] = "@page {\n" +
				"  margin: 1cm;\n" +
				"}\n\n" +
				".sidebar {\n" +
				"  display: none;\n" +
				"}\n\n" +
				"a[href]:after {\n" +
				"  content: \" (\" attr(href) \")\";\n" +
				"}\n\n" +
				"abbr[title]:after {\n" +
				"  content: \" (\" attr(title) \")\";\n" +
				"}"
				;
			_siteOutputFileContent = "@import url(./icons/computer.css);\n" +
				"@import url(\"./icons/database.css\");\n" +
				"@import url(\"./icons/google-plus.css\");\n" +
				"@import url(/styles/icons/headphone[1].css);\n" +
				"@import url(\"/styles/icons/joystick(2).css\");\n" +
				"@import url(\"/styles/icons/keyboard{3}.css\");\n" +
				"@import url(/styles/icons/license.css);\n" +
				"@import url(\"/styles/icons/@monitor.css\");\n" +
				"@import url(\"/styles/icons/$network.css\");\n" +
				"@import url(\"/styles/icons/open_source.css\");\n" +
				"@import url(\"/styles/icons/printer.css\");\n" +
				"@import url(/styles/icons/qr-code.css);\n" +
				"@import url(\"/styles/icons/radio.css\");\n" +
				"@import url(\"/styles/icons/server.css\");\n" +
				"@import \"http://fonts.googleapis.com/css?family=Droid+Sans\";\n" +
				"@import '/styles/fonts/bitstream-vera-sans-bold.css';\n" +
				"@import \"/styles/fonts/league-gothic.css\";\n" +
				"@import 'print' print and (min-resolution: 300dpi);\n" +
				"html,\n" +
				"body,\n" +
				"ul,\n" +
				"ol {\n" +
				"  margin: 0;\n" +
				"  padding: 0;\n" +
				"}\n\n" +
				".icons {\n" +
				"  display: inline-block;\n" +
				"  background-repeat: no-repeat;\n" +
				"  width: 16px;\n" +
				"  height: 16px;\n" +
				"  line-height: 0;\n" +
				"  vertical-align: bottom;\n" +
				"}\n\n" +
				".icon-scanner {\n" +
				"  background-image: url(DaTa:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAACGUlEQ" +
				"VR42qWRA4wYURCG/31n27Zt2zai2rZthW1jNDxHtW2FtW1zb6fTVxv3Jd8zZ/Ar6urqytiBbCx+g1JZWYlfMLizs3NVTU3NYABHp" +
				"D9B4Be8efNGvH37FitWrFitqmo8t+PxL+Tk5Ix48uQxnThxnE6fPkWFhYXDCgoKEvC3pKSkjLlz5zYdOLBfevLkCcrOzh6ZlZWVi" +
				"L8hMTExJSYmZvy1a1do164d0kOHDlBqauoYPjwZn8jNzfVle7Ejvjc9PX10SEjI5AsXLtCWLVuke/fuobi4uPGxsbEpYJTMzMzeK" +
				"1YsH29nZxfEgcPXvn2r4v79+zh58iSamprABwGMkZERhg4dOomI9uL9k27cuCEDFTuxjaKmb6SwyWspcGwX+Y5qJ7/RHZQ6tZ2ah" +
				"0+j8+cv0ubNW6V79uyjwMDAqYJTpBARwJChJaKiohAaGorAgAD4+vjAw9MTpq4BeB1UjFkr18DbOwCAHl6+VPH69Wuh8F/GR7dEL" +
				"xmrjsGQ9ivQMbOFEAJCUWSto6sLfQMD6POzdZ/fgXq8HXNGDJXf6dWrbrYSGRk5ce3atYuePHnCJ77ik1/J+kMM3qK7W4OmaSAir" +
				"gkPHjzAkSOHMWHCfM5U0DwlKChoCg/M19PTA3/n+yDKzYDCClbno7qyjo72XghHR8fZN2/epK6uLmptbf0njY2NF8HMzKy/iYnJw" +
				"v/RwMBgIHrKOw2CR46YSAM+AAAAAElFTkSuQmCC);\n" +
				"}\n\n" +
				"@font-face {\n" +
				"  font-family: \"Bebas Neue\";\n" +
				"  src: url(\"/fonts/bebas-neue/bebas-neue-webfont.eot\");\n" +
				"  src: url(\"/fonts/bebas-neue/bebas-neue-webfont.eot?#iefix\") format(\"embedded-opentype\"), " +
				"url(\"/fonts/bebas-neue/bebas-neue-webfont.woff\") format(\"woff\"), " +
				"url(\"/fonts/bebas-neue/bebas-neue-webfont.ttf\") format(\"truetype\"), " +
				"url(\"/fonts/bebas-neue/bebas-neue-webfont.svg#bebas_neueregular\") format(\"svg\");\n" +
				"  font-weight: normal;\n" +
				"  font-style: normal;\n" +
				"}\n" +
				"body {\n" +
				"  background-color: #fef;\n" +
				"  background-image: url(DATA:image/svg+xml," +
				"%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20width%3D%2220%22%20height%3D%2220%22%20" +
				"viewBox%3D%220%200%2020%2020%22%3E%3Cg%20stroke-width%3D%221%22%20fill%3D%22none%22%20" +
				"stroke%3D%22gray%22%20stroke-miterlimit%3D%228%22%3E%3Cpolygon%20" +
				"points%3D%2215.5%2C13.5%208%2C1.5%200.5%2C13.5%22%2F%3E%3C%2Fg%3E%3C%2Fsvg%3E" +
				");\n" +
				"  color: #699;\n" +
				"  font-size: 0.85em;\n" +
				"  font-family: \"League Gothic\", Helvetica, Sans-Serif;\n" +
				"}\n\n" +
				".sidebar {\n" +
				"  background-color: #f8f9fa;\n" +
				"  color: #6b717f;\n" +
				"  font-family: \"Droid Sans\", Arial, Sans-Serif;\n" +
				"  border: 1px solid #f5f6f8;\n" +
				"  border-radius: 5px;\n" +
				"  -webkit-border-radius: 5px;\n" +
				"  -moz-border-radius: 5px;\n" +
				"}\n\n/" +
				"*# sourceMappingURL=site.css.map */"
				;

			_appAbsolutePath = "/app01";
			_appImportedFiles = new Dictionary<string, string>();
			_appImportedFiles["/app01/styles/fonts/bebas-neue.css"] = "@font-face {\n" +
				"  font-family: \"Bebas Neue\";\n" +
				"  src: url(\"~/fonts/bebas-neue/bebas-neue-webfont.eot\");\n" +
				"  src: url(\"~/fonts/bebas-neue/bebas-neue-webfont.eot?#iefix\") format(\"embedded-opentype\"),\n" +
				"    url(\"~/fonts/bebas-neue/bebas-neue-webfont.woff\") format(\"woff\"),\n" +
				"    url(\"~/fonts/bebas-neue/bebas-neue-webfont.ttf\") format(\"truetype\"),\n" +
				"    url(\"~/fonts/bebas-neue/bebas-neue-webfont.svg#bebas_neueregular\") format(\"svg\");\n" +
				"  font-weight: normal;\n" +
				"  font-style: normal;\n" +
				"}"
				;
			_appImportedFiles["/app01/styles/fonts/bitstream-vera-sans-bold.css"] = "@font-face {\n" +
				"  font-family: \"Bitstream Vera Sans Bold\";\n" +
				"  src: url(\"../../fonts/bitstream-vera-sans-bold/vera-bold-webfont.eot\");\n" +
				"  src: url(\"../../fonts/bitstream-vera-sans-bold/vera-bold-webfont.eot?#iefix\" ) format(\"embedded-opentype\"),\n" +
				"    url(\"../../fonts/bitstream-vera-sans-bold/vera-bold-webfont.woff\") format(\"woff\"),\n" +
				"    url(\"../../fonts/bitstream-vera-sans-bold/vera-bold-webfont.ttf\") format(\"truetype\"),\n" +
				"    url(\"../../fonts/bitstream-vera-sans-bold/vera-bold-webfont.svg#bitstream_vera_sansbold\") format(\"svg\");\n" +
				"  font-weight: normal;\n" +
				"  font-style: normal;\n" +
				"}"
				;
			_appImportedFiles["/app01/styles/fonts/league-gothic.css"] = "@font-face {\n" +
				"  font-family: \"League Gothic\";\n" +
				"  src: url(\"../../fonts/league-gothic/league-gothic-webfont.eot\");\n" +
				"  src: url(\"../../fonts/league-gothic/league-gothic-webfont.eot?#iefix\") format(\"embedded-opentype\"),\n" +
				"    url(\"../../fonts/league-gothic/league-gothic-webfont.woff\") format(\"woff\"),\n" +
				"    url(\"../../fonts/league-gothic/league-gothic-webfont.ttf\") format(\"truetype\"),\n" +
				"    url(\"../../fonts/league-gothic/league-gothic-webfont.svg#league-gothicregular\") format(\"svg\");\n" +
				"  font-weight: normal;\n" +
				"  font-style: normal;\n" +
				"}"
				;
			_appImportedFiles["/app01/styles/icons/computer.css"] = ".icon-computer {\n" +
				"  background-image: url(/app01/images/icons/computer.gif);\n" +
				"}"
				;
			_appImportedFiles["/app01/styles/icons/database.css"] = ".icon-database {\n" +
				"  background-image: url(/app01/images/icons/database.png);\n" +
				"}"
				;
			_appImportedFiles["/app01/styles/icons/google-plus.css"] = ".icon-google-plus {\n" +
				"  background-image: url(/app01/images/icons/google-plus.svg);\n" +
				"}"
				;
			_appImportedFiles["/app01/styles/icons/headphone[1].css"] = ".icon-headphone {\n" +
				"  background-image: url(/app01/images/icons/headphone[1].gif);\n" +
				"}"
				;
			_appImportedFiles["/app01/styles/icons/joystick(2).css"] = ".icon-joystick {\n" +
				"  background-image: url(/app01/images/icons/joystick(2).png);\n" +
				"}"
				;
			_appImportedFiles["/app01/styles/icons/keyboard{3}.css"] = ".icon-keyboard {\n" +
				"  background-image: url(/app01/images/icons/keyboard{3}.png);\n" +
				"}"
				;
			_appImportedFiles["/app01/styles/icons/license.css"] = ".icon-license {\n" +
				"  background-image: url(/app01/images/icons/license.png);\n" +
				"}"
				;
			_appImportedFiles["/app01/styles/icons/@monitor.css"] = ".icon-monitor {\n" +
				"  background-image: url(/app01/images/icons/@monitor.png);\n" +
				"}"
				;
			_appImportedFiles["/app01/styles/icons/$network.css"] = ".icon-network {\n" +
				"  background-image: url(/app01/images/icons/$network.png);\n" +
				"}"
				;
			_appImportedFiles["/app01/styles/icons/open_source.css"] = ".icon-open-source {\n" +
				"  background-image: url(/app01/images/icons/open_source.png);\n" +
				"}"
				;
			_appImportedFiles["/app01/styles/icons/printer.css"] = ".icon-printer {\n" +
				"  background-image: url(/app01/images/icons/printer.png);\n" +
				"}"
				;
			_appImportedFiles["/app01/styles/icons/qr-code.css"] = ".icon-qr-code {\n" +
				"  background-image: url(/app01/images/icons/qr-code.png);\n" +
				"}";
			_appImportedFiles["/app01/styles/icons/radio.css"] = ".icon-radio {\n" +
				"  background-image: url(/app01/images/icons/radio.png);\n" +
				"}"
				;
			_appImportedFiles["/app01/styles/icons/server.css"] = ".icon-server {\n" +
				"  background-image: url(/app01/images/icons/server.png);\n" +
				"}"
				;
			_appImportedFiles["/app01/styles/print.css"] = "@page {\n" +
				"  margin: 1cm;\n" +
				"}\n\n" +
				".sidebar {\n" +
				"  display: none;\n" +
				"}\n\n" +
				"a[href]:after {\n" +
				"  content: \" (\" attr(href) \")\";\n" +
				"}\n\n" +
				"abbr[title]:after {\n" +
				"  content: \" (\" attr(title) \")\";\n" +
				"}"
				;
			_appOutputFileContent = "@import url(./icons/computer.css);\n" +
				"@import url(\"./icons/database.css\");\n" +
				"@import url(\"./icons/google-plus.css\");\n" +
				"@import url(/app01/styles/icons/headphone[1].css);\n" +
				"@import url(\"/app01/styles/icons/joystick(2).css\");\n" +
				"@import url(\"/app01/styles/icons/keyboard{3}.css\");\n" +
				"@import url(~/styles/icons/license.css);\n" +
				"@import url(\"/app01/styles/icons/@monitor.css\");\n" +
				"@import url(\"/app01/styles/icons/$network.css\");\n" +
				"@import url(\"/app01/styles/icons/open_source.css\");\n" +
				"@import url(\"/app01/styles/icons/printer.css\");\n" +
				"@import url(/app01/styles/icons/qr-code.css);\n" +
				"@import url(\"/app01/styles/icons/radio.css\");\n" +
				"@import url(\"/app01/styles/icons/server.css\");\n" +
				"@import \"http://fonts.googleapis.com/css?family=Droid+Sans\";\n" +
				"@import '/app01/styles/fonts/bitstream-vera-sans-bold.css';\n" +
				"@import \"/app01/styles/fonts/league-gothic.css\";\n" +
				"@import 'print' print and (min-resolution: 300dpi);\n" +
				"html,\n" +
				"body,\n" +
				"ul,\n" +
				"ol {\n" +
				"  margin: 0;\n" +
				"  padding: 0;\n" +
				"}\n\n" +
				".icons {\n" +
				"  display: inline-block;\n" +
				"  background-repeat: no-repeat;\n" +
				"  width: 16px;\n" +
				"  height: 16px;\n" +
				"  line-height: 0;\n" +
				"  vertical-align: bottom;\n" +
				"}\n\n" +
				".icon-scanner {\n" +
				"  background-image: url(DaTa:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAACGUlEQ" +
				"VR42qWRA4wYURCG/31n27Zt2zai2rZthW1jNDxHtW2FtW1zb6fTVxv3Jd8zZ/Ar6urqytiBbCx+g1JZWYlfMLizs3NVTU3NYABHp" +
				"D9B4Be8efNGvH37FitWrFitqmo8t+PxL+Tk5Ix48uQxnThxnE6fPkWFhYXDCgoKEvC3pKSkjLlz5zYdOLBfevLkCcrOzh6ZlZWVi" +
				"L8hMTExJSYmZvy1a1do164d0kOHDlBqauoYPjwZn8jNzfVle7Ejvjc9PX10SEjI5AsXLtCWLVuke/fuobi4uPGxsbEpYJTMzMzeK" +
				"1YsH29nZxfEgcPXvn2r4v79+zh58iSamprABwGMkZERhg4dOomI9uL9k27cuCEDFTuxjaKmb6SwyWspcGwX+Y5qJ7/RHZQ6tZ2ah" +
				"0+j8+cv0ubNW6V79uyjwMDAqYJTpBARwJChJaKiohAaGorAgAD4+vjAw9MTpq4BeB1UjFkr18DbOwCAHl6+VPH69Wuh8F/GR7dEL" +
				"xmrjsGQ9ivQMbOFEAJCUWSto6sLfQMD6POzdZ/fgXq8HXNGDJXf6dWrbrYSGRk5ce3atYuePHnCJ77ik1/J+kMM3qK7W4OmaSAir" +
				"gkPHjzAkSOHMWHCfM5U0DwlKChoCg/M19PTA3/n+yDKzYDCClbno7qyjo72XghHR8fZN2/epK6uLmptbf0njY2NF8HMzKy/iYnJw" +
				"v/RwMBgIHrKOw2CR46YSAM+AAAAAElFTkSuQmCC);\n" +
				"}\n\n" +
				"@font-face {\n" +
				"  font-family: \"Bebas Neue\";\n" +
				"  src: url(\"/app01/fonts/bebas-neue/bebas-neue-webfont.eot\");\n" +
				"  src: url(\"/app01/fonts/bebas-neue/bebas-neue-webfont.eot?#iefix\") format(\"embedded-opentype\"), " +
				"url(\"/app01/fonts/bebas-neue/bebas-neue-webfont.woff\") format(\"woff\"), " +
				"url(\"/app01/fonts/bebas-neue/bebas-neue-webfont.ttf\") format(\"truetype\"), " +
				"url(\"/app01/fonts/bebas-neue/bebas-neue-webfont.svg#bebas_neueregular\") format(\"svg\");\n" +
				"  font-weight: normal;\n" +
				"  font-style: normal;\n" +
				"}\n" +
				"body {\n" +
				"  background-color: #fef;\n" +
				"  background-image: url(DATA:image/svg+xml," +
				"%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20width%3D%2220%22%20height%3D%2220%22%20" +
				"viewBox%3D%220%200%2020%2020%22%3E%3Cg%20stroke-width%3D%221%22%20fill%3D%22none%22%20" +
				"stroke%3D%22gray%22%20stroke-miterlimit%3D%228%22%3E%3Cpolygon%20" +
				"points%3D%2215.5%2C13.5%208%2C1.5%200.5%2C13.5%22%2F%3E%3C%2Fg%3E%3C%2Fsvg%3E" +
				");\n" +
				"  color: #699;\n" +
				"  font-size: 0.85em;\n" +
				"  font-family: \"League Gothic\", Helvetica, Sans-Serif;\n" +
				"}\n\n" +
				".sidebar {\n" +
				"  background-color: #f8f9fa;\n" +
				"  color: #6b717f;\n" +
				"  font-family: \"Droid Sans\", Arial, Sans-Serif;\n" +
				"  border: 1px solid #f5f6f8;\n" +
				"  border-radius: 5px;\n" +
				"  -webkit-border-radius: 5px;\n" +
				"  -moz-border-radius: 5px;\n" +
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
			virtualFileManagerMock
				.Setup(fm => fm.FileExists(It.IsAny<string>()))
				.Returns((string p) => {
					if (fromFile && p == _siteInputFileAbsolutePath)
					{
						return true;
					}

					return _siteImportedFiles.ContainsKey(p);
				})
				;
			virtualFileManagerMock
				.Setup(fm => fm.ReadFile(It.IsAny<string>()))
				.Returns((string p) => {
					if (fromFile && p == _siteInputFileAbsolutePath)
					{
						return _siteInputFileContent;
					}

					return _siteImportedFiles[p];
				})
				;

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
			Assert.AreEqual(_siteIncludedFilePaths, result.IncludedFilePaths);
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
			virtualFileManagerMock
				.Setup(fm => fm.FileExists(It.IsAny<string>()))
				.Returns((string p) => {
					if (fromFile && (p == _appInputFileRelativePath || p == _appInputFileAbsolutePath))
					{
						return true;
					}

					return _appImportedFiles.ContainsKey(p);
				})
				;
			virtualFileManagerMock
				.Setup(fm => fm.ReadFile(It.IsAny<string>()))
				.Returns((string p) => {
					if (fromFile && p == _appInputFileAbsolutePath)
					{
						return _appInputFileContent;
					}

					return _appImportedFiles[p];
				})
				;

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
			Assert.AreEqual(_appIncludedFilePaths, result.IncludedFilePaths);
			Assert.AreEqual(_appSourceMapFileContent, result.SourceMap);
		}
	}
}