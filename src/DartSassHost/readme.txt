

   --------------------------------------------------------------------------------
               README file for Dart Sass Host for .NET v1.0.0 Preview 5

   --------------------------------------------------------------------------------

              Copyright (c) 2021 Andrey Taritsyn - http://www.taritsyn.ru


   ===========
   DESCRIPTION
   ===========
   .NET wrapper around the Dart Sass (https://github.com/sass/dart-sass) version
   1.38.2 with the ability to support a virtual file system.

   Since the original library is written in JavaScript, you will need a JS engine
   to run it. As a JS engine is used the JavaScript Engine Switcher library
   (https://github.com/Taritsyn/JavaScriptEngineSwitcher). For correct working, you
   need to install one of the following NuGet packages:

    * JavaScriptEngineSwitcher.ChakraCore
    * JavaScriptEngineSwitcher.V8
    * JavaScriptEngineSwitcher.Msie (only in the Chakra JsRT modes)

   After installing the packages, you will need to register the default JS engine
   (https://github.com/Taritsyn/JavaScriptEngineSwitcher/wiki/Registration-of-JS-engines).

   =============
   RELEASE NOTES
   =============
   1. Now, when reading a script code from the embedded resources, caching is
      performed;
   2. Optimized a source code fetching for warnings;
   3. For latest versions of .NET now uses the System.Text.Json library as JSON
      serializer;
   4. Added a .NET Core App 3.1 and .NET 5.0 targets;
   5. Optimized a generation of source maps;
   6. Added support of the Dart Sass version 1.38.2.

   ============
   PROJECT SITE
   ============
   https://github.com/Taritsyn/DartSassHost