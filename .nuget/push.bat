@echo off
dotnet nuget push ..\.nuget\artifacts\*.nupkg --skip-duplicate --source https://api.nuget.org/v3/index.json
pause