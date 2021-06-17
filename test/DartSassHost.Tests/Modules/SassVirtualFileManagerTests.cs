using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace DartSassHost.Tests.Modules
{
	[TestFixture]
	public class SassVirtualFileManagerTests : VirtualFileManagerTestsBase
	{
		public SassVirtualFileManagerTests()
		{
			_siteInputFileAbsolutePath = "/styles/site.sass";
			_siteInputFileContent = "@use \"variables\" as *\n" +
				"@use 'foundation' as f\n" +
				"@use \"/styles/icons\"\n" +
				"@use '/styles/fonts'\n\n" +
				"body\n" +
				"  background-color: $bg-color\n" +
				"  color: #699\n" +
				"  font-size: .85em\n" +
				"  font-family: \"League Gothic\", Helvetica, Sans-Serif\n" +
				"\n" +
				".sidebar\n" +
				"  background-color: #f8f9fa\n" +
				"  color: #6b717f\n" +
				"  font-family: \"Bebas Neue\", Arial, Sans-Serif\n" +
				"  border: 1px solid #f5f6f8\n" +
				"  @include f.border-radius(5px)"
				;
			_siteImportedFiles["/styles/_variables.sass"] = "$bg-color: #fef\n" +
				"$icons-path: \"/images/icons\""
				;
			_siteImportedFiles["/styles/foundation/_index.sass"] = "@forward 'mixins'\n" +
				"@forward 'reset'"
				;
			_siteImportedFiles["/styles/foundation/_mixins.sass"] = "@mixin border-radius($radius: 5px)\n" +
				"  border-radius: $radius\n" +
				"  -webkit-border-radius: $radius\n" +
				"  -moz-border-radius: $radius\n" +
				"\n" +
				"@mixin visible\n" +
				"  display: block"
				;
			_siteImportedFiles["/styles/foundation/_reset.sass"] = "html,\n" +
				"body,\n" +
				"ul,\n" +
				"ol\n" +
				"  margin: 0\n" +
				"  padding: 0"
				;
			_siteImportedFiles["/styles/_fonts.sass"] = "@use \"fonts/bebas-neue\"\n" +
				"@use \"fonts/league-gothic.sass\""
				;
			_siteImportedFiles["/styles/fonts/league-gothic.sass"] = "$fonts-path: \"/fonts\"\n\n" +
				"@font-face\n" +
				"  font-family: \"League Gothic\"\n" +
				"  src: url(\"#{$fonts-path}/league-gothic/league-gothic-webfont.eot\")\n" +
				"  src: url(\"#{$fonts-path}/league-gothic/league-gothic-webfont.eot?#iefix\") format(\"embedded-opentype\"), " +
				"url(\"#{$fonts-path}/league-gothic/league-gothic-webfont.woff\") format(\"woff\"), " +
				"url(\"#{$fonts-path}/league-gothic/league-gothic-webfont.ttf\") format(\"truetype\"), " +
				"url(\"#{$fonts-path}/league-gothic/league-gothic-webfont.svg#league-gothicregular\") format(\"svg\")\n" +
				"  font-weight: normal\n" +
				"  font-style: normal"
				;
			_siteImportedFiles["/styles/_icons.sass"] = "@use \"variables\" as *\n\n" +
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
			_siteIncludedFilePaths = new List<string>
			{
				_siteInputFileAbsolutePath,
				"/styles/_variables.sass",
				"/styles/foundation/_index.sass",
				"/styles/foundation/_mixins.sass",
				"/styles/foundation/_reset.sass",
				"/styles/_icons.sass",
				"/styles/_fonts.sass",
				"/styles/fonts/bebas-neue.css",
				"/styles/fonts/league-gothic.sass"
			};
			_siteSourceMapFileContent = "{\"version\":3,\"sourceRoot\":\"\"," +
				"\"sources\":[\"foundation/_reset.sass\",\"_icons.sass\",\"fonts/bebas-neue.css\",\"fonts/league-gothic.sass\"," +
				"\"site.sass\",\"_variables.sass\",\"foundation/_mixins.sass\"]," +
				"\"names\":[],\"mappings\":\"AAAA;AAAA;AAAA;AAAA;EAIE;EACA;;;ACHF;EACE;EACA;EACA;EACA;EACA;EACA;;;" +
				"AAEF;EACE;;;AAEF;EACE;;;AAEF;EACE;;;AAEF;EACE;;;AAEF;EACE;;;ACvBF;EACE;EACA;EACA;EAIA;EACA;;ACNF;" +
				"EACE;EACA;EACA;EACA;EACA;;ACFF;EACE,kBCNS;EDOT;EACA;EACA;;;AAEF;EACE;EACA;EACA;EACA;EEdA,eFeyB;" +
				"EEdzB,uBFcyB;EEbzB,oBFayB\"," +
				"\"file\":\"site.css\"}"
				;

			_appInputFileRelativePath = "~/styles/app.sass";
			_appInputFileAbsolutePath = "/app01/styles/app.sass";
			_appInputFileContent = "@use \"variables\" as *\n" +
				"@use 'foundation' as f\n" +
				"@use \"~/styles/icons\"\n" +
				"@use '~/styles/fonts'\n\n" +
				"body\n" +
				"  background-color: $bg-color\n" +
				"  color: #699\n" +
				"  font-size: .85em\n" +
				"  font-family: \"League Gothic\", Helvetica, Sans-Serif\n" +
				"\n" +
				".sidebar\n" +
				"  background-color: #f8f9fa\n" +
				"  color: #6b717f\n" +
				"  font-family: \"Bebas Neue\", Arial, Sans-Serif\n" +
				"  border: 1px solid #f5f6f8\n" +
				"  @include f.border-radius(5px)"
				;
			_appImportedFiles["/app01/styles/_variables.sass"] = "$bg-color: #fef\n" +
				"$icons-path: \"~/images/icons\""
				;
			_appImportedFiles["/app01/styles/foundation/_index.sass"] = "@forward 'mixins'\n" +
				"@forward 'reset'"
				;
			_appImportedFiles["/app01/styles/foundation/_mixins.sass"] = "@mixin border-radius($radius: 5px)\n" +
				"  border-radius: $radius\n" +
				"  -webkit-border-radius: $radius\n" +
				"  -moz-border-radius: $radius\n" +
				"\n" +
				"@mixin visible\n" +
				"  display: block"
				;
			_appImportedFiles["/app01/styles/foundation/_reset.sass"] = "html,\n" +
				"body,\n" +
				"ul,\n" +
				"ol\n" +
				"  margin: 0\n" +
				"  padding: 0"
				;
			_appImportedFiles["/app01/styles/_fonts.sass"] = "@use \"fonts/bebas-neue\"\n" +
				"@use \"fonts/league-gothic.sass\""
				;
			_appImportedFiles["/app01/styles/fonts/league-gothic.sass"] = "$fonts-path: \"~/fonts\"\n\n" +
				"@font-face\n" +
				"  font-family: \"League Gothic\"\n" +
				"  src: url(\"#{$fonts-path}/league-gothic/league-gothic-webfont.eot\")\n" +
				"  src: url(\"#{$fonts-path}/league-gothic/league-gothic-webfont.eot?#iefix\") format(\"embedded-opentype\"), " +
				"url(\"#{$fonts-path}/league-gothic/league-gothic-webfont.woff\") format(\"woff\"), " +
				"url(\"#{$fonts-path}/league-gothic/league-gothic-webfont.ttf\") format(\"truetype\"), " +
				"url(\"#{$fonts-path}/league-gothic/league-gothic-webfont.svg#league-gothicregular\") format(\"svg\")\n" +
				"  font-weight: normal\n" +
				"  font-style: normal"
				;
			_appImportedFiles["/app01/styles/_icons.sass"] = "@use \"variables\" as *\n\n" +
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
			_appIncludedFilePaths = new List<string>
			{
				_appInputFileAbsolutePath,
				"/app01/styles/_variables.sass",
				"/app01/styles/foundation/_index.sass",
				"/app01/styles/foundation/_mixins.sass",
				"/app01/styles/foundation/_reset.sass",
				"/app01/styles/_icons.sass",
				"/app01/styles/_fonts.sass",
				"/app01/styles/fonts/bebas-neue.css",
				"/app01/styles/fonts/league-gothic.sass"
			};
			_appSourceMapFileContent = "{\"version\":3,\"sourceRoot\":\"\"," +
				"\"sources\":[\"file:///app01/styles/foundation/_reset.sass\",\"file:///app01/styles/_icons.sass\"," +
				"\"file:///app01/styles/fonts/bebas-neue.css\",\"file:///app01/styles/fonts/league-gothic.sass\"," +
				"\"file:///app01/styles/app.sass\",\"file:///app01/styles/_variables.sass\"," +
				"\"file:///app01/styles/foundation/_mixins.sass\"]," +
				"\"names\":[],\"mappings\":\"AAAA;AAAA;AAAA;AAAA;EAIE;EACA;;;ACHF;EACE;EACA;EACA;EACA;EACA;EACA;;;" +
				"AAEF;EACE;;;AAEF;EACE;;;AAEF;EACE;;;AAEF;EACE;;;AAEF;EACE;;;ACvBF;EACE;EACA;EACA;EAIA;EACA;;ACNF;" +
				"EACE;EACA;EACA;EACA;EACA;;ACFF;EACE,kBCNS;EDOT;EACA;EACA;;;AAEF;EACE;EACA;EACA;EACA;EEdA,eFeyB;EEdzB,uBFcyB;EEbzB,oBFayB\"," +
				"\"file\":\"app.css\"}"
				;
		}
	}
}