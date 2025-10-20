using Automation_Framework.Framework.ElementWrappers;
using Automation_Framework.Framework.Logging;
using Automation_Framework.Framework.PowerApps.PowerAppsss;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Automation_Framework.Framework.Utilities
{
    public class CustomWaits
    {
        protected IWebDriver driver;
        protected By locator;
        protected WebDriverWait wait;
        protected Actions actions;

        public CustomWaits(By locator, IWebDriver driver, TimeSpan timeout)
        {
            this.locator = locator;
            this.driver = driver;
            this.actions = new Actions(driver);
            this.wait = new WebDriverWait(driver, timeout);
        }
        public void WaitUntilVisible()
        {
            wait.Until(driver =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    return element.Displayed ? element : null;
                }
                catch (StaleElementReferenceException)
                {

                    return null;
                }

            });
        }

        public ICollection<IWebElement> WaitUntilAllVisibleAndReturn()
        {
            List<IWebElement> elements;

            try
            {
                elements = wait.Until(driver =>
                {
                    var foundElements = driver.FindElements(locator);
                    Logger.Debug($"Found {foundElements.Count} elements... checking visibility");
                    return foundElements.All(e => e.Displayed) && foundElements.Count > 0 ? foundElements : null;
                }).ToList();
            }
            catch (WebDriverTimeoutException)
            {
                Logger.Warn("Timeout waiting for visible elements. Returning empty list.");
                elements = new List<IWebElement>();
            }
            catch (StaleElementReferenceException)
            {
                return null;
            }

            return elements;
        }

        public ICollection<IWebElement> WaitUntilAllVisibleAndReturn(WebDriverWait wait)
        {
            List<IWebElement> elements;

            try
            {
                elements = wait.Until(driver =>
                {
                    var foundElements = driver.FindElements(locator);
                    Logger.Debug($"Found {foundElements.Count} elements... checking visibility");
                    return foundElements.All(e => e.Displayed) && foundElements.Count > 0 ? foundElements : null;
                }).ToList();
            }
            catch (WebDriverTimeoutException)
            {
                Logger.Warn("Timeout waiting for visible elements. Returning empty list.");
                elements = new List<IWebElement>();
            }
            catch (StaleElementReferenceException)
            {
                return null;
            }

            return elements;
        }

        public void WaitUntilVisibleAt(int index)
        {
            wait.Until(driver =>
            {
                var elements = driver.FindElements(locator);
                if (index < 0 || index >= elements.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
                }
                return elements[index].Displayed;
            });
        }

        public void WaitUntilHidden()
        {
            wait.Until(driver =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    return !element.Displayed;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }

            });
        } 

        public void WaitUntilEnabled()
        {
            wait.Until(driver =>
            {
                try
                {
                    var element = driver.FindElement(locator);
                    return element.Enabled ? element : null;
                }
                catch (StaleElementReferenceException)
                {

                    return null;
                }                
            });
        }

        public void WaitUntilLookupRecordsLoad(FieldConfig lookupConfig, TimeSpan timeout)
        {
            var wait = new WebDriverWait(driver, timeout);
            wait.Until(dr =>
            {
                var records = driver.FindElements(By.XPath(lookupConfig.recordsListLocator));
                return records.Count > 0;
            });           
        }

        public void WaitUntilRecordFormLoads()
        {
            wait.Until(dr =>
            {
                var nameField = dr.FindElement(By.XPath("//input[contains(@data-id, 'space_name')]"));
                return nameField.Displayed ? nameField : null;
            });
        }

        public void WaitUntilRecordSaved()
        {
            wait.Until(dr =>
            {
                var saveIndicatorText = dr.FindElement(By.XPath("//span[@data-id='header_saveStatus']")).Text;
                return saveIndicatorText.Contains("Saved");
            });
        }

        public void WaitUntilElementTextChanges(string textBefore)
        {           
            wait.Until(dr =>
            {
                try
                {

                    var elementTextAfter = dr.FindElement(locator).Text;


                    Logger.Debug($"Before: {textBefore}, After: {elementTextAfter}");


                    return textBefore != elementTextAfter;
                }
                catch (StaleElementReferenceException)
                {

                    Logger.Debug("Element became stale, retrying...");
                    return false;
                }
                catch (Exception ex)
                {
                    // Optionally log the error if needed
                    Logger.Debug($"Error while waiting: {ex.Message}");
                    return false;
                }

            });
        }
            
    }
}
