### Cloning

<a name="Source-Code"></a>
#### Source Code
Clone the repository along with its requisite repositories to their respective relative path.

##### Repositories
The repositories listed in [external repositories](ExternalReposCommits.csv) are required:
* [Units Amounts Libraries - Bitbucket]

```
git clone git@bitbucket.org:davidhary/dn.UnitsAmounts.git
```

or  
* [Units Amounts Libraries - GitHub]

```
git clone https://github.com/AteCoder/units-amounts.git
```

Clone the repositories into the following folders (parents of the .git folder):
```
%vslib%\core\UnitsAmounts
```
where %vslib% is the root folder of the .NET libraries, e.g., %my%\lib\vs 
and %my% is the root folder of the .NET solutions

##### Global Configuration Files
ISR libraries use a global editor configuration file and a global test run settings file. 
These files can be found in the [IDE Repository].

Restoring Editor Configuration:
```
xcopy /Y %my%\.editorconfig %my%\.editorconfig.bak
xcopy /Y %vslib%\core\ide\code\.editorconfig %my%\.editorconfig
```

Restoring Run Settings:
```
xcopy /Y %userprofile%\.runsettings %userprofile%\.runsettings.bak
xcopy /Y %vslib%\core\ide\code\.runsettings %userprofile%\.runsettings
```
where %userprofile% is the root user folder.

[Units Amounts Libraries - Bitbucket]: https://bitbucket.org/davidhary/dn.UnitsAmounts
[Units Amounts Libraries - GitHub]: https://github.com/atecoder/units-amounts
[Rudi Breedenraedt]: https://www.codeproject.com/Articles/611731/Working-with-Units-and-Amounts

[IDE Repository]: https://www.bitbucket.org/davidhary/vs.ide
[external repositories]: ExternalReposCommits.csv

