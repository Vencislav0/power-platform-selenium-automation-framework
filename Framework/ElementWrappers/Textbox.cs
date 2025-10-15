using Automation_Framework.Framework.Logging;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.Framework.ElementWrappers
{
    public class Textbox : BaseElement
    {
        public Textbox(IWebDriver driver, By locator, string name) : base(driver, locator, name)
        {

        }

        public void SendKeys(string text)
        {
            try
            {
                Logger.Debug($"Sending text to element: {name}");
                customWaits.WaitUntilVisible();
                customWaits.WaitUntilEnabled();
                GetElement().SendKeys(text);
            }

            catch (Exception er)
            {

                Logger.Error($"Failed to send text to element: {name}", er);
                throw;
            }

        }

        public void Clear()
        {
            try
            {
                Logger.Debug($"Clearing Textbox: {name}");
                customWaits.WaitUntilVisible();
                customWaits.WaitUntilEnabled();
                GetElement().Clear();
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to clear Textbox: {name}", ex);
                throw;
            }
            
        }
    }
}
