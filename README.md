# HackerNews
Command line app for the Hacker News Scraper Test. The application targets .Net Core 3.0.

## Packages Used
 * [HTML Agility Pack (HAP)](https://github.com/zzzprojects/html-agility-pack)     
   This makes querying HTML nodes via XPath relatively straightforward.  
 * [System.CommandLine](https://github.com/dotnet/command-line-api)     
   Maps the command line parameter in the main method of the app. It also takes care of the input validation.  
   
## Prerequisites
Please ensure that you have the .Net Core 3.0 or later SDK installed. The SDK is available for download [here](https://dotnet.microsoft.com/download/dotnet-core/3.0).

## Building
The solution can be built by executing `Build.bat` located in the solution folder. Windows may give you a warning indicating that the batch is untrusted. If you are not comfortable to continue, you can also copy and paste the command from the batch file to command prompt. 

## Running the tests
The tests can be run by executing `Test.bat` located in the solution folder, or copying and pasting the command from the batch file to command prompt.

## Running the application
The application can be run by executing `Run.bat` located in the solution folder.

The application expects the following arguments:  
`--posts n`  
Where `n` is the number of posts to print. `n` must be an integer <= 100.

To execute from the command line use the following command from the folder containing the binaries:  
`hackernews --posts n`

## Implementation notes
The brief did not specify what to do when values cannot be scraped because they are not present on any given article, so I have assign the following default values for the output:
 * `Title - "Not specified"`  
 * `Author - "Anonymous"`  
 * `Url = ""`  
 * `Point - 0`  
 * `Comments - 0`  
 * `Rank - 0`  
