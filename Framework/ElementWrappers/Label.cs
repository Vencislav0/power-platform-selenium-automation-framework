using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.Framework.ElementWrappers
{
    public class Label : BaseElement
    {
        public Label(IWebDriver driver, By locator, string name) : base(driver, locator, name)
        {
        }
    }
}
