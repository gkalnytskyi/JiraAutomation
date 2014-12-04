using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JiraTests
{
    internal class IssueSearchResultPage : BasePage
    {
        private const string issueTableId = "issuetable";

        public IssueSearchResultPage(AutomationFramework af) : base(af) 
        {
            IssuesReport = new IssuesReportGridWidget(issueTableId, Af);
        }

        public IssuesReportGridWidget IssuesReport { get; private set; }

        public void GoToIssue(string issueIdentifier)
        {
            IssuesReport.Issues.First(x => x.Summary.Contains(issueIdentifier)).Select();            
        }
    }
}
