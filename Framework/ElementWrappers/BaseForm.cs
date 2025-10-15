using Automation_Framework.Framework.Logging;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.Framework.ElementWrappers
{
    public class BaseForm
    {
        protected IWebDriver _driver;
        protected By _locator;
        protected string _name;
        protected Label formElement;
        public BaseForm(IWebDriver driver, By locator, string name) 
        { 
            _driver = driver;
            _locator = locator;
            _name = name;
            formElement = new Label(driver, locator, name);

        }

        public void ClickFormElement()
        {
            formElement.Click();
        }

        public bool IsFormElementVisible()
        {
            return formElement.IsDisplayed();
        }
    }
}
