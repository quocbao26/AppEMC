using AgileSoftware.Developer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceCallApplication
{
    public class Communication
    {
        private bool connected = false;
        public ASXMLClient xmlClient;
        public ASXMLStation xmlStation;

        public string agentID { get; set; }
        public string agentPassword { get; set; }
        public string stationDN { get; set; }

        public bool Connected
        {
            get
            {
                return connected;
            }

            set
            {
                connected = value;
            }
        } 

    }
}
