using Allure.Net.Commons;
using Automation_Framework.Framework.PowerApps.Constants;
using Automation_Framework.Framework.Utilities;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.Country;
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
    public class UserStory5503_Engine_Status_Updates : BaseTest
    {
        private SpacecraftForm? _spacecraftForm;
        private SpacecraftView? _spacecraftView;
        private SideMapForm? _sidemapForm;
        private SpaceFlightForm? _spaceFlightForm;
        private LookupRecordsForm? _recordsForm;
        private EnginesSubgrid? _engineSubgrid;
        private CountryForm? _countryForm;
        public EnginesSubgrid engineSubgrid => _engineSubgrid ??= new EnginesSubgrid(driver);

        public SpacecraftForm spacecraftForm => _spacecraftForm ??= new SpacecraftForm(driver);
        public SpacecraftView spacecraftView => _spacecraftView ??= new SpacecraftView(driver);
        public SideMapForm sidemapForm => _sidemapForm ??= new SideMapForm(driver);
        public SpaceFlightForm spaceFlightForm => _spaceFlightForm ??= new SpaceFlightForm(driver);
        public LookupRecordsForm recordsForm => _recordsForm ??= new LookupRecordsForm(driver);
        public CountryForm countryForm => _countryForm ??= new CountryForm(driver);

        [Test]
        public void Test_TotalFlightHours_MoreThan100_UpdatesEngineStatus()
        {
            AllureApi.Step("Navigating to Spacecraft View, and click new button", () =>
            {
                sidemapForm.ClickSidemapItem("Spacecrafts");
                spacecraftForm.ClickNewButtonFromToolBar();
            });

            var regNumber = "";
            AllureApi.Step("Create a spacecraft and save the registration number for later use", () =>
            {
                SpacecraftSteps.CreateMilitarySpacecraft(spacecraftForm);
                regNumber = spacecraftForm.GetRegistrationNumber();
            });

            var enginesStatus = new List<string>();

            AllureApi.Step("Add 2 existing engines to the spacecraft and store the engines status for later comparison", () =>
            {
                SpacecraftSteps.AddEnginesToSpacecraft(2, spacecraftForm, engineSubgrid, recordsForm);
                enginesStatus = engineSubgrid.GetAllRecordsStatus();
            });

            AllureApi.Step("Create Space Flight with this spacecraft and set the duration higher than 100 hours", () =>
            {
                AllureApi.Step("Navigating to Spaceflight View, and click new button", () =>
                {
                    sidemapForm.ClickSidemapItem("Space Flights");
                    spaceFlightForm.ClickNewButtonFromToolBar();
                });

                AllureApi.Step("Filling name and selecting spacecraft, mission type, launch port and landing port", () =>
                {
                    spaceFlightForm.FillName("Test Spaceflight");
                    spaceFlightForm.SelectSpacecraft("Test");
                    spaceFlightForm.SelectMissionType(MissionTypeChoices.Cargo);
                    spaceFlightForm.SelectLaunchSpaceport("Starlink Terminal");
                    spaceFlightForm.SelectLandingSpaceport("Sofia Spaceport");
                });

                AllureApi.Step("Select the start and end date by more than 5 days to generate duration more than 100 hours", () =>
                {
                    spaceFlightForm.FillStartDate(DateTime.Now.Date.ToString("MM/dd/yyyy"));
                    spaceFlightForm.FillEndDate(DateTime.Now.Date.AddDays(5).ToString("MM/dd/yyyy"));
                });

                AllureApi.Step("Save the space flight and navigate back to the spacecraft", () =>
                {
                    spaceFlightForm.ClickSaveButtonFromToolBar(true);
                    sidemapForm.ClickSidemapItem("Spacecrafts");
                    spacecraftView.OpenRecord(regNumber);                    
                });
               
            });

            AllureApi.Step("Open Engines tab and verify the associated engines's status is lower by 3-5% and delete the spacecraft", () =>
            {
                spacecraftForm.NavigateToEnginesTab();

                for (int i = 0; i < enginesStatus.Count; i++)
                {
                    var statusBefore = int.Parse(enginesStatus[i]);                   
                    
                    AssertTrueWithRefresh(() => (int.Parse(engineSubgrid.GetRecordStatus(i + 1)) == statusBefore - 5) || (int.Parse(engineSubgrid.GetRecordStatus(i + 1)) == statusBefore - 3) || (int.Parse(engineSubgrid.GetRecordStatus(i + 1)) == statusBefore - 4) || (int.Parse(engineSubgrid.GetRecordStatus(i + 1)) == 0), spacecraftForm, 10, true);
                }

                sidemapForm.ClickSidemapItem("Spacecrafts");
                spacecraftView.DeleteRecord(regNumber);
            });



        }
    }
}
