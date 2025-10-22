using Automation_Framework.Framework.PowerApps.ElementWrappers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.SpacecraftManagementApp.Pages.Forms.Fleet
{
    public class FleetForm : BaseForm
    {
        public FleetForm(IWebDriver driver) : base(driver, By.XPath("//div[@data-id='grid-container']"), "Fleet Form")
        {
            
        }

        public void FillNameField(string input)
        {
            CompleteField("Name", input);
        }

        public double GetFleetTotalFlightHours()
        {
            return double.TryParse(GetFieldValue("Total Flight Hours"), NumberStyles.Any, CultureInfo.InvariantCulture, out double result) ? result : 0;
        }

    }
}
