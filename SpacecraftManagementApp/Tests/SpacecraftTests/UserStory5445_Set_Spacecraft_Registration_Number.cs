using Allure.Net.Commons;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.Country;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.Spacecraft;
using Automation_Framework.SpacecraftManagementApp.Steps;
using OpenQA.Selenium.BiDi.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Automation_Framework.SpacecraftManagementApp.Tests.SpacecraftTests
{
    public class UserStory5445_Set_Spacecraft_Registration_Number : BaseTest
    {
        private SpacecraftForm? _spacecraftForm;
        private SpacecraftView? _spacecraftView;
        private SideMapForm? _sidemapForm;
        private CountryForm? _countryForm;

        public SpacecraftForm spacecraftForm => _spacecraftForm ??= new SpacecraftForm(driver);
        public SpacecraftView spacecraftView => _spacecraftView ??= new SpacecraftView(driver);
        public SideMapForm sidemapForm => _sidemapForm ??= new SideMapForm(driver);
        public CountryForm countryForm => _countryForm ??= new CountryForm(driver);

        [Test]
        public void OnSpacecraftCreate_VerifyRegistrationNumber_IsGeneratedWithCorrectFormat()
        {
            AllureApi.Step("Navigating to Spacecraft View, and click new button", () =>
            {
                sidemapForm.ClickSidemapItem("Spacecrafts");
                spacecraftForm.ClickNewButtonFromToolBar();
            });

            AllureApi.Step("Create a spacecraft", () =>
            {
                SpacecraftSteps.CreateMilitarySpacecraft(spacecraftForm);
            });


            AllureApi.Step("Verifying the registration number is in correct format", () =>
            {
                var match = new Regex(@"^[A-Z]{2,3}-([A-Z0-9]{3,4})$");

                Assert.That(spacecraftForm.GetRegistrationNumber(), Does.Match(match.ToString()));
            });
            string prefix = "";
            string countryCode = "";
            AllureApi.Step("Storing the Registration Number prefix, Clicking on the country in the lookup and storing the country code value", () =>
            {
                prefix = spacecraftForm.GetRegistrationNumber().Split("-")[0];

                spacecraftForm.ClickOnCountryLookupRecord();

                countryCode = countryForm.GetCountryCodeValue();

                spacecraftForm.ClickSaveAndCloseButtonFromToolBar();


            });

            AllureApi.Step("Verify the prefix value is the same as the country code and deleting the record", () =>
            {
                Assert.That(prefix, Is.EqualTo(countryCode));  
                var registrationNumber = spacecraftForm.GetRegistrationNumber();
                SpacecraftSteps.DeleteSpacecraft(spacecraftForm, sidemapForm, spacecraftView, registrationNumber);
            });

        }

        [Test]
        public void RegistrationCodeUpdate_VerifySuccessfulUpdate_WhenCorrectFormat()
        {
            AllureApi.Step("Navigating to Spacecraft View, and open existing spacecraft record", () =>
            {
                sidemapForm.ClickSidemapItem("Spacecrafts");
                spacecraftView.OpenRecord(1);
            });

            var formatToMatch = new Regex(@"^[A-Z]{2,3}-([A-Z0-9]{3,4})$");

            SpacecraftSteps.UpdateRegistrationNumber(spacecraftForm, "BG-TEST", true);

            AllureApi.Step("Verify successful save and the registration number is in the correct format", () =>
            {
                Assert.That(spacecraftForm.GetSaveStatus(), Does.Contain("Saved"), "Failed saving spacecraft after correct registration number was entered.");
                Assert.That(spacecraftForm.GetRegistrationNumber(), Does.Match(formatToMatch), "the new value entered did not match the format requirments and was saved successfully");
            });


        }

        [Test]
        
        public void RegistrationCodeUpdate_VerifyErrorMessage_WhenInvalidInput()
        {
            AllureApi.Step("Navigating to Spacecraft View, and open existing spacecraft record", () =>
            {
                sidemapForm.ClickSidemapItem("Spacecrafts");
                spacecraftView.OpenRecord(1);
            });

            var formatToMatch = new Regex(@"^[A-Z]{2,3}-([A-Z0-9]{3,4})$");

            SpacecraftSteps.UpdateRegistrationNumber(spacecraftForm, "BG-INVALID", false);
            SpacecraftSteps.UpdateRegistrationNumber(spacecraftForm, "BG-test", false);
            SpacecraftSteps.UpdateRegistrationNumber(spacecraftForm, "BG-TEST1", false);
            SpacecraftSteps.UpdateRegistrationNumber(spacecraftForm, "BG-TE", false);
            SpacecraftSteps.UpdateRegistrationNumber(spacecraftForm, "BG-", false);
            SpacecraftSteps.UpdateRegistrationNumber(spacecraftForm, "BGNG-TEST", false);
            SpacecraftSteps.UpdateRegistrationNumber(spacecraftForm, "B-TEST", false);
            SpacecraftSteps.UpdateRegistrationNumber(spacecraftForm, "BG-TE$T", false);
            SpacecraftSteps.UpdateRegistrationNumber(spacecraftForm, "BG-INVALID", false);

            AllureApi.Step("Verify that the record didn't save and that the registration number format is incorrect", () =>
            {
                
                Assert.That(spacecraftForm.GetSaveStatus(), Does.Contain("Unsaved"), "Failed saving spacecraft after correct registration number was entered.");
                Assert.That(spacecraftForm.GetRegistrationNumber(), !Does.Match(formatToMatch), "the new value entered did not match the format requirments and was saved successfully");

            });
        }
    }
}
