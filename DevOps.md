# Developer Operations ([DevOps]) for the Units and Amounts Solution

* [General Notes](#General-Notes)
* [Continuous Integration Bootstrap](#Continuous-Integration-Bootstrap)
* [Continuous Integration Routine](#Continuous-Integration-Routine)
* [Continuous Integration and Deployment](#Continuous-Integration-Deployment)
* [CI/CD Passes](#CI-CD-Passes)

<a name="General-Notes"></a>
## General Notes

<a name="Solution_Repo"></a>
### Solution Repo
A Solution Repository is a typical repository, which consists of a set of projects. 

<a name="Solution"></a>
### Solution

* In the .NET terms a Solution consists of a set of projects. 
* The [Solution Repo](#Solution-Repo) consists of a single Solution.
* A [Solution](#Solution) typically consists of:
	* Dependent Projects: these are projects that reference other projects in the solution;
	* Independent Projects: these are projects that reference no other projects from the solution;
	* Test Projects: these  project are used for unit testing. 
		* Test Projects use project references to the projects under test;
		* Duplicate Test Projects using Package references could be used to 
		validate published packages. Upon failures of these tests, the package can be 
		unlisted ([unlisting a package]);
	* Application Projects: Applications such as console or windows forms projects.
	* Free Projects: these projects are not referenced by other projects in the solution. 
	Test projects and Application Projects are Free Projects.

<a name="Continuous-Integration-Bootstrap"></a>
## Continuous Integration Bootstrap
	
Because the inter-dependency of projects in a [Solution Repo](#Solution-Repo), 
the continuous integration (CI) process for this type of reposition is
bootstrapped manually when first implemented as follows:
* Packages are published for all the Independent Projects;
* References are added to packages that are used in the Dependent Projects other than the Test Projects;
* Packages are then published for the modified dependent projects;
* This is repeated until all projects contain the required package references.

<a name="Continuous-Integration-Routine"></a>
## Continuous Integration Routine

* Once bootstrapped, a Continuous Integration script can be written and triggered by
reposition actions. However, a change in the local repository must take place 
if a new package, which is used by a Dependent Project, is created and needs to be 
updated in one or more projects.
* Consequently, the CI process can be automated for solutions consisting 
only of Independent Projects or if changes occur only in projects that are not
referenced by other projects in the solution.
* CI process that include duplicate Test Projects for testing published packages
require manual updates of the package references in a local repo and, therefore, cannot be
full automated.

<a name="Continuous-Integration-Deployment"></a>
## Continuous Integration and Deployment

The Continuous Integration and Deployment (CI/CD) Process consists of the following steps:
• The source code is modified and unit tests locally;
* The modified code is committed to the development trunk upon successful unit tests;
* The version of the modified project is incremented;
	* The CI script ignores duplicate packages (packages having the same version suffix);
	* This allows for committing non-coding, such as documentation, changes;
* Upon completion, the development trunk is merged onto the main trunk;
* The main trunk is pushed to the remote repository;
* This triggers the continuous integration actions at the remote repo;
* The actions affect all the solution projects;
* If the CI actions publish a new package then:
	* Update package references in the dependent projects;
	* Increment the version of the modified projects;
	* Run unit tests and update the development trunk;
	* Merge the changes to the main trunk;
	* Commit the main trunk.

<a name="CI-CD-Passes"></a>
## CI/CD Passes

The CI/CD actions thus consist of the following Passes:	
* Pass 1: independent projects, e.g., cc.isr.UnitAmounts;
	* On Commit:
		* Build and ran unit tests;
		* Pack if unit tests passed;
			* Note that Dependent projects may be pointing to an earlier package;
* Pass 2: dependent projects, e.g., cc.isr.UnitsAmounts.StandardUnits;
	* Update package reference of related projects;
		* e.g., reference to cc.isr.UnitsAmounts in cc.isr.UnitsAmounts.StandardUnits;
	* Commit to main;
	* On Commit:
		* Actions will run again on the independent and dependent project;
		* Pack actions on independent projects will be ignored as duplicates;
		* Pack actions on dependent projects will generate new packages as necessary;
* Pass 3: Unit test of packages (options):
	* Update package reference of the duplicate test projects;
		* e.g., package references in cc.isr.UnitsAmounts.MSTest.Packages;
	* Commit to main;
	* On Commit:
		* Actions will run again on the test projects;
		* Unlist packages (see [unlisting a package]) should tests fail;
* Pass 4: UI projects (deployment);
	* Update package references;
	* Run end to end tests;
* Pass 5: Deployment to Production;
	* Deploy using WiX Toolset;
	* Explore Docker deployment.

[DevOps]: https://en.wikipedia.org/wiki/DevOps
[unlisting a package]: https://docs.microsoft.com/en-us/nuget/nuget-org/policies/deleting-packages#unlisting-a-package


