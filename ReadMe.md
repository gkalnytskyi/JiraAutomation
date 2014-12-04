For those who want to compile and run this solution.

To compile the solution you need to:
1. Have Visual Studio 2013 installed on your computer (Visual Studio 2012 might also work, but I haven't tested that);
2. Have and Internet connection, so that Nuget Packages can be downloaded;
3. Put chromedriver.exe to the JiraTest project folder. I deliberately didn't include the chromedriver.exe to git repository.

To run tests you need:
1. NUnit to launch tests;
2. Google Chrome to display a website under test.
3. Add path to the user Chrome profile who is logged into https://jira.atlassian.com/
   to the App.config file of the JiraTest project
4. Use nunit-x86.exe to run test because solutions is compiled for x86.


The assumptions I've made writing these tests:
1. User doesn't log into jira.attlassian.com every time. User credentials are stored in the Chrome user profile.
2. I don't have access to the Jira DB, so I cannot delete issues I've created, so I used GUID to identify issues.

Note: If I had access to the Jira DB, I would rather set up data through DB in second and third tests, and delete
created issue in the first one after a test run.
