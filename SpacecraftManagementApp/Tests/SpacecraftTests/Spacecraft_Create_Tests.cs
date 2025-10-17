using Allure.Net.Commons;
using Automation_Framework.Framework.PowerApps.Constants;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.Spacecraft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.SpacecraftManagementApp.Tests.SpacecraftTests
{
    public class Spacecraft_Create_Tests : BaseTest
    {
        private SpacecraftForm _spacecraftForm;
        private SpacecraftView _spacecraftView;
        private SideMapForm _sidemapForm;

        public SpacecraftForm spacecraftForm => _spacecraftForm ??= new SpacecraftForm(driver);
        public SpacecraftView spacecraftView => _spacecraftView ??= new SpacecraftView(driver);
        public SideMapForm sidemapForm => _sidemapForm ??= new SideMapForm(driver);

        [Test]
        public void CreateSpacecraft_WithValidData_SuccessfullyCreatesSpacecraft()
        {           

            var initialRecordCount = 0;
            AllureApi.Step("Navigating to Spacecraft View, and clicking New button", () =>
            {
                sidemapForm.ClickSidemapItem("Spacecrafts");
                initialRecordCount = spacecraftView.GetRecordsCount();
                spacecraftForm.ClickNewButtonFromToolBar();
            });

            AllureApi.Step("Filling Name and Year Of Manifacturer fields", () =>
            {
                spacecraftForm.FillName("Test");
                spacecraftForm.FillRandomYear();
            });

            AllureApi.Step("Selecting Country Bulgaria", () =>
            {
                spacecraftForm.SelectCountry("Bulgaria");
            });

            AllureApi.Step("Selecting Spaceport Sofia", () =>
            {
                spacecraftForm.SelectSpaceport("Sofia");
            });

            AllureApi.Step("Selecting Spacecraft Model Stellar Cruiser", () =>
            {
                spacecraftForm.SelectSpacecraftModel("Stellar Cruiser");
            });

            AllureApi.Step("Selecting Operational Company Nova Exploration Council", () =>
            {
                spacecraftForm.SelectOperationalCompany("Nova Exploration Council");
            });

            AllureApi.Step("Selecting Fleet Cosmic Wings Division", () =>
            {
                spacecraftForm.SelectFleet("Cosmic Wings Division");
            });

            AllureApi.Step("Saving the record verifying the creation and deleting it.", () =>
            {
                spacecraftForm.ClickSaveButtonFromToolBar(true);
                spacecraftForm.ChangeRegistrationNumber("BG-TEST");
                sidemapForm.ClickSidemapItem("Spacecrafts");

                Assert.That(spacecraftView.GetRecordsCount(), Is.EqualTo(initialRecordCount + 1), "Spacecraft was not displayed on the view");

                spacecraftView.DeleteRecord("BG-TEST");
               
            });
        }

        [Test]
        public void UserStory5414_CreateSpacecraft_WithIncompatibleModelToFleet_ThrowsErrorMessage()
        {
            AllureApi.Step("Navigate to Spacecraft View on the sidemap and click New button", () =>
            {
                sidemapForm.ClickSidemapItem("Spacecrafts");
                spacecraftForm.ClickNewButtonFromToolBar();
            });

            AllureApi.Step("Fill all mandatory fields", () =>
            {
                spacecraftForm.FillName("Tester");
                spacecraftForm.FillRandomYear();
                spacecraftForm.SelectCountry("Bulgaria");
                spacecraftForm.SelectSpaceport("Sofia");                
            });

            AllureApi.Step("Try to create with commercial model and military fleet and save the record", () =>
            {
                spacecraftForm.SelectSpacecraftModel("Commercial");
                spacecraftForm.SelectOperationalCompany("Nova");

                spacecraftForm.SelectFleet("Military");

                spacecraftForm.ClickSaveButtonFromToolBar(false);
            });

            AllureApi.Step("Verify Error Message is displayed with the correct text and close it", () =>
            {
                Assert.That(spacecraftForm.IsErrorMessageDisplayed(), Is.True, "Error Message was not visible within the time frame.");
                Assert.That(spacecraftForm.GetErrorMessageText(), Is.EqualTo("Cannot assign spacecraft to this fleet. Trying to assign Commercial Spacecraft to a fleet containing Military Spacecrafts. All spacecraft in the fleet must belong to the same category."), "Incorrect Error Message displayed.");

                spacecraftForm.ClickErrorOkayButton();
            });

            AllureApi.Step("Try to create with a military model and a commercial fleet and save the record", () =>
            {
                spacecraftForm.SelectSpacecraftModel("Military");               
                spacecraftForm.SelectIsArmed(IsArmedChoices.Yes.ToString());

                spacecraftForm.SelectFleet("Commercial");

                spacecraftForm.ClickSaveButtonFromToolBar(false);
            });

            AllureApi.Step("Verify Error Message is displayed with the correct text and close it", () =>
            {
                Assert.That(spacecraftForm.IsErrorMessageDisplayed(), Is.True, "Error Message was not visible within the time frame.");
                Assert.That(spacecraftForm.GetErrorMessageText(), Is.EqualTo("Cannot assign spacecraft to this fleet. Trying to assign Military Spacecraft to a fleet containing Commercial Spacecrafts. All spacecraft in the fleet must belong to the same category."), "Incorrect Error Message displayed.");

                spacecraftForm.ClickErrorOkayButton();
            });

            AllureApi.Step("Try to create with a research model and a commercial fleet and save the record", () =>
            {
                spacecraftForm.SelectSpacecraftModel("Research");
                spacecraftForm.SelectOrganizationType(OrganizationTypeChoices.Government.ToString());

                spacecraftForm.SelectFleet("Commercial");

                spacecraftForm.ClickSaveButtonFromToolBar(false);
            });

            AllureApi.Step("Verify Error Message is displayed with the correct text and close it", () =>
            {
                Assert.That(spacecraftForm.IsErrorMessageDisplayed(), Is.True, "Error Message was not visible within the time frame.");
                Assert.That(spacecraftForm.GetErrorMessageText(), Is.EqualTo("Cannot assign spacecraft to this fleet. Trying to assign Research Spacecraft to a fleet containing Commercial Spacecrafts. All spacecraft in the fleet must belong to the same category."), "Incorrect Error Message displayed.");

                spacecraftForm.ClickErrorOkayButton();
            });
        }





    }
}
