using Engines.Engines.Models;
using Engines.Engines.RegistrationEngine;
using InputData.InputModels;
using InputData.Interfaces;
using OpenQA.Selenium.Remote;

namespace FaceBook.Interfaces
{
    public interface IFaceBookService
    {
        StatusRegistrationModel Registration(RemoteWebDriver driver, RegistrationModel userModel);

        InputDataModel GetRegistrationUserData(IInputDataProvider inputDataProvider);
    }
}
