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
using OpenQA.Selenium.Support.UI;

namespace Engines.Engines.FillingGeneralInformationEngine
{
    public class FillingGeneralInformationEngine : AbstractEngine<FillingGeneralInformationModel, VoidResult>
    {
        private const int SleepTime = 4000;

        protected override VoidResult ExecuteEngine(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            FillingWorkAndEducation(driver, model);

            FillingLiving(driver, model);
            
            FillingsRelationship(driver, model);
            
            return new VoidResult();
        }

        private void FillingWorkAndEducation(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {

            NavigateToUrl(driver, model.UserHomePageUrl + SettingsUrl.OverviewOptionsEducationPostfix.GetDiscription());

            Thread.Sleep(SleepTime);

            AvoidFacebookMessage(driver);

            FillWorkSection(driver, model);

            Thread.Sleep(SleepTime);

            FillSkillsSection(driver, model);

            Thread.Sleep(SleepTime);

            FillUniversitySection(driver, model);

            Thread.Sleep(SleepTime);

            FillSchoolSection(driver, model);

            Thread.Sleep(SleepTime);
        }

        private void FillingLiving(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            NavigateToUrl(driver, model.UserHomePageUrl + SettingsUrl.OverviewOptionsLivingnPostfix.GetDiscription());

            Thread.Sleep(SleepTime);

            AvoidFacebookMessage(driver);

            FillCurrentCity(driver, model);

            Thread.Sleep(SleepTime);

            FillNativeCity(driver, model);

            Thread.Sleep(SleepTime);
        }
        
        private void FillingsRelationship(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            NavigateToUrl(driver,
                model.UserHomePageUrl + SettingsUrl.OverviewOptionsRelationshipPostfix.GetDiscription());

            Thread.Sleep(SleepTime);

            AvoidFacebookMessage(driver);

            FillFamilyStatus(driver, model);

            Thread.Sleep(SleepTime);
        }

        private void FillWorkSection(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            // work

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var workLink =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("._6a._5u5j._6b")))
                    .FirstOrDefault(m => m.Text == "Укажите место работы");
            if (workLink == null) return;
            HtmlHelper.ClickElement(workLink);

            Thread.Sleep(SleepTime);

