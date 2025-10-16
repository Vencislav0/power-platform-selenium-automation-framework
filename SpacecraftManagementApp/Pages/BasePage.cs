using Automation_Framework.Framework.Constants;
using Automation_Framework.Framework.ElementWrappers;
using Automation_Framework.Framework.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.SpacecraftManagementApp.Pages
{    
    public class BasePage
    {       
        protected IWebDriver _driver;
        protected Button newButton;
        protected Button saveButton;        
        public BasePage(IWebDriver driver) 
        { 
            _driver = driver;                       
            newButton = new Button(_driver, By.XPath("//button[@aria-label='New']"), "New Button");
            saveButton = new Button(_driver, By.XPath("//button[@aria-label='Save (CTRL+S)']"), "New Button");            
        }

        public void ClickNewButtonFromToolBar()
        {
            newButton.Click();
        }        
        
    }
}
