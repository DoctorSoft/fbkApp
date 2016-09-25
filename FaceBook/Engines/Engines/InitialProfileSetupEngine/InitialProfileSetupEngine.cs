﻿﻿using System.Threading;
﻿using System.Linq;
using Engines.EnumExtensions;
using Engines.Enums;
using Helpers.HtmlHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Engines.Engines.InitialProfileSetupEngine
{
    public class InitialProfileSetupEngine : AbstractEngine<InitialProfileSetupModel, VoidResult>
    {
        protected override VoidResult ExecuteEngine(RemoteWebDriver driver, InitialProfileSetupModel model)
        {
            SetPrivacyOptions(driver);

            DisableChat(driver);

            DisableVideocalls(driver);

            return new VoidResult();
        }

        private void SetPrivacyOptions(RemoteWebDriver driver)
        {
            // set privacy

            NavigateToUrl(driver, SettingsUrl.PrivacyUrl.GetDiscription());
            
            Thread.Sleep(1500);

            AvoidFacebookMessage(driver);

            Thread.Sleep(1500);

            IWebElement visibilityOfPublications = HtmlHelper.GetElementByCssSelector(driver,
                ".fbSettingsListItemContent.fcg");
            if (visibilityOfPublications.Displayed)
            {
                ClickElement(visibilityOfPublications);
            }

            Thread.Sleep(500);

            IWebElement visibilityOfPublicationsDropDownList = HtmlHelper.GetElementByClass(driver, "_55pe");
            ClickElement(visibilityOfPublicationsDropDownList);

            Thread.Sleep(500);

            IWebElement visibilityToAll = HtmlHelper.GetElementByCssSelector(driver, "._54nh._4chm._48u0");
            ClickElement(visibilityToAll);

        }

        private void DisableChat(RemoteWebDriver driver)
        {
            // disable chat

            Thread.Sleep(2000);

            var chatDisable = HtmlHelper.GetElementByXPath(driver, "//*[@id='fbDockChatBuddylistNub']/a/span[2]");
            if (!chatDisable.Text.Contains("Чат (Отключен)"))
            {
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

                //IWebElement hideChat = HtmlHelper.GetElementByCssSelector(driver, ".titlebarLabel.clearfix");
                //hideChat.Click();
            }
        }

        private void DisableVideocalls(RemoteWebDriver driver)
        {
            //disable video call

            Thread.Sleep(2500);

            //IWebElement videoChatOptionButton = HtmlHelper.GetElementByXPath(driver,
            //   "//*[@id='fbDockChatBuddylistNub']/a/div[1]");

            //videoChatOptionButton.Click();

            IWebElement chatOptionButton = HtmlHelper.GetElementByCssSelector(driver, ".clearfix.rfloat._ohf");
            chatOptionButton.Click();

            Thread.Sleep(2000);

            IWebElement disableVideoChatOptionButtonChild = driver.FindElement(By.CssSelector("[href*='videocall']"));

            if (!disableVideoChatOptionButtonChild.Text.Contains("Включить голосовые и видеовызовы"))
            {
                if (disableVideoChatOptionButtonChild.Text.Contains("Отключить голосовые и видеовызовы"))
                {
                    disableVideoChatOptionButtonChild.Click();
                }
                else
                {
                    IWebElement disableVideoChatOptionButton =
                        disableVideoChatOptionButtonChild.FindElement(By.XPath(".."));
                    disableVideoChatOptionButton.Click();
                }
                Thread.Sleep(1500);

                var yetIWillNotIncludeButtons = driver.FindElements(By.ClassName("uiInputLabelLabel"));
                IWebElement yetIWillNotInclude =
                    yetIWillNotIncludeButtons.FirstOrDefault(
                        yetIWillNotIncludeButton => yetIWillNotIncludeButton.Text.Contains("Пока я не включу"));
                ClickElement(yetIWillNotInclude);

                Thread.Sleep(1500);

                IWebElement applyDisableVideoButton = HtmlHelper.GetElementByCssSelector(driver,
                    "._42ft._4jy0.layerConfirm.uiOverlayButton._4jy3._4jy1.selected._51sy");
                applyDisableVideoButton.Click();
            }
        }
    }
}

