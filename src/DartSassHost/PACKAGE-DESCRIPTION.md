.NET wrapper around the [Dart Sass](https://github.com/sass/dart-sass) version 1.77.4 with the ability to support a virtual file system.

Since the original library is written in JavaScript, you will need a JS engine to run it. As a JS engine is used the [JavaScript Engine Switcher](https://github.com/Taritsyn/JavaScriptEngineSwitcher) library.
For correct working, you need to install one of the following NuGet packages:

 * [JavaScriptEngineSwitcher.ChakraCore](https://www.nuget.org/packages/JavaScriptEngineSwitcher.ChakraCore)
 * [JavaScriptEngineSwitcher.Jint](https://www.nuget.org/packages/JavaScriptEngineSwitcher.Jint)
 * [JavaScriptEngineSwitcher.Msie](https://www.nuget.org/packages/JavaScriptEngineSwitcher.Msie) (only in the Chakra “Edge” JsRT mode)
 * [JavaScriptEngineSwitcher.V8](https://www.nuget.org/packages/JavaScriptEngineSwitcher.V8)

After installing the packages, you will need to [register the default JS engine](https://github.com/Taritsyn/JavaScriptEngineSwitcher/wiki/Registration-of-JS-engines).