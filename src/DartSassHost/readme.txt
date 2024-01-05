

   --------------------------------------------------------------------------------
                    README file for Dart Sass Host for .NET v1.0.9

   --------------------------------------------------------------------------------

           Copyright (c) 2020-2024 Andrey Taritsyn - http://www.taritsyn.ru


   ===========
   DESCRIPTION
   ===========
   .NET wrapper around the Dart Sass (https://github.com/sass/dart-sass) version
   1.69.7 with the ability to support a virtual file system.

   Since the original library is written in JavaScript, you will need a JS engine
   to run it. As a JS engine is used the JavaScript Engine Switcher library
   (https://github.com/Taritsyn/JavaScriptEngineSwitcher). For correct working, you
   need to install one of the following NuGet packages:

    * JavaScriptEngineSwitcher.ChakraCore
    * JavaScriptEngineSwitcher.Jint
    * JavaScriptEngineSwitcher.V8

   After installing the packages, you will need to register the default JS engine
   (https://github.com/Taritsyn/JavaScriptEngineSwitcher/wiki/Registration-of-JS-engines).

   =============
   RELEASE NOTES
   =============
   1. Added support for the Dart Sass version 1.69.7;
   2. JavaScriptEngineSwitcher.Msie module is no longer supported as an JS engine.

   ============
   PROJECT SITE
   ============
   https://github.com/Taritsyn/DartSassHost