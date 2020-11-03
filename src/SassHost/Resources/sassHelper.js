/*global Sass, FileManager  */
var SassHelper = (function (sass, fileManager, undefined) {
	'use strict';

	var exports = {},
		versionRegEx = /^dart-sass\t(\d+(?:\.\d+){2,3})\t/,
		defaultOptions = {}
		;

	function mix(destination, source) {
		var propertyName;

		destination = destination || {};

		for (propertyName in source) {
			if (source.hasOwnProperty(propertyName)) {
				destination[propertyName] = source[propertyName];
			}
		}

		return destination;
	}

	function formatString() {
		var pattern = arguments[0],
			result = pattern,
			regex,
			argument,
			argumentIndex,
			argumentCount = arguments.length
			;

		for (argumentIndex = 0; argumentIndex < argumentCount - 1; argumentIndex++) {
			regex = new RegExp('\\{' + argumentIndex + '\\}', 'gm');
			argument = arguments[argumentIndex + 1];

			result = result.replace(regex, argument);
		}

		return result;
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
			includedFilePaths = compilationResult.stats.includedFiles;
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

	SassHelper.prototype.compile = function (content, indentedSyntax, inputPath, outputPath, sourceMapPath) {
		var compilationOptions,
			optionsFromParameters
			;

		optionsFromParameters = {
			data: content,
			indentedSyntax: indentedSyntax,
			file: inputPath,
			outFile: outputPath
		};
		if (this._options.sourceMap) {
			optionsFromParameters.sourceMap = sourceMapPath ? sourceMapPath : true;
		}
		compilationOptions = mix(mix(optionsFromParameters, defaultOptions), this._options);

		return innerCompile(compilationOptions);
	};

	SassHelper.prototype.compileFile = function (inputPath, outputPath, sourceMapPath) {
		var compilationOptions,
			optionsFromParameters
			;

		optionsFromParameters = {
			file: inputPath,
			outFile: outputPath
		};
		if (this._options.sourceMap) {
			optionsFromParameters.sourceMap = sourceMapPath ? sourceMapPath : true;
		}
		compilationOptions = mix(mix(optionsFromParameters, defaultOptions), this._options);

		return innerCompile(compilationOptions);
	};
	//#endregion

	return SassHelper;
}(Sass, FileManager));