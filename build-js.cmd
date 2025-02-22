@echo off
setlocal

cd ./src/DartSassHost/

::--------------------------------------------------------------------------------
:: Build
::--------------------------------------------------------------------------------

echo Installing Node.js packages ...
echo.
call npm install
if errorlevel 1 goto error
echo.

echo Minifying JS files ...
echo.
call npm run -s minify-js
if errorlevel 1 goto error
echo.

::--------------------------------------------------------------------------------
:: Exit
::--------------------------------------------------------------------------------

echo Succeeded!
goto exit

:error
echo *** Error: The previous step failed!

:exit
cd ../../
endlocal