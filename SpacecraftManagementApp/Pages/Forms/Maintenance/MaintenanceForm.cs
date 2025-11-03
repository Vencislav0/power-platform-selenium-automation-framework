using Automation_Framework.Framework.PowerApps.ElementWrappers;
using OpenQA.Selenium;
using Automation_Framework.Framework.PowerApps.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
       
    }
}
