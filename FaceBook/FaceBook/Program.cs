using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions.Internal;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SpecBind.Selenium;

namespace FaceBook
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var proxyUsername = "movses21";
            var proxyPassword = "2V1G10W6Xy";
            var proxyAddress = "5.101.64.137";
            var proxyPort = "34530";

            PhantomJSDriverService service = PhantomJSDriverService.CreateDefaultService();

            service.Proxy = string.Format("{0}:{1}", proxyAddress, proxyPort);
            service.ProxyAuthentication = string.Format("{0}:{1}", proxyUsername, proxyPassword);
            service.ProxyType = "http";
            service.DiskCache = true;
            service.LoadImages = false;
            service.WebSecurity = false;
            service.HideCommandPromptWindow = true;
            service.IgnoreSslErrors = true;
            service.WebSecurity = false;
            service.LocalToRemoteUrlAccess = true;
            service.LoadImages = false;

            var driver = new PhantomJSDriver();


            driver.Navigate().GoToUrl("https://2ip.ru/");

            Thread.Sleep(3000);

            var f = driver.FindElements(By.Id("d_clip_button"));


            Thread.Sleep(3000);


            Thread.Sleep(10000);
            //var service = new FaceBookService();

            /*var inpuDataProvider = new InputDataProvider("usersDB.xlsx");

            var userList = service.GetRegistrationUserData(inpuDataProvider);

            service.Registration(driver, userList.usersData);*/
        }
    }
}