            var company =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("employer_name"))).FirstOrDefault();
            var post =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("position_text"))).FirstOrDefault();
            var city =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("location_text"))).FirstOrDefault();
            var description =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("description"))).FirstOrDefault();
            var buttonWorkApply =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("__submit__"))).FirstOrDefault();

            if (company != null)
            {
                company.Clear();
                company.SendKeys(model.Company);
            }

            Thread.Sleep(SleepTime);

            var result = ChooseAnswerInComboBox(driver);
            HtmlHelper.ClickElement(result);
            
            Thread.Sleep(SleepTime);

            if (post != null)
            {
                post.Clear();
                post.SendKeys(model.Post);
            }

            Thread.Sleep(SleepTime);

            result = ChooseAnswerInComboBox(driver);
            HtmlHelper.ClickElement(result);

            Thread.Sleep(SleepTime);

            if ((city != null) && (city.Text == ""))
            {
                city.Clear();
                city.SendKeys(model.CityWork);
            }

            Thread.Sleep(SleepTime);

            result = ChooseAnswerInComboBox(driver);
            HtmlHelper.ClickElement(result);

            Thread.Sleep(SleepTime);

            if (description != null)
            {
                description.Clear();
                description.SendKeys(model.DescriptionWork);
            }

            Thread.Sleep(SleepTime);

            HtmlHelper.ClickElement(buttonWorkApply);
        }

        private void FillSkillsSection(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            // skills

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var skillLink =
                driver.GetElementsByCssSelector("._6a._5u5j._6b")
                    .FirstOrDefault(m => m.Text == "Укажите умения и навыки");

            if (skillLink == null) return;

            HtmlHelper.ClickElement(skillLink);

            Thread.Sleep(SleepTime);

            var skills = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("._58al")));
            var buttonSkillApply = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("__submit__"))).FirstOrDefault();

            if (skills != null)
            {
                skills.SendKeys(model.Skills);
            }

            Thread.Sleep(SleepTime);

            driver.Keyboard.PressKey(OpenQA.Selenium.Keys.Enter);
            Thread.Sleep(SleepTime);

            HtmlHelper.ClickElement(buttonSkillApply);
        }

        private void FillUniversitySection(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            // Univercity

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var univercityLink =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("._6a._5u5j._6b")))
                    .FirstOrDefault(m => m.Text == "Укажите вуз");

            if (univercityLink == null) return;
            HtmlHelper.ClickElement(univercityLink);

            Thread.Sleep(SleepTime);

            var univercity =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("school_text"))).FirstOrDefault();
            var descriptionUnivercity =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("description"))).FirstOrDefault();
            var specialization1 =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("concentration_text[0]")))
                    .FirstOrDefault();
            var specialization2 =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("concentration_text[1]")))
                    .FirstOrDefault();
            var specialization3 =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("concentration_text[2]")))
                    .FirstOrDefault();
            var buttonUnivercityApply =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("__submit__"))).FirstOrDefault();

            if (univercity != null)
            {
                univercity.Clear();
                univercity.SendKeys(model.Univercity);
            }

            Thread.Sleep(SleepTime);

            var result = ChooseAnswerInComboBox(driver);

            Thread.Sleep(SleepTime);
            
            HtmlHelper.ClickElement(result);

            var titlesNewUnivercityWindow = driver.FindElements(By.CssSelector("lfloat _ohe")).Where(m=>m.Displayed);
            var titleNewUnivercityWindow = titlesNewUnivercityWindow.FirstOrDefault(m => m.Text == "Создать новый вуз");

            if (titleNewUnivercityWindow != null) CreateNewUnivercity(driver, model);

            var city = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("city"))).FirstOrDefault();
            if (city != null && city.Displayed)
            {
                city.SendKeys(model.UnivercityCity);

                Thread.Sleep(SleepTime);

                result = ChooseAnswerInComboBox(driver);

                HtmlHelper.ClickElement(result);
            }

            Thread.Sleep(SleepTime);

            if (descriptionUnivercity != null)
            {
                descriptionUnivercity.Clear();
                descriptionUnivercity.SendKeys(model.DescriptionUnivercity);
            }

            Thread.Sleep(SleepTime);

            if (specialization1 != null)
            {
                specialization1.Clear();
                specialization1.SendKeys(model.Specializations);
            }

            Thread.Sleep(SleepTime);

            result = ChooseAnswerInComboBox(driver);

            if (result != null && result.Displayed)
            {
                HtmlHelper.ClickElement(result);
            }

            Thread.Sleep(SleepTime);

            HtmlHelper.ClickElement(buttonUnivercityApply);
        }

        private void FillSchoolSection(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            // School

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var schoolLink =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("._6a._5u5j._6b")))
                    .FirstOrDefault(m => m.Text == "Укажите среднюю школу");

            if (schoolLink == null) return;

            HtmlHelper.ClickElement(schoolLink);

            Thread.Sleep(SleepTime);

            var school =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("school_text"))).FirstOrDefault();
            var descriptionSchool =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("description"))).FirstOrDefault();
            var buttonSchoolApply =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("__submit__"))).FirstOrDefault();

            if (descriptionSchool != null)
            {
                descriptionSchool.Clear();
                descriptionSchool.SendKeys(model.DescriptionSchool);
            }

            Thread.Sleep(SleepTime);

            if (school != null)
            {
                school.Clear();
                school.SendKeys(model.School);
            }

            Thread.Sleep(SleepTime);

            var result = ChooseAnswerInComboBox(driver);

            HtmlHelper.ClickElement(result);

            Thread.Sleep(SleepTime);

            var titlesNewSchoolWindow = driver.FindElements(By.CssSelector("lfloat _ohe")).Where(m => m.Displayed);
            var titleNewSchoolWindow = titlesNewSchoolWindow.FirstOrDefault(m => m.Text == "Создать новую школу");
            if (titleNewSchoolWindow != null) CreateNewSchool(driver, model);

            var city = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("city"))).FirstOrDefault();
            if (city != null && city.Displayed)
            {
                city.SendKeys(model.SchoolCity);

                Thread.Sleep(SleepTime);

                result = ChooseAnswerInComboBox(driver);

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

            Thread.Sleep(SleepTime);

            var currentCity =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(".inputtext.textInput")))
                    .FirstOrDefault();
            var buttonCurrentCityApply =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("__submit__"))).FirstOrDefault();

            if (currentCity != null)
            {
                currentCity.Clear();
                currentCity.SendKeys(model.CurrentCity);
            }

            Thread.Sleep(SleepTime);

            var result = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.TagName("li")))
                .FirstOrDefault(m => m.GetAttribute("class") == "page");

            Thread.Sleep(SleepTime);

            HtmlHelper.ClickElement(result);

            Thread.Sleep(SleepTime);

            HtmlHelper.ClickElement(buttonCurrentCityApply);
        }

        private void FillNativeCity(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var linkNativeCity =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("._6a._5u5j._6b")))
                    .FirstOrDefault(m => m.Text == "Добавьте родной город");

            if (linkNativeCity == null) return;
            HtmlHelper.ClickElement(linkNativeCity);

            Thread.Sleep(SleepTime);

            var nativeCity =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(".inputtext.textInput")))
                    .FirstOrDefault();
            var buttonNativeCityApply =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("__submit__"))).FirstOrDefault();

            if (nativeCity != null)
            {
                nativeCity.Clear();
                nativeCity.SendKeys(model.NativeCity);
            }

            Thread.Sleep(SleepTime);

            var result = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.TagName("li")))
                .FirstOrDefault(m => m.GetAttribute("class") == "page");

            Thread.Sleep(SleepTime);

            HtmlHelper.ClickElement(result);

            Thread.Sleep(SleepTime);

            HtmlHelper.ClickElement(buttonNativeCityApply);
        }

        private void FillFamilyStatus(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var linkFamilyStatus =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("._6a._5u5j._6b")))
                    .FirstOrDefault(m => m.Text == "Укажите информацию о семейном положении");

            if (linkFamilyStatus == null) return;

            HtmlHelper.ClickElement(linkFamilyStatus);

            Thread.Sleep(SleepTime);

            var selectFamilyStatus =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("status"))).FirstOrDefault();

            if (selectFamilyStatus == null) return;

            HtmlHelper.ClickElement(selectFamilyStatus);

            Thread.Sleep(SleepTime);

            var answer =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.TagName("option")))
                    .FirstOrDefault(
                        m =>
                            m.GetAttribute("familystatus") == model.FamilyStatus.ToString("G") ||
                            m.GetAttribute("value") == model.FamilyStatus.ToString("G"));

            var buttonFamilyStatus =
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("__submit__"))).FirstOrDefault();

            HtmlHelper.ClickElement(answer);

            Thread.Sleep(SleepTime);

            HtmlHelper.ClickElement(buttonFamilyStatus);
        }

        private IWebElement ChooseAnswerInComboBox(RemoteWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            
            return wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.TagName("li")))
                       .FirstOrDefault(
                           m => m.GetAttribute("class") == "page" || m.GetAttribute("class") == "page selected") ?? 
                           wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.TagName("li")))
                .FirstOrDefault(m => m.GetAttribute("class") == "addnew calltoaction");
        }

        private void CreateNewUnivercity(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            Thread.Sleep(SleepTime);

            var city =
                wait.Until(
                    ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("._55r1._55r2._58ak._3ct8>._58al")))
                    .FirstOrDefault();
            if (city == null) return;

            city.Clear();
            city.SendKeys(model.UnivercityCity);

            Thread.Sleep(SleepTime);

            SendKeys.SendWait("{DOWN}");

            Thread.Sleep(SleepTime);
 
            SendKeys.SendWait("{ENTER}");

            Thread.Sleep(SleepTime);

            var button =
                wait.Until(
                    ExpectedConditions.PresenceOfAllElementsLocatedBy(
                        By.CssSelector("._42ft._4jy0.layerConfirm.uiOverlayButton._4jy3._4jy1.selected._51sy")))
                    .FirstOrDefault();

            HtmlHelper.ClickElement(button);
        }

        private void CreateNewSchool(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            Thread.Sleep(SleepTime);

            var city =
                wait.Until(
                    ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector("._55r1._55r2._58ak._3ct8>._58al")))
                    .FirstOrDefault();
            if (city == null) return;
            city.Clear();

            Thread.Sleep(SleepTime);

            city.SendKeys(model.SchoolCity);

            Thread.Sleep(SleepTime);

            SendKeys.SendWait("{DOWN}");

            Thread.Sleep(SleepTime);

            SendKeys.SendWait("{ENTER}");

            Thread.Sleep(SleepTime);

            var button =
                wait.Until(
                    ExpectedConditions.PresenceOfAllElementsLocatedBy(
                        By.CssSelector("._42ft._4jy0.layerConfirm.uiOverlayButton._4jy3._4jy1.selected._51sy")))
                    .FirstOrDefault();

            HtmlHelper.ClickElement(button);
        }
    }
}

