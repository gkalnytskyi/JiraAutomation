using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JiraTests
{
    public class BasePage : IDisposable
    {
        protected AutomationFramework Af { get; private set; }

        public BasePage(AutomationFramework af)
        {
            Af = af;
        }

        public void Dispose() { }

        public void PressCreateIssueButton()
        {
            Af.Driver.FindElement(By.Id("create_link")).Click();
        }

        public void Search(string searchItem)
        {
            var elem = Af.Driver.FindElement(By.Id("quickSearchInput"));
            elem.Clear();
            elem.SendKeys(searchItem + Keys.Enter);
        }
    }
}