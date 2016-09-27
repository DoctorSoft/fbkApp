using System.Linq;
using System.Threading;
using Engines.Engines.Models;
using Helpers.HtmlHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Engines.Engines.AuthorizationEngine
{
    public class AuthorizationEngine : AbstractEngine<AuthorizationModel, VoidResult>
    {
        protected override VoidResult ExecuteEngine(RemoteWebDriver driver, AuthorizationModel model)
        {
            NavigateToUrl(driver);

            var login = HtmlHelper.GetElementByName(driver, "email");
            var pass = HtmlHelper.GetElementByName(driver, "pass");
            var button =
                driver.FindElements(By.TagName("input"))
                    .Where(m => m.GetAttribute("type") == "submit")
                    .FirstOrDefault(m => m.GetAttribute("value") == "Вход");

            login.SendKeys(model.Login);
            Thread.Sleep(500);

            pass.SendKeys(model.Password);
            Thread.Sleep(500);

            HtmlHelper.ClickElement(button);

            return new VoidResult();
        }
    }
}
