# Units and Amounts Libraries
Libraries for implementing strongly typed units and amounts.

* [Source Code](#Source-Code)
* [License](LICENSE.md)
* [Change Log](CHANGELOG.md)
* [Facilitated By](#FacilitatedBy)
* [Repository Owner](#Repository-Owner)
* [Authors](#Authors)
* [Acknowledgments](#Acknowledgments)
* [Open Source](#Open-Source)
* [Closed Software](#Closed-software)

## Source Code[](#){name=Source-Code}
Clone the repository along with its requisite repositories to their respective relative path.

### Repositories
The repositories listed in [external repositories](ExternalReposCommits.csv) are required:
* [Units Amounts Libraries]

```
git clone git@bitbucket.org:davidhary/dn.UnitsAmounts.git
```

Clone the repositories into the following folders (parents of the .git folder):
```
%vslib%\core\UnitsAmounts
```
where %vslib% is the root folder of the .NET libraries, e.g., %my%\lib\vs 
and %my% is the root folder of the .NET solutions

#### Global Configuration Files
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

## Facilitated By[](#){name=FacilitatedBy}
* [Visual Studio]
* [Jarte RTF Editor]
* [Wix Toolset]
* [Atomineer Code Documentation]
* [EW Software Spell Checker]
* [Code Converter]
* [Funduc Search and Replace]

## Repository Owner[](#){name=Repository-Owner}

[ATE Coder]

## Authors[](#){name=Authors}

* [Rudi Breedenraedt]
* [ATE Coder]

## Acknowledgments[](#){name=Acknowledgments}

* [Rudi Breedenraedt]
* [Its all a remix]
* [Stack overflow]
* [Arebis Units Amounts]

### Open source  [](#){name=Open-Source}
Open source used by this software is described and licensed 
at the following sites:  
[Units Amounts Libraries]

### Closed software  [](#){name=Closed-software}
None

### Links
[Arebis Units Amounts]: https://www.codeproject.com/Articles/611731/Working-with-Units-and-Amounts
[Units Amounts Libraries]: https://bitbucket.org/davidhary/dn.UnitsAmounts
[Rudi Breedenraedt]: https://www.codeproject.com/Articles/611731/Working-with-Units-and-Amounts

[IDE Repository]: https://www.bitbucket.org/davidhary/vs.ide
[external repositories]: ExternalReposCommits.csv

[ATE Coder]: https://www.IntegratedScientificResources.com
[Its all a remix]: https://www.everythingisaremix.info
[John Simmons]: https://www.codeproject.com/script/Membership/View.aspx?mid=7741
[Stack overflow]: https://www.stackoveflow.com

[Visual Studio]: https://www.visualstudio.com/
[Jarte RTF Editor]: https://www.jarte.com/ 
[WiX Toolset]: https://www.wixtoolset.org/
[Atomineer Code Documentation]: https://www.atomineerutils.com/
[EW Software Spell Checker]: https://github.com/EWSoftware/VSSpellChecker/wiki/
[Code Converter]: https://github.com/icsharpcode/CodeConverter
[Funduc Search and Replace]: http://www.funduc.com/search_replace.htm

