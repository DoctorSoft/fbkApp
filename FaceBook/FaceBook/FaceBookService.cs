using System;
using Constants;
using Engines.Engines.RegistrationEngine;
using OpenQA.Selenium.Remote;

namespace FaceBook
{
    public class FaceBookService
    {
        public void Registration(RemoteWebDriver driver)
        {
            var reg = new RegistrationEngine().Execute(driver,
                new RegistrationModel
                {
                    FirstName = "Ivan",
                    LastName = "Ivanov",
                    Email = "mail@mail.ru",
                    Password = "1231241241",
                    Birthday = new DateTime(1998, 10, 15),
                    Gender = Gender.Female
                });
        }
    }
}
