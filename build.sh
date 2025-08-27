#!/bin/bash
# Script to build the BCFileDecryptor project

echo "Building BCFileDecryptor..."
dotnet build --configuration Release

echo "Running tests..."
dotnet test --no-build --configuration Release

read -p "Do you want to create standalone executables? (y/n) " -n 1 -r
echo
if [[ $REPLY =~ ^[Yy]$ ]]
then
    echo "Creating standalone executables..."
    
    echo "Building for Windows..."
    dotnet publish BCFileDecryptor/BCFileDecryptor.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true -o publish/win-x64
    
    echo "Building for Linux..."
    dotnet publish BCFileDecryptor/BCFileDecryptor.csproj -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true -o publish/linux-x64
    
    echo "Building for macOS..."
    dotnet publish BCFileDecryptor/BCFileDecryptor.csproj -c Release -r osx-x64 --self-contained true -p:PublishSingleFile=true -p:PublishTrimmed=true -o publish/osx-x64
    
    echo "Done! Executables are available in the 'publish' directory."
fi
