using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace JiraTests
{
    internal class IssuesReportGridWidget : BaseWidget
    {
        public IssuesReportGridWidget(string elementId, AutomationFramework af) : base(elementId, af) { }

        public IssueRow[] Issues
        {
            get
            {
                return BaseElement.FindElement(By.TagName("tbody"))
                                  .FindElements(By.TagName("tr"))
                                  .Select(row => new IssueRow(row)).ToArray();
            }
        }

        #region IssueRow
        public class IssueRow
        {
            private const string IssueTypeClassName = "issuetype";
            private const string IssueKeyClassName = "issuekey";
            private const string IssueSummaryClassName = "summary";
            private const string IssueStatusClassName = "status";

            private const string IssueLinkClassName = "issue-link";

            private IWebElement _Row;
            
            public IssueRow(IWebElement row)
            {
                _Row = row;
            }

            public string IssueType
            {
                get
                {
                    return _Row.FindElement(By.ClassName(IssueTypeClassName))
                               .FindElement(By.XPath(".//img")).GetAttribute("alt");
                }
            }

            public string Key
            {
                get
                {
                    return _Row.FindElement(By.ClassName(IssueKeyClassName)).Text;
                }
            }

            public string Summary
            {
                get
                {
                    return _Row.FindElement(By.ClassName(IssueSummaryClassName)).Text;
                }
            }

            public string Status
            {
                get
                {
                    return _Row.FindElement(By.ClassName(IssueStatusClassName)).Text;
                }
            }

            public void Select()
            {
                _Row.FindElement(By.ClassName(IssueLinkClassName)).Click();
            }
        }
        #endregion
    }
}
