using Automation_Framework.Framework.Constants;
using Automation_Framework.Framework.ElementWrappers;
using Automation_Framework.Framework.Logging;
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
    public class BaseSubgrid
    {
        protected IWebDriver _driver;
        protected Label _locator;
        protected Button newRecordButton;
        protected Button addExistingRecordButton;
        protected Button overflowButton;
        protected Button refreshButton;
        protected Button editButton;
        protected string _name;
        protected string _subgridLocator = "//div[contains(@data-id, 'dataSetRoot')]";

        public BaseSubgrid(IWebDriver driver, string name)
        {
            _driver = driver;
            _locator = new Label(_driver, By.XPath(_subgridLocator), "Subgrid Form");
            editButton = new Button(_driver, By.XPath("//button[contains(@id, 'SubGridStandard') and @aria-label='Edit']"), $"{name} Edit Button");
            newRecordButton = new Button(_driver, By.XPath("//button[contains(@title, 'Add New')]"), $"New Record Button: {name}");
            overflowButton = new Button(_driver, By.XPath("//button[contains(@id, 'OverflowButton') and contains(translate(@id, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), 'subgrid')]"), $"Overflow Button: {name}");
            addExistingRecordButton = new Button(_driver, By.XPath("//button[contains(@title, 'exists')]"), $"Existing Record Button: {name}");
            refreshButton = new Button(_driver, By.XPath("//button[contains(@data-id, 'RefreshButton') and contains(@data-id, 'SubGrid')]"), $"Refresh Button: {name}");
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
            var record = new Label(_driver, By.XPath($"{_subgridLocator}//span[contains(text(), '{recordName}')]"), $"{recordName} Record");

            record.Click();
            editButton.Click();
        }

        public void OpenRecord(int index)
        {
            var record = new Label(_driver, By.XPath($"(//div[contains(@aria-label, 'Press SPACE to')])[{index}]//div[contains(@col-id, 'space')][1]"), $"Record at index: {index}");

            record.Click();    
            editButton.Click();
        }

        public string GetRecordStatus(int index)
        {
            var recordStatus = new Label(_driver, By.XPath($"(//div[.//label[@aria-label]]/following-sibling::div[contains(@col-id,'status')])[{index}]//label"), $"Record on index: {index} Status");

            if (recordStatus.IsDisplayed(Timeouts.SHORT))
            {
                return recordStatus.GetAttribute("aria-label");
            }
            return "";
        }

        public string GetRecordStatus(string recordName)
        {
            var recordStatus = new Label(_driver, By.XPath($"//div[.//span[text()='{recordName}']]/following-sibling::div[contains(@col-id,'status')]//label"), $"{recordName} Record Status");

            return recordStatus.GetAttribute("aria-label");
        }

        public int GetRecordsCount()
        {
            var records = new ElementsCollection(_driver, By.XPath($"//label[@aria-label]/ancestor::div[contains(@aria-label, 'Press SPACE to')]"), $"{_name} Records");

            return records.Count();
        }

        public int GetRecordsCount(TimeSpan timeout)
        {
            var records = new ElementsCollection(_driver, By.XPath($"//label[@aria-label]/ancestor::div[contains(@aria-label, 'Press SPACE to')]"), $"{_name} Records");

            return records.Count(timeout);
        }

        public List<string> GetAllRecordsStatus()
        {
            var recordsCount = GetRecordsCount();
            var statuses = new List<string>();

            for (int i = 1; i <= recordsCount; i++)
            {               
                statuses.Add(GetRecordStatus(i));
            }

            Logger.Debug($"Status of all records[{string.Join(',', statuses)}]");
            return statuses;
        }

        public void ClickNewRecordButton()
        {
            newRecordButton.Click();            
        }

        public void ClickAddExistingRecordButton()
        {
            if (addExistingRecordButton.IsDisplayed(Timeouts.EXTRA_SHORT))
            {
                addExistingRecordButton.Click();
            }
            else
            {
                overflowButton.Click();
                addExistingRecordButton.Click();
            }

        }

        public void ClickRefreshButton()
        {
            if (refreshButton.IsDisplayed(Timeouts.DEFAULT_INTERVAL))
            {
                refreshButton.Click();
            }
            else
            {
                overflowButton.Click();
                refreshButton.Click();
            }
        }

        public void clickAddRecordButton()
        {
            if (newRecordButton.IsDisplayed(Timeouts.EXTRA_SHORT))
            {
                newRecordButton.Click();
            }
            else
            {
                overflowButton.Click();
                addExistingRecordButton.Click();
            }
        }
    }
}
