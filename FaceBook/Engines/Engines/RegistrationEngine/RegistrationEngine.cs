using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Constants;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Engines.Engines.RegistrationEngine
{
    public class RegistrationEngine: AbstractEngine<RegistrationModel, VoidResult>
    {
        protected override VoidResult ExecuteEngine(RemoteWebDriver driver, RegistrationModel model)
        {
            NavigateToUrl(driver);

            IWebElement lastNameInput = GetWebElementByName(driver, "firstname");
            IWebElement firstNameInput = GetWebElementByName(driver, "lastname");
            IWebElement emailInput = GetWebElementByName(driver, "reg_email__");
            IWebElement confirmationEmailInput = GetWebElementByName(driver, "reg_email_confirmation__");
            IWebElement passwordInput = GetWebElementByName(driver, "reg_passwd__");
            IWebElement birthdayDay = GetWebElementByName(driver, "birthday_day");
            IWebElement birthdayMonth = GetWebElementByName(driver, "birthday_month");
            IWebElement birthdayYear = GetWebElementByName(driver, "birthday_year");
            IWebElement gender = model.Gender == Gender.Female ? GetWebElementById(driver, "u_0_d") : GetWebElementById(driver, "u_0_e");

            if (lastNameInput != null)
            {
                lastNameInput.Clear();
                lastNameInput.SendKeys(model.LastName);
            }

            if (firstNameInput != null)
            {
                firstNameInput.Clear();
                firstNameInput.SendKeys(model.FirstName);
            }

            if (emailInput != null)
            {
                emailInput.Clear();
                emailInput.SendKeys(model.Email);
            }
            if (confirmationEmailInput != null)
            {
                confirmationEmailInput.Clear();
                confirmationEmailInput.SendKeys(model.Email);
            }
            if (passwordInput != null)
            {
                passwordInput.Clear();
                passwordInput.SendKeys(model.Password);
            }

            if (birthdayDay != null)
            {
                birthdayDay.FindElement(By.CssSelector("option[value='" + model.Birthday.Day + "']")).Click();
            }

            if (birthdayMonth != null)
            {
                birthdayMonth.FindElement(By.CssSelector("option[value='" + model.Birthday.Month + "']")).Click();
            }

            if (birthdayYear != null)
            {
                birthdayYear.FindElement(By.CssSelector("option[value='" + model.Birthday.Year + "']")).Click();
            }
            if (gender != null)
            {
                gender.Click();
            }

            /*var registrtionData = links.FirstOrDefault(element => element.Text == "Вход");
            if (registrtionData == null)
            {
                return new VoidResult();
            }
            registrtionData.Click();

            Thread.Sleep(500);

            IList<IWebElement> userNameInputs = driver.FindElements(By.Name("username"));
            userNameInputs.FirstOrDefault().SendKeys(model.UserName);

            Thread.Sleep(500);

            IList<IWebElement> passwordInuts = driver.FindElements(By.Name("password"));
            passwordInuts.FirstOrDefault().SendKeys(model.Password);

            Thread.Sleep(500);

            IList<IWebElement> buttons = driver.FindElements(By.TagName("button"));
            buttons.FirstOrDefault(element => element.Text == "Войти").Click();

            Thread.Sleep(2000);

            if (!base.NavigateToUrl(driver))
            {
                return GetDefaultResult();
            }

            Thread.Sleep(1000);*/

            return new VoidResult();
        }

        private IWebElement GetWebElementByName(RemoteWebDriver driver, string name)
        {
            return driver.FindElements(By.Name(name)).FirstOrDefault();
        }

        private IWebElement GetWebElementById(RemoteWebDriver driver, string idName)
        {
            return driver.FindElements(By.Id(idName)).FirstOrDefault();
        }
    }
}
