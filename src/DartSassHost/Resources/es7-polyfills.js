(function () {
	'use strict';

	var stringPrototype = String.prototype;

	//#region String methods

	// String.prototype.trimEnd
	if (!stringPrototype.hasOwnProperty('trimEnd')) {
		if (stringPrototype.hasOwnProperty('trimRight')) {
			stringPrototype.trimEnd = stringPrototype.trimRight;
		}
	}

	// String.prototype.trimStart
	if (!stringPrototype.hasOwnProperty('trimStart')) {
		if (stringPrototype.hasOwnProperty('trimLeft')) {
			stringPrototype.trimStart = stringPrototype.trimLeft;
		}
	}

	//#endregion
}());