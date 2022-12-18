

   --------------------------------------------------------------------------------
                    README file for Dart Sass Host for .NET v1.0.0

   --------------------------------------------------------------------------------

           Copyright (c) 2020-2022 Andrey Taritsyn - http://www.taritsyn.ru


   ===========
   DESCRIPTION
   ===========
   .NET wrapper around the Dart Sass (https://github.com/sass/dart-sass) version
   1.56.1 with the ability to support a virtual file system.

   Since the original library is written in JavaScript, you will need a JS engine
   to run it. As a JS engine is used the JavaScript Engine Switcher library
   (https://github.com/Taritsyn/JavaScriptEngineSwitcher). For correct working, you
   need to install one of the following NuGet packages:

    * JavaScriptEngineSwitcher.ChakraCore
    * JavaScriptEngineSwitcher.V8
    * JavaScriptEngineSwitcher.Msie (only in the Chakra “Edge” JsRT mode)

   After installing the packages, you will need to register the default JS engine
   (https://github.com/Taritsyn/JavaScriptEngineSwitcher/wiki/Registration-of-JS-engines).

   =============
   RELEASE NOTES
   =============
   1. Slightly improved performance of Sass compilation;
   2. JS files are no longer transpiled to ES5 to improve performance;
   3. Fixed a script error that occurred during handling non-Sass exceptions;
   4. Fixed a error that occurred during generation of the “pretty” file paths for
      errors and warnings.

   ============
   PROJECT SITE
   ============
   https://github.com/Taritsyn/DartSassHost