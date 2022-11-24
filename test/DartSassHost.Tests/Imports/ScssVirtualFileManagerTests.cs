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
				"@import url(/styles/icons/qr-code.css), url('/styles/icons/radio.css'), url(\"/styles/icons/server.css\");"
				;
			_siteImportedFiles["/styles/layout.scss"] = "body {\n" +
				"  background-color: $bg-color;\n" +
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
				"ACJR;AAAA;AAAA;AAAA;EAIE;EACA;;;AHLF;EACE;EACA;EACA;EACA;EACA;EACA;;;AINF;EACE;EACA;EACA;EAIA;EACA;;ACRF;" +
				"EACE,kBCDS;EDET;EACA;EACA;;;AAGF;EACE;EACA;EACA;EACA;EEVA,eFWuB;EEVvB,uBFUuB;EETvB,oBFSuB\"," +
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
				"url(\"/app01/styles/icons/server.css\");"
				;
			_appImportedFiles["/app01/styles/layout.scss"] = "body {\n" +
				"  background-color: $bg-color;\n" +
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
				"AAA+C;ACE/C;ACJR;AAAA;AAAA;AAAA;EAIE;EACA;;;AHLF;EACE;EACA;EACA;EACA;EACA;EACA;;;AINF;EACE;EACA;" +
				"EACA;EAIA;EACA;;ACRF;EACE,kBCDS;EDET;EACA;EACA;;;AAGF;EACE;EACA;EACA;EACA;EEVA,eFWuB;EEVvB,uBFUuB;" +
				"EETvB,oBFSuB\"," +
				"\"file\":\"app.css\"}"
				;
		}
	}
}