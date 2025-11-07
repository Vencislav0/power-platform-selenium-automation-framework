using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using Automation_Framework.Framework.Constants;
using Automation_Framework.Framework.ElementWrappers;
using Automation_Framework.Framework.Logging;
using Automation_Framework.Framework.PowerApps.ElementWrappers;
using Automation_Framework.Framework.WebDriver;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.Country;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.Spacecraft;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.SpaceFlight;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace Automation_Framework.SpacecraftManagementApp.Tests
{
    [AllureNUnit]
    [AllureSuite("Default Suite")]
    public class BaseTest
    {
        protected Random random = new Random();
        protected IWebDriver driver;
        

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            driver = WebDriverFactory.GetChromeDriver();
        }

        [SetUp]
        public void Setup()
        {           
            var testName = TestContext.CurrentContext.Test.Name;
            driver.Navigate().GoToUrl("https://org23ca5b26.crm4.dynamics.com/main.aspx?appid=faa9e15a-2f8a-f011-b4cb-7ced8d96a51b&forceUCI=1&pagetype=entitylist&etn=space_maintenance&viewid=b4d4d759-f4de-49cb-b88c-d0abb278bdf3&viewType=1039");
            Logger.SetLogFileForTest(testName);
        }

        [TearDown]
        public void Teardown()
        {
            string? logFilePath = Logger.GetCurrentLogFilePath();

            if (File.Exists(logFilePath))
            {
                byte[] logBytes = File.ReadAllBytes(logFilePath);
                AllureApi.AddAttachment("Test Execution Logs", "text/plain", logBytes, ".txt");
            }            
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            driver.Dispose();
            LogManager.Shutdown();
        }
        
        public void TestCleanup(Action cleanup)
        {
            AllureApi.Step("Performing cleanup of test data.", () =>
            {
                try
                {
                    driver.Navigate().Refresh();
                    cleanup();
                }
                catch (Exception ex) { Logger.Error($"Cleanup failed: {ex}"); }
            });
        }

        protected void HandleFailure(Exception ex)
        {
            try
            {
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                AllureApi.AddAttachment($"Screenshot", "image/png", screenshot.AsByteArray);
            }
            catch (Exception sx)
            {
                Logger.Error($"Failed to capture screenshot: {sx.Message}");
            }

            Assert.Fail($"Test failed. {ex}");
        }

        protected void RunTest(Action testSteps, Action cleanup = null)
        {
            try
            {
                testSteps();
            }
            catch (Exception ex)
            {
                HandleFailure(ex);
            }
            finally
            {
                if (cleanup != null)
                {
                    TestCleanup(cleanup);
                }
            }
        }

        //Power Platform
        public void AssertEqualWithRefresh<T>(Func<T> actualResult, T expectedResult, BaseForm form, int maxRetries = 3)
        {
            var retryCount = 0;

            while (retryCount < maxRetries)
            {

                var actual = actualResult();

                if (Equals(actual, expectedResult))
                {
                    return;
                }

                retryCount++;
                if (retryCount < maxRetries)
                {
                    Logger.Debug($"Attempt {retryCount} failed, refreshing and retrying..");
                                        
                    form.ClickRefreshButtonFromToolBar();
                                        
                    Thread.Sleep(1500);
                }
            }

            Assert.Fail($"Value did not match expected '{expectedResult}' after {maxRetries} retries.");
        }

        public void AssertEqualWithRefresh<T>(Func<T> actualResult, T expectedResult,BaseSubgrid subgrid, int maxRetries = 3)
        {
            var retryCount = 0;

            while (retryCount < maxRetries)
            {

                var actual = actualResult();

                if (Equals(actual, expectedResult))
                {
                    return;
                }

                retryCount++;
                if (retryCount < maxRetries)
                {
                    Logger.Debug($"Attempt {retryCount} failed, refreshing and retrying..");
                                                                                                                     
                    subgrid.ClickRefreshButton();
                    
                    Thread.Sleep(1500);
                }
            }

            Assert.Fail($"Value did not match expected '{expectedResult}' after {maxRetries} retries.");
        }

        public void AssertTrueWithRefresh(Func<bool> condition, BaseForm form, int maxRetries = 3)
        {
            var retryCount = 0;
            while (retryCount < maxRetries)
            {
                if (condition())
                {
                    return;
                }

                retryCount++;

                if (retryCount < maxRetries)
                {
                    Logger.Debug($"Attempt {retryCount + 1} failed, refreshing and retrying..");
                                       
                    form.ClickRefreshButtonFromToolBar();      
                    
                    Thread.Sleep(500);
                }
            }

            Assert.Fail($"Condition was not true after {maxRetries} retries with refreshes.");

        }

        public void AssertTrueWithRefresh(Func<bool> condition,BaseSubgrid subgrid, int maxRetries = 3)
        {
            var retryCount = 0;
            while (retryCount < maxRetries)
            {
                if (condition())
                {
                    return;
                }

                retryCount++;

                if (retryCount < maxRetries)
                {
                    Logger.Debug($"Attempt {retryCount + 1} failed, refreshing and retrying..");

                    subgrid.ClickRefreshButton();   
                    
                    Thread.Sleep(500);
                }
            }

            Assert.Fail($"Condition was not true after {maxRetries} retries with refreshes.");

        }

        public void LoginPowerApps(string username, string password)
        {
            var nextButton = new Button(driver, By.Id("idSIButton9"), "Next Button");

            var emailField = new Textbox(driver, By.Id("i0116"), "Email Field");

            if (!emailField.IsDisplayed(Timeouts.EXTRA_SHORT))
            {
                return;
            }
            emailField.SendKeys(username);
            nextButton.Click();
            Thread.Sleep(2000);
            nextButton.Click();


        }

    }
}