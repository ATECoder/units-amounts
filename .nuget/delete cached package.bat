@echo off
:: Syntax:  %0 Name Version
:: Example: %0 Arebis.UnitsAmounts 2.0.8109
if not exist "..\.nuget\artifacts\%1.%2.nupkg" (
	@echo not found: ..\.nuget\artifacts\%1.%2.nupkg
	@echo the ..\.nuget\artifacts\%1.%2.nupkg must be packed before deleting 
	@echo .
	goto done
) else (
	@echo processing %1.%2.nupkg
	@echo .
)
if exist "C:\Users\David\.nuget\packages\%1\%2" (
	@echo Delete?  %1.%2  from C:\Users\David\.nuget\packages
	@echo .
	pause 
	nuget delete %1 %2 -source C:\Users\David\.nuget\packages
	@echo.
) else (
	@echo not found:   C:\Users\David\.nuget\packages\%1\%2
)
:done
pause

