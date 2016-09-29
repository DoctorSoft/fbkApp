using FaceBook.Interfaces;
using InputData.InputModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace FaceBook.Implementation
{
    public class ChromeWebDriverFactory : IWebDriverFactory
    {
        public RemoteWebDriver GetDriver(ProxyData proxyData)
        {
            var options = new ChromeOptions
            {
                Proxy = new Proxy
                {
                    HttpProxy = proxyData.ProxyAddress + ":" + proxyData.ProxyPort,
                    SslProxy = proxyData.ProxyAddress + ":" + proxyData.ProxyPort
                }
            };

            // todo: add proxy
            return new ChromeDriver();
        }
    }
}
