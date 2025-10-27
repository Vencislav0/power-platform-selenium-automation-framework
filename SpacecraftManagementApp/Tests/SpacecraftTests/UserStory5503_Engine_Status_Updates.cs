using Allure.Net.Commons;
using Automation_Framework.Framework.PowerApps.Constants;
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
        private SpaceFlightForm _spaceFlightForm;
        
        public SpacecraftForm spacecraftForm => _spacecraftForm ??= new SpacecraftForm(driver);
        public SpacecraftView spacecraftView => _spacecraftView ??= new SpacecraftView(driver);
        public SideMapForm sidemapForm => _sidemapForm ??= new SideMapForm(driver);
        public SpaceFlightForm spaceFlightForm => _spaceFlightForm ??= new SpaceFlightForm(driver);

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

                AllureApi.Step("Save the space flight", () =>
                {
                    spaceFlightForm.ClickSaveAndCloseButtonFromToolBar();
                    sidemapForm.ClickSidemapItem("Spacecrafts");
                    spacecraftView.OpenRecord(regNumber);
                    Thread.Sleep(5000);
                });
            });
        }
    }
}
