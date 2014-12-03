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
            _Steps.GoToProjectsIssuesSummary();
            _Steps.GoToIssuesReportedByMe();

            // Assert
            _Steps.VerifyIssueCreated(issueIdentifier);
        }

        [Test]
        public void User_Can_Edit_Issue()
        {
            // Arrange
            // var issueIdentifier = Guid.NewGuid().ToString();
            var issueIdentifier = "38d3d4da-d497-4192-bb7a-c98f072310b7";
            _Steps.GoToProjectsIssuesSummary();
            //_Steps.CreateNewBugIssue(issueIdentifier);
            // Act
            _Steps.GoToIssuesReportedByMe();
            _Steps.GoToIssuePage(issueIdentifier);
            _Steps.UpdateIssue();

            // Assert
            _Steps.VerifyIssueUpdated();
        }
    }
}
