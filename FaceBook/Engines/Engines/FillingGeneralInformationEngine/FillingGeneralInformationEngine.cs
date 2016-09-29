using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Constants;
using Constants.EnumExtensions;
using Engines.Engines.Models;
using Helpers.HtmlHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
//using Serilog;
using OpenQA.Selenium.Support.UI;

namespace Engines.Engines.FillingGeneralInformationEngine
{
    public class FillingGeneralInformationEngine : AbstractEngine<FillingGeneralInformationModel, VoidResult>
    {
        protected override VoidResult ExecuteEngine(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            //Log.Logger = new LoggerConfiguration().WriteTo.File("facebook-logs.txt").CreateLogger();

            FillingWorkAndEducation(driver, model);

            FillingLiving(driver, model);

            FillingsRelationship(driver, model);

            return new VoidResult();
        }

        private void FillingWorkAndEducation(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {

            NavigateToUrl(driver, model.UserHomePageUrl + SettingsUrl.OverviewOptionsEducationPostfix.GetDiscription());

            Thread.Sleep(500);

            AvoidFacebookMessage(driver);

            FillWorkSection(driver, model);
            
            //FillSkillsSection(driver, model);

            FillUniversutySection(driver, model);

            FillSchoolSection(driver, model);
        }

        private void FillingLiving(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            NavigateToUrl(driver, model.UserHomePageUrl + SettingsUrl.OverviewOptionsLivingnPostfix.GetDiscription());

            Thread.Sleep(500);

            AvoidFacebookMessage(driver);

            FillCurrentCity(driver, model);

            FillNativeCity(driver, model);
        }

        private void FillingContactInfo(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            NavigateToUrl(driver, model.UserHomePageUrl + SettingsUrl.OverviewOptionsContactInfoPostfix.GetDiscription());

            Thread.Sleep(1000);
        }

        private void FillingsRelationship(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            NavigateToUrl(driver,
                model.UserHomePageUrl + SettingsUrl.OverviewOptionsRelationshipPostfix.GetDiscription());

            Thread.Sleep(500);

            AvoidFacebookMessage(driver);

            FillFamilyStatus(driver, model);
        }

        private void FillWorkSection(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            // work

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var workLink =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("._6a._5u5j._6b"))).FirstOrDefault(m => m.Text == "Укажите место работы");
            if (workLink == null) return;
            HtmlHelper.ClickElement(workLink);

            Thread.Sleep(2000);

