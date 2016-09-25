using System;
using System.Threading;
using System.Windows.Forms;
using Helpers.HtmlHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Engines.Engines
{
    public abstract class AbstractEngine<TModel, TResult> : IEngine<TModel, TResult>
        where TResult : new()
    {
        protected abstract TResult ExecuteEngine(RemoteWebDriver driver, TModel model);

        public TResult Execute(RemoteWebDriver driver, TModel model)
        {
            TResult result;

            try
            {
                result = ExecuteEngine(driver, model);
            }
            catch (Exception)
            {
                // todo: Log it
                result = new TResult();
            }

            return result;
        }

        protected bool NavigateToUrl(RemoteWebDriver driver, string url = "https://www.facebook.com/")
        {
            driver.Navigate().GoToUrl(url);

            Thread.Sleep(800);

            return true;
        }

        protected void AvoidFacebookMessage(RemoteWebDriver driver)
        {
            Thread.Sleep(500);
            var modalBackground = driver.GetElementByClass("_3ixn");
            if (modalBackground != null)
            {
                SendKeys.SendWait("{Tab}");
                SendKeys.SendWait("{Enter}");

                var okButton = driver.GetElementByClass("_2z1w");
                ClickElement(okButton);
            }
        }

        protected void ClickElement(IWebElement element)
        {
            if (element == null)
            {
                return;
            }
            element.Click();
        }
    }
}
