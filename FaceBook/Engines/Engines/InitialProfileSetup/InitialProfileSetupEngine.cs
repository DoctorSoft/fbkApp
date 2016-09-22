using System.Linq;
using System.Threading;
using Engines.EnumExtensions;
using Engines.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Engines.Engines.InitialProfileSetup
{
    public class InitialProfileSetupEngine : AbstractEngine<InitialProfileSetupModel, VoidResult>
    {
        protected override VoidResult ExecuteEngine(RemoteWebDriver driver, InitialProfileSetupModel model)
        {
            SetPrivacyOptions(driver);

            return new VoidResult();
        }
        private bool SetPrivacyOptions(RemoteWebDriver driver)
        {
            NavigateToUrl(driver, SettingsUrl.PrivacyUrl.GetDiscription());

            Thread.Sleep(6000);
            driver.Keyboard.SendKeys(Keys.Enter);

            IWebElement visibilityOfPublications = GetWebElementByClass(driver, "fbSettingsListItemContent fcg");
            ClickElement(visibilityOfPublications);

            Thread.Sleep(500);

            IWebElement visibilityOfPublicationsDropDownList = GetWebElementByClass(driver, "_55pe");
            ClickElement(visibilityOfPublicationsDropDownList);

            Thread.Sleep(500);

            IWebElement visibilityToAll = GetWebElementByClass(driver, "_54nh _4chm _48u0");
            ClickElement(visibilityToAll);
            
            return true;
            
        }
        private IWebElement GetWebElementByName(RemoteWebDriver driver, string name)
        {
            return driver.FindElements(By.Name(name)).FirstOrDefault();
        }

        private IWebElement GetWebElementById(RemoteWebDriver driver, string idName)
        {
            return driver.FindElements(By.Id(idName)).FirstOrDefault();
        }
        private IWebElement GetWebElementByClass(RemoteWebDriver driver, string className)
        {
            return driver.FindElements(By.ClassName(className)).FirstOrDefault();
        }
        private void ClickElement(IWebElement element)
        {
            if (element == null)
            {
                return;
            }
            element.Click();
        }
    }
}
