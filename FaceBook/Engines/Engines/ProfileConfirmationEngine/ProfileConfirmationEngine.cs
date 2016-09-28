using System.Linq;
using System.Threading;
using Engines.Engines.Models;
using Helpers.HtmlHelpers;
using OpenQA.Selenium.Remote;

namespace Engines.Engines.ProfileConfirmationEngine
{
    public class ProfileConfirmationEngine: AbstractEngine<ProfileConfirmationModel, VoidResult>
    {
        protected override VoidResult ExecuteEngine(RemoteWebDriver driver, ProfileConfirmationModel model)
        {
            var label = driver.FindElementsByCssSelector("._585r._50f4").FirstOrDefault(m => m.Text.Contains("Вы должны подтвердить ваш пароль, чтобы редактировать настройки вашего аккаунта."));
            if (label != null)
            {
                var login = HtmlHelper.GetElementByName(driver, "email");
                var pass = HtmlHelper.GetElementByName(driver, "pass");

                var button = driver.FindElementsByTagName("button").FirstOrDefault(m=>m.GetAttribute("Name") == "login");

                login.SendKeys(model.Login);
                Thread.Sleep(500);

                pass.SendKeys(model.Password);
                Thread.Sleep(500);

                HtmlHelper.ClickElement(button);
            }

            return new VoidResult();
        }
    }
}
