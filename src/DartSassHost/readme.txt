

   --------------------------------------------------------------------------------
               README file for Dart Sass Host for .NET v2.0.0 Preview 3

   --------------------------------------------------------------------------------

           Copyright (c) 2020-2025 Andrey Taritsyn - http://www.taritsyn.ru


   ===========
   DESCRIPTION
   ===========
   .NET wrapper around the Dart Sass (https://github.com/sass/dart-sass) version
   1.93.3 with the ability to support a virtual file system.

   Since the original library is written in JavaScript, you will need a JS engine
   to run it. As a JS engine is used the JavaScript Engine Switcher library
   (https://github.com/Taritsyn/JavaScriptEngineSwitcher). For correct working, you
   need to install one of the following NuGet packages:

    * JavaScriptEngineSwitcher.ChakraCore
    * JavaScriptEngineSwitcher.Jint
    * JavaScriptEngineSwitcher.Msie (only in the Chakra “Edge” JsRT mode)
    * JavaScriptEngineSwitcher.V8

   After installing the packages, you will need to register the default JS engine
   (https://github.com/Taritsyn/JavaScriptEngineSwitcher/wiki/Registration-of-JS-engines).

   =============
   RELEASE NOTES
   =============
   1. Size of minified JS bundle containing the original library has been reduced
      by 25.32%;
   2. Performed a migration to the modern C# null/not-null checks;
   3. Added support for .NET 10;
   4. In the `lock` statements for .NET 10 target now uses a instances of the
      `System.Threading.Lock` class.

   ============
   PROJECT SITE
   ============
   https://github.com/Taritsyn/DartSassHost