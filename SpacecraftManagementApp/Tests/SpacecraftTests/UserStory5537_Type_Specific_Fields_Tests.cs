using Allure.Net.Commons;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms;
using Automation_Framework.SpacecraftManagementApp.Pages.Forms.Spacecraft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation_Framework.SpacecraftManagementApp.Tests.SpacecraftTests
{
    public class UserStory5537_Type_Specific_Fields_Tests : BaseTest
    {
        
        [Test]
        public void SwitchingSpacecraftModels_VerifySpecificFields_AreDisplayed()
        {
            var spacecraftForm = new SpacecraftForm(driver);
            var spacecraftView = new SpacecraftView(driver);
            var sidemapForm = new SideMapForm(driver);

            AllureApi.Step("Navigating to Spacecraft View, and open existing spacecraft record", () =>
            {
                sidemapForm.ClickSidemapItem("Spacecrafts");
                spacecraftForm.ClickNewButtonFromToolBar();
            });

            AllureApi.Step("Changing the spacecraft model to commercial and verifying Operating Company Lookup is Displayed", () =>
            {
                spacecraftForm.SelectSpacecraftModel("Commercial");
                Assert.That(spacecraftForm.IsOperatingCompanyDisplayed(), Is.True);
            });

            AllureApi.Step("Changing the spacecraft model to research and verifying Organisation Type Choice is Displayed", () =>
            {
                spacecraftForm.SelectSpacecraftModel("Research");
                Assert.That(spacecraftForm.IsOrganisationTypeFieldDisplayed(), Is.True);
            });

            AllureApi.Step("Changing the spacecraft model to military and verifying Is Armed? Choice is Displayed", () =>
            {
                spacecraftForm.SelectSpacecraftModel("Military");
                Assert.That(spacecraftForm.IsArmedDisplayed(), Is.True);
            });


        }
    }
}
