using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using FluentAssertions;

namespace JiraTests
{
    public class Steps
    {
        private readonly AutomationFramework _Af;

        public Steps(AutomationFramework af)
        {
            _Af = af;
        }

        public virtual void GoToProjectSummaryPage()
        {
            _Af.GoToUrl("browse/TST/");
        }

        public virtual void CreateNewBugIssue(string id)
        {
            using (var page = new BasePage(_Af))
            {
                var testMessage = "Test bug issue " + id;
                page.PressCreateIssueButton();
                var createIssue = new CreateIssueWidget("create-issue-dialog", _Af);
                createIssue.SelectIssueType("Bug");
                createIssue.EnterSummary(testMessage);
                createIssue.EnterDescription(testMessage);
                createIssue.PressSubmitButton();
            }
        }

        public virtual void GoToProjectsIssuesSummary()
        {
            _Af.GoToUrl("browse/TST/?selectedTab=com.atlassian.jira.jira-projects-plugin:issues-panel");
        }

        public virtual void GoToIssuesReportedByMe()
        {
            var issuesSummary = new IssuesSummaryWidget("project-tab", _Af);
            issuesSummary.PressReportedByMe();
        }

        public virtual void GoToIssuePage(string issueIdentifier)
        {
            using (var issueReportPage = new IssueSearchResultPage(_Af))
            {
                issueReportPage.GoToIssue(issueIdentifier);
            }
        }

        public virtual void UpdateIssue()
        {
            using (var issuePage = new IssuePage(_Af))
            {
                issuePage.PressEditButton();
                var editWidget = new EditIssueWidget("edit-issue-dialog",_Af);

                var oldDescription = editWidget.Description;
                editWidget.EnterDescription(Keys.Enter + "Issue updated.");
                editWidget.PressUpdateButton();
            }
        }

        public virtual void VerifyIssueCreated(string issueIdentifier)
        {
            using (var issueReportPage = new IssueSearchResultPage(_Af))
            {
                var issues = issueReportPage.IssuesReport.Issues;
                var issue = issues.FirstOrDefault(x => x.Summary.Contains(issueIdentifier));
                issue.Should().NotBeNull();
                issue.Status.Should().Be("OPEN");
                issue.IssueType.Should().Be("Bug");
            }
        }

        public virtual void VerifyIssueUpdated()
        {
            using (var issuePage = new IssuePage(_Af))
            {
                var description = issuePage.Description;
                description.Should().EndWith("Issue updated.");
            }
        }
    }
}
