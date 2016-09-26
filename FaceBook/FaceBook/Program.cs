using System.Linq;
﻿using InputData.Implementation;
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

            Log.Information("Registration");
            service.Registration(driver, userList.UsersData.FirstOrDefault());

            Log.Information("Loading avatar");
            service.LoadUserAvatar(driver);

            Log.Information("End application running");
        }

    }
}
