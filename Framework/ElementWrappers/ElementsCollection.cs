using System;
using System.Collections.Generic;
using Automation_Framework.Framework.Constants;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation_Framework.Framework.Utilities;

namespace Automation_Framework.Framework.ElementWrappers
{
    public class ElementsCollection
    {
        protected IWebDriver driver;
        protected By locator;
        protected string name;
        protected WebDriverWait wait;
        protected CustomWaits customWaits;
        protected Actions actions;
        protected TimeSpan timeout = Timeouts.DEFAULT_WAIT;
        public ElementsCollection(IWebDriver driver, By locator, string name)
        {
            this.driver = driver;
            this.locator = locator;
            this.name = name;
            this.actions = new Actions(driver);
            this.wait = new WebDriverWait(driver, timeout);
            this.customWaits = new CustomWaits(locator, driver, timeout);
        }

        public ICollection<IWebElement> GetElements()
        {
            
            var elements = customWaits.WaitUntilAllVisibleAndReturn();
            return elements;
        }

        public ICollection<IWebElement> GetElements(TimeSpan timeout)
        {
            var wait = new WebDriverWait(driver, timeout);
            var elements = customWaits.WaitUntilAllVisibleAndReturn(wait);
            return elements;
        }




        public int Count(By locator)
        {
            var elements = GetElements();
            return elements.Count;
        }

        public List<string> GetTexts(By locator)
        {
            var elements = GetElements();
            var elementsList = new List<string>();
            foreach (var element in elements)
            {
                elementsList.Add(element.Text);
            }

            return elementsList;
        }

        public Label GetElementAt(int index)
        {         
            var element = GetElements().Select((element, i) => i == index ? element : null)
                    .FirstOrDefault();

            return element != null ? new Label(driver, locator, $"{name} Collection Element : {index + 1}") : throw new NoSuchElementException($"No element found at index {index} in collection {name}.");

        }




    }
}

    

    
