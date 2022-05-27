@echo off
del ..\.nuget\artifacts\Arebis.UnitsAmounts*.*
dotnet pack ..\src\libs\units.amounts\Arebis.UnitsAmounts.csproj -c Release -o ..\.nuget\artifacts
pause