#!/usr/bin/env bash

function handle_error()
{
    _RET=${PIPESTATUS[0]}
    if [[ $_RET != 0 ]]; then
        echo "*** Error: The previous step failed!"

        cd ../../
        exit $_RET
    fi
}

cd ./src/DartSassHost/

echo "Installing Node.js packages ..."
echo ""
npm install
handle_error
echo ""

echo "Transpiling ES6 files ..."
echo ""
npm run -s transpile-es6
handle_error
echo ""

echo "Minifying JS files ..."
echo ""
npm run -s minify-js
handle_error
echo ""

echo "Succeeded!"

cd ../../
exit $_RET