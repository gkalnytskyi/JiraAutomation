using System;
using System.Configuration;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace JiraTests
{
    public class AutomationFramework : IDisposable
    {
        private Uri _BaseUrl;
        public static readonly int Timeout = int.Parse(ConfigurationManager.AppSettings["Timeout"]);

        public IWebDriver Driver { get; private set; }

        public AutomationFramework(string baseUri)
        {
            _BaseUrl = new Uri(baseUri);
            Init();
        }

        public void GoToUrl(string relativeUrl)
        {
            var uri = new Uri(_BaseUrl, relativeUrl);
            Driver.Navigate().GoToUrl(uri);
        }

        private void Init()
        {
            StopAllDrivers();

            var options = new ChromeOptions();
            var profilePath = ConfigurationManager.AppSettings["ProfilePath"];
            options.AddArgument("--start-maximized");
            options.AddArgument("--user-data-dir=" + profilePath);
            Driver = new ChromeDriver(options);
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(Timeout));
        }
        
        private void StopAllDrivers()
        {
            foreach (var process in Process.GetProcessesByName("chromedriver"))
            {
                process.Kill();
            }
        }

        #region Dispose Pattern
        public void Dispose()
        {
            if (Driver != null)
            {
                Driver.Close();
                Driver.Quit();
                Driver.Dispose();
                Driver = null;
            }
            GC.SuppressFinalize(this);
        }

        ~AutomationFramework()
        {
            Dispose();
        }
        #endregion
    }
}
