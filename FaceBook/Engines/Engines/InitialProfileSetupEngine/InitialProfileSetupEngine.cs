using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
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

            var chatDisable = HtmlHelper.GetElementByXPath(driver, "//*[@id='fbDockChatBuddylistNub']/a/span[2]");
            if (!chatDisable.Text.Contains("Чат (Отключен)"))
            {
                IWebElement dd = HtmlHelper.GetElementByClass(driver, "_3ixn");
                dd.Click();

                IWebElement chatOptionButton = HtmlHelper.GetElementByCssSelector(driver, ".clearfix.rfloat._ohf");
                chatOptionButton.Click();

                Thread.Sleep(1500);

                IWebElement otherOptions = HtmlHelper.GetElementByCssSelector(driver,
                    "[href*='/ajax/chat/privacy/settings_dialog.php'");
                otherOptions.Click();

                Thread.Sleep(1500);

                IWebElement disableChatAllFriendsButtonId = HtmlHelper.GetElementByXPath(driver, "//*[@id='offline']");
                disableChatAllFriendsButtonId.Click();

                Thread.Sleep(1500);

                IWebElement applyButton = HtmlHelper.GetElementByCssSelector(driver,
                    "._42ft._42fu.layerConfirm.uiOverlayButton.selected._42g-._42gy");
                applyButton.Click();

                Thread.Sleep(1500);

                IWebElement hideChat = HtmlHelper.GetElementByCssSelector(driver, ".titlebarLabel.clearfix");
                hideChat.Click();
            }
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
