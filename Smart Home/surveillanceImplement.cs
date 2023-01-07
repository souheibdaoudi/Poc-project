using Iremoting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Home
{
    internal class surveillanceImplement : MarshalByRefObject, Iremoting.SurveillanceSysteme
    {
        int SurveillanceSysteme.HumedityRecord(DateTime time)
        {
            Random rnd = new Random();
            int Humedity = rnd.Next(1, 100);
            throw new NotImplementedException();
        }

        int SurveillanceSysteme.TempuratureRecord(DateTime time)
        {
            Random rnd = new Random();
            int Tempurature = rnd.Next(-10,50);
            throw new NotImplementedException();
        }
    }
}
