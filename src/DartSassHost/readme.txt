

   --------------------------------------------------------------------------------
                    README file for Dart Sass Host for .NET v2.0.0

   --------------------------------------------------------------------------------

           Copyright (c) 2020-2026 Andrey Taritsyn - http://www.taritsyn.ru


   ===========
   DESCRIPTION
   ===========
   .NET wrapper around the Dart Sass (https://github.com/sass/dart-sass) version
   1.97.2 with the ability to support a virtual file system.

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
   1. Added support for the Dart Sass version 1.97.2;
   2. Migration to the modern JS API of the Dart Sass library has been completed;
   3. In compilation options three properties have been deprecated: `IndentType`,
      `IndentWidth` and `LineFeedType`;
   4. Added a `DeprecationId` class containing constants for deprecation IDs;
   5. Size of minified JS bundle containing the original library has been reduced
      by 27.74%;
   6. Performed a migration to the modern C# null/not-null checks;
   7. Added support for .NET 10;
   8. In the `lock` statements for .NET 10 target now uses a instances of the
      `System.Threading.Lock` class;
   9. An unnecessary cache that responsible for storing file contents has been
      removed from the `DshFileManagerProxy` class;
   10. Data URIs are excluded from processing by the `IsAppRelativeVirtualPath`
       method of the `IFileManager` interface;
   11. A new cache that responsible for storing converted paths has been added to
       the `DshFileManagerProxy` class.

   ============
   PROJECT SITE
   ============
   https://github.com/Taritsyn/DartSassHost