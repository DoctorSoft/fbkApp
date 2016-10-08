using System.IO;
using System.Linq;
using System.Threading;
using ChangeExcel.Implementation;
using Constants;
using Helpers.FacebookHelpers;
using InputData.Implementation;
﻿using FaceBook.Constants;
﻿using FaceBook.Implementation;
﻿using FaceBook.Interfaces;
using Serilog;

namespace FaceBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("facebook-logs.txt")
                .CreateLogger();

            var proxyData = ProxyConstants.GetMockProxyData();

            Log.Information("Start connecting to proxy ({@ProxyData})", proxyData);
            IWebDriverFactory webDriverFactory = new ChromeWebDriverFactory();
            var driver = webDriverFactory.GetDriver(proxyData);

            var service = new FaceBookService();

            Log.Information("Connecting to file usersDB.xlsx");
            var inpuDataProvider = new InputDataProvider("usersDB.xlsx");

            Log.Information("Getting users");
            var userList = service.GetRegistrationUserData(inpuDataProvider);
            var user = userList.UsersData.FirstOrDefault();

            //var status = service.GetCurrentPageStatus(driver, RegistrationSteps.LoadRegistrationPage);

            var status = service.Registration(driver, user);

            //status = service.ProcessingStatus(driver, RegistrationSteps.SubmitRegistrationData);

            if (status.StatusRegistration == false)
            {
                new RecordInExcel("usersDB.xlsx").RecordRegistratedData(user, status.Error);
                return;
            }

//            if (status.Error != null)
//            {
//                if (status.Error.Code == ErrorCodes.VerifyAccount)
//                {
            var conformationError = service.ConfirmRegistration(driver, user);
            if (conformationError == null)
            {
                user.HomepageUrl = FacebookHelper.GetHomepageUrl(driver);

                new RecordInExcel("usersDB.xlsx").RecordRegistratedData(user, null);

                var folder = Directory.GetCurrentDirectory();
                var imagesDirectory = Path.Combine(folder, "images");
                service.LoadUserAvatar(driver, imagesDirectory);

                service.InitialProfileSetup(driver);

                service.FillingGeneralInformation(driver, user);
                return;
            }
            new RecordInExcel("usersDB.xlsx").RecordRegistratedData(user, conformationError);
            return;

            //Log.Information("Loading avatar");


        }

    }
}

//                }
//
//                new RecordInExcel("usersDB.xlsx").RecordRegistratedData(user, status.Error);
//                return;
            
//                    user.HomepageUrl = FacebookHelper.GetHomepageUrl(driver);
//
//                    new RecordInExcel("usersDB.xlsx").RecordRegistratedData(user, null);
//
//                    service.InitialProfileSetup(driver);
//            service.ConfirmRegistration(driver, user);
//            service.FillingGeneralInformation(driver, user);
//            service.InitialProfileSetup(driver);
//            Log.Information("End application running");

