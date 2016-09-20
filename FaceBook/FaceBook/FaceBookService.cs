using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using Engines.Engines.ConformationRegistrationEngine;
using Engines.Engines.RegistrationEngine;
using FaceBook.Interfaces;
using InputData.Implementation;
using InputData.InputModels;
using InputData.Interfaces;
using OpenQA.Selenium.Remote;

namespace FaceBook
{
    public class FaceBookService: IFaceBookService
    {
        public void Registration(RemoteWebDriver driver, List<RegistrationModel> userList)
        {
            foreach (var user in userList)
            {
                var successRegistration = new RegistrationEngine().Execute(driver,
                    new RegistrationModel
                    {
                        LastName = user.LastName,
                        FirstName = user.FirstName,
                        Email = user.Email,
                        FacebookPassword = user.FacebookPassword,
                        EmailPassword = user.EmailPassword,
                        Birthday = user.Birthday,
                        Gender = user.Gender
                    });

                if (successRegistration)
                {
                    new ConformationRegistrationEngine().Execute(driver,
                    new ConformationRegistrationModel
                    {
                        EmailLogin = user.Email,
                        EmailPassword = user.EmailPassword,
                        FacebookPassword = user.FacebookPassword
                    });
                }

                Thread.Sleep(2000);
            }
        }

        public InputDataModel GetRegistrationUserData(IInputDataProvider inputDataProvider)
        {
            var inputData = inputDataProvider.GetInputData();
            return inputData;
        }
    }
}
