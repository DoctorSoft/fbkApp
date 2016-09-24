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
            //driver.Navigate().GoToUrl("http://movses21:2V1G10W6Xy@5.101.64.137:34530/");
            driver.Navigate().GoToUrl("http://stackoverflow.com/");

            Thread.Sleep(1500);

            driver.Navigate().GoToUrl("https://2ip.ru/");
       

            return new VoidResult();
        }
    }
}
