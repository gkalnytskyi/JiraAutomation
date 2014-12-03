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

        public void GoToProjectSummaryPage()
        {
            _Af.GoToUrl("browse/TST/");
        }

        public void CreateNewBugIssue(string id)
        {
            const string widgetId = "create-issue-dialog";
            using (var page = new BasePage(_Af))
            {
                var testMessage = "Test bug issue " + id;
                page.PressCreateIssueButton();
                var createIssue = new CreateIssueWidget(widgetId, _Af);
                createIssue.SelectIssueType("Bug");
                createIssue.EnterSummary(testMessage);
                createIssue.EnterDescription(testMessage);
                createIssue.PressSubmitButton();

                WaitForWidgetToDisappear(widgetId);
            }
        }

        public void GoToProjectsIssuesSummary()
        {
            _Af.GoToUrl("browse/TST/?selectedTab=com.atlassian.jira.jira-projects-plugin:issues-panel");
        }

        public void GoToIssuesReportedByMe()
        {
            var issuesSummary = new IssuesSummaryWidget("project-tab", _Af);
            issuesSummary.PressReportedByMe();
        }

        public void GoToIssuePage(string issueIdentifier)
        {
            using (var issueReportPage = new IssueSearchResultPage(_Af))
            {
                issueReportPage.GoToIssue(issueIdentifier);
            }
        }

        public void UpdateIssue()
        {
            using (var issuePage = new IssuePage(_Af))
            {
                const string editId = "edit-issue-dialog";
                issuePage.PressEditButton();
                var editWidget = new EditIssueWidget(editId,_Af);

                var oldDescription = editWidget.Description;
                editWidget.EnterDescription(Keys.Enter + "Issue updated.");
                editWidget.PressUpdateButton();

                WaitForWidgetToDisappear(editId);
            }
        }

        public void SearchForIssue(string issueIdentifier)
        {
            using (var page = new BasePage(_Af))
            {
                page.Search(issueIdentifier);
            }
        }

        public void VerifyIssuePresent(string issueIdentifier)
        {
            using (var issueReportPage = new IssueSearchResultPage(_Af))
            {
                var issues = issueReportPage.IssuesReport.Issues;
                var issue = issues.FirstOrDefault(x => x.Summary.Contains(issueIdentifier));
                issue.Should().NotBeNull();
                issue.Summary.Should().Contain("Test bug issue " + issueIdentifier);
                issue.Status.Should().Be("OPEN");
                issue.IssueType.Should().Be("Bug");
            }
        }

        public void VerifyIssueUpdated()
        {
            using (var issuePage = new IssuePage(_Af))
            {
                var description = issuePage.Description;
                description.Should().EndWith("Issue updated.");
            }
        }

        #region Helper functions

        private void WaitForWidgetToDisappear(string editId)
        {
            var wait = new WebDriverWait(_Af.Driver, TimeSpan.FromSeconds(AutomationFramework.Timeout));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            wait.Until(_ => _Af.Driver.FindElements(By.Id(editId)).Count == 0);
        }

        #endregion
    }
}
