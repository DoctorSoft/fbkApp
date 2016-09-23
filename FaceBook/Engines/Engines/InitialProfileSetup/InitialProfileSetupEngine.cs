using System.Linq;
using System.Threading;
using Engines.EnumExtensions;
using Engines.Enums;
using Helpers.HtmlHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;

namespace Engines.Engines.InitialProfileSetup
{
    public class InitialProfileSetupEngine : AbstractEngine<InitialProfileSetupModel, VoidResult>
    {
        protected override VoidResult ExecuteEngine(RemoteWebDriver driver, InitialProfileSetupModel model)
        {
            SetPrivacyOptions(driver);

            return new VoidResult();
        }
        private bool SetPrivacyOptions(RemoteWebDriver driver)
        {
            // set privacy

            NavigateToUrl(driver, SettingsUrl.PrivacyUrl.GetDiscription());

            Thread.Sleep(1500);

            IWebElement visibilityOfPublications = HtmlHelper.GetElementByCssSelector(driver, ".fbSettingsListItemContent.fcg");
            if (visibilityOfPublications.Displayed)
            {
                IWebElement dd = HtmlHelper.GetElementByClass(driver, "_3ixn");
                dd.Click();

                ClickElement(visibilityOfPublications);
            }

            Thread.Sleep(500);

            IWebElement visibilityOfPublicationsDropDownList = HtmlHelper.GetElementByClass(driver, "_55pe");
            ClickElement(visibilityOfPublicationsDropDownList);

            Thread.Sleep(500);

            IWebElement visibilityToAll = HtmlHelper.GetElementByCssSelector(driver, "._54nh._4chm._48u0");
            ClickElement(visibilityToAll);


            // disable chat

            IWebElement chatOptionButton = HtmlHelper.GetElementById(driver, "._5vmb.button._p");

            chatOptionButton.Click();
            
            IWebElement disableChatButton = HtmlHelper.GetElementByClass(driver, "_54nh");
            disableChatButton.Click();


            
            return true;
            
        }

        private void ClickElement(IWebElement element)
        {
            if (element == null)
            {
                return;
            }
            element.Click();
        }
    }
}
