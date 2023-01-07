using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iremoting
{
    public interface SurveillanceSysteme
    {
        int TempuratureRecord( DateTime time );
        int HumedityRecord(DateTime time);



    }
}
