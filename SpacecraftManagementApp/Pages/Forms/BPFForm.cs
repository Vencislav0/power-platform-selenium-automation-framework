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
    public class BPFForm : BaseForm
    {
       public BPFForm(IWebDriver driver) : base(driver, By.XPath(""), "Business Process Flow Form")
        {

        }


        public void ClickStageButton(string stageTitle)
        {
            var stageButton = new Button(_driver, By.XPath($"//div[@title='{stageTitle}']/div"), $"{stageTitle} Stage");

            stageButton.Click();
        }

        public void ClickNextStageButton(bool shouldWait = true)
        {
            ClickButton("Next Stage");
            if (shouldWait)
            {
                customWaits.WaitUntilRecordSaved();
            }            
        }

        public void ClickBackButton()
        {
            ClickButton("Back");
            customWaits.WaitUntilRecordSaved();
        }

        public void ClickFinishButton()
        {
            ClickButton("Finish");
            customWaits.WaitUntilRecordSaved();
        }

        public void ClickSetActiveButton()
        {
            ClickButton("Set Active");
            customWaits.WaitUntilRecordSaved();
        }
    }
}
