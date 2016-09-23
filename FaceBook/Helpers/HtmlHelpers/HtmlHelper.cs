using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Helpers.HtmlHelpers
{
    public static class HtmlHelper
    {
        public static IWebElement GetElementById(RemoteWebDriver driver, string id)
        {
            return driver.FindElements(By.Id(id)).FirstOrDefault();
        }

        public static IWebElement GetElementByClass(RemoteWebDriver driver, string className)
        {
            return driver.FindElements(By.ClassName(className)).FirstOrDefault();
        }

        public static IWebElement GetElementByName(RemoteWebDriver driver, string name)
        {
            return driver.FindElements(By.Name(name)).FirstOrDefault();
        }

        public static IWebElement GetElementByCssSelector(RemoteWebDriver driver, string cssSelector)
        {
            return driver.FindElements(By.CssSelector(cssSelector)).FirstOrDefault();
        }
    }
}
