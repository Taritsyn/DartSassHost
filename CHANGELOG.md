Change log
==========

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