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

<a name="Source-Code"></a>
## Source Code
Clone the repository along with its requisite repositories to their respective relative path.

### Repositories
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

#### Packages
Presently, packages are consumed from a _source feed_ residing in a local folder, e.g., _%my%\nuget\packages_. 
The packages are 'packed', using the _Pack_ command from each packable project,
into the _%my%\nuget_ folder as specified in the project file and then
added to the source feed. Alternatively, the packages can be downloaded from the 
private [MEGA packages folder].

<a name="FacilitatedBy"></a>
## Facilitated By
* [Visual Studio]
* [Jarte RTF Editor]
* [Wix Toolset]
* [Atomineer Code Documentation]
* [EW Software Spell Checker]
* [Code Converter]
* [Funduc Search and Replace]

<a name="Repository-Owner"></a>
## Repository Owner

[ATE Coder]

<a name="Authors"></a>
## Authors

* [Rudi Breedenraedt]
* [ATE Coder]

<a name="Acknowledgments"></a>
## Acknowledgments

* [Rudi Breedenraedt]
* [Its all a remix]
* [Stack overflow]
* [Arebis Units Amounts]

<a name="Open-Source"></a>
### Open source
Open source used by this software is described and licensed 
at the following sites:  
[Units Amounts Libraries - Bitbucket]
[Units Amounts Libraries - GitHub]

<a name="Closed-software"></a>
### Closed software
None

[MEGA packages folder]: https://mega.nz/folder/KEcVxC5a#GYnmvMcwP4yI4tsocD31Pg
[Arebis Units Amounts]: https://www.codeproject.com/Articles/611731/Working-with-Units-and-Amounts
[Units Amounts Libraries - Bitbucket]: https://bitbucket.org/davidhary/dn.UnitsAmounts
[Units Amounts Libraries - GitHub]: https://github.com/atecoder/units-amounts
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

