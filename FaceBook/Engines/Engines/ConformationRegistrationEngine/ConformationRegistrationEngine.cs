﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Engines.Engines.ConformationRegistrationEngine
{
    public class ConformationRegistrationEngine: AbstractEngine<ConformationRegistrationModel, VoidResult>
    {
        protected override VoidResult ExecuteEngine(RemoteWebDriver driver, ConformationRegistrationModel model)
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
            ClickElement(logOutButton);
        }

        private void ClickElement(IWebElement element)
        {
            if (element == null)
            {
                return;
            }
            element.Click();
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

            ClickElement(authButton);
        }

        private void ConformatRegistration(RemoteWebDriver driver, string password)
        {

            List<IWebElement> lettersTitle = driver.FindElements(By.ClassName("b-datalist__item__subj")).ToList();

            string link = "";

            foreach (var letterTitle in lettersTitle)
            {
                if (letterTitle.Text.Contains("Последний этап регистрации на Facebook"))
                {
                    ClickElement(letterTitle);

                    Thread.Sleep(1500);

                    link = driver.FindElement(By.XPath("//*[@id='style_14743898780000000596_BODY']/table/tbody/tr/td/table/tbody/tr[5]/td[2]/table/tbody/tr[2]/td[1]/a/table/tbody/tr/td/a")).GetAttribute("href");
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
            ClickElement(applyButton);
        }
    }
}
