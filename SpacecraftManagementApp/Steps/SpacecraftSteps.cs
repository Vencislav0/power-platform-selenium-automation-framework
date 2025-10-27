using Allure.Net.Commons;
using Automation_Framework.Framework.PowerApps.Constants;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.Spacecraft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.SpacecraftManagementApp.Steps
{
    public static class SpacecraftSteps
    {

        public static void CreateMilitarySpacecraft(SpacecraftForm spacecraftForm)
        {

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
                spacecraftForm.SelectSpacecraftModel("Military Model");
            });

            AllureApi.Step("Selecting Is Armed? option to Yes", () =>
            {
                spacecraftForm.SelectIsArmed(IsArmedChoices.Yes);
            });

            AllureApi.Step("Selecting Fleet Military Fleet", () =>
            {
                spacecraftForm.SelectFleet("Military Fleet");
            });

            AllureApi.Step("Saving the spacecraft", () =>
            {
                spacecraftForm.ClickSaveButtonFromToolBar(true);
            });

        }

        public static int GetEngineCount(SpacecraftForm spacecraftForm)
        {
            var engineCount = 0;   
                if (int.TryParse(spacecraftForm.GetFieldValue("Engine Count"), out int result))
                {
                    engineCount = result;
                }
                else
                {
                    throw new InvalidOperationException("Couldn't parse the engine count something went wrong.");
                }

                return engineCount;          
        }

        public static void AddEnginesToSpacecraft(int amount, SpacecraftForm spacecraftForm, EnginesSubgrid engineSubgrid, LookupRecordsForm lookupRecordsForm)
        {
            AllureApi.Step($"Adding {amount} engines to spacecraft", () =>
            {
                spacecraftForm.NavigateToEnginesTab();

                engineSubgrid.clickAddExistingRecordButton();
                lookupRecordsForm.ClickAllRecordsButton();
                for (int i = 1; i <= amount; i++)
                {
                    lookupRecordsForm.addRecord(1);
                }
                lookupRecordsForm.ClickAddButton();
            });
        }

        public static void DeleteSpacecraft(SpacecraftForm spacecraftForm, SideMapForm sidemapForm, SpacecraftView spacecraftView, string registrationNumber)
        {
            sidemapForm.ClickSidemapItem("Spacecrafts");
            spacecraftView.DeleteRecord(registrationNumber);
        }

        public static void SaveVerifyErrorMessageAndClose(SpacecraftForm spacecraftForm, string expectedMessage)
        {
            AllureApi.Step("Save and verify Error Message is displayed with the correct text and close it", () =>
            {
                spacecraftForm.ClickSaveButtonFromToolBar(false);
                Assert.That(spacecraftForm.IsErrorMessageDisplayed(), Is.True, "Error Message was not visible within the time frame.");
                Assert.That(spacecraftForm.GetErrorMessageText(), Is.EqualTo(expectedMessage), "Incorrect Error Message displayed.");

                spacecraftForm.ClickErrorOkayButton();
            });
        }

        public static void UpdateRegistrationNumber(SpacecraftForm spacecraftForm, string input, bool correctDataEntered)
        {
            if (correctDataEntered)
            {
                AllureApi.Step("Update the registration number with correct input, verify update is successful", () =>
                {
                    spacecraftForm.ChangeRegistrationNumber(input);
                    spacecraftForm.ClickSaveButtonFromToolBar(true);                    
                });
            }
            else
            {
                AllureApi.Step("Update the registration number with invalid input, verify error message and close it", () =>
                {
                    spacecraftForm.ChangeRegistrationNumber(input);
                    SaveVerifyErrorMessageAndClose(spacecraftForm, "Registration number format is invalid. Expected: CC-XXX or CC-XXXX Upper Case Letters(A to Z 0 to 10) .");
                });
            }
        }
    }
}
