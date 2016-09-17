using System;
using System.Threading;
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
            catch (Exception ex)
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
    }
}
