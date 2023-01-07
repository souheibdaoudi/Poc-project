using Iremoting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Home
{
    internal class SecuriteImplement : MarshalByRefObject, Iremoting.securtitySysteme
    {
        void securtitySysteme.CameraRecord(string CameraId)
        {
            Console.WriteLine("Camera N°" + CameraId + "record now ");

            throw new NotImplementedException();
        }
    }
}
