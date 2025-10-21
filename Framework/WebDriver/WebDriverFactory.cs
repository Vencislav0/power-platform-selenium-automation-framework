using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.Framework.WebDriver
{
    public class WebDriverFactory
    {
        public static IWebDriver GetChromeDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--ignore-certificate-errors");
            options.AddArgument("--disable-dev-shm-usage");
            options.PageLoadStrategy = PageLoadStrategy.Normal;
            return new ChromeDriver(options);
        }
    }
}
