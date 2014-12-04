using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JiraTests
{
    internal class IssuePage : BasePage
    {
        public IssuePage(AutomationFramework af) : base(af) { }

        public void PressEditButton()
        {
            var wait = new WebDriverWait(Af.Driver, TimeSpan.FromSeconds(AutomationFramework.Timeout));
            wait.Until(_ => Af.Driver.FindElement(By.Id("edit-issue")).Displayed);
            Af.Driver.FindElement(By.Id("edit-issue")).Click();
        }

        public string Description
        {
            get
            {
                return Af.Driver.FindElement(By.Id("description-val")).Text;
            }
        }
    }
}
