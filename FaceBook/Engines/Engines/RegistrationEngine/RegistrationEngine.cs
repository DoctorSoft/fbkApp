using System;
using System.Data;
using System.Linq;
using System.Threading;
using Constants;
using Engines.Engines.Models;
using Helpers.FacebookHelpers;
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
                if (FacebookHelper.GetLogOutStatus(driver))
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

                HtmlHelper.GetSelectElement(birthdayDay, model.Birthday.Day);
                HtmlHelper.GetSelectElement(birthdayMonth, model.Birthday.Month);
                HtmlHelper.GetSelectElement(birthdayYear, model.Birthday.Year);

                HtmlHelper.ClickElement(gender);

                HtmlHelper.ClickElement(submitButton);

                if (CheckErrors(driver))
                {
                    return new ErrorModel
                    {
                        ErrorText = GetErrorText(driver)
                    };
                }

                AvoidFacebookMessage(driver);
            }
            catch (Exception)
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

        private void LogOut(RemoteWebDriver driver)
        {
            driver.Keyboard.SendKeys(Keys.Enter);
            var profileOptionElement = HtmlHelper.GetElementById(driver, "userNavigationLabel");
            ClickElement(profileOptionElement);
            var logOutButton = driver.GetElementByClass("_54ni navSubmenu __MenuItem");
            ClickElement(logOutButton);
        }

        private bool CheckErrors(RemoteWebDriver driver)
        {
            Thread.Sleep(2000);
            driver.Keyboard.SendKeys(Keys.Enter);
            var errorElement = HtmlHelper.GetElementById(driver, "reg_error_inner");
            return errorElement != null ? true : false;
        }

        private string GetErrorText(RemoteWebDriver driver)
        {
            var errorElement = HtmlHelper.GetElementById(driver, "reg_error_inner");
            var textError = errorElement.Text;
            return textError;
        }
    }
}
