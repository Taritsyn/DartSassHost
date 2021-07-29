Dart Sass Host for .NET
=======================

![Dart Sass Host logo](https://raw.githubusercontent.com/Taritsyn/DartSassHost/main/images/DartSassHost_Logo.png)

.NET wrapper around the [Dart Sass](https://github.com/sass/dart-sass) with the ability to support a virtual file system.

## Installation
This library can be installed through NuGet - [https://nuget.org/packages/DartSassHost](https://nuget.org/packages/DartSassHost).
Since the original library is written in JavaScript, you will need a JS engine to run it.
As a JS engine is used the [JavaScript Engine Switcher](https://github.com/Taritsyn/JavaScriptEngineSwitcher) library.
For correct working, you need to install one of the following NuGet packages:

 * [JavaScriptEngineSwitcher.ChakraCore](https://nuget.org/packages/JavaScriptEngineSwitcher.ChakraCore/)
 * [JavaScriptEngineSwitcher.V8](https://nuget.org/packages/JavaScriptEngineSwitcher.V8)
 * [JavaScriptEngineSwitcher.Msie](https://nuget.org/packages/JavaScriptEngineSwitcher.Msie) (only in the Chakra JsRT modes)

After installing the packages, you will need to [register the default JS engine](https://github.com/Taritsyn/JavaScriptEngineSwitcher/wiki/Registration-of-JS-engines).

## Usage
When we create an instance of the <code title="DartSassHost.SassCompiler">SassCompiler</code> class by using the constructor without parameters:

```csharp
var sassCompiler = new SassCompiler();
```

Then we always use a JS engine registered by default. In fact, a constructor without parameters is equivalent to the following code:

```csharp
var sassCompiler = new SassCompiler(JsEngineSwitcher.Current.CreateDefaultEngine);
```

This approach is great for web applications, but in some cases the usage of JS engine registration at global level will be redundant.
It is for such cases that the possibility of passing of the JS engine factory to the constructor is provided:

```csharp
var sassCompiler = new SassCompiler(new ChakraCoreJsEngineFactory());
```

You can also use a delegate that creates an instance of the JS engine:

```csharp
var sassCompiler = new SassCompiler(() => new ChakraCoreJsEngine());
```

The main feature of this library is ability to support a virtual file system. You can pass an file manager through constructor of the <code title="DartSassHost.SassCompiler">SassCompiler</code> class:

```csharp
var sassCompiler = new SassCompiler(CustomFileManager());
```

Any class, that implements an <code title="DartSassHost.IFileManager">IFileManager</code> interface, can be used as a file manager.

It should also be noted, that this library does not write the result of compilation to disk. `Compile` and `CompileFile` methods of the <code title="DartSassHost.SassCompiler">SassCompiler</code> class return the result of compilation in the form of an instance of the <code title="DartSassHost.CompilationResult">CompilationResult</code> class. Consider in detail properties of the <code title="DartSassHost.CompilationResult">CompilationResult</code> class:

<table border="1" style="font-size: 0.7em">
    <thead>
        <tr valign="top">
            <th>Property name</th>
            <th>Data&nbsp;type</th>
            <th>Description</th>
        </tr>
    </thead>
    <tbody>
        <tr valign="top">
            <td><code>CompiledContent</code></td>
            <td><code title="System.String">String</code></td>
            <td>CSS code.</td>
        </tr>
        <tr valign="top">
            <td><code>IncludedFilePaths</code></td>
            <td><code title="System.Collections.Generic.IList&lt;string&gt;">IList&lt;string&gt;</code></td>
            <td>List of included files.</td>
        </tr>
        <tr valign="top">
            <td><code>SourceMap</code></td>
            <td><code title="System.String">String</code></td>
            <td>Source map.</td>
        </tr>
        <tr valign="top">
            <td><code>Warnings</code></td>
            <td><code title="System.Collections.Generic.IList&lt;DartSassHost.ProblemInfo&gt;">IList&lt;ProblemInfo&gt;</code></td>
            <td>List of the warnings.</td>
        </tr>
    </tbody>
</table>

Consider a simple example of usage of the `Compile` method:

```csharp
using System;

using DartSassHost;
using DartSassHost.Helpers;
using JavaScriptEngineSwitcher.ChakraCore;

namespace DartSassHost.Example.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            const string inputContent = @"$font-stack: Helvetica, sans-serif;
$primary-color: #333;

body {
  font: 100% $font-stack;
  color: $primary-color;
}";

            var options = new CompilationOptions { SourceMap = true };

            try
            {
                using (var sassCompiler = new SassCompiler(new ChakraCoreJsEngineFactory(), options))
                {
                    CompilationResult result = sassCompiler.Compile(inputContent, "input.scss",
                        "output.css", "output.css.map", options);

                    Console.WriteLine("Compiled content:{1}{1}{0}{1}", result.CompiledContent,
                        Environment.NewLine);
                    Console.WriteLine("Source map:{1}{1}{0}{1}", result.SourceMap, Environment.NewLine);
                    Console.WriteLine("Included file paths: {0}",
                        string.Join(", ", result.IncludedFilePaths));
                }
            }
            catch (SassCompilerLoadException e)
            {
                Console.WriteLine("During loading of Sass compiler an error occurred. See details:");
                Console.WriteLine();
                Console.WriteLine(SassErrorHelpers.GenerateErrorDetails(e));
            }
            catch (SassCompilationException e)
            {
                Console.WriteLine("During compilation of SCSS code an error occurred. See details:");
                Console.WriteLine();
                Console.WriteLine(SassErrorHelpers.GenerateErrorDetails(e));
            }
            catch (SassException e)
            {
                Console.WriteLine("During working of Sass compiler an unknown error occurred. See details:");
                Console.WriteLine();
                Console.WriteLine(SassErrorHelpers.GenerateErrorDetails(e));
            }
        }
    }
}
```

First we create an instance of the <code title="DartSassHost.SassCompiler">SassCompiler</code> class, in the constructor of which we pass the JS engine factory and compilation options.
Let's consider in detail properties of the <code title="DartSassHost.CompilationOptions">CompilationOptions</code> class:

<table border="1" style="font-size: 0.7em">
    <thead>
        <tr valign="top">
            <th>Property name</th>
            <th>Data&nbsp;type</th>
            <th>Default value</th>
            <th>Description</th>
        </tr>
    </thead>
    <tbody>
        <tr valign="top">
            <td><code>IncludePaths</code></td>
            <td><code title="System.Collections.Generic.IList&lt;string&gt;">IList&lt;string&gt;</code></td>
            <td>Empty list</td>
            <td>List of paths that library can look in to attempt to resolve <code>@import</code> declarations.</td>
        </tr>
        <tr valign="top">
            <td><code>IndentType</code></td>
            <td><code title="DartSassHost.IndentType">IndentType</code> enumeration</td>
            <td><code>Space</code></td>
            <td>Indent type. Can take the following values:
                <ul>
                    <li><code>Space</code> - space character</li>
                    <li><code>Tab</code> - tab character</li>
                </ul>
            </td>
        </tr>
        <tr valign="top">
            <td><code>IndentWidth</code></td>
            <td><code title="System.Int32">Int32</code></td>
            <td><code>2</code></td>
            <td>Number of spaces or tabs to be used for indentation.</td>
        </tr>
        <tr valign="top">
            <td><code>InlineSourceMap</code></td>
            <td><code title="System.Boolean">Boolean</code></td>
            <td><code>false</code></td>
            <td>Flag for whether to embed <code>sourceMappingUrl</code> as data uri.</td>
        </tr>
        <tr valign="top">
            <td><code>LineFeedType</code></td>
            <td><code title="DartSassHost.LineFeedType">LineFeedType</code> enumeration</td>
            <td><code>Lf</code></td>
            <td>Line feed type. Can take the following values:
                <ul>
                    <li><code>Cr</code> - Macintosh (CR)</li>
                    <li><code>CrLf</code> - Windows (CR LF)</li>
                    <li><code>Lf</code> - Unix (LF)</li>
                    <li><code>LfCr</code></li>
                </ul>
            </td>
        </tr>
        <tr valign="top">
            <td><code>OmitSourceMapUrl</code></td>
            <td><code title="System.Boolean">Boolean</code></td>
            <td><code>false</code></td>
            <td>Flag for whether to disable <code>sourceMappingUrl</code> in css output.</td>
        </tr>
        <tr valign="top">
            <td><code>OutputStyle</code></td>
            <td><code title="DartSassHost.OutputStyle">OutputStyle</code> enumeration</td>
            <td><code>Expanded</code></td>
            <td>Output style for the generated css code. Can take the following values:
                <ul>
                    <li><code>Expanded</code></li>
                    <li><code>Compressed</code></li>
                </ul>
            </td>
        </tr>
        <tr valign="top">
            <td><code>SourceMap</code></td>
            <td><code title="System.Boolean">Boolean</code></td>
            <td><code>false</code></td>
            <td>Flag for whether to enable source map generation.</td>
        </tr>
        <tr valign="top">
            <td><code>SourceMapIncludeContents</code></td>
            <td><code title="System.Boolean">Boolean</code></td>
            <td><code>false</code></td>
            <td>Flag for whether to include contents in maps.</td>
        </tr>
        <tr valign="top">
            <td><code>SourceMapRootPath</code></td>
            <td><code title="System.String">String</code></td>
            <td>Empty string</td>
            <td>Value will be emitted as <code>sourceRoot</code> in the source map information.</td>
        </tr>
    </tbody>
</table>

Then we call the `Compile` method with the following parameters:

 1. `content` - text content written on Sass/SCSS.
 1. `inputPath` - path to input Sass/SCSS file. Needed for generation of source map.
 1. `outputPath` (optional) - path to output CSS file. Needed for generation of source map. If path to output file is not specified, but specified a path to input file, then value of this parameter is obtained by replacing extension in the input file path by `.css` extension.
 1. `sourceMapPath` (optional) - path to source map file. If path to source map file is not specified, but specified a path to output file, then value of this parameter is obtained by replacing extension in the output file path by `.css.map` extension.

Then output result of compilation to the console.
In addition, we provide handling of the following exception types: <code title="DartSassHost.SassCompilerLoadException">SassCompilerLoadException</code>, <code title="DartSassHost.SassCompilationException">SassCompilationException</code> and <code title="DartSassHost.SassException">SassException</code>.
In the Dart Sass Host, exceptions have the following hierarchy:

  * <code title="DartSassHost.SassException">SassException</code>
    * <code title="DartSassHost.SassCompilerLoadException">SassCompilerLoadException</code>
    * <code title="DartSassHost.SassCompilationException">SassCompilationException</code>

Using of the `CompileFile` method quite a bit different from using of the `Compile` method:

```csharp
using System;
using System.IO;

using DartSassHost;
using DartSassHost.Helpers;
using JavaScriptEngineSwitcher.ChakraCore;

namespace DartSassHost.Example.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            const string basePath = "/Projects/TestSass";
            string inputFilePath = Path.Combine(basePath, "style.scss");
            string outputFilePath = Path.Combine(basePath, "style.css");
            string sourceMapFilePath = Path.Combine(basePath, "style.css.map");

            var options = new CompilationOptions { SourceMap = true };

            try
            {
                using (var sassCompiler = new SassCompiler(new ChakraCoreJsEngineFactory(), options))
                {
                    CompilationResult result = sassCompiler.CompileFile(inputFilePath, outputFilePath,
                        sourceMapFilePath, options);

                    Console.WriteLine("Compiled content:{1}{1}{0}{1}", result.CompiledContent,
                        Environment.NewLine);
                    Console.WriteLine("Source map:{1}{1}{0}{1}", result.SourceMap, Environment.NewLine);
                    Console.WriteLine("Included file paths: {0}",
                        string.Join(", ", result.IncludedFilePaths));
                }
            }
            catch (SassCompilerLoadException e)
            {
                Console.WriteLine("During loading of Sass compiler an error occurred. See details:");
                Console.WriteLine();
                Console.WriteLine(SassErrorHelpers.GenerateErrorDetails(e));
            }
            catch (SassCompilationException e)
            {
                Console.WriteLine("During compilation of SCSS code an error occurred. See details:");
                Console.WriteLine();
                Console.WriteLine(SassErrorHelpers.GenerateErrorDetails(e));
            }
            catch (SassException e)
            {
                Console.WriteLine("During working of Sass compiler an unknown error occurred. See details:");
                Console.WriteLine();
                Console.WriteLine(SassErrorHelpers.GenerateErrorDetails(e));
            }
        }
    }
}
```

In this case, the `inputPath` parameter is used instead of the `content` parameter. Moreover, value of the `inputPath` parameter now should contain the path to real file.