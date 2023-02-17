SET RunSourceDir=.\MLConsole\bin\Debug\net7.0\
dotnet clean
msbuild .\MLConsole.sln
dotnet  %RunSourceDir%\MLConsole.dll