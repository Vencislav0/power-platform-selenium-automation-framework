using Allure.Net.Commons;
using Allure.Net.Commons.Steps;
using Automation_Framework.Framework.Utilities;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.Engine;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.Maintenance;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.Spacecraft;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.SpaceFlight;
using Automation_Framework.SpacecraftManagementApp.Steps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.SpacecraftManagementApp.Tests.SpacecraftTests.e2e
{
    public class Spacecraft_Tests_e2e : BaseTest
    {
        private SpacecraftForm? _spacecraftForm;
        private SpacecraftView? _spacecraftView;
        private SpaceFlightForm? _spaceFlightForm;
        private SideMapForm? _sidemapForm;
        private MaintenanceForm? _maintenanceForm;
        private MaintenanceTaskForm? _maintenanceTaskForm;
        private MaintenanceTasksSubgrid? _maintenanceTaskSubgrid;
        private BPFForm? _bpfForm;
        private MaintenanceView? _maintenanceView;
        private EngineForm? _engineForm;
        private EnginesSubgrid? _enginesSubgrid;
        private MaintenanceSteps? _maintenanceSteps;
        private SpaceFlightView? _spaceFlightView;
        private AreaSwitcherForm? _areaSwitcherForm;
        private EngineView? _engineView;

        public SpacecraftForm spacecraftForm => _spacecraftForm ??= new SpacecraftForm(driver);
        public SpacecraftView spacecraftView => _spacecraftView ??= new SpacecraftView(driver);
        public SpaceFlightForm spaceFlightForm => _spaceFlightForm ??= new SpaceFlightForm(driver);
        public SideMapForm sidemapForm => _sidemapForm ??= new SideMapForm(driver);
        public MaintenanceForm maintenanceForm => _maintenanceForm ??= new MaintenanceForm(driver);
        public BPFForm BPFForm => _bpfForm ??= new BPFForm(driver);
        public MaintenanceView maintenanceView => _maintenanceView ??= new MaintenanceView(driver);
        public MaintenanceTaskForm maintenanceTaskForm => _maintenanceTaskForm ??= new MaintenanceTaskForm(driver);
        public MaintenanceTasksSubgrid maintenanceTasksSubgrid => _maintenanceTaskSubgrid ??= new MaintenanceTasksSubgrid(driver);
        public EngineForm engineForm => _engineForm ??= new EngineForm(driver);
        public EnginesSubgrid engineSubgrid => _enginesSubgrid ??= new EnginesSubgrid(driver);
        public MaintenanceSteps maintenanceSteps => _maintenanceSteps ??= new MaintenanceSteps();
        public SpaceFlightView spaceFlightView => _spaceFlightView ??= new SpaceFlightView(driver);
        public AreaSwitcherForm areaSwitcherForm => _areaSwitcherForm ??= new AreaSwitcherForm(driver);
        public EngineView engineView => _engineView ??= new EngineView(driver);

        [Test]
        public void WhenSpacecraftWithLowStatusEnginesIsMaintained_ShouldReplenishEnginesAndSetStationedStatus()
        {
            AllureApi.Step("Navigate to spacecraft view and click New button", () =>
            {
                sidemapForm.ClickSidemapItem("Spacecrafts");
                spacecraftForm.ClickNewButtonFromToolBar();
            });

            var engineCount = 0;
            var regNumber = "";
            AllureApi.Step("Creating a military spacecraft and storing the max engine count and the reg number", () =>
            {
                SpacecraftSteps.CreateMilitarySpacecraft(spacecraftForm);
                regNumber = spacecraftForm.GetRegistrationNumber();

                engineCount = SpacecraftSteps.GetEngineCount(spacecraftForm);
            });

            var enginesStatus = new List<string>();
            AllureApi.Step("Add engines with 5% status then navigate to spaceflight form and create a spaceflight of more than 100 hours", () =>
            {
                SpacecraftSteps.AddNewEnginesToSpacecraft(engineCount, spacecraftForm, engineForm, engineSubgrid, "5", "Low Status Engine");
                enginesStatus = engineSubgrid.GetAllRecordsStatus();

                sidemapForm.ClickSidemapItem("Space Flights");
                spaceFlightForm.ClickNewButtonFromToolBar();

                CommonSteps.CreateSpaceflight(spaceFlightForm, sidemapForm, spacecraftView, "Test", 5);

            });

            AllureApi.Step("Navigate back to the spacecraft and verify notification for low status engines is displayed and the status decreased on all engines", () =>
            {
                sidemapForm.ClickSidemapItem("Spacecrafts");
                spacecraftView.OpenRecord(regNumber);

                AssertTrueWithRefresh(() => spacecraftForm.IsWarningNotificationDisplayed(), spacecraftForm, 30);
                AssertEqualWithRefresh(() => spacecraftForm.GetWarningNotificationText(), $"Warning: This spacecraft has {engineCount} engine(s) with low status. Please check the engines for maintenance.", spacecraftForm, 30);

                spacecraftForm.NavigateToEnginesTab();

                for (int i = 0; i < enginesStatus.Count; i++)
                {
                    var statusBefore = int.Parse(enginesStatus[i]);

                    AssertTrueWithRefresh(() => (int.Parse(engineSubgrid.GetRecordStatus(i + 1)) == statusBefore - 5) || (int.Parse(engineSubgrid.GetRecordStatus(i + 1)) == statusBefore - 3) || (int.Parse(engineSubgrid.GetRecordStatus(i + 1)) == statusBefore - 4) || (int.Parse(engineSubgrid.GetRecordStatus(i + 1)) == 0), spacecraftForm, 10, true);
                }

            });

            AllureApi.Step("Click Create Maintenance button and complete a successful maintenance", () =>
            {                
                maintenanceSteps.CreateMaintenanceWithCreateMaintenanceButton(spacecraftForm, maintenanceForm, BPFForm);
            });

            AllureApi.Step("Navigate back to spacecraft view and verify that after successful maintenance the spacecraft is in Stationed state", () =>
            {
                sidemapForm.ClickSidemapItem("Spacecrafts");
                AssertEqualWithRefresh(() => spacecraftView.GetRecordStatus(regNumber), "Stationed", spacecraftForm, 30);
            });

            AllureApi.Step("Open the spacecraft and verify the engines status is back to 100% and warning notification is not present", () =>
            {
                spacecraftView.OpenRecord(regNumber);

                AssertTrueWithRefresh(() => !spacecraftForm.IsWarningNotificationDisplayed(), spacecraftForm, 30);

                spacecraftForm.NavigateToEnginesTab();

                for (int i = 0; i < enginesStatus.Count; i++)
                {                  

                    AssertTrueWithRefresh(() => int.Parse(engineSubgrid.GetRecordStatus(i + 1)) == 100, spacecraftForm, 10, true);
                }
            });

            AllureApi.Step("Clear all test data(Engines, Spacecraft, Spaceflights and Maintenance records)", () =>
            {
                sidemapForm.ClickSidemapItem("Spacecrafts");
                spacecraftView.DeleteRecord(regNumber);

                sidemapForm.ClickSidemapItem("Space Flights");
                spaceFlightView.DeleteAllRecords();

                sidemapForm.ClickSidemapItem("Maintenances");
                maintenanceView.DeleteAllRecords();

                areaSwitcherForm.SelectArea("Engine Department");

                sidemapForm.ClickSidemapItem("Engines");

                engineView.DeleteAllRecordsWithName("Low Status Engine");

                
            });
        }
    }
}
