using System.Runtime.InteropServices;
using OpenQA.Selenium.Chrome;

namespace FaceBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var driver = new ChromeDriver();
            new FaceBookService().Registration(driver);
        }
    }
}
