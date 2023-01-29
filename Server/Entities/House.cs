using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [Serializable]
    class House
    {
        public int id { get; set; }
        public string owner_first_name { get; set; }
        public string owner_last_name { get; set; }
        public string type { get; set; }
        public int id_Admin { get; set; }
        public string descriptive_name { get; set; }
        public string address { get; set; }

    }
}
