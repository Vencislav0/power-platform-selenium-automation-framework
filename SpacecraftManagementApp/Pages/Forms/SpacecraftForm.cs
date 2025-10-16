using Automation_Framework.Framework.PowerApps;
using Automation_Framework.Framework.Constants;
using Automation_Framework.Framework.PowerApps.ElementWrappers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.SpacecraftManagementApp.Pages.Forms
{
    public class SpacecraftForm : BaseForm
    {       
        protected Lookup countryLookup;
        protected Lookup spaceportLookup;
        protected Lookup spacecraftModelLookup;
        protected Lookup operatingCompanyLookup;
        protected Lookup fleetLookup;
        public SpacecraftForm(IWebDriver driver) : base(driver, By.XPath(""), "Spacecraft Form")  
        {
            countryLookup = new Lookup(driver, By.XPath("//input[@aria-label='Country, Lookup']"), LookupTypes.Country, "Country Lookup");
            spaceportLookup = new Lookup(driver, By.XPath("//input[@aria-label='Spaceport, Lookup']"), LookupTypes.Spaceport, "Spaceport Lookup");
            spacecraftModelLookup = new Lookup(driver, By.XPath("//input[@aria-label='Spacecraft Model, Lookup']"), LookupTypes.SpacecraftModel, "Spacecraft Model Lookup");
            operatingCompanyLookup = new Lookup(driver, By.XPath("//input[@aria-label='Operating Company, Lookup']"), LookupTypes.OperatingCompany, "Operating Company Lookup");
            fleetLookup = new Lookup(driver, By.XPath("//input[@aria-label='Fleet, Lookup']"), LookupTypes.Fleet, "Fleet Lookup");

        }

        public void FillForm()
        {
            CompleteField("Name", "Test");
            CompleteField("Year Of Manifacturer", "2000");
            countryLookup.EnterValue("Bulgaria");
            spaceportLookup.EnterValue("Sofia");
            spacecraftModelLookup.EnterValue("Stellar");
            operatingCompanyLookup.EnterValue("Nova");
            fleetLookup.EnterValue("Cosmic Wings Division");
        }
    }
}
