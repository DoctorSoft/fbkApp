using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Constants;
using Constants.EnumExtensions;
using Engines.Engines.LoadUserAvatar;
using Engines.Engines.Models;
using Helpers.HtmlHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using Serilog;

namespace Engines.Engines.LoadUserAvatarEngine
{
    public class LoadUserAvatarEngine : AbstractEngine<LoadUserAvatarModel, VoidResult>
    {
        protected override VoidResult ExecuteEngine(RemoteWebDriver driver, LoadUserAvatarModel model)
        {
            var url = SettingsUrl.PrivacyUrl.GetDiscription();
            Log.Information("Going to url: {url}", url);
            NavigateToUrl(driver, url);

            Thread.Sleep(1500);
            Log.Information("Trying to avoid chrome message");
            AvoidFacebookMessage(driver);

            Log.Information("Moving to page with user info");
            var userPageButton = driver.GetElementByClass("_2s25");
            ClickElement(userPageButton);

            Thread.Sleep(1500);

            Log.Information("Clicking to change avatar button");
            var changeAvatarButton = driver.GetElementByClass("fbTimelineProfilePicSelector");
            ClickElement(changeAvatarButton);

            Thread.Sleep(3000);

            var rootDirectory = Directory.GetDirectoryRoot(model.AvatarName);
            var fileName = Path.GetFileName(model.AvatarName);

            if (driver is PhantomJSDriver)
            {
                Log.Information("Trying to select image from hard drive");
                ((PhantomJSDriver) driver).ExecutePhantomJS(String.Format("var page = this; page.uploadFile('input[type=file]', '{0}');", model.AvatarName));
            }
            else
            {
                Log.Information("Clicking to choose avatar button");
                var chooseAvatarButton = driver.GetElementByClass("_5uar");
                chooseAvatarButton.Click();

                Thread.Sleep(3000);

                Log.Information("Trying to select image from hard drive");
                SendKeys.SendWait(rootDirectory);
                Thread.Sleep(500);
                SendKeys.SendWait(@"{Enter}");
                Thread.Sleep(500);
                SendKeys.SendWait(fileName);
                Thread.Sleep(500);
                SendKeys.SendWait(@"{Enter}");
            }

            Thread.Sleep(10000);

            Log.Information("Uploading image");
            var footer = driver.GetElementByClass("uiOverlayFooter");
            var submitAvatarButton =
                footer.FindElements(By.TagName("button"))
                    .Where(
                        element =>
                            element.GetAttribute("type") == "submit" &&
                            element.GetAttribute("class").Contains("_4jy1") &&
                            element.GetAttribute("class").Contains("selected"))
                    .FirstOrDefault();
            submitAvatarButton.Click();

            Thread.Sleep(10000);

            return new VoidResult();
        }
    }
}
