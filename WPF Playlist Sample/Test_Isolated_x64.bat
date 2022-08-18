@setlocal enableextensions
@cd /d "%~dp0"

rem rem Delete old
del ".\bin\x64\Debug\WPF Playlist Sample.exe.manifest" /f /q
rd /S /Q  .\bin\x64\Debug\DLLs

xcopy ..\..\..\Bin\x64 .\bin\x64\Debug\DLLs /c /i /y

xcopy ..\..\..\Bin\x64\BlueFish .\bin\x64\Debug\DLLs  /c /i /y /e
xcopy ..\..\..\Bin\x64\DecoderLib .\bin\x64\Debug\DLLs  /c /i /y /e
xcopy ..\..\..\Bin\x64\DekTec .\bin\x64\Debug\DLLs  /c /i /y /e
xcopy ..\..\..\Bin\x64\DELTACAST .\bin\x64\Debug\DLLs  /c /i /y /e
xcopy ..\..\..\Bin\x64\EncoderLib .\bin\x64\Debug\DLLs  /c /i /y /e
xcopy ..\..\..\Bin\x64\Magewell .\bin\x64\Debug\DLLs  /c /i /y /e
xcopy ..\..\..\Bin\x64\MFOverlayHTML .\bin\x64\Debug\DLLs  /c /i /y /e
xcopy ..\..\..\Bin\x64\NDI .\bin\x64\Debug\DLLs  /c /i /y /e
xcopy ..\..\..\Bin\x64\StreamLabs .\bin\x64\Debug\DLLs  /c /i /y /e

copy "..\..\..\..\Medialooks Common\x64\Medialooks.Runtime.x64.dll" .\bin\x64\Debug\DLLs\Medialooks.Runtime.x64.dll /y
copy ..\..\..\Bin\Manifests\x64\MServer.exe.manifest .\bin\x64\Debug\DLLs\MServer.exe.manifest /y
copy ..\..\..\Bin\Manifests\x64\[YourApplicationExeFile].exe.manifest ".\bin\x64\Debug\WPF Playlist Sample.exe.manifest" /y 

rem Run
cd .\bin\x64\Debug
"WPF Playlist Sample.exe"

cd ..\..\..\ 
