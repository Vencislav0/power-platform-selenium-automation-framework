using Allure.Net.Commons;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.Engine;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.Fleet;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.Spacecraft;
using Automation_Framework.SpacecraftManagementApp.Steps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.SpacecraftManagementApp.Tests.SpacecraftTests
{
    public class UserStory5568_Notification_On_Low_Status_Engines : BaseTest
    {
        private SpacecraftForm? _spacecraftForm;
        private SpacecraftView? _spacecraftView;
        private SideMapForm? _sidemapForm;
        private EnginesSubgrid? _engineSubgrid;       
        private EngineForm? _engineForm;        
        private AreaSwitcherForm? _areaSwitcherForm;
        private EngineView? _engineView;

        public SpacecraftForm spacecraftForm => _spacecraftForm ??= new SpacecraftForm(driver);
        public SpacecraftView spacecraftView => _spacecraftView ??= new SpacecraftView(driver);
        public SideMapForm sidemapForm => _sidemapForm ??= new SideMapForm(driver);
        public EnginesSubgrid engineSubgrid => _engineSubgrid ??= new EnginesSubgrid(driver);        
        public EngineForm engineForm => _engineForm ??= new EngineForm(driver);                
        public AreaSwitcherForm areaSwitcherForm => _areaSwitcherForm ??= new AreaSwitcherForm(driver);
        public EngineView engineView => _engineView ??= new EngineView(driver);

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void Test_AddEngines_WithLowStatus_ShouldNotify_FleetAndSpacecraft(int engineAmount)
        {
            AllureApi.Step("Navigating to Spacecraft View, and click new button", () =>
            {
                sidemapForm.ClickSidemapItem("Spacecrafts");
                spacecraftForm.ClickNewButtonFromToolBar();
            });

            var regNumber = "";
            AllureApi.Step("Creating a military spacecraft and storing the max engine count and the reg number", () =>
            {
                SpacecraftSteps.CreateMilitarySpacecraft(spacecraftForm);
                regNumber = spacecraftForm.GetRegistrationNumber();


            });

            AllureApi.Step("Navigate to engines tab and on the subrid add new engine with low status", () =>
            {
                SpacecraftSteps.AddNewEnginesToSpacecraft(engineAmount, spacecraftForm, engineForm, engineSubgrid, "0", "Low Status Engine");
            });

            AllureApi.Step("Verify a warning notification is displayed with the correct text and delete spacecraft and engines", () =>
            {
                Assert.That(spacecraftForm.IsWarningNotificationDisplayed(), Is.True);
                Assert.That(spacecraftForm.GetWarningNotificationText(), Is.EqualTo($"Warning: This spacecraft has {engineAmount} engine(s) with low status. Please check the engines for maintenance."));

                sidemapForm.ClickSidemapItem("Spacecrafts");
                spacecraftView.DeleteRecord(regNumber);

                areaSwitcherForm.SelectArea("Engine Department");
                sidemapForm.ClickSidemapItem("Engines");
                engineView.DeleteAllRecordsWithName("Low Status Engine");

            });

        }
    }
}
