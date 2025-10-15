using Automation_Framework.Framework.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.Framework.ElementWrappers
{
    public class Dropdown : BaseElement
    {
        public Dropdown(IWebDriver driver, By locator, string name) : base(driver, locator, name) 
        { 

        }

        public void SelectByText(string text)
        {
            try
            {
                Logger.Debug($"Selecting option using text: {text} from Dropdown: {name}");
                var dropDown = new SelectElement(GetElement());
                customWaits.WaitUntilVisible();
                dropDown.SelectByText(text);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to select option {text} from Dropdown {name}", ex);
                throw;
            }
            
        }

        public void SelectByValue(string value)
        {
            try
            {
                Logger.Debug($"Selecting option using text: {value} from Dropdown: {name}");
                var dropDown = new SelectElement(GetElement());
                customWaits.WaitUntilVisible();
                dropDown.SelectByValue(value);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to select option {value} from Dropdown {name}", ex);
                throw;
            }

        }
    }
}
