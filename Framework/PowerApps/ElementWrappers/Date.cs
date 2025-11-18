using Automation_Framework.Framework.ElementWrappers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.Framework.PowerApps.ElementWrappers
{
    public class Date : BaseElement
    {
        public Date(IWebDriver driver,By locator, string name) : base(driver, locator, name) 
        { 

        }

        public void CompleteDateField(string input)
        {
            var dateButton = new Label(driver, By.XPath($"//input[contains(@aria-label, '{name}')]/following-sibling::span"), $"{name} Date Field");
            var inputField = new Label(driver, locator, $"{name} Date Field");


            dateButton.DoubleClick();
            inputField.SendKeys(Keys.Control + "a");
            inputField.SendKeys(Keys.Backspace);
            inputField.SendKeys(input);
        }
    }
}
