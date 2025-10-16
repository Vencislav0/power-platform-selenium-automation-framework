using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.Framework.PowerApps.PowerAppsss
{
    public class FieldConfig
    {
        public string xButtonLocator { get; set; }
        public string searchButtonLocator { get; set; }
        public string recordOnLookupLocator { get; set; }
        public string recordsListLocator { get; set; }
    }

    public class ConfigRoot : Dictionary<string, FieldConfig> { }
}
