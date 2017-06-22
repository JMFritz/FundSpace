# Fund Space

####  _June 22nd, 2017_

#### By **_Tyler Allen, Daniela Trulls, Lena Kuchko, Jun Fritz_**

## Description
Fund Space is our 2nd group week project that incorporates the creative design of CSS with the object-oriented C# language.  Fund Space is a web application that allows a user to create, view, and donate to campaigns to help a certain cause.  We used popular crowdfunding apps like Kick Starter and Go Fund Me to use as references throughout the project and declared the Home Page, User Profile, and Campaign pages as our minimum viable product (MVP).  Fund Space is meant to be run on a Windows computer so replicating and viewing instructions are included in this README file.  

## Specifications
|Behavior| Input (User Action/Selection)| Output (Program Action)|
|---|---|---|
|User can create a profile| Select "Log-in/Sign-Up" | Display "Log-in/Sign-Up" form page |
|User can create a campaign when logged-in| Select "Create Campaign" in profile | Display "Campaign Information" form page |
|User can edit delete information in profile| Select "Edit" in User Information | Display "Edit User Profile" form |
|User can edit information in campaign| Select "Edit Campaign" in User Campaigns Section | Display "Edit Campaign Information" form |
|User can donate to a campaign | Enter $10, Select "Donate" | Display "$10 from BOB123" in Campaign's Donation section |
|User can view all campaigns by category| Select "Medical" | Display "Medical Campaigns" page with list of campaigns|
|User can search all campaigns by name| Enter "dog" | Display "Help my Dog", "Dog Training", "Dog Obedience School" |
|User can log-in and log-out of their profile| Enter "BOB123" & "pa55w0rd" | Display BOB123 User Profile |
|Campaigns will update the current balance after each donation| Enter $10, Select "Donate" | Display "Current Balance: $10" in Campaign Info section|
|Campaigns will be declared "trending" based off of popularity | Select "Medical Campaigns" | Display "Bob's Finger Splinter: 10 Donations" in "all Campaigns" page|
|BEHAVIORS |INPUT|OUTPUT|

## Setup/Installation Requirements

#### _**Replicating this Project**_
* Windows Based Program.
* Clone this directory.
* Using Windows PowerShell, run dnu restore to update necessary packages.
* Using Windows PowerShell, go into this directory and run dnx kestrel to run server.
* Once server is running, enter "localhost:5004" in your browser.
* Navigate through the application with links or URL paths.
* If your hoping to replicate RESTful routing practice, utilize HomeModule.cs in Module folder.
* All HTML can be accessed in Views folder.
* All Object Tests can be accessed in Test folder.
* All Class declarations can be accessed in Objects folder.

#### _**Starting from Scratch**_
* If you intend on starting from scratch, you have to create Startup.cs and project.json initially.
* Fill project.json and Startup.cs with necessary packages and "using" statements.
* If you plan on testing functions before implementing Nancy, make sure you have the [Collection("Charity")] line under namespace of ALL your tests to make sure tests aren't run in parallel to each other.
* Run dnu restore in Windows PowerShell which will create the project.lock.json file.
* Start creating your unique Module, View, and Object folders and use this directory as a template!
* For testing, be sure to include updated DB Configuration call in both Startup.cs and each one of your test files.

#### _**Project Database Usage Setup/Requirements**_
* Open Microsoft SQL Server Management Studio 2016 (MSSMS).
* Utilize the "band_tracker.sql" & "band_tracker_test.sql" files in this repository. Copy all content from both.
* In MSSMS toolbar, select "New Query".
* In the empty field at the top of the new query page, paste "charity.sql" content.
* Select "! Execute" button in toolbar.
* This will create the entire database created and altered for this project.
* If you plan on utilizing this project's testing, repeat the steps for "charity_test.sql" content (starting from New Query).

#### _**Microsoft SQL Server Management Studio instructions for Restore (Testing Database)**_
* Right-Click the "charity" database.
* Select "Restore".
* Click "Next" to verify information and when prompted, insert path to your repository (ex. Desktop/go-fund-me/charity.sql).
* This will create a "testscript.sql" file that holds all SQL Commands for creating a testing database you can use along with X-Unit.

## Known bugs

* KNOWN BUGS

## Support and contact details

Contact us with any comments, concerns, or questions.
Tyler Allen: mynameistylerallen@gmail.com
Daniella Trulls: 
Lena Kuchko: kuchkoel@gmail.com
Jun Fritz: jun.fritz@gmail.gom

## Technologies Used

_HTML, CSS, SASS, Bootstrap, JavaSscript, JQuery, C#, Razor, X-Unit_

### License

MIT

Copyright (c) 2017 **_Tyler Allen, Daniela Trulls, Lena Kuchko, Jun Fritz_**
