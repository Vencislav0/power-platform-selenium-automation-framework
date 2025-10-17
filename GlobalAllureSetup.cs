using Allure.Net.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework
{
    [SetUpFixture]
    public class GlobalAllureSetup
    {
        [OneTimeSetUp]
        public void GlobalSetup()
        {
            AllureLifecycle.Instance.CleanupResultDirectory();
        }
    }
}
