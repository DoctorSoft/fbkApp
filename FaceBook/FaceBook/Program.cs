using System.Linq;
﻿using InputData.Implementation;
﻿using FaceBook.Constants;
﻿using FaceBook.Implementation;
﻿using FaceBook.Interfaces;

namespace FaceBook
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var proxyData = ProxyConstants.GetMockProxyData();
            IWebDriverFactory webDriverFactory = new ChromeWebDriverFactory();
            var driver = webDriverFactory.GetDriver(proxyData);

            var service = new FaceBookService();

            var inpuDataProvider = new InputDataProvider("usersDB.xlsx");

            var userList = service.GetRegistrationUserData(inpuDataProvider);

            service.Registration(driver, userList.UsersData.FirstOrDefault());
            //service.LoadUserAvatar(driver);
        }

    }
}
