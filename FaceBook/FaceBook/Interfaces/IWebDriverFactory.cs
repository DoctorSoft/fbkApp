using InputData.InputModels;
using OpenQA.Selenium.Remote;

namespace FaceBook.Interfaces
{
    public interface IWebDriverFactory
    {
        RemoteWebDriver GetDriver(ProxyData proxyData);
    }
}
