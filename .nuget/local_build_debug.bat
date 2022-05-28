@echo off
@echo packing units amounts ...
@echo .
rem pack restores unless --norestore flag is used 
rem dotnet restore ..\src\libs\units.amounts\Arebis.UnitsAmounts.csproj
dotnet pack ..\src\libs\units.amounts\Arebis.UnitsAmounts.csproj -c Debug -o ..\.nuget\artifacts\debug
pause