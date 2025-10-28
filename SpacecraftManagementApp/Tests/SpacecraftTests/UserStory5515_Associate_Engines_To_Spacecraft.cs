﻿using Allure.Net.Commons;
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
    public class UserStory5515_Associate_Engines_To_Spacecraft : BaseTest
    {
        private SpacecraftForm? _spacecraftForm;
        private SpacecraftView? _spacecraftView;
        private SideMapForm? _sidemapForm;
        private EnginesSubgrid? _engineSubgrid;
        private LookupRecordsForm? _lookupRecordsForm;
        private EngineForm? _engineForm;
        private FleetView? _fleetView;
        private FleetForm? _fleetForm;

        public SpacecraftForm spacecraftForm => _spacecraftForm ??= new SpacecraftForm(driver);
        public SpacecraftView spacecraftView => _spacecraftView ??= new SpacecraftView(driver);
        public SideMapForm sidemapForm => _sidemapForm ??= new SideMapForm(driver);
        public EnginesSubgrid engineSubgrid => _engineSubgrid ??= new EnginesSubgrid(driver);
        public LookupRecordsForm lookupRecordsForm => _lookupRecordsForm ??= new LookupRecordsForm(driver);
        public EngineForm engineForm => _engineForm ??= new EngineForm(driver);
        public FleetView fleetView => _fleetView ??= new FleetView(driver);
        public FleetForm fleetForm => _fleetForm ??= new FleetForm(driver);

        [Test]
        public void Test_AddEnginesToSpacecraft_SameAmountAsModelEngineCount_AddsEnginesSuccessfuly()
        {
            AllureApi.Step("Navigating to Spacecraft View, and click new button", () =>
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

            AllureApi.Step("Navigate to engines tab and on the subrid add existing engines with the amount of the military model engine count", () =>
            {            
                SpacecraftSteps.AddEnginesToSpacecraft(engineCount, spacecraftForm, engineSubgrid, lookupRecordsForm);                            
            });

            AllureApi.Step("Verify engines were added successfuly to the subgrid and it contains the same number of engines as the model engine count then delete the spacecraft", () =>
            {
                Assert.That(engineSubgrid.GetRecordsCount(), Is.GreaterThan(0));
                Assert.That(engineSubgrid.GetRecordsCount(), Is.EqualTo(engineCount));

                sidemapForm.ClickSidemapItem("Spacecrafts");
                spacecraftView.DeleteRecord(regNumber);
            });

        }

        [Test]
        public void Test_AddEnginesToSpacecraft_MoreEnginesThanAllowed_ThrowsError()
        {
            AllureApi.Step("Navigating to Spacecraft View, and click new button", () =>
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

            AllureApi.Step("Navigate to engines tab and on the subrid add more existing engines than the model allows", () =>
            {
                SpacecraftSteps.AddEnginesToSpacecraft(engineCount + 1, spacecraftForm, engineSubgrid, lookupRecordsForm);
            });

            AllureApi.Step("Verify error message is shown and has correct text and delete the spacecraft", () =>
            {
                Assert.That(spacecraftForm.IsErrorMessageDisplayed(), Is.True);
                Assert.That(spacecraftForm.GetErrorMessageText(), Is.EqualTo($"Cannot associate engine to spacecraft. The spacecraft model allows a maximum of {engineCount} engines, but the spacecraft already has {engineCount} engines associated."));
                spacecraftForm.ClickErrorOkayButton();

                sidemapForm.ClickSidemapItem("Spacecrafts");
                spacecraftView.DeleteRecord(regNumber);
            });

        }

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
                SpacecraftSteps.AddNewEnginesToSpacecraft(engineAmount, spacecraftForm, engineForm, engineSubgrid, "0");
            });

            AllureApi.Step("Verify a warning notification is displayed with the correct text and delete spacecraft", () =>
            {
                Assert.That(spacecraftForm.IsWarningNotificationDisplayed(), Is.True);
                Assert.That(spacecraftForm.GetWarningNotificationText(), Is.EqualTo($"Warning: This spacecraft has {engineAmount} engine(s) with low status. Please check the engines for maintenance."));

                sidemapForm.ClickSidemapItem("Spacecrafts");
                spacecraftView.DeleteRecord(regNumber);
            });
           
        }
    }
}
