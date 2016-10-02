using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Engines.Engines.Models;
using Helpers.HtmlHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using registrationEngine = Engines.Engines.RegistrationEngine.RegistrationEngine;

namespace Engines.Engines.ConformationRegistrationEngine
{
    public class ConfirmationRegistrationEngine: AbstractEngine<ConfirmationRegistrationModel, ErrorModel>
    {
        protected override ErrorModel ExecuteEngine(RemoteWebDriver driver, ConfirmationRegistrationModel model)
        {
            NavigateToUrl(driver, "http://www.mail.ru");

            if (!GetLogOutStatus(driver))
            {
                LogOut(driver);
            }

            Authorize(driver, model.EmailLogin, model.EmailPassword);
            return ConformatRegistration(driver, model.FacebookPassword);
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

        private ErrorModel ConformatRegistration(RemoteWebDriver driver, string password)
        {
            Thread.Sleep(5000);
            List<IWebElement> lettersTitle = driver.FindElements(By.ClassName("b-datalist__item__subj")).ToList();

            string link = "";

            foreach (var letterTitle in lettersTitle)
            {
                if (letterTitle.Text.Contains("Последний этап регистрации на Facebook"))
                {
                    HtmlHelper.ClickElement(letterTitle);

                    Thread.Sleep(5000);

                    link = driver.FindElements(By.TagName("a")).Where(m => m.GetAttribute("rel") == "noopener").FirstOrDefault(m => m.Text == "Не забудьте подтвердить свой аккаунт Facebook"
                        || m.Text == "Подтвердите свой аккаунт").GetAttribute("href");
                    break;
                }
            }

            NavigateToUrl(driver, link);
            
            Thread.Sleep(2000);
            AvoidFacebookMessage(driver);

            Thread.Sleep(3000);
            var error = registrationEngine.ChekLockStatus(driver);
            if (error != null)
                return new ErrorModel
                {
                    Code = error.Code,
                    ErrorText = error.ErrorText
                };

            EnterPassword(driver, password);

            return null;
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
