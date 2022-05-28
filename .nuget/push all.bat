@echo off
@echo pushing all packages
@echo .
dotnet nuget push ..\.nuget\artifacts\*.nupkg --skip-duplicate --source https://api.nuget.org/v3/index.json
pause