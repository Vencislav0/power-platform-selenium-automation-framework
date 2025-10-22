using Automation_Framework.Framework.PowerApps;
using Automation_Framework.Framework.Constants;
using Automation_Framework.Framework.PowerApps.ElementWrappers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Automation_Framework.Framework.ElementWrappers;
using Automation_Framework.Framework.PowerApps.Constants;

namespace Automation_Framework.SpacecraftManagementApp.Pages.Forms.Spacecraft
{
    public class SpacecraftForm : BaseForm
    {       
        protected Lookup countryLookup;
        protected Lookup spaceportLookup;
        protected Lookup spacecraftModelLookup;
        protected Lookup operatingCompanyLookup;
        protected Lookup fleetLookup;
        protected Choice organizationTypeChoice;
        protected Choice isArmedChoice;
        protected Label errorMessage;
        protected Button errorMessageOkButton;
        protected Button createMaintenanceButton;

        public SpacecraftForm(IWebDriver driver) : base(driver, By.XPath("//div[@data-id='grid-container']"), "Spacecraft Form")  
        {
            countryLookup = new Lookup(driver, By.XPath("//input[@aria-label='Country, Lookup']"), LookupTypes.Country, "Country Lookup");
            spaceportLookup = new Lookup(driver, By.XPath("//input[@aria-label='Spaceport, Lookup']"), LookupTypes.Spaceport, "Spaceport Lookup");
            spacecraftModelLookup = new Lookup(driver, By.XPath("//input[@aria-label='Spacecraft Model, Lookup']"), LookupTypes.SpacecraftModel, "Spacecraft Model Lookup");
            operatingCompanyLookup = new Lookup(driver, By.XPath("//input[@aria-label='Operating Company, Lookup']"), LookupTypes.OperatingCompany, "Operating Company Lookup");
            fleetLookup = new Lookup(driver, By.XPath("//input[@aria-label='Fleet, Lookup']"), LookupTypes.Fleet, "Fleet Lookup");

            organizationTypeChoice = new Choice(driver, By.XPath("//button[@aria-label='Organisation Type']"), "Organisation Type Choice");
            isArmedChoice = new Choice(driver, By.XPath("//button[@aria-label='Is Armed?']"), "Is Armed? Choice");

            errorMessage = new Label(driver, By.XPath("//span[@data-id='errorDialog_subtitle']"), "Error Message");
            errorMessageOkButton = new Button(driver, By.XPath("//button[@data-id='errorOkButton']"), "Error Message OK Button");

            createMaintenanceButton = new Button(driver, By.XPath("//span[text()='Create Maintenance']"), "Create Maintenance Button");            
        }

        public bool IsOrganisationTypeFieldDisplayed()
        {
            return organizationTypeChoice.IsDisplayed(Timeouts.EXTRA_SHORT);
        }

        public bool IsArmedDisplayed()
        {
            return isArmedChoice.IsDisplayed(Timeouts.EXTRA_SHORT);
        }

        public bool IsOperatingCompanyDisplayed()
        {
            return operatingCompanyLookup.IsDisplayed(Timeouts.EXTRA_SHORT);
        }

        public void FillName(string input)
        {
            CompleteField("Name", input);           
        }        

        public void FillRandomYear()
        {
            CompleteField("Year Of Manifacturer", $"{random.Next(1990, 2026)}");
        }

        public void ChangeRegistrationNumber(string input)
        {
            CompleteField("Registration Number", input);
        }

        public string GetRegistrationNumber()
        {
            return GetFieldValue("Registration Number");
        }    

        public void SelectCountry(string input)
        {
            countryLookup.EnterValue(input);
        }

        public void SelectSpaceport(string input)
        {
            spaceportLookup.EnterValue(input);
        }

        public void SelectSpacecraftModel(string input)
        {
            spacecraftModelLookup.EnterValue(input);
        }

        public void SelectOperationalCompany(string input)
        {
            operatingCompanyLookup.EnterValue(input);
        }

        public void SelectFleet(string input)
        {
            fleetLookup.EnterValue(input);
        }

        public void SelectIsArmed(string input)
        {
            isArmedChoice.SelectChoice(input);
        }

        public void SelectOrganizationType(string input)
        {
            organizationTypeChoice.SelectChoice(input);
        }

        public void ClickOnCountryLookupRecord()
        {
            countryLookup.ClickRecordOnLookup();
        }

        public bool IsErrorMessageDisplayed()
        {
            return errorMessage.IsDisplayed(Timeouts.SHORT);
        }

        public string GetErrorMessageText()
        {
            return errorMessage.GetText();
        }

        public void ClickErrorOkayButton()
        {
            errorMessageOkButton.Click();
        }

        public void ClickCreateMaintenanceButton()
        {
            if (createMaintenanceButton.IsDisplayed(Timeouts.EXTRA_SHORT))
            {
                createMaintenanceButton.Click();
            }
            else
            {
                overflowButton.Click();
                createMaintenanceButton.Click();
            }
        }


    }
}
