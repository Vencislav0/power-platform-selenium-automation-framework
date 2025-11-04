using Automation_Framework.Framework.PowerApps.ElementWrappers;
using OpenQA.Selenium;
using Automation_Framework.Framework.PowerApps.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation_Framework.Framework.ElementWrappers;
using Automation_Framework.Framework.Constants;
using Automation_Framework.Framework.Logging;

namespace Automation_Framework.SpacecraftManagementApp.Pages.Forms.Maintenance
{
    public class MaintenanceForm : BaseForm
    {
        protected Lookup incidentCategoryLookup;
        protected Lookup spacecraftLookup;
        protected Date estimatedCompletionDate;
        protected Date actualCompletionDate;
        protected Choice finalOutcomeChoice;
        protected MaintenanceTasksSubgrid maintenanceTasksSubgrid;
        protected Label incompleteTasksErrorMessage;
        protected Button dialogPopupOkButton;
        protected MaintenanceTaskForm maintenanceTaskForm;
        protected IncidentCategorySubgrid incidentCategorySubgrid;
        public MaintenanceForm(IWebDriver driver) : base(driver, By.XPath("//div[@data-id='grid-container']"), "Maintenance Form") 
        {
            incidentCategoryLookup = new Lookup(driver, By.XPath("//input[@aria-label='Incident Category, Lookup']"), "incidentCategory", "Incident Category Lookup");
            spacecraftLookup = new Lookup(driver, By.XPath("//input[@aria-label='Spacecraft, Lookup']"), "spacecraft", "Spacecraft Lookup");
            estimatedCompletionDate = new Date(driver, By.XPath("//input[contains(@aria-label, 'Estimated Completion Date')]"), "Estimated Completion Date");
            actualCompletionDate = new Date(driver, By.XPath("//input[contains(@aria-label, 'Actual Completion Date')]"), "Actual Completion Date");
            finalOutcomeChoice = new Choice(driver, By.XPath("//button[@aria-label='Final Outcome']"), "Final Outcome Choice");
            maintenanceTasksSubgrid = new MaintenanceTasksSubgrid(driver);
            maintenanceTaskForm = new MaintenanceTaskForm(driver);
            dialogPopupOkButton = new Button(driver, By.XPath("(//div[contains(@id, 'modalDialogContentContainer')])[1]//button[@data-id='okButton']"), "Dialog Popup OK Button");
            incompleteTasksErrorMessage = new Label(driver, By.XPath("(//div[contains(@id, 'modalDialogContentContainer')])[1]//span[@data-id='dialogMessageText']"), "Incomplete Tasks Error Message");
            incidentCategorySubgrid = new IncidentCategorySubgrid(driver);
        }


        public void FillResolutionSummary(string input)
        {
            CompleteField("Resolution Summary", input);
        }

        public void SelectEstimatedCompletionDate(string input)
        {
            estimatedCompletionDate.CompleteDateField(input);
        }

        public void SelectActualCompletionDate(string input)
        {
            actualCompletionDate.CompleteDateField(input);
        }

        public void SelectFinalOutcome(string input)
        {
            finalOutcomeChoice.SelectChoice(input);
        }

        public void SelectIncidentCategory(string input)
        {
            incidentCategoryLookup.EnterValue(input);
        }

        public void SelectSpacecraft(string input)
        {
            spacecraftLookup.EnterValue(input);
        }

        public void ClickOnIncidentCategoryRecord()
        {
            incidentCategoryLookup.ClickRecordOnLookup();
        }
      

        public int GetMaintenaceTasksAmount()
        {
            return maintenanceTasksSubgrid.GetRecordsCount();
        }

        public List<string> GetAllMaintenanceTasksStatus()
        {
            return maintenanceTasksSubgrid.GetAllRecordsStatus();
        }

        public int GetIncidentCategoryLv2Amount()
        {           
            return incidentCategorySubgrid.GetRecordsCount();
        }

        public void SwitchToRepairTab()
        {
            SwitchToTab("Repair");
        }

        public void CompleteMaintenanceTask(string taskName)
        {
            maintenanceTasksSubgrid.OpenRecord(taskName);

            maintenanceTaskForm.SelectStatus(MaintenanceTasksStatusChoices.Completed);
            maintenanceTaskForm.ClickSaveAndCloseButtonFromToolBar();
        }

        public void CompleteMaintenanceTask(int index)
        {
            maintenanceTasksSubgrid.OpenRecord(index);

            maintenanceTaskForm.SelectStatus(MaintenanceTasksStatusChoices.Completed);
            maintenanceTaskForm.ClickSaveAndCloseButtonFromToolBar();
        }

        public void ClickDialogPopupOKButton()
        {
            while (true)
            {
                if (dialogPopupOkButton.IsDisplayed(Timeouts.DEFAULT_INTERVAL))
                {
                    Logger.Debug("No more popups detected.");
                    dialogPopupOkButton.Click();
                    Thread.Sleep(500);
                }
                else
                {
                    Logger.Debug("No more popups detected.");
                    break;
                }               
            }           
        }

        public bool IsIncompleteTasksErrorMessageDisplayed()
        {
            return incompleteTasksErrorMessage.IsDisplayed(Timeouts.DEFAULT_INTERVAL);
        }

        public string GetIncompleteTasksErrorMessageText()
        {
            return incompleteTasksErrorMessage.GetText();
        }
       
    }
}
