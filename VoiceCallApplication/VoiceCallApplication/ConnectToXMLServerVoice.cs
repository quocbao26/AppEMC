//This class is used to make a connection with XML Server.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AgileSoftware.Developer;
using AgileSoftware.Developer.CSTA;
using AgileSoftware.Developer.XML;
using AgileSoftware.Developer.Station;
using AgileSoftware.Developer.Device;
using System.Threading;
using System.Text.RegularExpressions;
using log4net;
using log4net.Config;
using System.Drawing.Imaging;

namespace VoiceCallApplication
{
    public partial class VoiceCallForm : Form
    {



        void xmlClient_CSTAAgentLoggedOff(object sender, CSTAAgentLoggedOffEventArgs arg)
        {
            log.Info("Agent " + arg.AgentID + " Logged Off");
            agent_state = AgentState.agentLoggedOff;
            
        }

        private void AgentLoggenOn()
        {
            //Image img = Image.FromFile(@"..\..\Resources\images\logout.png");
            btnAvailable.Enabled = true;
            btnLogout.Enabled = true;
            //btnACW.Enabled = true;
            //btnLogin.Image = img;

        }

        void xmlClient_CSTAAgentLoggedOn(object sender, CSTAAgentLoggedOnEventArgs arg)
        {
            //rtbStatus.Text = "Agent Logged On";
            log.Info("Agent  " + arg.AgentID + " Logged On");
            agent_state = AgentState.agentLoggedOn;
            AgentLoggenOn();
            xmlClient.CSTAGetAgentState(new CSTADeviceID(lblStation.Text.Trim(), enDeviceIDType.deviceNumber));
        }

        void xmlClient_CSTAGetAgentStateResponse(object sender, CSTAGetAgentStateResponseEventArgs arg)
        {
            //logged_state - Stores Agent State
            bool logged_state = arg.AgentStateList[0].LoggedOnState;
            if (logged_state)
            {
                //state - gets and stores Agent State
                string state = arg.AgentStateList[0].AgentInfo[0].AgentState.ToString();
                string myAgentID = arg.AgentStateList[0].AgentID;
                if (state.Equals("agentNotReady"))
                {
                   
                    log.Info("Agent State : Aux");
                    lblStation.Text = myAgentID;
                    agent_state = AgentState.agentNotReady;
                    //AgentNotReady();
                }
                else if (state.Equals("agentReady"))
                {
                   
                    log.Info("Agent State : Available");
                    lblStation.Text = myAgentID;
                    agent_state = AgentState.agentReady;
                    //AgentReady();
                }
                else if (state.Equals("agentWorkingAfterCall"))
                {
                    log.Info("Agent State : ACW");
                    lblStation.Text = myAgentID;
                    agent_state = AgentState.agentWorkingAfterCall;
                    //AgentWorkingAfterCall();
                }
                else
                {
                    MessageBox.Show(arg.AgentStateList[0].AgentInfo[0].AgentState.ToString());
                }
            }
        }



        void xmlClient_StreamConnected(object sender, EventArgs e)
        {
            //rtbStatus.Text = "Stream Connected. ";
            log.Info("Stream Connected");
            connectionState = Connection.Connect;
            ConnectionEstablished();
            //eventConnectSuccessfully(sender, e);
        }

        void xmlClient_SocketConnected(object sender, EventArgs e)
        {
            //rtbStatus.Text = "Socket Connected.";
            log.Info("Socket Connected");
        }

        //Connection Established
        private void ConnectionEstablished()
        {
            btnLogin.Enabled = false;
            btnLogout.Enabled = true;
            btnPlaceCall.Enabled = true;
            //txtbStationID.Enabled = true;
            //txtbAgentID.Enabled = true;
            //txtbAgentPassword.Enabled = true;
            ////btnMonitorStart.Enabled = true;
            //btnConnect.Enabled = false;

            //myToolTip.SetToolTip(btnConnect, "Disconnect from XML Server");
            //gbAgentDetails.Enabled = true;

        }

        //private void eventConnectSuccessfully(object sender, EventArgs e)
        //{
        //    if(connectionState == Connection.Connect)
        //    {
        //        connectSuccessfully(sender, e);
        //    }
        //}

        //Connection Disconnected
        private void ConnectionDisconnected()
        {
           
            
            //Image img = Image.FromFile(@"..\..\Resources\images\login.png");
            //lblAgentID.Enabled = false;
            //lblAgentPassword.Enabled = false;
            //lblStationID.Enabled = false;
            //txtbStationID.Enabled = false;
            //txtbAgentID.Enabled = false;
            //txtbAgentPassword.Enabled = false;
            //btnMonitorStart.Enabled = false;
            //btnLogin.Enabled = false;
            //btnMonitorStart.Text = "Monitor Start";
            //btnLogin.Image = img;
            //myToolTip.SetToolTip(btnLogin, "Agent Login");
            //btnAvailable.Enabled = false;
            //btnAux.Enabled = false;
            //btnACW.Enabled = false;
            btnLogin.Enabled = true;
            btnLogout.Enabled = false;
            //txtbAgentPassword.Clear();
            //txtbStationID.Clear();
            ////lblStation.Text = "";
            //btnConnect.Enabled = true;
            //myToolTip.SetToolTip(btnConnect, "Connect to XML Server");
            //gbAgentDetails.Enabled = false;
            //gbTelephony.Enabled = false;
        }

    }
}
