using System;
using OpenQA.Selenium;

namespace JiraTests
{
    public abstract class BaseWidget
    {
        protected AutomationFramework _Af;
        protected IWebElement BaseElement {get; private set;}

        protected BaseWidget(string elementId, AutomationFramework af)
        {
            BaseElement = af.Driver.FindElement(By.Id(elementId));
            _Af = af;
        }
    }
}
