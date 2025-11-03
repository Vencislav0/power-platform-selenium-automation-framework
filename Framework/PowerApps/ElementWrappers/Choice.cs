using Automation_Framework.Framework.ElementWrappers;
using Automation_Framework.Framework.Logging;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.Framework.PowerApps.ElementWrappers
{
    public class Choice : BaseElement
    {     public Choice(IWebDriver driver, By locator, string name) : base(driver, locator, name)
          {

          }

        public void SelectChoice(string value)
        {
            try
            {
                Logger.Debug($"Selecting value: {value} on choice type field: {name}");
                customWaits.WaitUntilVisible();
                customWaits.WaitUntilEnabled();
                Thread.Sleep(500);
                GetElement().Click();
                
                var option = new Label(driver, By.XPath($"//div[text()='{value}']"), $"{value} Option From Choice Field: {name}");
                option.Click();
            }
            catch(NoSuchElementException ex){
                Logger.Error($"Choice: {value} not found.", ex);
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to select choice {value} on Choice Field: {name}", ex);
                throw;
            }            
        }
    }
}
