using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.Framework.PowerApps.ElementWrappers
{
    public class BaseView
    {
        protected IWebDriver _driver;
        protected By _locator;
        protected string _name;
        protected string _recordLocator;

        public BaseView(IWebDriver driver, By locator, string name)
        {
            _driver = driver;
            _locator = locator;
            _name = name;
            _recordLocator = "//div[@aria-label='Press SPACE to select this row.']";
        }
    }
}
