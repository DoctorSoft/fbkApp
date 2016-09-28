using System;
using System.IO;
using System.Threading;
using ChangeExcel.Implementation;
using Engines.Engines.AuthorizationEngine;
using Engines.Engines.ConformationRegistrationEngine;
using Engines.Engines.FillingGeneralInformationEngine;
using Engines.Engines.GetIpEngine;
using Engines.Engines.InitialProfileSetupEngine;
using Engines.Engines.LoadUserAvatar;
using Engines.Engines.LoadUserAvatarEngine;
using Engines.Engines.ProfileConfirmationEngine;
using Engines.Engines.RegistrationEngine;
using FaceBook.Interfaces;
using Helpers.FacebookHelpers;
using InputData.Implementation;
using InputData.InputModels;
using InputData.Interfaces;
using OpenQA.Selenium.Remote;

namespace FaceBook
{
    public class FaceBookService : IFaceBookService
    {
        private readonly Random random;

        public FaceBookService()
        {
            this.random = new Random();
        }

        public void Registration(RemoteWebDriver driver, RegistrationModel user)
        {
            /*var statusRegistration = new RegistrationEngine().Execute(driver,
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

            user.HomepageUrl = FacebookHelper.GetHomepageUrl(driver);
            user.UserInfo.UserHomePageUrl = user.HomepageUrl; //Заменить

            if (statusRegistration != null)
            {
                new RecordInExcel("usersDB.xlsx").RecordRegistratedData(user, statusRegistration);
            }
            else
            {
                user.HomepageUrl = FacebookHelper.GetHomepageUrl(driver);

                new RecordInExcel("usersDB.xlsx").RecordRegistratedData(user, null);

                Thread.Sleep(1500);

                ConfirmRegistration(driver, user); 

                InitialProfileSetup(driver);

                FillingGeneralInformation(driver, user);

                LoadUserAvatar(driver);
            }

            Thread.Sleep(2000);
             */

            Authorize(driver, user);
            FillingGeneralInformation(driver, user);
        }


        public InputDataModel GetRegistrationUserData(IInputDataProvider inputDataProvider)
        {
            var inputData = inputDataProvider.GetInputData();
            return inputData;
        }

        public void GetIpAddress(RemoteWebDriver driver)
        {
            new GetIpEngine().Execute(driver, new GetIpModel());
        }

        public void InitialProfileSetup(RemoteWebDriver driver)
        {
            var settingsList = new GeneralProfileSettings().GetGeneralProfileSettings();

            new InitialProfileSetupEngine().Execute(driver, new InitialProfileSetupModel
            {
                ProfileSettings = settingsList
            });
        }

        public void LoadUserAvatar(RemoteWebDriver driver, string folder)
        {
            var files = Directory.GetFiles(folder);
            var randomFileName = files[random.Next(files.Length)];

            new LoadUserAvatarEngine().Execute(driver, new LoadUserAvatarModel
            {
                AvatarName = randomFileName
            });
        }

        public void Authorize(RemoteWebDriver driver, RegistrationModel model)
        {
            new AuthorizationEngine().Execute(driver, new AuthorizationModel
            {
                Login = model.Email,
                Password = model.FacebookPassword
            });
        }

        public void FillingGeneralInformation(RemoteWebDriver driver, RegistrationModel model)
        {
            new FillingGeneralInformationEngine().Execute(driver, model.UserInfo);
        }

        public void ConfirmRegistration(RemoteWebDriver driver, RegistrationModel model)
        {
            new ConfirmationRegistrationEngine().Execute(driver, new ConfirmationRegistrationModel
            {
                EmailLogin = model.Email,
                EmailPassword = model.EmailPassword,
                FacebookPassword = model.FacebookPassword
            });
        }

        public void ProfileConfirm(RemoteWebDriver driver, RegistrationModel model)
        {
            new ProfileConfirmationEngine().Execute(driver, new ProfileConfirmationModel
            {
                Login = model.Email,
                Password = model.FacebookPassword
            });
        }
    }
}
