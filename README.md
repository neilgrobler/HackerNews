# HackerNews
Command line app for the Hacker News Scraper Test. The application targets .Net Core 3.0.

## Packages Used
 * [HTML Agility Pack (HAP)](https://github.com/zzzprojects/html-agility-pack)
   
   This makes querying nodes via XPath relatively straightforward.
 * [System.CommandLine](https://github.com/dotnet/command-line-api)
   
   Maps the command line parameter in the main method of the app. It also takes care of the input validation.

## Prerequisits
Please ensure that you have the .Net Core 3.0 or later SDK installed. The SDK is available for download [here](https://dotnet.microsoft.com/download/dotnet-core/3.0).

## Building
The solution can be built by executing Build.bat located in the solution folder. Windows may give you a warning indicating that the batch is untrusted. If you are not comfortable to continue, you can also copy and paste the command from the batch file to command prompt. 

## Running the tests
The tests can be run by executing Build.bat located in the solution folder, or copying and pasting the command from the batch file to command prompt.

## Running the application
The application can be run by executing Run.bat located in the solution folder.

