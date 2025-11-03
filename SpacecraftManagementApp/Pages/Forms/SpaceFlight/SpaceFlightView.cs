using Automation_Framework.Framework.PowerApps.ElementWrappers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.SpacecraftManagementApp.Pages.Forms.SpaceFlight
{
    public class SpaceFlightView : BaseView
    {
        public SpaceFlightView(IWebDriver driver) : base(driver, By.XPath("//div[@data-id='grid-container']"), "Space Flight View")
        {

        }
    }
}
