using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Constants;
using Constants.EnumExtensions;
using Engines.Engines.LoadUserAvatar;
using Helpers.HtmlHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Engines.Engines.LoadUserAvatarEngine
{
    public class LoadUserAvatarEngine : AbstractEngine<LoadUserAvatarModel, VoidResult>
    {
        protected override VoidResult ExecuteEngine(RemoteWebDriver driver, LoadUserAvatarModel model)
        {
            NavigateToUrl(driver, SettingsUrl.PrivacyUrl.GetDiscription());

            Thread.Sleep(1500);

            AvoidFacebookMessage(driver);

            var userPageButton = driver.GetElementByClass("_2s25");

            ClickElement(userPageButton);

            Thread.Sleep(1500);

            var changeAvatarButton = driver.GetElementByClass("fbTimelineProfilePicSelector");

            ClickElement(changeAvatarButton);

            Thread.Sleep(3000);

            var chooseAvatarButton = driver.GetElementByClass("_5uar");

            chooseAvatarButton.Click();

            Thread.Sleep(3000);

            var rootDirectory = Directory.GetDirectoryRoot(model.AvatarName);
            var fileName = Path.GetFileName(model.AvatarName);
            SendKeys.SendWait(rootDirectory);
            Thread.Sleep(500);
            SendKeys.SendWait(@"{Enter}");
            SendKeys.SendWait(fileName);
            Thread.Sleep(500);
            SendKeys.SendWait(@"{Enter}");

            Thread.Sleep(10000);

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
