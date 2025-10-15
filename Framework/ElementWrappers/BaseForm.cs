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
        protected Textbox nameField;
        public BaseForm(IWebDriver driver, By locator, string name) 
        { 
            _driver = driver;
            _locator = locator;
            _name = name;

            formElement = new Label(driver, locator, name);
            nameField = new Textbox(driver, By.XPath("//input[@aria-label='Name']"), "Name Field");

        }

        public void ClickFormElement()
        {
            formElement.Click();
        }

        public bool IsFormElementVisible()
        {
            return formElement.IsDisplayed();
        }

        public string GetFormTitle()
        {
            return new Label(_driver, By.Id("header_title"), "Form Title/Name").GetText();
        }

        public void CompleteNameField(string input)
        {
            nameField.SendKeys(input);
        }
    }
}
