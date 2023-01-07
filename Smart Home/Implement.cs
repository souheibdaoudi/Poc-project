using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iremoting;
using MySql.Data.MySqlClient;
namespace Smart_Home
{
    
      class Implement : MarshalByRefObject, Iremoting.Interface
    {

      
        void Interface.signup(string username,string email, string password, string dob, string gender)
        {
            string cs = @"server=localhost;userid=root;password=daoudi;database=bd";
            var con = new MySqlConnection(cs);
            try
            {
                con.Open();
                string stm = "INSERT INTO `user`(`username`, `email`, `password`, `gender`,`dob`) VALUES(@username, @email, @password, @gender, @dob)";
                var cmd = new MySqlCommand(stm, con);

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@dob", dob);

                cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine("signup failed");
            }
            con.Close();

        }
        void Interface.login(string email , string password)
        {
            string cs = @"server=localhost;userid=root;password=daoudi;database=bd";
            var con = new MySqlConnection(cs);
            
            try
            {
                con.Open();
                string stm = "select email,password from user WHERE email =@email AND password =@Password";
                var cmd = new MySqlCommand(stm, con);

                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    Console.WriteLine("login succed ");

                }
                else
                {
                    dr.Close();
                    Console.WriteLine("No Account avilable with this username and password ");
                }
;            }
            catch (Exception ex)
            {
                Console.WriteLine("login failed");
            }
            con.Close();
        }
    }
}
