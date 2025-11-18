using Automation_Framework.Framework.PowerApps.ElementWrappers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.SpacecraftManagementApp.Pages.Forms.Fleet
{
    public class FleetSubgrid : BaseSubgrid
    {
        public FleetSubgrid(IWebDriver driver) : base(driver, "Fleet Subgrid")
        {

        }
    }
}
