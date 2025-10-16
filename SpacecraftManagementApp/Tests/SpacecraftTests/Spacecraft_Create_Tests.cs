using Allure.Net.Commons;
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
        [Test]
        public void CreateSpacecraft_WithValidData_SuccessfullyCreatesSpacecraft()
        {
            var spacecraftForm = new SpacecraftForm(driver);
            var spacecraftView = new SpacecraftView(driver);
            var sidemapForm = new SideMapForm(driver);

            var initialRecordCount = 0;
            AllureApi.Step("Navigating to Spacecraft View, Deleting all existing spacecrafts and clicking New button", () =>
            {          
                initialRecordCount = spacecraftView.GetRecordsCount();
                sidemapForm.ClickSidemapItem("Spacecrafts");
                spacecraftView.DeleteAllRecords();                
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

            AllureApi.Step("Saving the record and verifying the creation", () =>
            {
                spacecraftForm.ClickSaveButtonFromToolBar();
                sidemapForm.ClickSidemapItem("Spacecrafts");

                Assert.That(spacecraftView.GetRecordsCount(), Is.EqualTo(initialRecordCount), "Spacecraft was not displayed on the view");
            });
        }
    }
}
