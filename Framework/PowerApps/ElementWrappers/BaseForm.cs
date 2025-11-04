using Automation_Framework.Framework.Constants;
using Automation_Framework.Framework.ElementWrappers;
using Automation_Framework.Framework.Logging;
using Automation_Framework.Framework.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.Support.UI;
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
        protected Button refreshButton;
        protected Button overflowButton;
        protected Button saveAndCloseButton;
        protected Label saveStatusHeader;
        protected Label titleHeader;
        protected CustomWaits customWaits;
        protected Random random;
        protected WebDriverWait wait;
        protected Label warningNotification;

        public BaseForm(IWebDriver driver, By locator, string name) 
        { 
            _driver = driver;
            _locator = locator;
            _name = name;

            formElement = new Label(_driver, locator, name);           
            newButton = new Button(_driver, By.XPath("//button[@aria-label='New']"), "New Button");
            overflowButton = new Button(_driver, By.XPath("//button[@data-id='OverflowButton' and contains(@aria-label, 'More commands for')]"), "Overflow Button");
            saveButton = new Button(_driver, By.XPath("//button[@aria-label='Save (CTRL+S)']"), "New Button");
            saveStatusHeader = new Label(_driver, By.XPath("//span[@data-id='header_saveStatus']"), "Save Status Header");
            saveAndCloseButton = new Button(_driver, By.XPath("//button[contains(@title, 'Save & Close')]"), "Save & Close Button");
            customWaits = new CustomWaits(By.XPath("//div[@id='topBar']"), driver, Timeouts.API);
            titleHeader = new Label(_driver, By.XPath("//h1[@data-id='header_title']"), "Form Title");            
            refreshButton = new Button(_driver, By.XPath("//button[@aria-label='Refresh']"), "Refresh Button");
            warningNotification = new Label(driver, By.XPath("//span[@data-id='warningNotification']"), "Warning Notification on Spacecraft Form");
            wait = new WebDriverWait(_driver, Timeouts.API);
            random = new Random();

        }

        public void FillName(string input)
        {
            CompleteField("Name", input);
        }
        public void ClickFormElement()
        {
            formElement.Click();
        }

        public bool IsFormElementVisible()
        {
            return formElement.IsDisplayed();
        }
     
        public void CompleteField(string fieldName, string input)
        {
            var field = new Textbox(_driver, By.XPath($"//input[@aria-label='{fieldName}'] | //textarea[@aria-label='{fieldName}']"), $"{fieldName} Field");
            field.SendKeys(Keys.Control + "a");
            field.SendKeys(Keys.Backspace);
            field.SendKeys(input);
        }

        public void ClickButton(string buttonName)
        {
            var button = new Button(_driver, By.XPath($"//button[@aria-label='{buttonName}']"), $"{buttonName} Button");
            button.Click();
        }

        public void SwitchToTab(string tabName)
        {
            var tab = new Label(_driver, By.XPath($"//li[@aria-label='{tabName}']"), $"{tabName} Tab");
            tab.Click();
        }

        public string GetFieldValue(string fieldName)
        {
            var field = new Textbox(_driver, By.XPath($"//input[@aria-label='{fieldName}']"), $"{fieldName} Field");

            return wait.Until(driver =>
            {
                var value = field.GetAttribute("value");
                return !string.IsNullOrEmpty(value) ? value : null;
            });
        }

        public void ClickSaveButtonFromToolBar(bool shouldWait)
        {
            if (saveButton.IsDisplayed(Timeouts.EXTRA_SHORT))
            {
                saveButton.Click();
            }
            else
            {
                overflowButton.Click();
                saveButton.Click();
            }

            if (shouldWait)
            {
                customWaits.WaitUntilRecordSaved();
            }            
        }

        public void ClickRefreshButtonFromToolBar()
        {
            if (refreshButton.IsDisplayed(Timeouts.EXTRA_SHORT))
            {
                refreshButton.Click();
            }
            else
            {
                overflowButton.Click();
                refreshButton.Click();
            }
        }

        public string GetSaveStatus()
        {
            return saveStatusHeader.GetText();
        }

        public void ClickNewButtonFromToolBar()
        {
            if (newButton.IsDisplayed(Timeouts.EXTRA_SHORT))
            {
                newButton.Click();
            }
            else
            {
                overflowButton.Click();
                newButton.Click();
            }
        }

        public void ClickSaveAndCloseButtonFromToolBar()
        {
            if (saveAndCloseButton.IsDisplayed(Timeouts.EXTRA_SHORT))
            {
                saveAndCloseButton.Click();
            }
            else
            {
                overflowButton.Click();
                saveAndCloseButton.Click();
            }
        }

        public string GetFormTitleText()
        {
            return titleHeader.GetAttribute("title");
        }

        public void WaitUntilFormTitleChanges(string titleBefore)
        {
            wait.Until(dr =>
            {
                try
                {

                    var elementTextAfter = dr.FindElement(By.XPath("//h1[@data-id='header_title']")).GetAttribute("title");


                    Logger.Debug($"Before: {titleBefore}, After: {elementTextAfter}");


                    return titleBefore != elementTextAfter && !string.IsNullOrEmpty(elementTextAfter);
                }
                catch (StaleElementReferenceException)
                {

                    Logger.Debug("Element became stale, retrying...");
                    return false;
                }
                catch (Exception ex)
                {                    
                    Logger.Debug($"Error while waiting: {ex.Message}");
                    return false;
                }

            });
        }

        public bool IsWarningNotificationDisplayed()
        {
            return warningNotification.IsDisplayed(Timeouts.DEFAULT_INTERVAL);
        }

        public string GetWarningNotificationText()
        {
            return warningNotification.GetText();
        }


    }
}
