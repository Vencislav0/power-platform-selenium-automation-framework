using Automation_Framework.Framework.PowerApps.Constants;
using Automation_Framework.Framework.PowerApps.ElementWrappers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.SpacecraftManagementApp.Pages.Forms.Engine
{
    public class EngineForm : BaseForm
    {
        protected Lookup engineModelLookup;
        public EngineForm(IWebDriver driver) : base(driver, By.XPath(""), "Engine Form")
        {
            engineModelLookup = new Lookup(driver, By.XPath("//input[@aria-label='Engine Model, Lookup']"), LookupTypes.EngineModel, "Engine Model Lookup");
        }

        public void SelectEngineModel(string input)
        {
            engineModelLookup.EnterValue(input);
        }

    }
}
