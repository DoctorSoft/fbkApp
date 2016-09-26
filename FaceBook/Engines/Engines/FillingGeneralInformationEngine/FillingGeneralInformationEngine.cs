using System.Linq;
using System.Threading;
using Constants;
using Constants.EnumExtensions;
using Engines.Engines.Models;
using Helpers.HtmlHelpers;
using OpenQA.Selenium.Remote;

namespace Engines.Engines.FillingGeneralInformationEngine
{
    public class FillingGeneralInformationEngine: AbstractEngine<FillingGeneralInformationModel, VoidResult>
    {
        protected override VoidResult ExecuteEngine(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            AvoidFacebookMessage(driver);

            FillingEducation(driver, model);

            return new VoidResult();
        }

        private void FillingEducation(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            NavigateToUrl(driver, model.UserHomePageUrl + SettingsUrl.OverviewOptionsEducationPostfix.GetDiscription());

            Thread.Sleep(1000);

            var links = HtmlHelper.GetElementsByCssSelector(driver, "_21ok _50f5");

            HtmlHelper.ClickElement(links.FirstOrDefault(m => m.GetAttribute("Text").Contains("Укажите место работы")));
            
            Thread.Sleep(1000);
        }

        private void FillingLiving(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            NavigateToUrl(driver, model.UserHomePageUrl + SettingsUrl.OverviewOptionsLivingnPostfix.GetDiscription());

            Thread.Sleep(1000);
        }

        private void FillingContactInfo(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            NavigateToUrl(driver, model.UserHomePageUrl + SettingsUrl.OverviewOptionsContactInfoPostfix.GetDiscription());

            Thread.Sleep(1000);
        }

        private void FillingsRelationship(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            NavigateToUrl(driver, model.UserHomePageUrl + SettingsUrl.OverviewOptionsRelationshipPostfix.GetDiscription());

            Thread.Sleep(1000);
        }
    }
}
