using System;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace JiraTests
{
    [TestFixture]
    public class Tests
    {
        protected static AutomationFramework _Af;
        protected Steps _Steps;
        
        [TestFixtureSetUp]
        public static void FixtureSetUp()
        {
            try
            {
                _Af = new AutomationFramework("https://jira.atlassian.com/");
            }
            catch
            {
                throw;
            }
        }

        [TestFixtureTearDown]
        public static void FixtureTeardown()
        {
            _Af.Dispose();
        }
        
        [SetUp]
        public void SetUp()
        {
            _Steps = new Steps(_Af);

            Setup();
        }

        protected virtual void Setup() { }

        [Test]
        public void User_Can_Create_New_Issue()
        {
            // Arrange
            var issueIdentifier = Guid.NewGuid().ToString();

            // Act
            _Steps.GoToProjectSummaryPage();
            _Steps.CreateNewBugIssue(issueIdentifier);

            // Assert
            _Steps.GoToProjectsIssuesSummary();
            _Steps.GoToIssuesReportedByMe();
            _Steps.VerifyIssuePresent(issueIdentifier);
        }

        [Test]
        public void User_Can_Edit_Issue()
        {
            // Arrange
            var issueIdentifier = Guid.NewGuid().ToString();
            _Steps.GoToProjectsIssuesSummary();
            _Steps.CreateNewBugIssue(issueIdentifier);
            
            // Act
            _Steps.GoToIssuesReportedByMe();
            _Steps.GoToIssuePage(issueIdentifier);
            _Steps.UpdateIssue();

            // Assert
            _Steps.VerifyIssueUpdated();
        }

        [Test]
        public void User_Can_Find_Issue_Using_Search()
        {
            // Arrange
            var issueIdentifier = Guid.NewGuid().ToString();
            _Steps.GoToProjectSummaryPage();
            _Steps.CreateNewBugIssue(issueIdentifier);
            _Steps.GoToProjectSummaryPage();
            
            // Act
            _Steps.SearchForIssue(issueIdentifier);

            // Assert
            _Steps.VerifyIssuePresent(issueIdentifier);
        }
    }
}
