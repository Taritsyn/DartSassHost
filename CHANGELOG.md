Change log
==========

## v2.0.0 Preview 2 - October 29, 2025
 * Added support for the Dart Sass version 1.93.2

## v2.0.0 Preview 1 - October 10, 2025
 * Migration to the modern JS API of the Dart Sass library has been completed
 * In compilation options three properties have been deprecated: `IndentType`, `IndentWidth` and `LineFeedType`

## v1.1.1 - June 25, 2025
 * Added support for the Dart Sass version 1.89.2

## v1.1.0 - February 6, 2025
 * Added support for the Dart Sass version 1.83.4
 * In compilation options was added three new properties: `SilenceDeprecations` (default empty list), `FatalDeprecations` (default empty list) and `FutureDeprecations` (default empty list)
 * No longer supports a .NET Core App 3.1 and .NET 6
 * Added support for .NET 8

## v1.0.14 - August 27, 2024
 * Fixed a [error #14](https://github.com/Taritsyn/DartSassHost/issues/14) “Exception while reading compilation error”. Special thanks to [Peter Wurzinger](https://github.com/peterwurzinger)

## v1.0.13 - June 5, 2024
 * Added support for the Dart Sass version 1.77.4

## v1.0.12 - March 22, 2024
 * Added support for the Dart Sass version 1.72.0

## v1.0.11 - February 16, 2024
 * Added support for the Dart Sass version 1.70.0
 * JS files are now bundled and minified by using the Google Closure Compiler to improve performance

## v1.0.10 - January 7, 2024
 * JavaScriptEngineSwitcher.Msie module is again supported as a JS engine

## v1.0.9 - January 5, 2024
 * Added support for the Dart Sass version 1.69.7
 * JavaScriptEngineSwitcher.Msie module is no longer supported as an JS engine

## v1.0.8 - October 19, 2023
 * Added support for the Dart Sass version 1.69.3

## v1.0.7 - September 26, 2023
 * Added support for the Dart Sass version 1.68.0

## v1.0.6 - August 25, 2023
 * Added support of the Dart Sass version 1.66.1

## v1.0.5 - July 27, 2023
 * Added support of the Dart Sass version 1.64.1

## v1.0.4 - June 15, 2023
 * Added support of the Dart Sass version 1.63.3

## v1.0.3 - April 26, 2023
 * Added support of the Dart Sass version 1.62.0

## v1.0.2 - March 9, 2023
 * Added support of the Dart Sass version 1.58.3

## v1.0.1 - December 26, 2022
 * Added support of the Dart Sass version 1.57.1

## v1.0.0 - December 18, 2022
 * Slightly improved performance of Sass compilation
 * JS files are no longer transpiled to ES5 to improve performance
 * Fixed a script error that occurred during handling non-Sass exceptions
 * Fixed a error that occurred during generation of the “pretty” file paths for errors and warnings

## v1.0.0 Preview 9 - December 9, 2022
 * Added support of the Dart Sass version 1.56.1
 * Fixed a error that occurred during generation of source fragment for warnings on Unix-like operating systems
 * Slightly improved performance of Sass compilation

## v1.0.0 Preview 8 - July 13, 2022
 * Added support of the Dart Sass version 1.53.0
 * .NET 5.0 target was replaced by a .NET 6 target
 * `FileManagerWrapper` class is no longer used
 * Fixed a error that occurred during generation of file paths in the stack trace for errors and warnings on Unix-like operating systems

## v1.0.0 Preview 7 - November 5, 2021
 * Added support of the Dart Sass version 1.43.4
 * In compilation options was added three new properties: `Charset` (default `true`), `WarningLevel` (default `Default`) and `QuietDependencies` (default `false`)
 * Fragments of source code is now added to warning messages
 * Call stack is now added to compilation exceptions and error messages

## v1.0.0 Preview 6 - September 8, 2021
 * Reduced a number of accesses to the file system due to caching

## v1.0.0 Preview 5 - September 1, 2021
 * Now, when reading a script code from the embedded resources, caching is performed
 * Optimized a source code fetching for warnings
 * For latest versions of .NET now uses the System.Text.Json library as JSON serializer
 * Added a .NET Core App 3.1 and .NET 5.0 targets
 * Optimized a generation of source maps
 * Added support of the Dart Sass version 1.38.2

## v1.0.0 Preview 4 - August 12, 2021
 * Added support of the Dart Sass version 1.37.5

## v1.0.0 Preview 3 - July 29, 2021
 * Added support of the Dart Sass version 1.36.0
 * Now can use the JavaScriptEngineSwitcher.V8 as a JS engine

## v1.0.0 Preview 2 - July 23, 2021
 * Fixed a error that caused generation of absolute paths in source maps

## v1.0.0 Preview 1 - July 22, 2021
 * Initial version uploaded