using FaceBook.Constants;
using FaceBook.Interfaces;
using InputData.InputModels;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;

namespace FaceBook.Implementation
{
    public class PhantomJsWebDriverFactory : IWebDriverFactory
    {
        public RemoteWebDriver GetDriver(ProxyData proxyData)
        {
            PhantomJSDriverService service = PhantomJSDriverService.CreateDefaultService();

            /*service.Proxy = string.Format("{0}:{1}", proxyData.ProxyAddress, proxyData.ProxyPort);
            //service.ProxyAuthentication = string.Format("{0}:{1}", proxyData.ProxyUserName, proxyData.ProxyPassword);
            //service.ProxyType = proxyData.ProxyType;
            service.DiskCache = true;
            service.LoadImages = false;
            service.WebSecurity = false;
            service.HideCommandPromptWindow = true;
            service.IgnoreSslErrors = true;
            service.WebSecurity = false;
            service.LocalToRemoteUrlAccess = true;
            service.LoadImages = false;*/

            var driver = new PhantomJSDriver(service);

            return driver;
        }
    }
}
