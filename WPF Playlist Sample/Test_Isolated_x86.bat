@setlocal enableextensions
@cd /d "%~dp0"

rem Delete old
del ".\bin\x86\Debug\WPF Playlist Sample.exe.manifest" /f /q
rd /S /Q  .\bin\x86\Debug\DLLs

xcopy ..\..\..\Bin\x86 .\bin\x86\Debug\DLLs /c /i /y

xcopy ..\..\..\Bin\x86\BlueFish .\bin\x86\Debug\DLLs  /c /i /y /e
xcopy ..\..\..\Bin\x86\DecoderLib .\bin\x86\Debug\DLLs  /c /i /y /e
xcopy ..\..\..\Bin\x86\DekTec .\bin\x86\Debug\DLLs  /c /i /y /e
xcopy ..\..\..\Bin\x86\DELTACAST .\bin\x86\Debug\DLLs  /c /i /y /e
xcopy ..\..\..\Bin\x86\EncoderLib .\bin\x86\Debug\DLLs  /c /i /y /e
xcopy ..\..\..\Bin\x86\Magewell .\bin\x86\Debug\DLLs  /c /i /y /e
xcopy ..\..\..\Bin\x86\MFOverlayHTML .\bin\x86\Debug\DLLs  /c /i /y /e
xcopy ..\..\..\Bin\x86\NDI .\bin\x86\Debug\DLLs  /c /i /y /e
xcopy ..\..\..\Bin\x86\StreamLabs .\bin\x86\Debug\DLLs  /c /i /y /e

copy "..\..\..\..\Medialooks Common\x86\Medialooks.Runtime.x86.dll" .\bin\x86\Debug\DLLs\Medialooks.Runtime.x86.dll /y
copy ..\..\..\Bin\Manifests\x86\MServer.exe.manifest .\bin\x86\Debug\DLLs\MServer.exe.manifest /y
copy ..\..\..\Bin\Manifests\x86\[YourApplicationExeFile].exe.manifest ".\bin\x86\Debug\WPF Playlist Sample.exe.manifest" /y 

rem Run
cd .\bin\x86\Debug
"WPF Playlist Sample.exe"

cd ..\..\..\ 
