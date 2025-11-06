using Allure.Net.Commons;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.Maintenance;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.Spacecraft;
using Automation_Framework.Framework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation_Framework.SpacecraftManagementApp.Tests;
using Automation_Framework.Framework.PowerApps.ElementWrappers;
using Automation_Framework.Framework.Constants;

namespace Automation_Framework.SpacecraftManagementApp.Steps
{    
    public class MaintenanceSteps : BaseTest
    {        
        public void CreateMaintenanceWithCreateMaintenanceButton(SpacecraftForm spacecraftForm, BaseSubgrid subgrid, MaintenanceForm maintenanceForm, BPFForm BPFForm, string finalOutcome)
        {
            AllureApi.Step("Click the Create Maintenance button", () =>
            {
                var formTitle = spacecraftForm.GetFormTitleText();
                spacecraftForm.ClickCreateMaintenanceButton();

                spacecraftForm.WaitUntilFormTitleChanges(formTitle);
            });

            var lv2CategoriesCount = 0;
            AllureApi.Step("Choosing Incident Category", () =>
            {
                maintenanceForm.SelectIncidentCategory("Engine Failure");
            });

            AllureApi.Step("Saving the record and navigating to the chosen incident category", () =>
            {
                var formTitle = maintenanceForm.GetFormTitleText();

                maintenanceForm.ClickSaveButtonFromToolBar(true);
                maintenanceForm.ClickOnIncidentCategoryRecord();
                maintenanceForm.WaitUntilFormTitleChanges(formTitle);
            });

            AllureApi.Step("Getting the lv 2 incident categories and navigating back to maintenance form", () =>
            {
                lv2CategoriesCount = maintenanceForm.GetIncidentCategoryLv2Amount();

                maintenanceForm.ClickSaveButtonFromToolBar(true);
                maintenanceForm.ClickSaveAndCloseButtonFromToolBar();
            });

            AllureApi.Step("Completing the Triage Stage and moving on to the Repair stage", () =>
            {
                BPFForm.ClickStageButton("Triage");
                BPFForm.ClickNextStageButton();
            });

            AllureApi.Step("Fill Resolution Summary field and select Estimated Completion Date", () =>
            {
                maintenanceForm.FillResolutionSummary("Testing");
                maintenanceForm.SelectEstimatedCompletionDate(DateTime.Now.Date.ToString("MM/dd/yyyy"));
            });

            AllureApi.Step("Open the Repair tab and verify the generated maintenance tasks are the same number as the Engine Failure lv 2 tasks", () =>
            {
                maintenanceForm.SwitchToRepairTab();

                AssertEqualWithRefresh(() => maintenanceForm.GetMaintenaceTasksAmount(Timeouts.DEFAULT_INTERVAL), lv2CategoriesCount, subgrid, maintenanceForm, 30, true);
            });

            AllureApi.Step("Complete each maintenance task then complete the repair stage", () =>
            {
                var tasksAmount = maintenanceForm.GetMaintenaceTasksAmount();

                for (int i = 1; i <= tasksAmount; i++)
                {
                    maintenanceForm.CompleteMaintenanceTask(i);
                }

                BPFForm.ClickStageButton("Repair");
                BPFForm.ClickNextStageButton();
            });


            AllureApi.Step($"Complete the Close stage by choosing final outcome {finalOutcome} and actual completion date", () =>
            {
                maintenanceForm.SelectActualCompletionDate(DateTime.Now.Date.ToString("MM/dd/yyyy"));
                maintenanceForm.SelectFinalOutcome(finalOutcome);
                BPFForm.ClickFinishButton();
            });
        }
    }
}
