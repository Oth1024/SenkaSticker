@echo off
cd ..
dotnet clean
dotnet publish -c Release -r win-x64 -o ".\\Publish\\SenkaSticker"