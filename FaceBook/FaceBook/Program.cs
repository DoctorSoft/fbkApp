using System.Runtime.InteropServices;
using InputData.Implementation;
using OpenQA.Selenium.Chrome;

namespace FaceBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var driver = new ChromeDriver();
            var service = new FaceBookService();

            var inpuDataProvider = new InputDataProvider("usersDB.xlsx");

            var userList = service.GetRegistrationUserData(inpuDataProvider);

            service.Registration(driver, userList.usersData);
        }
    }
}
