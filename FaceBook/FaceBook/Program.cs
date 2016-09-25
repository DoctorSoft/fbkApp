using InputData.Implementation;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace FaceBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var driver =  new ChromeDriver();
            
            var service = new FaceBookService();

           // service.GetIpAddress(driver);

            var inpuDataProvider = new InputDataProvider("usersDB.xlsx");

            var userList = service.GetRegistrationUserData(inpuDataProvider);

            service.Registration(driver, userList.UsersData);
        }

    }
}
