using Allure.Net.Commons;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.SpacecraftManagementApp.Steps
{
    public static class CommonSteps
    {
        public static void CreateEngine(EngineForm engineForm, string engineStatusInput = "100")
        {
            AllureApi.Step("Fill the engine name and status then select engine model", () =>
            {
                engineForm.FillName("Test Engine");
                engineForm.ChangeStatusField(engineStatusInput);
                engineForm.SelectEngineModel("AeroFan X3");
            });

            AllureApi.Step("Save the engine and close it", () =>
            {
                engineForm.ClickSaveButtonFromToolBar(true);
                engineForm.ClickSaveAndCloseButtonFromToolBar();
            });

        }
    }
}
