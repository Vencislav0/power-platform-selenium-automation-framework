using Automation_Framework.Framework.ElementWrappers;
using Automation_Framework.Framework.PowerApps.ElementWrappers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.SpacecraftManagementApp.Pages.Forms
{
    public class SideMapForm : BaseForm
    {
        public SideMapForm(IWebDriver driver) : base(driver, By.XPath("//ul[@aria-label='Navigate Dynamics 365']"), "Side Map Form")
        {

        }

        public void ClickSidemapItem(string name)
        {
            var element = new Label(_driver, By.XPath($"//li[@aria-label='{name}']"), $"{name} Side Map Item");            
            element.Click();
        }
    }
}
