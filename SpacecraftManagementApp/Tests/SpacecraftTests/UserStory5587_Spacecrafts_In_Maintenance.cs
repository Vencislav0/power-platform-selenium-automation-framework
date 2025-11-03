using Allure.Net.Commons;
using Automation_Framework.Framework.Utilities;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.Country;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.Fleet;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.Maintenance;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.Spacecraft;
using Automation_Framework.SpacecraftManagementApp.Steps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.SpacecraftManagementApp.Tests.SpacecraftTests
{
    public class UserStory5587_Spacecrafts_In_Maintenance : BaseTest
    {
        private SpacecraftForm? _spacecraftForm;
        private SpacecraftView? _spacecraftView;
        private SideMapForm? _sidemapForm;
        private CountryForm? _countryForm;
        private MaintenanceView? _maintenanceView;
        private FleetForm? _fleetForm;
        private FleetView _fleetView;

        public SpacecraftForm spacecraftForm => _spacecraftForm ??= new SpacecraftForm(driver);
        public MaintenanceView maintenanceView => _maintenanceView ??= new MaintenanceView(driver);
        public SpacecraftView spacecraftView => _spacecraftView ??= new SpacecraftView(driver);
        public SideMapForm sidemapForm => _sidemapForm ??= new SideMapForm(driver);
        public CountryForm countryForm => _countryForm ??= new CountryForm(driver);
        public FleetForm fleetForm => _fleetForm ??= new FleetForm(driver);
        public FleetView fleetView => _fleetView ??= new FleetView(driver);

        [Test]
        public void Test_Spacecraft_WhenCreatedMaintenance_StatusUpdates()
        {
            AllureApi.Step("Navigate to Spacecraft View and create a new military spacecraft", () =>
            {
                sidemapForm.ClickSidemapItem("Spacecrafts");
                sidemapForm.ClickNewButtonFromToolBar();
                SpacecraftSteps.CreateMilitarySpacecraft(spacecraftForm);
            });
            var spacecraftRegistrationNumber = "";
            AllureApi.Step("Retrive the registration code for later use", () =>
            {
                spacecraftRegistrationNumber = spacecraftForm.GetRegistrationNumber();
            });

            AllureApi.Step("Click the \"Create Maintenace\" button and navigate back to the spacecrafts view", () => {
               var formTitle =  spacecraftForm.GetFormTitleText();
                spacecraftForm.ClickCreateMaintenanceButton();

                spacecraftForm.WaitUntilFormTitleChanges(formTitle);
                sidemapForm.ClickSidemapItem("Spacecrafts");
            });

            AllureApi.Step("On the Spacecraft View find the spacecraft we put into maintenance using the reg number and verify that the status is \"In Maintenance\".", () => {
                
           
                AssertEqualWithRefresh(() => spacecraftView.GetRecordStatus(spacecraftRegistrationNumber), "In Maintenance", spacecraftForm, 30);             
            });

            AllureApi.Step("Navigate to the military fleet and verify presence of warning notification with the correct spacecraft name in maintenance", () => {

                sidemapForm.ClickSidemapItem("Fleets");
                fleetView.OpenRecord("Military Fleet");

                AssertTrueWithRefresh(() => fleetForm.IsWarningNotificationDisplayed(), fleetForm, 10);
                Assert.That(fleetForm.GetWarningNotificationText(), Is.EqualTo("Warning: The following spacecraft(s) are currently in maintenance: Test. Please check the spacecraft details for more information."));
            });

            AllureApi.Step("Navigate to Maintenance View and delete all maintenance records", () => {

                sidemapForm.ClickSidemapItem("Maintenances");
                maintenanceView.DeleteAllRecords();
            });

            AllureApi.Step("Navigate to Spacecraft View and delete the spacecraft", () =>
            {
                sidemapForm.ClickSidemapItem("Spacecrafts");
                spacecraftView.DeleteRecord(spacecraftRegistrationNumber);
            });

            AllureApi.Step("Navigate to Military Fleet and verify that the warning notification is not present after spacecraft deletion", () =>
            {
                sidemapForm.ClickSidemapItem("Fleets");
                fleetView.OpenRecord("Military Fleet");

                AssertTrueWithRefresh(() => !fleetForm.IsWarningNotificationDisplayed(), fleetForm, 10);
            });
        }
    }
}
