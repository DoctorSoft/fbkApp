using FaceBook.Interfaces;
using InputData.InputModels;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace FaceBook.Implementation
{
    public class ChromeWebDriverFactory : IWebDriverFactory
    {
        public RemoteWebDriver GetDriver(ProxyData proxyData)
        {
            // todo: add proxy
            return new ChromeDriver();
        }
    }
}
