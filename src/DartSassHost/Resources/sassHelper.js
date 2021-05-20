/*global Sass, FileManager  */
var SassHelper = (function (sass, fileManager, undefined) {
	'use strict';

	var versionRegEx = /^dart-sass\t(\d+(?:\.\d+){2,3})\t/;

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
			errors = []
			;

		try
		{
			compilationResult = sass.renderSync(compilationOptions);
			compiledContent = compilationResult.css || '';
			sourceMap = compilationResult.map ? compilationResult.map.toString() : '';
			includedFilePaths = fixIncludedFilePaths(compilationResult.stats.includedFiles);
		}
		catch (e)
		{
			if (e.formatted) {
				errors.push({
					'message': e.message,
					'description': e.description || '',
					'type': e.name || '',
					'file': e.file || '',
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
}(Sass, FileManager));