using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YelpProject.SQL
{
    class SQLConnString
    {
        private String database;
        private String password;
        private String port;

        public SQLConnString(String database, String pass) {
            this.database = database;
            this.password = pass;
        }

        public SQLConnString(String database, String pass, String port)
        {
            this.database = database;
            this.password = pass;
            this.port = port;
        }

        public String BuildConnString()
        {
            if(port == null)
                return "Host=localhost; Username=postgres; Password= " + password + " ; Database =" + database + ";";
            else
                return "Host=localhost; Username=postgres; Password= " + password + " ; Database =" + database + " ;Port = " + port +";";
        }
    }
}
