using System;
using System.Threading;
﻿using System.Linq;
using Constants.EnumExtensions;
using Engines.Engines.Models;
using Helpers.HtmlHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Engines.Engines.InitialProfileSetupEngine
{
    public class InitialProfileSetupEngine : AbstractEngine<InitialProfileSetupModel, VoidResult>
    {
        protected override VoidResult ExecuteEngine(RemoteWebDriver driver, InitialProfileSetupModel model)
        {
            DisableChat(driver);

            DisableVideocalls(driver);
            
            SetTimelineOptions(driver, model);

            return new VoidResult();
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
            }
        }

        private void DisableVideocalls(RemoteWebDriver driver)
        {
            //disable video call

            Thread.Sleep(2500);
            IWebElement chatOptionButton = HtmlHelper.GetElementByCssSelector(driver, ".clearfix.rfloat._ohf");
            if (chatOptionButton.Displayed == false)
            {
                chatOptionButton = HtmlHelper.GetElementByXPath(driver, "//*[@id='fbDockChatBuddylistNub']/a/div[1]/div");
            }
            chatOptionButton.Click();

            Thread.Sleep(2000);

            IWebElement disableVideoChatOptionButton;
            try
            {
                disableVideoChatOptionButton = driver.FindElement(By.CssSelector("[href*='videocall']"));
            }
            catch (Exception)
            {
                var disableVideoChatOptionButtons = driver.FindElements(By.CssSelector("._54nh"));
                disableVideoChatOptionButton =
                    disableVideoChatOptionButtons.FirstOrDefault(
                        m => m.Text.Contains("Отключить голосовые и видеовызовы") || m.Text.Contains("Включить голосовые и видеовызовы"));
            }

            if (!disableVideoChatOptionButton.Text.Contains("Включить голосовые и видеовызовы"))
            {
                if (disableVideoChatOptionButton.Text.Contains("Отключить голосовые и видеовызовы"))
                {
                    disableVideoChatOptionButton.Click();
                }
                else
                {
                    IWebElement disableVideoChatOptionPrev =
                        disableVideoChatOptionButton.FindElement(By.XPath(".."));
                    disableVideoChatOptionPrev.Click();
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
            else
            {
                return;
            }
        }

        private void SetTimelineOptions(RemoteWebDriver driver, InitialProfileSetupModel model)
        {
            AvoidFacebookMessage(driver);

            foreach (var settings in model.ProfileSettings)
            {
                NavigateToUrl(driver, settings.Link.GetDiscription());

                var currenrOptions = HtmlHelper.GetElementByCssSelector(driver, "._42ft._4jy0._55pi._5vto._55_p._2agf._p._4jy3._517h._51sy");
                if (currenrOptions == null) currenrOptions = HtmlHelper.GetElementByCssSelector(driver, "._42ft._42fu._4-s1._2agf._p");
                HtmlHelper.ClickElement(currenrOptions);

                var userOptions = HtmlHelper.GetElementsByCssSelector(driver, "._54nc");
                foreach (var userOption in userOptions)
                {
                    if (userOption.Text.Contains(settings.Answer.GetDiscription()))
                    {
                        HtmlHelper.ClickElement(userOption);
                        break;
                    }
                }

                Thread.Sleep(3000);
            }
        }
    }
}

