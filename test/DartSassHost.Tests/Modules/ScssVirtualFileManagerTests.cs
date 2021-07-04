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
				"AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AC5BF;EACE;EACA;EACA;EAIA;EACA;;ACNF;" +
				"EACE;EACA;EACA;EAIA;EACA;;ACLF;EACE,kBCNS;EDOT;EACA;EACA;;;AAGF;EACE;EACA;EACA;EACA;EEfA,eFgByB;" +
				"EEfzB,uBFeyB;EEdzB,oBFcyB\"," +
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
				"\"sources\":[\"file:///app01/styles/foundation/_reset.scss\",\"file:///app01/styles/_icons.scss\"," +
				"\"file:///app01/styles/fonts/bebas-neue.css\",\"file:///app01/styles/fonts/league-gothic.scss\"," +
				"\"file:///app01/styles/app.scss\",\"file:///app01/styles/_variables.scss\"," +
				"\"file:///app01/styles/foundation/_mixins.scss\"]," +
				"\"names\":[],\"mappings\":\"AAAA;AAAA;AAAA;AAAA;EAIE;EACA;;;ACHF;EACE;EACA;EACA;EACA;EACA;EACA;;;" +
				"AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AAGF;EACE;;;AC5BF;EACE;EACA;EACA;EAIA;EACA;;ACNF;" +
				"EACE;EACA;EACA;EAIA;EACA;;ACLF;EACE,kBCNS;EDOT;EACA;EACA;;;AAGF;EACE;EACA;EACA;EACA;EEfA,eFgByB;" +
				"EEfzB,uBFeyB;EEdzB,oBFcyB\"," +
				"\"file\":\"app.css\"}"
				;
		}
	}
}