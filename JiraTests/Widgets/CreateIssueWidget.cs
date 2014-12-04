using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JiraTests
{
    public class CreateIssueWidget : BaseWidget
    {
        protected const string WidgetLoadXPath = ".//div[contains(concat(' ',normalize-space(@class),' '), " +
                                               "' buttons-container form-footer ')]//span";

        protected const string LoadingClass = "icon throbber loading";

        public CreateIssueWidget(string elementId, AutomationFramework af) : base(elementId, af) { }

        
        public void SelectProject(string projectName)
        {
            var projectField = BaseElement.FindElement(By.Id("project-field"));
            projectField.SendKeys(projectName+Keys.Enter);
        }

        public void SelectIssueType(string issueType)
        {
            var issueField = BaseElement.FindElement(By.Id("issuetype-field"));
            issueField.SendKeys(issueType+Keys.Enter);

            var wait = new WebDriverWait(_Af.Driver, TimeSpan.FromSeconds(AutomationFramework.Timeout));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            wait.Until(_ => BaseElement.FindElement(By.XPath(WidgetLoadXPath))
                                       .GetAttribute("class") != LoadingClass);
        }

        public void EnterSummary(string summary)
        {
            var summaryField = BaseElement.FindElement(By.Id("summary"));
            summaryField.SendKeys(summary);
        }

        public void EnterDescription(string description)
        {
            var descriptionField = BaseElement.FindElement(By.Id("description"));
            descriptionField.SendKeys(description);
        }

        public void PressSubmitButton()
        {
            BaseElement.FindElement(By.Id("create-issue-submit")).Click();
        }
    }
}
