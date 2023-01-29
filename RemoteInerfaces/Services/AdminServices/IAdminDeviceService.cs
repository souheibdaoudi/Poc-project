using RemoteInerfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteInerfaces.Services.AdminServices
{
    public interface IAdminDeviceService
    {
        void addNewDevice(Device device);
        List<Device> getRoomDevices(int id_room);
        void changeDeviceState(int id_device, bool isActive);
    }
}