            var company = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("employer_name"))).FirstOrDefault();
            var post = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("position_text"))).FirstOrDefault();
            var city = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("location_text"))).FirstOrDefault();
            var description = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("description"))).FirstOrDefault();
            var buttonWorkApply = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("__submit__"))).FirstOrDefault();

            if (company != null)
            {
                company.Clear();
                company.SendKeys(model.Company);
            }

            Thread.Sleep(2000);

            var result = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.TagName("li")))
                .FirstOrDefault(m => m.GetAttribute("class") == "page");
            HtmlHelper.ClickElement(result);

            if (post != null)
            {
                post.Clear();
                post.SendKeys(model.Post);
            }

            Thread.Sleep(2000);

            result = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.TagName("li")))
                .FirstOrDefault(m => m.GetAttribute("class") == "page");

            HtmlHelper.ClickElement(result);

            if (city != null)
            {
                city.Clear();
                city.SendKeys(model.CityWork);
            }

            Thread.Sleep(2000);

            result = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.TagName("li")))
                .FirstOrDefault(m => m.GetAttribute("class") == "page");
               
            HtmlHelper.ClickElement(result);

            if (description != null)
            {
                description.Clear();
                description.SendKeys(model.DescriptionWork);
            }
            HtmlHelper.ClickElement(result);

            HtmlHelper.ClickElement(buttonWorkApply);
        }

        private void FillSkillsSection(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            // skills

            var skillLink =
                driver.GetElementsByCssSelector("._6a._5u5j._6b")
                    .FirstOrDefault(m => m.Text == "Укажите умения и навыки");
            if (skillLink != null)
            {
                HtmlHelper.ClickElement(skillLink);

                var skills = HtmlHelper.GetElementByClass(driver, "_58al");
                var buttonSkillApply = HtmlHelper.GetElementByName(driver, "__submit__");

                skills.Clear();
                skills.SendKeys(model.Skills);
                Thread.Sleep(500);
                SendKeys.SendWait("{Enter}");
                Thread.Sleep(500);


                HtmlHelper.ClickElement(buttonSkillApply);
                Thread.Sleep(1000);
            }
        }

        private void FillUniversutySection(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            // Univercity

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var univercityLink =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("._6a._5u5j._6b"))).FirstOrDefault(m => m.Text == "Укажите вуз");

            if (univercityLink == null) return;
            HtmlHelper.ClickElement(univercityLink);

            Thread.Sleep(1500);

            var univercity = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("school_text"))).FirstOrDefault();
            var descriptionUnivercity = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("description"))).FirstOrDefault();
            var specialization1 = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("concentration_text[0]"))).FirstOrDefault();
            var specialization2 = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("concentration_text[1]"))).FirstOrDefault();
            var specialization3 = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("concentration_text[2]"))).FirstOrDefault();
            var buttonUnivercityApply = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("__submit__"))).FirstOrDefault();

            if (univercity != null)
            {
                univercity.Clear();
                univercity.SendKeys(model.Univercity);
            }

            Thread.Sleep(2000);

            var result = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.TagName("li")))
                .FirstOrDefault(m => m.GetAttribute("class") == "page");

            HtmlHelper.ClickElement(result);

            var city = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("city"))).FirstOrDefault();
            if (city != null)
            {
                city.SendKeys(model.UnivercityCity);
            }

            if (descriptionUnivercity != null)
            {
                descriptionUnivercity.Clear();
                descriptionUnivercity.SendKeys(model.DescriptionUnivercity);
            }

            if (specialization1 != null)
            {
                specialization1.Clear();
                specialization1.SendKeys(model.Specializations);
            }

            Thread.Sleep(2000);
                
            result = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.TagName("li")))
                .FirstOrDefault(m => m.GetAttribute("class") == "page");

            if (result != null && result.Displayed)
            {
                HtmlHelper.ClickElement(result);
            }

            HtmlHelper.ClickElement(buttonUnivercityApply);
        }

        private void FillSchoolSection(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            // School

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var schoolLink = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("._6a._5u5j._6b"))).FirstOrDefault(m => m.Text == "Укажите среднюю школу");
           
            if (schoolLink == null) return;

            HtmlHelper.ClickElement(schoolLink);
                
            var school = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("school_text"))).FirstOrDefault();
            var descriptionSchool = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("description"))).FirstOrDefault();
            var buttonSchoolApply = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("__submit__"))).FirstOrDefault();

            if (descriptionSchool != null)
            {
                descriptionSchool.Clear();
                descriptionSchool.SendKeys(model.DescriptionSchool);
            }
            
            if (school != null)
            {
                school.Clear();
                school.SendKeys(model.School);
            }
            
            Thread.Sleep(2000);

            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.ClassName("compact")))
                .FirstOrDefault(m => m.GetAttribute("class") == "page");

            var result = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.TagName("li")))
                .FirstOrDefault(m => m.GetAttribute("class") == "page");
            HtmlHelper.ClickElement(result);

            var city = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("city"))).FirstOrDefault();
            if (city != null && city.Displayed)
            {
                city.SendKeys(model.SchoolCity);
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.ClassName("basic")))
                    .FirstOrDefault(m => m.GetAttribute("class") == "page");

                Thread.Sleep(2000);

                result = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.TagName("li")))
                .FirstOrDefault(m => m.GetAttribute("class") == "page");
                HtmlHelper.ClickElement(result);
            }

            HtmlHelper.ClickElement(buttonSchoolApply);
        }

        private void FillCurrentCity(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var linkCurrentCity =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("._6a._5u5j._6b")))
                    .FirstOrDefault(m => m.Text == "Добавьте город проживания");

            if (linkCurrentCity == null) return;

            HtmlHelper.ClickElement(linkCurrentCity);

            Thread.Sleep(2000);

            var currentCity =   wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(".inputtext.textInput"))).FirstOrDefault();
            var buttonCurrentCityApply = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("__submit__"))).FirstOrDefault();

            if (currentCity != null)
            {
                currentCity.Clear();
                currentCity.SendKeys(model.CurrentCity);
            }

            Thread.Sleep(2000);

            var result = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.TagName("li")))
                .FirstOrDefault(m => m.GetAttribute("class") == "page");

            HtmlHelper.ClickElement(result);

            HtmlHelper.ClickElement(buttonCurrentCityApply);

            Thread.Sleep(2000);
        }

        private void FillNativeCity(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var linkNativeCity =
                 wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("._6a._5u5j._6b"))).FirstOrDefault(m => m.Text == "Добавьте родной город");
            
            if (linkNativeCity == null) return;
            HtmlHelper.ClickElement(linkNativeCity);

            var nativeCity =  wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(".inputtext.textInput"))).FirstOrDefault();
            var buttonNativeCityApply =  wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("__submit__"))).FirstOrDefault();

            if (nativeCity != null)
            {
                nativeCity.Clear();
                nativeCity.SendKeys(model.NativeCity);
            }

            Thread.Sleep(2000);

            var result = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.TagName("li")))
                .FirstOrDefault(m => m.GetAttribute("class") == "page");

            HtmlHelper.ClickElement(result);

            HtmlHelper.ClickElement(buttonNativeCityApply);

            Thread.Sleep(2000);
        }

        private void FillFamilyStatus(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var linkFamilyStatus =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("._6a._5u5j._6b")))
                    .FirstOrDefault(m => m.Text == "Укажите информацию о семейном положении");

            if (linkFamilyStatus == null) return;

            HtmlHelper.ClickElement(linkFamilyStatus);

            Thread.Sleep(2000);

            var selectFamilyStatus = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("status"))).FirstOrDefault();

            if (selectFamilyStatus == null) return;

            HtmlHelper.ClickElement(selectFamilyStatus);

            Thread.Sleep(2000);

            var answer =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.TagName("option")))
                    .FirstOrDefault(
                        m =>
                            m.GetAttribute("familystatus") == model.FamilyStatus.ToString("G") ||
                            m.GetAttribute("value") == model.FamilyStatus.ToString("G"));

            var buttonFamilyStatus = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("__submit__"))).FirstOrDefault();
            
            HtmlHelper.ClickElement(answer);

            HtmlHelper.ClickElement(buttonFamilyStatus);
        }
    }
}
