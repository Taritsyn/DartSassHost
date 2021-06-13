using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace DartSassHost.Tests.Imports
{
	[TestFixture]
	public class SassVirtualFileManagerTests : VirtualFileManagerTestsBase
	{
		public SassVirtualFileManagerTests()
		{
			_siteInputFileAbsolutePath = "/styles/site.sass";
			_siteInputFileContent = "@import \"variables\"\n" +
				"@import 'foundation'\n" +
				"@import \"/styles/icons\", '/styles/fonts'\n" +
				"@import \"/styles/layout.sass\"\n" +
				"@import 'print' print and (min-resolution: 300dpi)"
				;
			_siteImportedFiles["/styles/_variables.sass"] = "$bg-color: #fef\n" +
				"$icons-path: \"/styles/icons\""
				;
			_siteImportedFiles["/styles/foundation/_index.sass"] = "@import 'mixins', 'reset'";
			_siteImportedFiles["/styles/foundation/_mixins.sass"] = "@mixin border-radius($radius: 5px)\n" +
				"  border-radius: $radius\n" +
				"  -webkit-border-radius: $radius\n" +
				"  -moz-border-radius: $radius\n" +
				"\n" +
				"@mixin visible\n" +
				"  display: block\n"
				;
			_siteImportedFiles["/styles/foundation/_reset.sass"] = "html,\n" +
				"body,\n" +
				"ul,\n" +
				"ol\n" +
				"  margin: 0\n" +
				"  padding: 0"
				;
			_siteImportedFiles["/styles/_fonts.sass"] = "@import \"fonts/bebas-neue\"\n" +
				"@import \"http://fonts.googleapis.com/css?family=Droid+Sans\"\n" +
				"@import \"fonts/league-gothic.css\""
				;
			_siteImportedFiles["/styles/_icons.sass"] = ".icons\n"+
				"  display: inline-block\n" +
				"  background-repeat: no-repeat\n" +
				"  width: 16px\n" +
				"  height: 16px\n" +
				"  line-height: 0\n" +
				"  vertical-align: bottom\n" +
				"\n" +
				"@import url(./icons/google-plus.css)\n" +
				"@import URL(  '/styles/icons/headphone.css'  )\n" +
				"@import url(\"#{$icons-path}/monitor.css\")\n" +
				"@import url($icons-path + \"/robot.css\")\n" +
				"@import Url(  /styles/icons/usb-flash-drive.css  )"
				;
			_siteImportedFiles["/styles/layout.sass"] = "body\n" +
				"  background-color: $bg-color\n" +
				"  color: #699\n" +
				"  font-size: .85em\n" +
				"  font-family: \"League Gothic\", Helvetica, Sans-Serif\n" +
				"\n" +
				".sidebar\n" +
				"  background-color: #f8f9fa\n" +
				"  color: #6b717f\n" +
				"  font-family: \"Droid Sans\", Arial, Sans-Serif\n" +
				"  border: 1px solid #f5f6f8\n" +
				"  @include border-radius(5px)\n"
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
				"/styles/layout.sass"
			};
			_siteSourceMapFileContent = "{\"version\":3,\"sourceRoot\":\"\"," +
				"\"sources\":[\"_icons.sass\",\"_fonts.sass\",\"site.sass\",\"foundation/_reset.sass\"," +
				"\"fonts/bebas-neue.css\",\"layout.sass\",\"_variables.sass\",\"foundation/_mixins.sass\"]," +
				"\"names\":[],\"mappings\":\"AAQQ;AACA;AACA;AACA;AACA;ACXA;AACA;ACEA;ACJR;AAAA;AAAA;AAAA;EAIE;EACA;;;" +
				"AHLF;EACE;EACA;EACA;EACA;EACA;EACA;;;AINF;EACE;EACA;EACA;EAIA;EACA;;ACRF;EACE,kBCDS;EDET;EACA;EACA;;;" +
				"AAEF;EACE;EACA;EACA;EACA;EETA,eFUuB;EETvB,uBFSuB;EERvB,oBFQuB\"," +
				"\"file\":\"site.css\"}"
				;

			_appInputFileRelativePath = "~/styles/app.sass";
			_appInputFileAbsolutePath = "/app01/styles/app.sass";
			_appInputFileContent = "@import \"variables\"\n" +
				"@import 'foundation'\n" +
				"@import \"~/styles/icons\", '~/styles/fonts'\n" +
				"@import \"~/styles/layout.sass\"\n" +
				"@import 'print' print and (min-resolution: 300dpi)"
				;
			_appImportedFiles["/app01/styles/_variables.sass"] = "$bg-color: #fef\n" +
				"$icons-path: \"~/styles/icons\""
				;
			_appImportedFiles["/app01/styles/foundation/_index.sass"] = "@import 'mixins', 'reset'";
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
			_appImportedFiles["/app01/styles/_fonts.sass"] = "@import \"fonts/bebas-neue\"\n" +
				"@import \"http://fonts.googleapis.com/css?family=Droid+Sans\"\n" +
				"@import \"fonts/league-gothic.css\""
				;
			_appImportedFiles["/app01/styles/_icons.sass"] = ".icons\n"+
				"  display: inline-block\n" +
				"  background-repeat: no-repeat\n" +
				"  width: 16px\n" +
				"  height: 16px\n" +
				"  line-height: 0\n" +
				"  vertical-align: bottom\n" +
				"\n" +
				"@import url(./icons/google-plus.css)\n" +
				"@import URL(  '~/styles/icons/headphone.css'  )\n" +
				"@import url(\"#{$icons-path}/monitor.css\")\n" +
				"@import url($icons-path + \"/robot.css\")\n" +
				"@import Url(  ~/styles/icons/usb-flash-drive.css  )"
				;
			_appImportedFiles["/app01/styles/layout.sass"] = "body\n" +
				"  background-color: $bg-color\n" +
				"  color: #699\n" +
				"  font-size: .85em\n" +
				"  font-family: \"League Gothic\", Helvetica, Sans-Serif\n" +
				"\n" +
				".sidebar\n" +
				"  background-color: #f8f9fa\n" +
				"  color: #6b717f\n" +
				"  font-family: \"Droid Sans\", Arial, Sans-Serif\n" +
				"  border: 1px solid #f5f6f8\n" +
				"  @include border-radius(5px)"
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
				"/app01/styles/layout.sass"
			};
			_appSourceMapFileContent = "{\"version\":3,\"sourceRoot\":\"\"," +
				"\"sources\":[\"file:///app01/styles/_icons.sass\",\"file:///app01/styles/_fonts.sass\"," +
				"\"file:///app01/styles/app.sass\",\"file:///app01/styles/foundation/_reset.sass\"," +
				"\"file:///app01/styles/fonts/bebas-neue.css\",\"file:///app01/styles/layout.sass\"," +
				"\"file:///app01/styles/_variables.sass\",\"file:///app01/styles/foundation/_mixins.sass\"]," +
				"\"names\":[],\"mappings\":\"AAQQ;AACA;AACA;AACA;AACA;ACXA;AACA;ACEA;ACJR;AAAA;AAAA;AAAA;EAIE;EACA;;;" +
				"AHLF;EACE;EACA;EACA;EACA;EACA;EACA;;;AINF;EACE;EACA;EACA;EAIA;EACA;;ACRF;EACE,kBCDS;EDET;EACA;EACA;;;" +
				"AAEF;EACE;EACA;EACA;EACA;EETA,eFUuB;EETvB,uBFSuB;EERvB,oBFQuB\"," +
				"\"file\":\"app.css\"}"
				;
		}
	}
}