using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    class DoorRequestService
    {
        DB_Connection connection;

        public DoorRequestService()
        {
            connection = new DB_Connection();
        }

        public void saveDoorRequest(DoorRequest request)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                cnn.ExecuteScalar($"INSERT INTO door_requests_history (id_house, full_name, face_image, request_time, request_message) VALUES (@id_house, @full_name, @face_image, @request_time, @request_message)", request);
                Console.WriteLine("door request saved with success");
            }
        }

        public void respondToDoorRequest(int id, int responce_by, Boolean request_responce, String responce_message)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                cnn.ExecuteScalar($"UPDATE door_requests_history SET request_responce = '{request_responce}', responce_by = '{responce_by}', responce_message = '{responce_message}' WHERE id = '{id}'");
                Console.WriteLine("request responce saved with success");
            }
        }

        public DoorRequest getRequest(int id)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                DoorRequest request = cnn.Query<DoorRequest>($"SELECT * FROM door_requests_history WHERE id = '{id}'").FirstOrDefault();
                return request;
            }
        }

        public List<DoorRequest> getHouseRequest(int house_id)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                List<DoorRequest> requests = cnn.Query<DoorRequest>($"SELECT * FROM door_requests_history WHERE house_id = '{house_id}'").ToList();
                return requests;
            }
        }

    }
}
