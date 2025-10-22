using Automation_Framework.Framework.Constants;
using Automation_Framework.Framework.ElementWrappers;
using Automation_Framework.Framework.Logging;
using Automation_Framework.Framework.PowerApps.PowerAppsss;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V136.BackgroundService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Automation_Framework.Framework.PowerApps.ElementWrappers
{
    public class Lookup : BaseElement
    {
        protected ElementsCollection recordsList;

        
        protected Button recordXButton;
        protected Button lookupSearchButton;
        protected Label recordOnLookup;

        protected Textbox inputField;
        protected FieldConfig config;
        public Lookup(IWebDriver driver, By locator, string lookupType, string name) : base(driver, locator, name)
        {
            String lookupData = File.ReadAllText($"{AppContext.BaseDirectory}/Framework/PowerApps/Configuration/lookupFieldConfigs.json");

            Dictionary<string, FieldConfig> lookupConfigs =
                JsonSerializer.Deserialize<Dictionary<string, FieldConfig>>(lookupData);

            
            config = lookupConfigs[lookupType];
            
            recordXButton = new Button(driver, By.XPath(config.xButtonLocator), "Record X Button");
            lookupSearchButton = new Button(driver, By.XPath(config.searchButtonLocator), "Lookup Search Button");
            recordOnLookup = new Label(driver, By.XPath(config.recordOnLookupLocator), "Record Inside Lookup");
            recordsList = new ElementsCollection(driver, By.XPath(config.recordsListLocator), "Records Collection");
            
        }

        public void EnterValue(string value)
        {
            customWaits.WaitUntilRecordFormLoads(); 
            //Thread.Sleep(Timeouts.WAIT_FOR_INTERVAL);
            try
            {
                Logger.Debug($"Sending value to Lookup: {name}");                
                RemoveRecordFromLookup();                
                customWaits.WaitUntilVisible();
                customWaits.WaitUntilEnabled();
                GetElement(Timeouts.EXTRA_SHORT).Click();
                //Thread.Sleep(Timeouts.WAIT_FOR_INTERVAL);
                GetElement(Timeouts.EXTRA_SHORT).SendKeys(value.Trim());                
                Logger.Debug($"value sent: {value}");
                GetElement(Timeouts.EXTRA_SHORT).Click();
                customWaits.WaitUntilLookupRecordsLoad(config, Timeouts.EXTRA_SHORT);
                Thread.Sleep(Timeouts.WAIT_FOR_INTERVAL);
                recordsList.GetElementAt(0).Click();

            }
            catch(WebDriverTimeoutException) 
            {
                Logger.Debug("Something went wrong. Attempting fix by clicking Search Button");
                lookupSearchButton.Click();
                customWaits.WaitUntilLookupRecordsLoad(config, Timeouts.DEFAULT_INTERVAL);
                recordsList.GetElementAt(0).Click();

            }
            catch (Exception)
            {
                Logger.Error($"Failed sending value to Lookup: {name}");
                throw;
            }
            
        }

        public void RemoveRecordFromLookup()
        {           
            if (recordOnLookup.IsDisplayed(Timeouts.DEFAULT_INTERVAL))
            {
                recordXButton.Click();
            }            
        }

        public void ClickRecordOnLookup()
        {
            if (recordOnLookup.IsDisplayed(Timeouts.DEFAULT_INTERVAL))
            {
                recordOnLookup.Click();
            }
        }

    }
}
