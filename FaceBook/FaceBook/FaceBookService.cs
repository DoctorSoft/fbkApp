using System.Collections.Generic;
using System.Threading;
using ChangeExcel.Implementation;
using Engines.Engines.ConformationRegistrationEngine;
using Engines.Engines.InitialProfileSetup;
using Engines.Engines.RegistrationEngine;
using FaceBook.Interfaces;
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
                var statusRegistration = new RegistrationEngine().Execute(driver,
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

                if (statusRegistration == null)
                {
                    /*new RecordInExcel("usersDB.xlsx").RecordRegistratedStatus(user, statusRegistration);

                    Thread.Sleep(1500);
                    new ConformationRegistrationEngine().Execute(driver,
                    new ConformationRegistrationModel
                    {
                        EmailLogin = user.Email,
                        EmailPassword = user.EmailPassword,
                        FacebookPassword = user.FacebookPassword
                    });*/

                    InitialProfileSetup(driver); //start setup service
                }
                else
                {
                    new RecordInExcel("usersDB.xlsx").RecordRegistratedStatus(user, statusRegistration);
                }

                Thread.Sleep(2000);
            }
        }

        public InputDataModel GetRegistrationUserData(IInputDataProvider inputDataProvider)
        {
            var inputData = inputDataProvider.GetInputData();
            return inputData;
        }

        public void InitialProfileSetup(RemoteWebDriver driver)
        {
            new InitialProfileSetupEngine().Execute(driver, new InitialProfileSetupModel());
        }
    }
}
