using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Dapper;
using MySql.Data.MySqlClient;
namespace DoorApp
{
    
    
    class Connection
    {
        string server;
        string db;
        string user;
        string pass;
        MySqlConnection conn;
        public Connection()
        {

        }
        public string initialize()
        {
            server = "localhost";
            db = "smart_home";
            user = "root";
            pass = "root1234";
            return "DataSource=" + server + ";Database=" + db + ";User Id=" + user + ";Password=" + pass + ";SSL Mode=0";
            //conn = new MySqlConnection(connectionString);
        }

        public bool OpenConnection()
        {
            try
            {
                conn.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.");
                        break;
                    case 1045:
                        MessageBox.Show("Invalid username/passsword");
                        break;
                }
                return false;
            }
        }

        public void close_connection()
        {
            this.conn.Close();
        }

        public MySqlConnection get_connection()
        {
            initialize();
            return this.conn;
        }

        public Account getAccount(int userID)
        {
            using (IDbConnection cnn = new MySqlConnection(initialize()))
            {
                Account account = cnn.Query<Account>($"SELECT * FROM account WHERE id = '{userID}'").FirstOrDefault();
                return account;
            }
        }

        public DoorRequest getRequest(int id)
        {
            using (IDbConnection cnn = new MySqlConnection(initialize()))
            {
                DoorRequest request = cnn.Query<DoorRequest>($"SELECT * FROM door_requests_history WHERE id = '{id}'").FirstOrDefault();
                return request;
            }
        }

        public List<FaceID> getAccountFacesWithIDs()
        {
            using (IDbConnection cnn = new MySqlConnection(initialize()))
            {
                List<FaceID> accountFaces = cnn.Query<FaceID>($"SELECT id, face_id FROM account").ToList();
                return accountFaces;
                
            }
        }

        public void saveDoorRequest(DoorRequest request)
        {
            using (IDbConnection cnn = new MySqlConnection(initialize()))
            {
                cnn.ExecuteScalar($"INSERT INTO door_requests_history (house_id, full_name, face_image, request_time, request_message) VALUES (@house_id, @full_name, @face_image, @request_time, @request_message)", request);
                Console.WriteLine("saved with success");
            }
        }

        public void saveAccount(Account account)
        {
            using (IDbConnection cnn = new MySqlConnection(initialize()))
            {
                cnn.ExecuteScalar($"INSERT INTO account  (first_name, last_name, birthdate, privilege, face_id, password, is_active) VALUES (@first_name, @last_name, @birthdate, @privilege, @face_id, @password, @is_active)", account);
                Console.WriteLine("saved with success");
            }
        }

        public void respondeToDoorRequest(Boolean positiveResponce, String responeMessage)
        {
            using (IDbConnection cnn = new MySqlConnection(initialize()))
            {
                //cnn.ExecuteScalar($"INSERT INTO door_requests_history (full_name, face_image, request_time, request_message) VALUES (@full_name, @face_image, @request_time, @request_message)", request);
                //Console.WriteLine("saved with success");
            }
        }

    }
}
