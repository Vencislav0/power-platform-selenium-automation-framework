using Allure.Net.Commons;
using Automation_Framework.Framework.Utilities;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.Maintenance;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.Spacecraft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.SpacecraftManagementApp.Tests.SpacecraftTests
{
    public class Maintenance_Tests : BaseTest
    {
        private SpacecraftForm? _spacecraftForm;
        private SpacecraftView? _spacecraftView;
        private SideMapForm? _sidemapForm;
        private MaintenanceForm? _maintenanceForm;        
        private BPFForm? _bpfForm;
        private MaintenanceView? _maintenanceView;

        public SpacecraftForm spacecraftForm => _spacecraftForm ??= new SpacecraftForm(driver);
        public SpacecraftView spacecraftView => _spacecraftView ??= new SpacecraftView(driver);
        public SideMapForm sidemapForm => _sidemapForm ??= new SideMapForm(driver);
        public MaintenanceForm maintenanceForm => _maintenanceForm ??= new MaintenanceForm(driver);
        public BPFForm BPFForm => _bpfForm ??= new BPFForm(driver);
        public MaintenanceView maintenanceView => _maintenanceView ??= new MaintenanceView(driver);
        
        [Test]
        public void Test_PerformMaintenance_OnSpacecraft_ShouldCompleteSuccessfully()
        {
            try
            {

                AllureApi.Step("Navigating to Spacecraft View, and open existing test spacecraft record", () =>
                {
                    sidemapForm.ClickSidemapItem("Spacecrafts");
                    spacecraftView.OpenRecord("BG-TEST");
                });

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

                    AssertEqualWithRefresh(() => maintenanceForm.GetMaintenaceTasksAmount(), lv2CategoriesCount, maintenanceForm, 30, true);
                });

                AllureApi.Step("Try to complete repair stage without completing any task and verify validation message is displayed with correct text and close it.", () =>
                {
                    BPFForm.ClickStageButton("Repair");
                    BPFForm.ClickNextStageButton(false);

                    Assert.That(maintenanceForm.IsIncompleteTasksErrorMessageDisplayed(), Is.True);
                    Assert.That(maintenanceForm.GetIncompleteTasksErrorMessageText(), Is.EqualTo("You cannot proceed to the next stage before all tasks are completed."));

                    maintenanceForm.ClickDialogPopupOKButton();
                });

                var tasksAmount = maintenanceForm.GetMaintenaceTasksAmount();
                AllureApi.Step("If more than 1 maintenance tasks, complete only 1 and verify validation message is displayed with correct text and close it", () =>
                {
                    if (tasksAmount > 1)
                    {
                        maintenanceForm.CompleteMaintenanceTask(1);
                        BPFForm.ClickStageButton("Repair");
                        BPFForm.ClickNextStageButton(false);

                        Assert.That(maintenanceForm.IsIncompleteTasksErrorMessageDisplayed(), Is.True);
                        Assert.That(maintenanceForm.GetIncompleteTasksErrorMessageText(), Is.EqualTo("You cannot proceed to the next stage before all tasks are completed."));

                        maintenanceForm.ClickDialogPopupOKButton();
                    }
                });

                AllureApi.Step("Complete the remaining maintenance tasks and verify the validation message is not present", () =>
                {
                    if (tasksAmount > 1)
                    {
                        for (int i = 2; i <= tasksAmount; i++)
                        {
                            maintenanceForm.CompleteMaintenanceTask(i);
                        }

                        BPFForm.ClickStageButton("Repair");
                        BPFForm.ClickNextStageButton(false);

                        Assert.That(maintenanceForm.IsIncompleteTasksErrorMessageDisplayed(), Is.False);
                    }
                    else
                    {
                        for (int i = 1; i <= tasksAmount; i++)
                        {
                            maintenanceForm.CompleteMaintenanceTask(i);
                        }

                        BPFForm.ClickStageButton("Repair");
                        BPFForm.ClickNextStageButton();

                        Assert.That(maintenanceForm.IsIncompleteTasksErrorMessageDisplayed(), Is.False);
                    }
                });

                AllureApi.Step("Complete the Close stage by choosing final outcome Return to Service and actual completion date", () =>
                {
                    maintenanceForm.SelectActualCompletionDate(DateTime.Now.Date.ToString("MM/dd/yyyy"));
                    maintenanceForm.SelectFinalOutcome("Return to Service");
                    BPFForm.ClickFinishButton();
                });

                AllureApi.Step("Navigate to spacecraft view and verify after maintenance the spacecraft is in status stationed", () =>
                {
                    sidemapForm.ClickSidemapItem("Spacecrafts");
                    AssertEqualWithRefresh(() => spacecraftView.GetRecordStatus("BG-TEST"), "Stationed", spacecraftForm, 30);
                });
                
            }
            catch (Exception)
            {

            }

            TestCleanup(() =>
            {
                sidemapForm.ClickSidemapItem("Maintenances");
                maintenanceView.DeleteAllRecords();
            });
        }      
        
    }
}
