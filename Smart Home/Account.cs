using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Home
{
    internal class Account
    {
            String username;
            string password;
            DateTime dob;
            Boolean gender;


            public Account(string name, string password, DateTime dob, Boolean gender)
            {
                this.username = username;
                this.password = password;
                this.dob = dob;
                this.gender = gender;
            }


        
    }
}

