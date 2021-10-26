/*global Sass, FileManager, CURRENT_OS_PLATFORM_NAME, Map */
var SassHelper = (function (sass, fileManager, currentOsPlatformName, undefined) {
	'use strict';

	var dshUtils,
		DshFileManagerProxy,
		DshLogger
		;

	//#region dshUtils module
	dshUtils = (function (currentOsPlatformName, undefined) {
		var exports = {},
			fileScheme = 'file://',
			pathWithDriveLetterRegEx = /^[/\\]?[a-zA-z]:[/\\]/
			;

		function mix() {
			var arg,
				argIndex,
				propertyName,
				result = {}
				;

			for (argIndex = 0; argIndex < arguments.length; argIndex++) {
				arg = arguments[argIndex];

				for (propertyName in arg) {
					if (arg.hasOwnProperty(propertyName)) {
						result[propertyName] = arg[propertyName];
					}
				}
			}

			return result;
		}

		function removeFileSchemeFromPath(path) {
			var processedPath = path;

			if (path && path.startsWith(fileScheme)) {
				processedPath = path.substring(fileScheme.length);
			}

			return processedPath;
		}

		function getCanonicalFilePath(path) {
			var canonicalPath = path.replace(/\\/g, '/');
			if (currentOsPlatformName === 'win32') {
				canonicalPath = canonicalPath.toLowerCase();
			}

			return canonicalPath;
		}

		function fixAbsolutePath(path) {
			var processedPath;

			if (!path) {
				return path;
			}

			processedPath = removeFileSchemeFromPath(path);

			if (pathWithDriveLetterRegEx.test(processedPath)
				&& (processedPath.startsWith('/') || processedPath.startsWith('\\'))) {
				processedPath = processedPath.substring(1);
			}

			if (currentOsPlatformName === 'win32') {
				processedPath = processedPath.replace(/\//g, '\\');
			}

			return processedPath;
		}

		function removeEndingParenthesesFromMemberName(memberName) {
			var processedMemberName = memberName,
				endingParentheses = '()'
				;

			if (memberName && memberName.endsWith(endingParentheses)) {
				processedMemberName = memberName.substring(0, memberName.length - endingParentheses.length);
			}

			return processedMemberName;
		}

		function mapStackFrames(frames) {
			var stackFrames = [],
				frameIndex,
				frame
				;

			if (!frames) {
				return stackFrames;
			}

			for (frameIndex = 0; frameIndex < frames.length; frameIndex++) {
				frame = frames[frameIndex];
				stackFrames.push({
					'file': fixAbsolutePath(frame.uri.path),
					'lineNumber': frame.line,
					'columnNumber': frame.column,
					'memberName': removeEndingParenthesesFromMemberName(frame.member)
				});
			}

			return stackFrames;
		}

		exports.mix = mix;
		exports.removeFileSchemeFromPath = removeFileSchemeFromPath;
		exports.getCanonicalFilePath = getCanonicalFilePath;
		exports.fixAbsolutePath = fixAbsolutePath;
		exports.mapStackFrames = mapStackFrames;

		return exports;
	})(currentOsPlatformName);
	//#endregion

	//#region DshFileManagerProxy class
	DshFileManagerProxy = (function (dshUtils, undefined) {
		var urlFunctionBeginPart = 'url(',
			urlFunctionEndPart = ')'
			;

		function unquote(quotedValue) {
			var value = quotedValue,
				quoteChar = '',
				firstChar,
				lastChar
				;

			if (quotedValue && quotedValue.length >= 2) {
				firstChar = quotedValue.charAt(0);
				lastChar = quotedValue.charAt(quotedValue.length - 1);

				if (firstChar === lastChar) {
					value = quotedValue.substring(1, value.length - 1);
					quoteChar = firstChar;
				}
			}

			return { value: value, quoteChar: quoteChar };
		}

		function quote(value, quoteChar) {
			var quotedValue = quoteChar + value + quoteChar;

			return quotedValue;
		}

		function isUrlFunction(value) {
			return value && value.length > 6 && value.startsWith(urlFunctionBeginPart)
				&& value.endsWith(urlFunctionEndPart);
		}

		function extractPathFromUrlFunction(value) {
			return value.substring(urlFunctionBeginPart.length, value.length - urlFunctionEndPart.length);
		}

		function wrapPathInUrlFunction(value) {
			return urlFunctionBeginPart + value + urlFunctionEndPart;
		}


		function DshFileManagerProxy(fileManager) {
			this._fileManager = fileManager;
			this._currentDirectory = fileManager.GetCurrentDirectory();
			this._fileExistenceCache = new Map();
			this._fileContentCache = new Map();

			this.supportsVirtualPaths = fileManager.SupportsVirtualPaths;
		}


		DshFileManagerProxy.prototype.getCurrentDirectory = function () {
			return this._currentDirectory;
		};

		DshFileManagerProxy.prototype.convertPathToAbsolute = function (path) {
			var processedPath = path;

			if (path && this.supportsVirtualPaths && this._fileManager.IsAppRelativeVirtualPath(path)) {
				processedPath = this._fileManager.ToAbsoluteVirtualPath(path);
			}

			return processedPath;
		};

		DshFileManagerProxy.prototype.convertPathToAbsoluteInQuotedValue = function (quotedValue) {
			var processedQuotedValue = quotedValue,
				quotedResult,
				path,
				quoteChar
				;

			if (this.supportsVirtualPaths) {
				quotedResult = unquote(quotedValue);
				path = quotedResult.value;
				quoteChar = quotedResult.quoteChar;

				if (path && this._fileManager.IsAppRelativeVirtualPath(path)) {
					path = this._fileManager.ToAbsoluteVirtualPath(path);
					processedQuotedValue = quote(path, quoteChar);
				}
			}

			return processedQuotedValue;
		};

		DshFileManagerProxy.prototype.convertPathToAbsoluteInUrlFunction = function (urlfunction) {
			var path,
				processedUrlFunction = urlfunction
				;

			if (isUrlFunction(urlfunction) && this.supportsVirtualPaths) {
				path = extractPathFromUrlFunction(urlfunction);
				if (path && this._fileManager.IsAppRelativeVirtualPath(path)) {
					path = this._fileManager.ToAbsoluteVirtualPath(path);
					processedUrlFunction = wrapPathInUrlFunction(path);
				}
			}

			return processedUrlFunction;
		};

		DshFileManagerProxy.prototype.fileExists = function (path) {
			var processedPath,
				cacheItemName,
				result
				;

			processedPath = dshUtils.removeFileSchemeFromPath(path);
			cacheItemName = dshUtils.getCanonicalFilePath(processedPath);

			if (this._fileExistenceCache.has(cacheItemName)) {
				result = this._fileExistenceCache.get(cacheItemName);
			}
			else {
				result = this._fileManager.FileExists(processedPath);
				this._fileExistenceCache.set(cacheItemName, result);
			}

			return result;
		};

		DshFileManagerProxy.prototype.readFile = function (path) {
			var processedPath,
				cacheItemName,
				content
				;

			processedPath = dshUtils.removeFileSchemeFromPath(path);
			cacheItemName = dshUtils.getCanonicalFilePath(processedPath);

			if (this._fileContentCache.has(cacheItemName)) {
				content = this._fileContentCache.get(cacheItemName);
			}
			else {
				content = this._fileManager.ReadFile(processedPath);
				this._fileContentCache.set(cacheItemName, content);
			}

			return content;
		};

		DshFileManagerProxy.prototype.dispose = function () {
			this._fileManager = null;

			this._fileExistenceCache.clear();
			this._fileExistenceCache = null;

			this._fileContentCache.clear();
			this._fileContentCache = null;
		};

		return DshFileManagerProxy;
	})(dshUtils);
	//#endregion

	//#region DshLogger class
	DshLogger = (function (sass, currentOsPlatformName, dshUtils, undefined) {
		function DshLogger() {
			this._warnings = [];
			this._sources = {};
		}


		DshLogger.prototype.warn$4$deprecation$span$trace = function (_, message, deprecation, span, trace) {
			var warning,
				file,
				fileLocation,
				fileSpan,
				filePath,
				stackFrames = [],
				firstStackFrame
				;

			warning = {
				'message': message,
				'deprecation': deprecation
			};

			if (span && span.file) {
				file = span.file;
				fileLocation = new sass.FileLocation(file, span._file$_start);
				fileSpan = new sass.FileSpan(file, 0, file._decodedChars.length);
				filePath = dshUtils.fixAbsolutePath(fileLocation.get$sourceUrl().path);

				warning.file = filePath;
				warning.lineNumber = fileLocation.get$line() + 1;
				warning.columnNumber = fileLocation.get$column() + 1;

				if (!this._sources.hasOwnProperty(filePath)) {
					this._sources[filePath] = fileSpan.get$text(file._decodedChars);
				}
			}

			if (trace && trace.frames) {
				stackFrames = dshUtils.mapStackFrames(trace.frames);

				if (stackFrames.length > 0) {
					if (!span) {
						firstStackFrame = stackFrames[0];

						warning.file = firstStackFrame.file;
						warning.lineNumber = firstStackFrame.lineNumber;
						warning.columnNumber = firstStackFrame.columnNumber;
					}

					warning.stackFrames = stackFrames;
				}
			}

			this._warnings.push(warning);
		};

		DshLogger.prototype.warn$1 = function ($receiver, message) {
			return this.warn$4$deprecation$span$trace($receiver, message, false, null, null);
		};

		DshLogger.prototype.warn$2$span = function ($receiver, message, span) {
			return this.warn$4$deprecation$span$trace($receiver, message, false, span, null);
		};

		DshLogger.prototype.warn$2$deprecation = function ($receiver, message, deprecation) {
			return this.warn$4$deprecation$span$trace($receiver, message, deprecation, null, null);
		};

		DshLogger.prototype.warn$3$deprecation$span = function ($receiver, message, deprecation, span) {
			return this.warn$4$deprecation$span$trace($receiver, message, deprecation, span, null);
		};

		DshLogger.prototype.warn$2$trace = function ($receiver, message, trace) {
			return this.warn$4$deprecation$span$trace($receiver, message, false, null, trace);
		};

		DshLogger.prototype.debug$2 = function (_, message, span) {
			// Do nothing
		};

		DshLogger.prototype.getWarnings = function () {
			return this._warnings;
		};

		DshLogger.prototype.getSources = function () {
			return this._sources;
		};

		DshLogger.prototype.dispose = function () {
			this._warnings = null;
			this._sources = null;
		};

		return DshLogger;
	})(sass, currentOsPlatformName, dshUtils);
	//#endregion

	//#region SassHelper class
	SassHelper = (function (sass, fileManager, DshFileManagerProxy, DshLogger, dshUtils, undefined) {
		var versionRegEx = /^dart-sass\t(\d+(?:\.\d+){2,3})\t/;

		function fixIncludedPaths(paths, currentDirectory) {
			var fixedPaths,
				path,
				pathIndex,
				canonicalCurrentDirectory
				;

			if (paths.length == 0) {
				return paths;
			}

			canonicalCurrentDirectory = dshUtils.getCanonicalFilePath(currentDirectory);
			fixedPaths = [];

			for (pathIndex = 0; pathIndex < paths.length; pathIndex++) {
				path = dshUtils.removeFileSchemeFromPath(paths[pathIndex]);
				if (dshUtils.getCanonicalFilePath(path) !== canonicalCurrentDirectory) {
					fixedPaths.push(path);
				}
			}

			return fixedPaths;
		}

		function innerCompile(compilationOptions) {
			var result,
				compilationResult,
				compiledContent = '',
				sourceMap = '',
				includedFilePaths = [],
				stackFrames,
				errors = [],
				error,
				warnings = [],
				warningSources = {},
				fileManagerProxy,
				logger
				;

			fileManagerProxy = new DshFileManagerProxy(fileManager);
			logger = new DshLogger();

			sass.dsh.fileManagerProxy = fileManagerProxy;
			compilationOptions.logger = logger;

			try
			{
				compilationResult = sass.renderSync(compilationOptions);
				compiledContent = compilationResult.css || '';
				sourceMap = compilationResult.map ? compilationResult.map : '';
				includedFilePaths = fixIncludedPaths(compilationResult.stats.includedFiles,
					fileManagerProxy.getCurrentDirectory());
				warnings = logger.getWarnings();
				warningSources = logger.getSources();
			}
			catch (e)
			{
				if (e.formatted) {
					stackFrames = dshUtils.mapStackFrames(e.stackFrames);
					error = {
						'message': e.message,
						'description': e.description || e.message,
						'type': e.name || '',
						'file': e.file ? dshUtils.removeFileSchemeFromPath(e.file) : '',
						'lineNumber': e.line || 0,
						'columnNumber': e.column || 0,
						'source': e.source || '',
						'status': e.status
					};
					if (stackFrames && stackFrames.length > 0) {
						error.stackFrames = stackFrames;
					}

					errors.push(error);
				}
				else {
					throw (e);
				}
			}
			finally {
				fileManagerProxy.dispose();
				sass.dsh.fileManagerProxy = null;

				logger.dispose();
				compilationOptions.logger = null;
			}

			result = {
				compiledContent: compiledContent,
				sourceMap: sourceMap,
				includedFilePaths: includedFilePaths
			};
			if (errors.length > 0) {
				result.errors = errors;
			}
			if (warnings.length > 0) {
				result.warningSources = warningSources;
				result.warnings = warnings;
			}

			return JSON.stringify(result);
		}


		function SassHelper(options) {
			this._options = options || {};
		}


		SassHelper.getVersion = function() {
			var version = '0.0.0',
				versionMatch
				;

			versionMatch = sass.info.match(versionRegEx);
			if (versionMatch) {
				version = versionMatch[1];
			}

			return version;
		};

		SassHelper.prototype.compile = function (content, indentedSyntax, inputPath, outputPath, sourceMapPath, options) {
			var currentOptions,
				compilationOptions,
				optionsFromParameters
				;

			currentOptions = options || this._options;
			optionsFromParameters = {
				data: content,
				indentedSyntax: indentedSyntax,
				file: inputPath,
				outFile: outputPath
			};
			if (currentOptions.sourceMap) {
				optionsFromParameters.sourceMap = sourceMapPath ? sourceMapPath : true;
			}
			compilationOptions = dshUtils.mix(currentOptions, optionsFromParameters);

			return innerCompile(compilationOptions);
		};

		SassHelper.prototype.compileFile = function (inputPath, outputPath, sourceMapPath, options) {
			var currentOptions,
				compilationOptions,
				optionsFromParameters
				;

			currentOptions = options || this._options;
			optionsFromParameters = {
				file: inputPath,
				outFile: outputPath
			};
			if (currentOptions.sourceMap) {
				optionsFromParameters.sourceMap = sourceMapPath ? sourceMapPath : true;
			}
			compilationOptions = dshUtils.mix(currentOptions, optionsFromParameters);

			return innerCompile(compilationOptions);
		};

		return SassHelper;
	})(sass, fileManager, DshFileManagerProxy, DshLogger, dshUtils);
	//#endregion

	return SassHelper;
}(Sass, FileManager, CURRENT_OS_PLATFORM_NAME));