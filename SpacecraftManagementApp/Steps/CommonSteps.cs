using Allure.Net.Commons;
using Automation_Framework.Framework.PowerApps.Constants;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.Engine;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.Spacecraft;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.SpaceFlight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.SpacecraftManagementApp.Steps
{
    public static class CommonSteps
    {
        public static void CreateEngine(EngineForm engineForm, string engineStatusInput = "100", string engineName = "Test Engine")
        {
            AllureApi.Step("Fill the engine name and status then select engine model", () =>
            {
                engineForm.FillName(engineName);
                engineForm.ChangeStatusField(engineStatusInput);
                engineForm.SelectEngineModel("AeroFan X3");
            });

            AllureApi.Step("Save the engine and close it", () =>
            {
                engineForm.ClickSaveButtonFromToolBar(true);
                engineForm.ClickSaveAndCloseButtonFromToolBar();
            });

        }

        public static void CreateSpaceflight(SpaceFlightForm spaceFlightForm, SideMapForm sidemapForm, SpacecraftView spacecraftView,string spacecraftName ,int durationDays)
        {
            AllureApi.Step($"Creating Space Flight with spacecraft: {spacecraftName} with duration: {durationDays} days.", () =>
            {
                
                AllureApi.Step("Filling name and selecting spacecraft, mission type, launch port and landing port", () =>
                {
                    spaceFlightForm.FillName("Test Spaceflight");
                    spaceFlightForm.SelectSpacecraft(spacecraftName);
                    spaceFlightForm.SelectMissionType(MissionTypeChoices.Cargo);
                    spaceFlightForm.SelectLaunchSpaceport("Starlink Terminal");
                    spaceFlightForm.SelectLandingSpaceport("Sofia Spaceport");
                });

                AllureApi.Step("Select the start and end date by more than 5 days to generate duration more than 100 hours", () =>
                {
                    spaceFlightForm.FillStartDate(DateTime.Now.Date.ToString("MM/dd/yyyy"));
                    spaceFlightForm.FillEndDate(DateTime.Now.Date.AddDays(durationDays).ToString("MM/dd/yyyy"));
                });

                AllureApi.Step("Save the space flight", () =>
                {
                    spaceFlightForm.ClickSaveButtonFromToolBar(true);                                    
                });

            });
        }
    }
}
