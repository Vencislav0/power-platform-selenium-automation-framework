using Automation_Framework.Framework.PowerApps.ElementWrappers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.SpacecraftManagementApp.Pages.Forms.Fleet
{
    public class FleetView : BaseView
    {
        public FleetView(IWebDriver driver) : base(driver, By.XPath("//div[@data-id='grid-container']"), "Country View")
        {

        }
    }
}
