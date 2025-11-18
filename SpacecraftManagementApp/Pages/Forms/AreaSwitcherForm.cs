using Automation_Framework.Framework.ElementWrappers;
using Automation_Framework.Framework.PowerApps.ElementWrappers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.SpacecraftManagementApp.Pages.Forms
{
    public class AreaSwitcherForm : BaseForm
    {
        public AreaSwitcherForm(IWebDriver driver) : base(driver, By.Id("areaSwitcherId"), "Area Switcher Form")
        {

        }


        public void SelectArea(string input)
        {
            ClickFormElement();
         
            var areaOption = new Label(_driver, By.XPath($"//li[contains(@title, '{input}')]"), $"{input} Area");

            areaOption.Click();
        }
    }
}
