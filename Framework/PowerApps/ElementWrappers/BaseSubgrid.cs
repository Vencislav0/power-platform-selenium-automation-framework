using Automation_Framework.Framework.Constants;
using Automation_Framework.Framework.ElementWrappers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.Framework.PowerApps.ElementWrappers
{
    public class BaseSubgrid
    {
        protected IWebDriver _driver;
        protected Label _locator;
        protected string _name;
        protected string _subgridLocator = "//div[contains(@data-id, 'dataSetRoot')]";

        public BaseSubgrid(IWebDriver driver, string name) 
        { 
            _driver = driver;
            _locator = new Label(_driver, By.XPath(_subgridLocator), "Subgrid Form");
            _name = name;
        }

        public void CheckRecordCheckbox(string recordName)
        {
            var checkbox = new Label(_driver, By.XPath($"{_subgridLocator}[.//span[text()='{recordName}']]//div[contains(@class, 'ms-Checkbox is') and contains(@class, 'Row')]"), $"{recordName} Checkbox");

            if (checkbox.GetAttribute("class").Contains("enabled"))
            {
                checkbox.Click();
            }
        }

        public void CheckRecordCheckbox(int index)
        {
            var checkbox = new Label(_driver, By.XPath($"({_subgridLocator}[.//span]//div[contains(@class, 'ms-Checkbox is') and contains(@class, 'Row')])[{index}]"), $"Checkbox on index: {index}");

            if (checkbox.GetAttribute("class").Contains("enabled"))
            {
                checkbox.Click();
            }
        }

        public bool IsRecordDisplayed(string recordName)
        {
            var record = new Label(_driver, By.XPath($"//span[text()='{recordName}']/ancestor::div[contains(@aria-label, 'Press SPACE to')]"), $"{recordName} Record");

            return record.IsDisplayed();
        }

        public void OpenRecord(string recordName)
        {
            var record = new Label(_driver, By.XPath($"{_subgridLocator}//span[text()='{recordName}']"), $"{recordName} Record");

            record.Click();
        }

        public void OpenRecord(int index)
        {
            var record = new Label(_driver, By.XPath($"(//div[contains(@aria-label, 'Press SPACE to')])[{index}]//div[contains(@col-id, 'space')][1]"), $"Record at index: {index}");

            record.Click();
        }

        public string GetRecordStatus(int index)
        {
            var recordStatus = new Label(_driver, By.XPath($"(//div[.//label[@aria-label]]/following-sibling::div[contains(@col-id,'space_statuscode')])[{index}]//label"), $"Record on index: {index} Status");

            if (recordStatus.IsDisplayed(Timeouts.SHORT))
            {
                return recordStatus.GetAttribute("aria-label");
            }
            return "";
        }

        public string GetRecordStatus(string recordName)
        {
            var recordStatus = new Label(_driver, By.XPath($"//div[.//span[text()='{recordName}']]/following-sibling::div[contains(@col-id,'space_statuscode')]//label"), $"{recordName} Record Status");

            return recordStatus.GetAttribute("aria-label");
        }
    }
}
