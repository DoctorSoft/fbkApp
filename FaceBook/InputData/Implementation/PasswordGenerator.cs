using System;
using InputData.Interfaces;

namespace InputData.Implementation
{
    public class PasswordGenerator: IPasswordGenerator
    {
        private readonly Random rnd;

        public PasswordGenerator()
        {
            rnd = new Random();
        }
        public string Generate(int passwordLength)
        {
            int[] arr = new int[passwordLength];
            string Password = "";

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rnd.Next(33, 125);
                Password += (char)arr[i];
            }
            return Password;
        }
    }
}
