using Constants;
using Constants.EnumExtensions;
using Engines.Engines.Models;
using OpenQA.Selenium.Remote;

namespace Engines.Engines.FillingGeneralInformationEngine
{
    public class FillingGeneralInformationEngine: AbstractEngine<FillingGeneralInformationModel, VoidResult>
    {
        protected override VoidResult ExecuteEngine(RemoteWebDriver driver, FillingGeneralInformationModel model)
        {
            AvoidFacebookMessage(driver);

            NavigateToUrl(driver, model.UserHomePageUrl + SettingsUrl.OverviewOptionsPostfix.GetDiscription());

            return new VoidResult();
        }
    }
}
