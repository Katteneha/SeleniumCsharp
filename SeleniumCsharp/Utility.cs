using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCsharp
{
    public class Utility
    {
        private static Random random = new Random();
        public static String GenerateRandomEmail(int length) {

            StringBuilder randomName = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                char randomChar = (char)(random.Next(0, 25) + 65);
                randomName.Append(randomChar);
            }
            return randomName + GenerateTime() + "@gmail.com";
        }

  

        public static String GenerateTime() {
            return DateTime.Now.ToString("HHmmssfff");

        }
    }
}
