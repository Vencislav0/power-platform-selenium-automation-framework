using Automation_Framework.Framework.PowerApps.Constants;
using Automation_Framework.Framework.PowerApps.ElementWrappers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.SpacecraftManagementApp.Pages.Forms.SpaceFlight
{
    public class SpaceFlightForm : BaseForm
    {
        protected Date startDateField;
        protected Date endDateField;
        protected Choice missionType;
        protected Lookup launchSpaceport;
        protected Lookup landingSpaceport;
        public SpaceFlightForm(IWebDriver driver) : base(driver, By.XPath("//div[@data-id='grid-container']"), "Spaceflight Form")
        {
             startDateField = new Date(driver, By.XPath("//input[contains(@aria-label, 'Date of Start Date/Time')]"), "Start Date/Time");
             endDateField = new Date(driver, By.XPath("//input[contains(@aria-label, 'Date of End Date/Time')]"), "End Date/Time");
             missionType = new Choice(driver, By.XPath("//button[@aria-label='Mission Type']"), "Mission Type Choice");
             launchSpaceport = new Lookup(driver, By.XPath("//input[@aria-label='Launch Space Port, Lookup']"), "launchSpaceport", "Launch Space Port Lookup");
             landingSpaceport = new Lookup(driver, By.XPath("//input[@aria-label='Landing Space Port, Lookup']"), "landingSpaceport", "Landing Spaceport Lookup");
        }


        public void FillStartDate(string input)
        {
            startDateField.CompleteDateField(input);           
        }

        public void FillEndDate(string input)
        {
            endDateField.CompleteDateField(input);
        }

        public void SelectMissionType(MissionTypeChoices value)
        {
            missionType.SelectChoice(value.ToString());
        }

        public void SelectLaunchSpaceport(string input)
        {
            launchSpaceport.EnterValue(input);
        }

        public void SelectLandingSpaceport(string input)
        {
            landingSpaceport.EnterValue(input);
        }
    }
}
