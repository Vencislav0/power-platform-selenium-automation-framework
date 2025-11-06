using Allure.Net.Commons;
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

namespace Automation_Framework.SpacecraftManagementApp.Tests.SpacecraftTests
{
    public class UserStory5654_Create_Maintenance_Button : BaseTest
    {
        private SpacecraftForm? _spacecraftForm;
        private SpacecraftView? _spacecraftView;
        private SpaceFlightForm? _spaceFlightForm;
        private SideMapForm? _sidemapForm;
        private MaintenanceForm? _maintenanceForm;
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
        public EngineForm engineForm => _engineForm ??= new EngineForm(driver);
        public EnginesSubgrid engineSubgrid => _enginesSubgrid ??= new EnginesSubgrid(driver);
        public MaintenanceSteps maintenanceSteps => _maintenanceSteps ??= new MaintenanceSteps();
        public SpaceFlightView spaceFlightView => _spaceFlightView ??= new SpaceFlightView(driver);
        public AreaSwitcherForm areaSwitcherForm => _areaSwitcherForm ??= new AreaSwitcherForm(driver);
        public EngineView engineView => _engineView ??= new EngineView(driver);
        [Test]
        public void Test_CreateMaintenanceButton_GeneratesMaintenanceForSpacecraftWithGeneratedName()
        {
            var regNumber = "";
            try
            {
                AllureApi.Step("Navigating to spacecraft view and creating a military spacecraft", () =>
                {
                    sidemapForm.ClickSidemapItem("Spacecrafts");
                    spacecraftForm.ClickNewButtonFromToolBar();
                    SpacecraftSteps.CreateMilitarySpacecraft(spacecraftForm);
                    regNumber = spacecraftForm.GetRegistrationNumber();
                });

                AllureApi.Step("Clicking the Create Maintenance Button", () =>
                {
                    var titleBefore = spacecraftForm.GetFormTitleText();

                    spacecraftForm.ClickCreateMaintenanceButton();

                    spacecraftForm.WaitUntilFormTitleChanges(titleBefore);
                    
                });

                AllureApi.Step("Verify the maintenance creation by checking the form title text verifying the redirection and the name field has generated text", () =>
                {
                    Assert.That(spacecraftForm.GetFormTitleText(), Does.Contain("Maintenance"));
                    Assert.That(spacecraftForm.GetFieldValue("Name"), Does.Contain("Maintenance for"));
                });

                AllureApi.Step("Verify spacecraft status is In Maintenance and a maintenance record is present on the maintenance view", () =>
                {
                    sidemapForm.ClickSidemapItem("Spacecrafts");
                    AssertEqualWithRefresh(() => spacecraftView.GetRecordStatus(regNumber), "In Maintenance", spacecraftForm, 30);

                    sidemapForm.ClickSidemapItem("Maintenances");

                    Assert.That(maintenanceView.IsRecordDisplayed(1), Is.True);


                });
            }
            catch (Exception ex)
            {
                Assert.Fail($"Test Failed. {ex}");
            }
            finally
            {
                TestCleanup(() => {
                    sidemapForm.ClickSidemapItem("Maintenances");
                    maintenanceView.DeleteAllRecords();

                    sidemapForm.ClickSidemapItem("Spacecrafts");
                    spacecraftView.DeleteRecord(regNumber);
                });
            }
            
        }
    }
}
