using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YelpProject.StringHasher
{
    class SimpleStringHasher
    {
        private int hashLength = 22;
        private String hashChars = "0123456789qwertyuioplkjhgfdsazxcvbnmMNBVCXZASDFGHJKLPOQIWUEYRT-_!";
        Random rand = new Random();

        public String GetHash()
        {
            String code = "";
            for (int i = 0; i < hashLength; i++)
            {
                code += hashChars[rand.Next(hashChars.Length)];
            }
            return code;
        }
    }
}
