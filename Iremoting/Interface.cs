using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iremoting
{
    
        public interface Interface

        {
            void signup(string username, string email, string password, string dob, string gender);
            void login(string email, string password);


        }
    

}
