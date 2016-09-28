using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Engines.Engines.Models;
using Helpers.HtmlHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Engines.Engines.ConformationRegistrationEngine
{
    public class ConfirmationRegistrationEngine: AbstractEngine<ConfirmationRegistrationModel, VoidResult>
    {
        protected override VoidResult ExecuteEngine(RemoteWebDriver driver, ConfirmationRegistrationModel model)
        {
            NavigateToUrl(driver, "http://www.mail.ru");

            if (!GetLogOutStatus(driver))
            {
                LogOut(driver);
            }

            Authorize(driver, model.EmailLogin, model.EmailPassword);
            ConformatRegistration(driver, model.FacebookPassword);

            Thread.Sleep(1500);

            return new VoidResult();
        }

        private IWebElement GetWebElementById(RemoteWebDriver driver, string idName)
        {
            return driver.FindElements(By.Id(idName)).FirstOrDefault();
        }

        private bool GetLogOutStatus(RemoteWebDriver driver)
        {
            IWebElement profileOptionElement = GetWebElementById(driver, "userNavigationLabel");
            return profileOptionElement == null;
        }

        private void LogOut(RemoteWebDriver driver)
        {
            IWebElement logOutButton = GetWebElementById(driver, "PH_logoutLink");
            HtmlHelper.ClickElement(logOutButton);
        }

        private void AddTextInElement(IWebElement element, string text)
        {
            if (element == null || text == null)
            {
                return;
            }

            element.Clear();
            element.SendKeys(text);
        }

        private void Authorize(RemoteWebDriver driver, string login, string password)
        {

            IWebElement loginInput = GetWebElementById(driver, "mailbox__login");
            IWebElement passwordInput = GetWebElementById(driver, "mailbox__password");
            IWebElement authButton = GetWebElementById(driver, "mailbox__auth__button");

            AddTextInElement(loginInput, login);
            AddTextInElement(passwordInput, password);

            HtmlHelper.ClickElement(authButton);
        }

        private void ConformatRegistration(RemoteWebDriver driver, string password)
        {

            List<IWebElement> lettersTitle = driver.FindElements(By.ClassName("b-datalist__item__subj")).ToList();

            string link = "";

            foreach (var letterTitle in lettersTitle)
            {
                if (letterTitle.Text.Contains("Последний этап регистрации на Facebook"))
                {
                    HtmlHelper.ClickElement(letterTitle);

                    Thread.Sleep(1500);

                    link = driver.FindElements(By.TagName("a")).Where(m => m.GetAttribute("rel") == "noopener").FirstOrDefault(m => m.Text == "Не забудьте подтвердить свой аккаунт Facebook").GetAttribute("href");
                    break;
                }
            }

            NavigateToUrl(driver, link);
            EnterPassword(driver, password);

        }

        private void EnterPassword(RemoteWebDriver driver, string password)
        {
            IWebElement passwordTextBox = GetWebElementById(driver, "pass");
            IWebElement applyButton = GetWebElementById(driver, "loginbutton");
            
            AddTextInElement(passwordTextBox, password);
            HtmlHelper.ClickElement(applyButton);
        }
    }
}
