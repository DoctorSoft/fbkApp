using System.Threading;
using Constants;
using Engines.Engines.Models;
using Helpers.HtmlHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Engines.Engines.GetCurrentStatusEngine
{
    public class GetCurrentStatusEngine: AbstractEngine<GetCurrentStatusModel, ErrorModel>
    {
        private const int SleepingTime = 4000;
        protected override ErrorModel ExecuteEngine(RemoteWebDriver driver, GetCurrentStatusModel model)
        {
            switch (model.Step)
            {
                case RegistrationSteps.SubmitRegistrationData:
                        Thread.Sleep(SleepingTime);
                        var errorElement = HtmlHelper.GetElementById(driver, "reg_error_inner");
                        if (errorElement != null)
                        {
                            return new ErrorModel
                            {
                                Code = ErrorCodes.WrongRegistrationData,
                                ErrorText = "Неверные регистрационные данные"
                            };
                        }
                    break;

                case RegistrationSteps.LoadHomePageBeforeConformation:
                    break;

                case RegistrationSteps.LoadHomePageAfterConformation:
                    break;
            }

            return new ErrorModel
            {
                Code = 0,
                ErrorText = ""
            };
        }
    }
}
