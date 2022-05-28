@echo off
:: Syntax:  %0 Name Project
:: Example: %0 Arebis.UnitsAmounts ..\src\libs\standard.units\Arebis.StandardUnits.csproj
@echo processing project %2
@echo .
@echo removing package %1
@echo .
pause
dotnet remove %2 package %1
@echo adding package %1
@echo .
pause
dotnet add %2 package %1
:done
pause