using Automation_Framework.Framework.PowerApps.ElementWrappers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.SpacecraftManagementApp.Pages.Forms.Country
{
    public class CountryForm : BaseForm
    {
        public CountryForm(IWebDriver driver) : base(driver, By.XPath(""), "Country Form")
        {

        }


        public string GetCountryCodeValue()
        {
            return GetFieldValue("Country Code");
        }
    }
}
