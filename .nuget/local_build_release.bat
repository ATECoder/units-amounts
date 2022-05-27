@echo off
rem pack restores unless --norestore flag is used 
rem dotnet restore ..\src\libs\units.amounts\Arebis.UnitsAmounts.csproj
dotnet pack ..\src\libs\units.amounts\Arebis.UnitsAmounts.csproj -c Release -o ..\.nuget\artifacts\release
pause