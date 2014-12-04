using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace JiraTests
{
    internal class IssuesSummaryWidget : BaseWidget
    {
        public IssuesSummaryWidget(string elementId, AutomationFramework af) : base(elementId, af) { }

        public void PressReportedByMe()
        {
            BaseElement.FindElement(By.Id("filter_reportedbyme")).Click();
        }

    }
}
