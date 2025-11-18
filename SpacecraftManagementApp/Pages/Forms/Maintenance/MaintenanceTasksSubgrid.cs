using Automation_Framework.Framework.PowerApps.ElementWrappers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.SpacecraftManagementApp.Pages.Forms.Maintenance
{
    public class MaintenanceTasksSubgrid : BaseSubgrid
    {
        public MaintenanceTasksSubgrid(IWebDriver driver) : base(driver, "Maintenance Tasks Subgrid")
        {

        }
    }
}
