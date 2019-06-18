using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YelpProject.SQL
{
    class IllegalInput
    {
        private SQLConstants cc = new SQLConstants();
        private readonly List<String> testList = new List<String>() { "SELECT", "UNION", "FROM", "SET", "INSERT", "DELETE", "UPDATE", "--", "information_schema", "'" };

        public bool SQLInjectionCheck(String input)
        {
            foreach (String item in testList)
            {
                if (input.ToLower().Trim().Contains(item.ToLower().Trim()))
                    return false;
            }
            return true;
        }
    }
}
