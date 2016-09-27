using System.Collections.Generic;
using Engines.Engines.RegistrationEngine;
using InputData.InputModels;
using InputData.Interfaces;
using OpenQA.Selenium.Remote;

namespace FaceBook.Interfaces
{
    public interface IFaceBookService
    {
        void Registration(RemoteWebDriver driver, RegistrationModel userModel);

        InputDataModel GetRegistrationUserData(IInputDataProvider inputDataProvider);
    }
}
