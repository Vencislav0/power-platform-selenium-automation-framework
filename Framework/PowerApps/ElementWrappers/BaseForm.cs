using Automation_Framework.Framework.Constants;
using Automation_Framework.Framework.ElementWrappers;
using Automation_Framework.Framework.Logging;
using Automation_Framework.Framework.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.Framework.PowerApps.ElementWrappers
{
    public class BaseForm
    {
        protected IWebDriver _driver;
        protected By _locator;
        protected string _name;
        protected Label formElement;
        protected Button saveButton;
        protected Button newButton;
        protected CustomWaits customWaits;
        public BaseForm(IWebDriver driver, By locator, string name) 
        { 
            _driver = driver;
            _locator = locator;
            _name = name;

            formElement = new Label(driver, locator, name);           
            newButton = new Button(_driver, By.XPath("//button[@aria-label='New']"), "New Button");
            saveButton = new Button(_driver, By.XPath("//button[@aria-label='Save (CTRL+S)']"), "New Button");
            customWaits = new CustomWaits(By.XPath("//div[@id='topBar']"), driver, Timeouts.API);

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

        public void CompleteField(string fieldName, string input)
        {
            var field = new Textbox(_driver, By.XPath($"//input[@aria-label='{fieldName}']"), $"{fieldName} Field");
            field.SendKeys(input);
        }

        public void ClickSaveButtonFromToolBar()
        {            
            saveButton.Click();
            customWaits.WaitUntilRecordSaved();
        }

        public void ClickNewButtonFromToolBar()
        {
            newButton.Click();
        }
    }
}
