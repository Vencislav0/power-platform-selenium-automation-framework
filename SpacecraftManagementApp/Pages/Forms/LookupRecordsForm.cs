using Automation_Framework.Framework.ElementWrappers;
using Automation_Framework.Framework.PowerApps.Constants;
using Automation_Framework.Framework.PowerApps.ElementWrappers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.SpacecraftManagementApp.Pages.Forms
{
    public class LookupRecordsForm : BaseForm
    {
        protected Lookup selectRecordLookup;
        protected Button addButton;
        protected Button allRecordsButton;
        public LookupRecordsForm(IWebDriver driver) : base(driver, By.XPath(""), "Lookup Records Form")
        {
            selectRecordLookup = new Lookup(_driver, By.XPath("//input[contains(@aria-label, 'Select record')]"), LookupTypes.SelectRecord, "Select Record Lookup");
            addButton = new Button(_driver, By.XPath("//button[@title='Add']"), "Add Button");
            allRecordsButton = new Button(_driver, By.XPath("//a[..//span[text()='All records']]"), "All records Button");
        }

        public void SelectRecord(string input)
        {
            selectRecordLookup.EnterValue(input);
        }

        public void ClickAddButton()
        {
            addButton.Click();
        }

        public void ClickAllRecordsButton()
        {
            allRecordsButton.Click();
        }

        public void addRecord(int index)
        {
            var record = new Label(_driver, By.XPath($"(//li[contains(@data-id, 'MscrmControls') and @aria-describedBy])[{index}]"), $"Lookup Record: {index}");

            record.Click();
        }
        

    }
}
