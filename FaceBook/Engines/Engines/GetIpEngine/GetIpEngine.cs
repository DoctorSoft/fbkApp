using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace Engines.Engines.GetIpEngine
{
    public class GetIpEngine: AbstractEngine<GetIpModel, VoidResult>
    {
        protected override VoidResult ExecuteEngine(RemoteWebDriver driver, GetIpModel model)
        {
            driver.Navigate().GoToUrl("https://2ip.ru/");

            return new VoidResult();
        }
    }
}
