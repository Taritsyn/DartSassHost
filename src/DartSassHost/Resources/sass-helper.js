/*global Sass, FileManager, CURRENT_OS_PLATFORM_NAME  */
var SassHelper = (function (sass, fileManager, currentOsPlatformName, undefined) {
	'use strict';

	var versionRegEx = /^dart-sass\t(\d+(?:\.\d+){2,3})\t/,
		DshLogger
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

	function fixIncludedFilePaths(paths) {
		var fixedPaths,
			currentDirectory,
			path,
			pathIndex
			;

		if (paths.length == 0) {
			return paths;
		}

		currentDirectory = getCanonicalFilePath(fileManager.GetCurrentDirectory());
		fixedPaths = [];

		for (pathIndex = 0; pathIndex < paths.length; pathIndex++) {
			path = sass.removeFileSchemeFromPath(paths[pathIndex]);
			if (getCanonicalFilePath(path) !== currentDirectory) {
				fixedPaths.push(path);
			}
		}

		return fixedPaths;
	}

	function getCanonicalFilePath(path) {
		return path.replace(/\\/g, '/');
	}

	//#region DshLogger class
	DshLogger = (function () {
		var pathWithDriveLetterRegEx = /^[/\\]?[a-zA-z]:[/\\]/;

		function fixAbsolutePath(path) {
			var processedPath = path;

			if (pathWithDriveLetterRegEx.test(path)
				&& (path.startsWith('/') || path.startsWith('\\'))) {
				processedPath = path.substring(1);
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

			if (memberName.endsWith(endingParentheses)) {
				processedMemberName = memberName.substring(0, memberName.length - endingParentheses.length);
			}

			return processedMemberName;
		}


		function DshLogger() {
			this._warnings = [];
		}


		DshLogger.prototype.warn$4$deprecation$span$trace = function (_, message, deprecation, span, trace) {
			var warning,
				file,
				fileLocation,
				fileSpan,
				frames,
				frameIndex,
				frame,
				firstFrame,
				stackFrames = []
				;

			warning = {
				'message': message,
				'deprecation': deprecation
			};

			if (span && span.file) {
				file = span.file;
				fileLocation = new sass.FileLocation(file, span._file$_start);
				fileSpan = new sass.FileSpan(file, 0, file._decodedChars.length);

				warning.file = fixAbsolutePath(fileLocation.get$sourceUrl().path);
				warning.lineNumber = fileLocation.get$line() + 1;
				warning.columnNumber = fileLocation.get$column() + 1;
				warning.source = fileSpan.get$text(file._decodedChars);
			}

			if (trace && trace.frames) {
				frames = trace.frames;

				if (!span) {
					firstFrame = frames[0];

					warning.file = fixAbsolutePath(firstFrame.uri.path);
					warning.lineNumber = firstFrame.line;
					warning.columnNumber = firstFrame.column;
					warning.source = '';
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

				if (stackFrames.length > 0) {
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

		DshLogger.prototype.dispose = function () {
			this._warnings = null;
		};

		return DshLogger;
	})();
	//#endregion

	//#region SassHelper class
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

	function innerCompile(compilationOptions) {
		var result,
			compilationResult,
			compiledContent = '',
			sourceMap = '',
			includedFilePaths = [],
			errors = [],
			warnings = [],
			logger
			;

		logger = new DshLogger();
		compilationOptions.dshLogger = logger;

		try
		{
			compilationResult = sass.renderSync(compilationOptions);
			compiledContent = compilationResult.css || '';
			sourceMap = compilationResult.map ? compilationResult.map.toString() : '';
			includedFilePaths = fixIncludedFilePaths(compilationResult.stats.includedFiles);
			warnings = logger.getWarnings();
		}
		catch (e)
		{
			if (e.formatted) {
				errors.push({
					'message': e.message,
					'description': e.description || e.message,
					'type': e.name || '',
					'file': e.file ? sass.removeFileSchemeFromPath(e.file) : '',
					'lineNumber': e.line || 0,
					'columnNumber': e.column || 0,
					'source': e.source || '',
					'status': e.status
				});
			}
			else {
				throw (e);
			}
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
			result.warnings = warnings;
		}

		compilationOptions.dshLogger = null;
		logger.dispose();

		return JSON.stringify(result);
	}

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
		compilationOptions = mix(currentOptions, optionsFromParameters);

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
		compilationOptions = mix(currentOptions, optionsFromParameters);

		return innerCompile(compilationOptions);
	};
	//#endregion

	return SassHelper;
}(Sass, FileManager, CURRENT_OS_PLATFORM_NAME));