using Automation_Framework.Framework.Configuration.PowerApps;
using Automation_Framework.Framework.ElementWrappers;
using Automation_Framework.Framework.Logging;
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
            String lookupData = File.ReadAllText($"{AppContext.BaseDirectory}/Framework/Configuration/PowerApps/lookupFieldConfigs.json");

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
            Thread.Sleep(500);
            try
            {
                Logger.Debug($"Sending value to Lookup: {name}");                
                RemoveRecordFromLookup();                
                customWaits.WaitUntilVisible();
                customWaits.WaitUntilEnabled();
                GetElement(TimeSpan.FromMilliseconds(3000)).Click();
                Thread.Sleep(500);
                GetElement(TimeSpan.FromMilliseconds(3000)).SendKeys(value.Trim());                
                Logger.Debug($"value sent: {value}");
                GetElement(TimeSpan.FromMilliseconds(3000)).Click();
                customWaits.WaitUntilLookupRecordsLoad(config, TimeSpan.FromMilliseconds(3000));
                Thread.Sleep(500);
                recordsList.GetElementAt(0).Click();

            }
            catch(WebDriverTimeoutException) 
            {
                Logger.Debug("Something went wrong. Attempting fix by clicking Search Button");
                lookupSearchButton.Click();
                customWaits.WaitUntilLookupRecordsLoad(config, TimeSpan.FromMilliseconds(500));
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
            if (recordOnLookup.IsDisplayed(TimeSpan.FromMilliseconds(1000)))
            {
                recordXButton.Click();
            }            
        }

    }
}
