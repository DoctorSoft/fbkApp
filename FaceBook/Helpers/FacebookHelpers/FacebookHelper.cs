using System.Linq;
using Helpers.HtmlHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Helpers.FacebookHelpers
{
    public static class FacebookHelper
    {
        public static bool GetLogOutStatus(RemoteWebDriver driver)
        {
            var profileOptionElement = HtmlHelper.GetElementById(driver, "userNavigationLabel");
            return profileOptionElement != null;
        }

        public static string GetHomepageUrl(RemoteWebDriver driver)
        {
            IWebElement links = null;

            if (GetLogOutStatus(driver))
            {
                links =
                    driver.FindElements(By.TagName("a"))
                        .FirstOrDefault(
                            a => a.GetAttribute("title").Contains("Профиль") && a.GetAttribute("class") == "_2s25");
            }

            if (links != null) return links.GetAttribute("href");
            return null;
        }
    }
}
