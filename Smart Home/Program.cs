using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Runtime;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
namespace Smart_Home
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpChannel ch = new TcpChannel(8085);
            ChannelServices.RegisterChannel(ch);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(Smart_Home.Implement), "obj", WellKnownObjectMode.Singleton);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(Smart_Home.SecuriteImplement), "obj3", WellKnownObjectMode.Singleton);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(Smart_Home.surveillanceImplement), "obj2", WellKnownObjectMode.Singleton);

            Console.WriteLine("sserver is ready.....");
            Console.ReadLine();
        }
    }
}
