using Automation_Framework.Framework.ElementWrappers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Automation_Framework.Framework.PowerApps.ElementWrappers
{
    public class BaseView
    {
        protected IWebDriver _driver;
        protected By _locator;
        protected string _name;
        protected string _recordLocator;
        protected Label testRecord;

        public BaseView(IWebDriver driver, By locator, string name)
        {
            _driver = driver;
            _locator = locator;
            _name = name;
            _recordLocator = "//div[contains(@aria-label, 'Press SPACE to')]";            
        }

        public void CheckRecordCheckbox(string recordName)
        {
            var checkbox = new Label(_driver, By.XPath($"{_recordLocator}[.//label[@aria-label='{recordName}']]//div[contains(@class, 'ms-Checkbox is')]"), $"{recordName} Checkbox");

            if (checkbox.GetAttribute("class").Contains("enabled"))
            {
                checkbox.Click();
            }
        }

        public void CheckRecordCheckbox(int index)
        {
            var checkbox = new Label(_driver, By.XPath($"({_recordLocator}[.//label[@aria-label]]//div[contains(@class, 'ms-Checkbox is')])[{index}]"), $"Checkbox on index: {index}");

            if (checkbox.GetAttribute("class").Contains("enabled"))
            {
                checkbox.Click();
            }
        }

        public bool IsRecordDisplayed(string name)
        {
            var record = new Label(_driver, By.XPath($"//label[@aria-label='${name}']/ancestor::div[@aria-label='Press SPACE to select this row.']"), $"{name} Record");

            return record.IsDisplayed();
        }

        public void OpenRecord(string name)
        {
            var record = new Label(_driver, By.XPath($"{_recordLocator}//label[@aria-label='{name}']"), $"{name} Record");

            record.DoubleClick();
        }

        public void OpenRecord(int index)
        {
            var record = new Label(_driver, By.XPath($"({_recordLocator}//label[@aria-label])[{index}]"), $"{index}st Record");

            record.DoubleClick();
        }

        public int GetRecordsCount()
        {
            var records = new ElementsCollection(_driver, By.XPath($"//label[@aria-label]/ancestor::div[contains(@aria-label, 'Press SPACE to')]"), $"{_name} Records");

            return records.Count();
        }

        public void DeleteRecord(string name)
        {
            CheckRecordCheckbox(name);
            PerformDelete();

        }
        public void DeleteAllRecords()
        {
            var recordsCount = GetRecordsCount();

            if(recordsCount <= 0)
            {
                return;
            }

            for(int i = 1; i < recordsCount; i++)
            {
                CheckRecordCheckbox(i);
            }
            PerformDelete();
        }

        public void PerformDelete()
        {
            var deleteButton = new Button(_driver, By.XPath("//button[@aria-label='Delete']"), "Delete Button Navigation bar");
            var notificationDeleteButton = new Button(_driver, By.XPath("//button[@title='Delete']"), "Notification Delete Button");

            Thread.Sleep(500);
            deleteButton.Click();
            notificationDeleteButton.Click();
        }
    }
}
