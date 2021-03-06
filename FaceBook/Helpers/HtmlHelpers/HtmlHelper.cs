﻿using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Helpers.HtmlHelpers
{
    public static class HtmlHelper
    {
        public static IWebElement GetElementById(this IWebDriver driver, string id)
        {
            return driver.FindElements(By.Id(id)).FirstOrDefault();
        }

        public static IWebElement GetElementByClass(this IWebDriver driver, string className)
        {
            return driver.FindElements(By.ClassName(className)).FirstOrDefault();
        }

        public static IWebElement GetElementByName(this IWebDriver driver, string name)
        {
            return driver.FindElements(By.Name(name)).FirstOrDefault();
        }

        public static IWebElement GetElementByCssSelector(this IWebDriver driver, string cssSelector)
        {
            return driver.FindElements(By.CssSelector(cssSelector)).FirstOrDefault();
        }

        public static IReadOnlyCollection<IWebElement> GetElementsByCssSelector(this IWebDriver driver, string cssSelector)
        {
            return driver.FindElements(By.CssSelector(cssSelector));
        }

        public static IWebElement GetElementByXPath(this IWebDriver driver, string xPath)
        {
            return driver.FindElements(By.XPath(xPath)).FirstOrDefault();
        }

        public static IWebElement GetElementByTagName(this IWebDriver driver, string tagName)
        {
            return driver.FindElements(By.XPath(tagName)).FirstOrDefault();
        }

        public static void ClickElement(IWebElement element)
        {
            if (element == null)
            {
                return;
            }
            element.Click();
        }

        public static void GetSelectElement(IWebElement selectElement, int value)
        {
            if (selectElement == null || value == 0)
            {
                return;
            }

            ClickElement(selectElement.FindElement(By.CssSelector("option[value='" + value + "']")));
        }
    }
}
