using System.Threading;
using Engines.Engines.Models;
using OpenQA.Selenium.Remote;

namespace Engines.Engines.GetIpEngine
{
    public class GetIpEngine: AbstractEngine<GetIpModel, VoidResult>
    {
        protected override VoidResult ExecuteEngine(RemoteWebDriver driver, GetIpModel model)
        {
            driver.Navigate().GoToUrl("https://2ip.ru/");

            Thread.Sleep(30000);

            return new VoidResult();
        }
    }
}
