using System.Collections.Generic;
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

namespace Engines.Engines.FillingGeneralInformationEngine
{
    public class FillingGeneralInformationEngine : AbstractEngine<FillingGeneralInformationModel, VoidResult>
    {
        protected override VoidResult ExecuteEngine(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {

            //Log.Logger = new LoggerConfiguration().WriteTo.File("facebook-logs.txt").CreateLogger();

            //FillingWorkAndEducation(driver, model);

            FillingLiving(driver, model);

            //FillingsRelationship(driver, model);

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

            var workLink =
                driver.GetElementsByCssSelector("._6a._5u5j._6b").FirstOrDefault(m => m.Text == "Укажите место работы");
            if (workLink != null)
            {
                HtmlHelper.ClickElement(workLink);

                Thread.Sleep(2000);

                var company = HtmlHelper.GetElementByName(driver, "employer_name");
                var post = HtmlHelper.GetElementByName(driver, "position_text");
                var city = HtmlHelper.GetElementByName(driver, "location_text");
                var description = HtmlHelper.GetElementByName(driver, "description");
                var buttonWorkApply = HtmlHelper.GetElementByName(driver, "__submit__");

                company.Clear();
                company.SendKeys(model.Company);
                Thread.Sleep(1000);
                var result = driver.FindElements(By.TagName("li"))
                    .FirstOrDefault(m => m.GetAttribute("class") == "page");
                Thread.Sleep(500);
                HtmlHelper.ClickElement(result);
                Thread.Sleep(500);

                post.Clear();
                post.SendKeys(model.Post);
                Thread.Sleep(1000);
                result = driver.FindElements(By.TagName("li")).FirstOrDefault(m => m.GetAttribute("class") == "page");
                Thread.Sleep(500);
                HtmlHelper.ClickElement(result);
                Thread.Sleep(500);

                city.Clear();
                city.SendKeys(model.CityWork);
                Thread.Sleep(1000);
                result = driver.FindElements(By.TagName("li")).FirstOrDefault(m => m.GetAttribute("class") == "page");
                Thread.Sleep(500);
                HtmlHelper.ClickElement(result);
                Thread.Sleep(500);

                description.Clear();
                description.SendKeys(model.DescriptionWork);
                Thread.Sleep(1000);
                result = driver.FindElements(By.TagName("li")).FirstOrDefault(m => m.GetAttribute("class") == "page");
                Thread.Sleep(500);
                HtmlHelper.ClickElement(result);
                Thread.Sleep(500);


                HtmlHelper.ClickElement(buttonWorkApply);
                Thread.Sleep(1000);
            }
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

            Thread.Sleep(1000);

            var univercityLink =
                driver.GetElementsByCssSelector("._6a._5u5j._6b").FirstOrDefault(m => m.Text == "Укажите вуз");
            if (univercityLink != null)
            {
                HtmlHelper.ClickElement(univercityLink);

                Thread.Sleep(1500);

                var univercity = HtmlHelper.GetElementByName(driver, "school_text");
                var descriptionUnivercity = HtmlHelper.GetElementByName(driver, "description");
                var specialization1 = HtmlHelper.GetElementByName(driver, "concentration_text[0]");
                var specialization2 = HtmlHelper.GetElementByName(driver, "concentration_text[1]");
                var specialization3 = HtmlHelper.GetElementByName(driver, "concentration_text[2]");
                var buttonUnivercityApply = HtmlHelper.GetElementByName(driver, "__submit__");

                univercity.Clear();
                univercity.SendKeys(model.Univercity);
                Thread.Sleep(1000);
                var result = driver.FindElements(By.TagName("li"))
                    .FirstOrDefault(m => m.GetAttribute("class") == "page");
                Thread.Sleep(500);
                HtmlHelper.ClickElement(result);
                Thread.Sleep(500);


                var city = HtmlHelper.GetElementByName(driver, "city");
                if (city != null)
                {
                    city.SendKeys(model.UnivercityCity);
                }

                descriptionUnivercity.Clear();
                descriptionUnivercity.SendKeys(model.DescriptionUnivercity);
                Thread.Sleep(500);

                specialization1.Clear();
                specialization1.SendKeys(model.Specializations);
                Thread.Sleep(1000);

                result = driver.FindElements(By.TagName("li"))
                    .FirstOrDefault(m => m.GetAttribute("class") == "page");
                if (result != null && result.Displayed)
                {
                    HtmlHelper.ClickElement(result);
                }
                Thread.Sleep(500);
                HtmlHelper.ClickElement(buttonUnivercityApply);
                Thread.Sleep(1000);
            }
        }

        private void FillSchoolSection(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            // School

            Thread.Sleep(1000);

            var schoolLink =
                driver.GetElementsByCssSelector("._6a._5u5j._6b").FirstOrDefault(m => m.Text == "Укажите среднюю школу");
            if (schoolLink != null)
            {
                HtmlHelper.ClickElement(schoolLink);

                Thread.Sleep(2000);

                var school = HtmlHelper.GetElementByName(driver, "school_text");
                var descriptionSchool = HtmlHelper.GetElementByName(driver, "description");
                var buttonSchoolApply = HtmlHelper.GetElementByName(driver, "__submit__");

                school.Clear();
                school.SendKeys(model.School);
                Thread.Sleep(1000);
                var result = driver.FindElements(By.TagName("li"))
                    .FirstOrDefault(m => m.GetAttribute("class") == "page");
                Thread.Sleep(500);
                HtmlHelper.ClickElement(result);
                Thread.Sleep(500);

                var city = HtmlHelper.GetElementByName(driver, "city");
                if (city != null && city.Displayed)
                {
                    city.SendKeys(model.SchoolCity);
                    result = driver.FindElements(By.TagName("li"))
                    .FirstOrDefault(m => m.GetAttribute("class") == "page");
                    HtmlHelper.ClickElement(result);
                }

                descriptionSchool.Clear();
                descriptionSchool.SendKeys(model.DescriptionSchool);
                Thread.Sleep(500);
                HtmlHelper.ClickElement(result);
                Thread.Sleep(500);

                HtmlHelper.ClickElement(buttonSchoolApply);
                Thread.Sleep(1000);
            }
        }

        private void FillCurrentCity(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            var linkCurrentCity =
                driver.GetElementsByCssSelector("._6a._5u5j._6b")
                    .FirstOrDefault(m => m.Text == "Добавьте город проживания");
            if (linkCurrentCity != null)
            {
                HtmlHelper.ClickElement(linkCurrentCity);

                Thread.Sleep(2000);

                var currentCity = HtmlHelper.GetElementByCssSelector(driver, ".inputtext.textInput");
                var buttonCurrentCityApply = HtmlHelper.GetElementByName(driver, "__submit__");

                currentCity.Clear();
                currentCity.SendKeys(model.CurrentCity);

                Thread.Sleep(2000);
                var result = driver.FindElements(By.TagName("li"))
                    .FirstOrDefault(m => m.GetAttribute("class") == "page");
                Thread.Sleep(500);
                HtmlHelper.ClickElement(result);
                Thread.Sleep(500);

                HtmlHelper.ClickElement(buttonCurrentCityApply);
                Thread.Sleep(1000);
            }
        }

        private void FillNativeCity(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            var linkNativeCity =
                driver.GetElementsByCssSelector("._6a._5u5j._6b").FirstOrDefault(m => m.Text == "Добавьте родной город");
            if (linkNativeCity != null)
            {
                HtmlHelper.ClickElement(linkNativeCity);


                Thread.Sleep(2000);

                var nativeCity = HtmlHelper.GetElementByCssSelector(driver, ".inputtext.textInput");
                var buttonNativeCityApply = HtmlHelper.GetElementByName(driver, "__submit__");

                Thread.Sleep(2000);

                nativeCity.Clear();
                nativeCity.SendKeys(model.NativeCity);
                Thread.Sleep(2000);
                var result = driver.FindElements(By.TagName("li")).FirstOrDefault(m => m.GetAttribute("class") == "page");
                Thread.Sleep(500);
                HtmlHelper.ClickElement(result);
                Thread.Sleep(500);

                HtmlHelper.ClickElement(buttonNativeCityApply);
                Thread.Sleep(1000);
            }
        }

        private void FillFamilyStatus(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            var linkFamilyStatus =
                driver.GetElementsByCssSelector("._6a._5u5j._6b")
                    .FirstOrDefault(m => m.Text == "Укажите информацию о семейном положении");
            if (linkFamilyStatus != null)
            {
                HtmlHelper.ClickElement(linkFamilyStatus);

                Thread.Sleep(2000);
                var selectFamilyStatus = driver.GetElementByName("status");

                if (selectFamilyStatus != null)
                {
                    HtmlHelper.ClickElement(selectFamilyStatus);

                    Thread.Sleep(2000);

                    var answer =
                        driver.FindElements(By.TagName("option"))
                            .FirstOrDefault(
                                m =>
                                    m.GetAttribute("familystatus") == model.FamilyStatus.ToString("G") ||
                                    m.GetAttribute("value") == model.FamilyStatus.ToString("G"));
                    var buttonFamilyStatus = HtmlHelper.GetElementByName(driver, "__submit__");

                    Thread.Sleep(500);

                    HtmlHelper.ClickElement(answer);

                    HtmlHelper.ClickElement(buttonFamilyStatus);
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
