(function () {
	'use strict';

	var WHITESPACES = '\u0009\u000A\u000B\u000C\u000D\u0020\u00A0\u1680\u2000\u2001\u2002\u2003' +
		'\u2004\u2005\u2006\u2007\u2008\u2009\u200A\u202F\u205F\u3000\u2028\u2029\uFEFF',
		stringPrototype = String.prototype
		;

	//#region String methods

	// String.prototype.trimEnd
	if (!stringPrototype.hasOwnProperty('trimEnd')) {
		if (stringPrototype.hasOwnProperty('trimRight')) {
			stringPrototype.trimEnd = stringPrototype.trimRight;
		}
		else {
			stringPrototype.trimEnd = (function (re) {
				return function () {
					return this.replace(re, '');
				};
			} (new RegExp('[' + WHITESPACES + ']+$')));
		}
	}

	// String.prototype.trimStart
	if (!stringPrototype.hasOwnProperty('trimStart')) {
		if (stringPrototype.hasOwnProperty('trimLeft')) {
			stringPrototype.trimStart = stringPrototype.trimLeft;
		}
		else {
			stringPrototype.trimStart = (function (re) {
				return function () {
					return this.replace(re, '');
				};
			} (new RegExp('^[' + WHITESPACES + ']+')));
		}
	}

	//#endregion
}());