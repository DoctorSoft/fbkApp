using System;
using System.Linq;
using System.Threading;
using Constants;
using Engines.Engines.Models;
using Helpers.HtmlHelpers;
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

                IWebElement lastNameInput = HtmlHelper.GetElementByName(driver, "firstname");
                IWebElement firstNameInput = HtmlHelper.GetElementByName(driver, "lastname");
                IWebElement emailInput = HtmlHelper.GetElementByName(driver, "reg_email__");
                IWebElement confirmationEmailInput = HtmlHelper.GetElementByName(driver, "reg_email_confirmation__");
                IWebElement passwordInput = HtmlHelper.GetElementByName(driver, "reg_passwd__");
                IWebElement birthdayDay = HtmlHelper.GetElementByName(driver, "birthday_day");
                IWebElement birthdayMonth = HtmlHelper.GetElementByName(driver, "birthday_month");
                IWebElement birthdayYear = HtmlHelper.GetElementByName(driver, "birthday_year");
                IWebElement submitButton = HtmlHelper.GetElementByName(driver, "websubmit");

                IWebElement gender = model.Gender == Gender.Female ? HtmlHelper.GetElementById(driver, "u_0_d") : HtmlHelper.GetElementById(driver, "u_0_e");

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
            IWebElement profileOptionElement = HtmlHelper.GetElementById(driver, "userNavigationLabel");
            return profileOptionElement == null;
        }

        private void LogOut(RemoteWebDriver driver)
        {
            driver.Keyboard.SendKeys(Keys.Enter);
            IWebElement profileOptionElement = HtmlHelper.GetElementById(driver, "userNavigationLabel");
            ClickElement(profileOptionElement);
            IWebElement logOutButton = HtmlHelper.GetElementByClass(driver, "_54ni navSubmenu __MenuItem");
            ClickElement(logOutButton);
        }

        private bool CheckErrors(RemoteWebDriver driver)
        {
            Thread.Sleep(2000);
            driver.Keyboard.SendKeys(Keys.Enter);
            IWebElement errorElement = HtmlHelper.GetElementById(driver, "reg_error_inner");
            return errorElement != null ? true : false;
        }

        private string GetErrorText(RemoteWebDriver driver)
        {
            IWebElement errorElement = HtmlHelper.GetElementById(driver, "reg_error_inner");
            string textError = errorElement.Text;
            return textError;
        }
    }
}
