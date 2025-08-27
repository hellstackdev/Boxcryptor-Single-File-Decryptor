@echo off
echo Building BCFileDecryptor...
dotnet build --configuration Release

echo Running tests...
dotnet test --no-build --configuration Release

echo:
set /p answer="Do you want to create standalone executables? (y/n) "
if /i "%answer%"=="y" (
    echo Creating standalone executables...
    
    echo Building for Windows...
    dotnet publish BCFileDecryptor\BCFileDecryptor.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true -o publish\win-x64
    
    echo Building for Linux...
    dotnet publish BCFileDecryptor\BCFileDecryptor.csproj -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true -o publish\linux-x64
    
    echo Building for macOS...
    dotnet publish BCFileDecryptor\BCFileDecryptor.csproj -c Release -r osx-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true -o publish\osx-x64
    
    echo Done! Executables are available in the 'publish' directory.
)

pause
