using Automation_Framework.Framework.PowerApps.ElementWrappers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.SpacecraftManagementApp.Pages.Forms.Maintenance
{
    public class MaintenanceView : BaseView
    {
        public MaintenanceView(IWebDriver driver) : base(driver, By.XPath("//div[@data-id='grid-container']"), "Maintenance View")
        {

        }
    }
}
