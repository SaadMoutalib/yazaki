using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yazaki.Data
{
    static class Options
    {
        public static string PORT = SerialPort.GetPortNames().First();
    }
}
