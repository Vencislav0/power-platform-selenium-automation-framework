using Automation_Framework.Framework.PowerApps.Constants;
using Automation_Framework.Framework.PowerApps.ElementWrappers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.SpacecraftManagementApp.Pages.Forms.Maintenance
{
    
    public class MaintenanceTaskForm : BaseForm
    {
        protected Choice statusChoice;
       public MaintenanceTaskForm(IWebDriver driver) : base(driver, By.XPath("//div[@data-id='grid-container']"), "Maintenance Form")
        {
            statusChoice = new Choice(driver, By.XPath("//button[@aria-label='Status']"), "Status Choice");
        }

        public void SelectStatus(MaintenanceTasksStatusChoices choice)
        {
            statusChoice.SelectChoice(choice.ToString());
        }
    }
}
