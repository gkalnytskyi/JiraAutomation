using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JiraTests
{
    internal class EditIssueWidget : BaseWidget
    {
        public EditIssueWidget(string elementId, AutomationFramework af) : base(elementId, af) { }

        public string Description
        {
            get
            {
                return BaseElement.FindElement(By.Id("description")).Text;
            }
        }

        public void EnterDescription(string description)
        {
            BaseElement.FindElement(By.Id("description")).SendKeys(description);
        }

        public void PressUpdateButton()
        {
            BaseElement.FindElement(By.Id("edit-issue-submit")).Click();
        }
    }
}
