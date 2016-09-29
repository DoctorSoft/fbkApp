using System.IO;
using System.Linq;
using System.Threading;
using ChangeExcel.Implementation;
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
            var user = userList.UsersData.FirstOrDefault(); //current user
            
            Log.Information("Registration");

            //service.Authorize(driver, user);

           
            var status = service.Registration(driver, userList.UsersData.FirstOrDefault());
            if (status.StatusRegistration == false)
            {
                new RecordInExcel("usersDB.xlsx").RecordRegistratedData(user, status.Error);
            }
            else
            {
                user.HomepageUrl = FacebookHelper.GetHomepageUrl(driver);

                new RecordInExcel("usersDB.xlsx").RecordRegistratedData(user, null);

                //service.ConfirmRegistration(driver, user);

                //service.InitialProfileSetup(driver);

                service.FillingGeneralInformation(driver, user);
                
                //Log.Information("Loading avatar");

                var folder = Directory.GetCurrentDirectory();
                var imagesDirectory = Path.Combine(folder, "images");
                service.LoadUserAvatar(driver, imagesDirectory);
            }
            Log.Information("End application running");
        
        }

    }
}
