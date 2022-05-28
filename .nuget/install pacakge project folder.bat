@echo off
:: Syntax:  %0 Name Folder
:: Example: %0 Arebis.UnitsAmounts ..\src\libs\standard.units
set odir=%CD%
chdir %2
@echo processing projects in %2
@echo .
@echo removing package %1
@echo .
pause
dotnet remove package %1
@echo adding package %1
@echo .
pause
@echo adding package %1
@echo .
dotnet add package %1
:done
@echo restoring folder to %odir% 
chdir /d %odir% 
pause