using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteInerfaces.Entities;
using RemoteInerfaces.Services.AdminServices;
using RemoteInerfaces.Services;

namespace Server.Services.AdminService
{
     class AdminDeviceService : MarshalByRefObject, IAdminDeviceService
    {
        DB_Connection connection;

        public AdminDeviceService()
        {
            connection = new DB_Connection();
        }

        public void addNewDevice(Device device)
        {
            using (IDbConnection cnn = connection.get_connection())
            {

                cnn.ExecuteScalar($"INSERT INTO device (id_room, descriptive_name, type, state, url) VALUES (@id_room, @descriptive_name, @type, @state, @url)", device);
                Console.WriteLine("device added with success");
            }
        }

        public List<Device> getRoomDevices(int id_room)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                List<Device> devices = cnn.Query<Device>($"SELECT * FROM device WHERE id_room = '{id_room}'").ToList();
                return devices;
            }
        }

        public Device getDevice(int id_device)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                Device device = cnn.Query<Device>($"SELECT * FROM device WHERE id_device = '{id_device}'").FirstOrDefault();
                return device;
            }
        }

        public void changeDeviceState(int id_device, bool isActive)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                cnn.ExecuteScalar($"UPDATE device SET state = {isActive} WHERE id_device = '{id_device}'");
                Console.WriteLine("device state changed with success");
            }
            string device_url = getDevice(id_device).url;
            IDeviceService DeviceService = (IDeviceService)Activator.GetObject(typeof(IDeviceService), device_url);
            DeviceService.changeDeviceState(isActive);
        }
    }
}
