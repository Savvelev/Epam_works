dotnet build
dotnet publish -c Release -r win10-x64 -o publishWin64 --self-contained true
dotnet publish -c Release -r linux-x64 -o publishLinux --self-contained true
pause >nul