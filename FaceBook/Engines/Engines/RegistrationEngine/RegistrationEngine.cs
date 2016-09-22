using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Constants;
using Engines.Engines.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Keys = OpenQA.Selenium.Keys;

namespace Engines.Engines.RegistrationEngine
{
    public class RegistrationEngine: AbstractEngine<RegistrationModel, ErrorModel>
    {
        protected override ErrorModel ExecuteEngine(RemoteWebDriver driver, RegistrationModel model)
        {
            NavigateToUrl(driver);
            try
            {
                if (!GetLogOutStatus(driver))
                {
                    LogOut(driver);
                }

                IWebElement lastNameInput = GetWebElementByName(driver, "firstname");
                IWebElement firstNameInput = GetWebElementByName(driver, "lastname");
                IWebElement emailInput = GetWebElementByName(driver, "reg_email__");
                IWebElement confirmationEmailInput = GetWebElementByName(driver, "reg_email_confirmation__");
                IWebElement passwordInput = GetWebElementByName(driver, "reg_passwd__");
                IWebElement birthdayDay = GetWebElementByName(driver, "birthday_day");
                IWebElement birthdayMonth = GetWebElementByName(driver, "birthday_month");
                IWebElement birthdayYear = GetWebElementByName(driver, "birthday_year");
                IWebElement submitButton = GetWebElementByName(driver, "websubmit");

                IWebElement gender = model.Gender == Gender.Female ? GetWebElementById(driver, "u_0_d") : GetWebElementById(driver, "u_0_e");

                AddTextInElement(lastNameInput, model.LastName);
                AddTextInElement(firstNameInput, model.FirstName);
                AddTextInElement(emailInput, model.Email);
                AddTextInElement(confirmationEmailInput, model.Email);
                AddTextInElement(passwordInput, model.FacebookPassword);

                GetSelectElement(birthdayDay, model.Birthday.Day);
                GetSelectElement(birthdayMonth, model.Birthday.Month);
                GetSelectElement(birthdayYear, model.Birthday.Year);

                ClickElement(gender);

                ClickElement(submitButton);

                if (CheckErrors(driver))
                {
                    return new ErrorModel
                    {
                        ErrorText = GetErrorText(driver)
                    };
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        private IWebElement GetWebElementByName(RemoteWebDriver driver, string name)
        {
            return driver.FindElements(By.Name(name)).FirstOrDefault();
        }

        private IWebElement GetWebElementById(RemoteWebDriver driver, string idName)
        {
            return driver.FindElements(By.Id(idName)).FirstOrDefault();
        }
        private IWebElement GetWebElementByClass(RemoteWebDriver driver, string className)
        {
            return driver.FindElements(By.ClassName(className)).FirstOrDefault();
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

        private void GetSelectElement(IWebElement selectElement, int value)
        {
            if (selectElement == null || value == 0)
            {
                return;
            }
            selectElement.FindElement(By.CssSelector("option[value='" + value + "']")).Click();
        }

        private void ClickElement(IWebElement element)
        {
            if (element == null)
            {
                return;
            }
            element.Click();
        }

        private bool GetLogOutStatus(RemoteWebDriver driver)
        {
            IWebElement profileOptionElement = GetWebElementById(driver, "userNavigationLabel");
            return profileOptionElement == null;
        }

        private void LogOut(RemoteWebDriver driver)
        {
            driver.Keyboard.SendKeys(Keys.Enter);
            IWebElement profileOptionElement = GetWebElementById(driver, "userNavigationLabel");
            ClickElement(profileOptionElement);
            IWebElement logOutButton = GetWebElementByClass(driver, "_54ni navSubmenu __MenuItem");
            ClickElement(logOutButton);
        }

        private bool CheckErrors(RemoteWebDriver driver)
        {
            IWebElement errorElement = GetWebElementById(driver, "reg_error_inner");
            return errorElement != null ? true : false;
        }

        private string GetErrorText(RemoteWebDriver driver)
        {
            IWebElement errorElement = GetWebElementById(driver, "reg_error_inner");
            string textError = errorElement.Text;
            return textError;
        }
    }
}
