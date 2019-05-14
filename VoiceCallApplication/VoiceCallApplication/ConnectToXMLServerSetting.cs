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
    public partial class SettingConfigForm : Form
    {

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            cmbServerLink.Items.Clear();
            if ((txtbServerIP.Text.Length <= 0) && (txtbServerPort.Text.Length <= 0))
            {
                MessageBox.Show("Server IP and Server Port are not entered", "Server IP and Port", MessageBoxButtons.OK, MessageBoxIcon.Error);
                log.Error("Server IP and Port not entered");
                txtbServerIP.Focus();
            }
            else if (txtbServerIP.Text.Length <= 0)
            {
                MessageBox.Show("Server IP is not entered", "Server IP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                log.Error("Server IP not entered");
                txtbServerIP.Focus();
            }
            else if (txtbServerPort.Text.Length <= 0)
            {
                MessageBox.Show("Port not entered", "Port", MessageBoxButtons.OK, MessageBoxIcon.Error);
                log.Error("Port not entered");
                txtbServerPort.Focus();
            }
            else
            {
                string _ip;
                string _no;
                _ip = txtbServerIP.Text;
                _no = txtbServerPort.Text;
                bool check_ip;
                bool check_no;
                check_ip = checkIP(_ip);
                check_no = checkNum(_no);

                if (!check_ip)
                {
                    MessageBox.Show("Invalid Server IP", "Server IP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Error("Invalid IP entered");
                    txtbServerIP.Focus();
                }
                else if (!check_no)
                {
                    MessageBox.Show("Invalid Port Number", "Port", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Error("Invalid Port entered");
                    txtbServerPort.Focus();
                }
                else
                {
                  //  MessageBox.Show(_ip + "-" + _no, "Port", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    xmlClient.ServerIP = txtbServerIP.Text.Trim();
                    xmlClient.ServerPort = int.Parse(txtbServerPort.Text.Trim());
                    xmlClient.XMLEnumerateServices();
                }
            }
        }

        //Links Browsed
        private void LinksBrowsed()
        {
            cmbServerLink.Text = cmbServerLink.Items[0].ToString();
            btnConnect.Enabled = true;
            btnConnect.Text = "Connect";
        }

        void xmlClient_XMLEnumerateServicesReturned(object sender, XMLEnumerateServicesReturnedEventArgs arg)
        {
            for (int i = 0; i < arg.TLinkConnectionList.Count; i++)
            {
                cmbServerLink.Items.Add(arg.TLinkConnectionList[i].TServerLinkName);
            }
            LinksBrowsed();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            
            //Connect
            if (connectionState == Connection.Disconnect)
            {
                xmlClient.TServerLinkName = cmbServerLink.Text.Trim();
                err = xmlClient.Connect();
                //rtbStatus.Text = err.ToString();
                //MessageBox.Show(err.ToString());
            }

            //Disconnect
            if (connectionState == Connection.Connect)
            {
                //Logs off Agent if Agent is in Logged in state.
                if (agent_state == AgentState.agentLoggedOn || agent_state == AgentState.agentReady || agent_state == AgentState.agentNotReady || agent_state == AgentState.agentWorkingAfterCall)
                {
                    xmlClient.CSTASetAgentState(new CSTADeviceID(txtbStationID.Text.Trim(), enDeviceIDType.deviceNumber), enReqAgentState.loggedOff, txtbAgentID.Text.Trim(), txtbAgentPassword.Text.Trim(), null, null);
                }

                //Stops Monitor initiated before.
                if (monitor_state == MonitorState.MonitorStart)
                {
                    xmlClient.CSTAMonitorStop(xmlClient.CSTAMonitorList[0]);
                }

                err = xmlClient.Disconnect();
                if (err.ToString().Equals("NoError"))
                {
                    //rtbStatus.Text = "Disconnected.";
                    log.Info("Server Disconnected");
                    connectionState = Connection.Disconnect;
                    clearEventHandlers();
                    
                    //this.Close();
                    log.Info("Exited AgentLoginApplication");
                }
                //else
                    //rtbStatus.Text = err.ToString();
            }
            if (err.ToString().Equals("AlreadyConnected"))
            {
                
                btnConnect.Text = "Disconnect";
                this.Hide();
            }
            
        }


        private void loginStation()
        {
            if (monitor_state == MonitorState.MonitorStop)
            {

                if (txtbStationID.Text.Length > 0)
                {
                    string station = txtbStationID.Text.Trim();
                    bool isStation = checkNum(station);
                    if (isStation)
                    {
                        xmlStation.StationDN = txtbStationID.Text.Trim();
                        xmlStation.CSTASource = xmlClient;
                        //ConnectionDisconnected();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Station ID");
                        log.Error("Invalid Station ID entered");
                        txtbStationID.Focus();
                    }
                }
                else
                {
                    //MessageBox.Show("Station ID not entered", "Station ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Error("Station ID not entered");
                    txtbStationID.Focus();
                }
            }

            //Monitor Stop
            if (monitor_state == MonitorState.MonitorStart)
            {
                //If Agent is Logged in first Logs off Agent and then Stops Monitor.
                if (agent_state == AgentState.agentLoggedOn || agent_state == AgentState.agentNotReady || agent_state == AgentState.agentReady || agent_state == AgentState.agentWorkingAfterCall)
                {
                    xmlClient.CSTASetAgentState(new CSTADeviceID(txtbStationID.Text.Trim(), enDeviceIDType.deviceNumber), enReqAgentState.loggedOff, txtbAgentID.Text.Trim(), txtbAgentPassword.Text.Trim(), null, null);
                }
                xmlClient.CSTAMonitorStop(xmlClient.CSTAMonitorList[0]);
            }
        }

        void xmlStation_MonitorStarted(object sender, EventArgs e)
        {
            monitor_state = MonitorState.MonitorStart;
            string myStation = txtbStationID.Text.Trim();
            CSTADeviceID myDevice = new CSTADeviceID(myStation, enDeviceIDType.deviceNumber);
            xmlClient.CSTAGetAgentState(myDevice);
            connectAgent();
        }

    

        void xmlStation_MonitorStopped(object sender, EventArgs e)
        {
            log.Info("Monitor Stopped");
            monitor_state = MonitorState.MonitorStop;           
        }


        void xmlClient_StreamConnected(object sender, EventArgs e)
        {
            //rtbStatus.Text = "Stream Connected. ";
            log.Info("Stream Connected");
            connectionState = Connection.Connect;
            ConnectionEstablished();
            loginStation();
            
        }

        void xmlClient_SocketConnected(object sender, EventArgs e)
        {
            //rtbStatus.Text = "Socket Connected.";
            log.Info("Socket Connected");
        }


        void agentLogin()
        {
            if (agent_state == AgentState.agentLoggedOff)
            {
                if (txtbAgentID.Text.Length > 0)
                {
                    string ag_id = txtbAgentID.Text;
                    bool isagentID = checkNum(ag_id);
                    if (isagentID)
                    {
                        if (txtbAgentPassword.Text.Length > 0)
                        {
                            string ag_pwd = txtbAgentPassword.Text;
                            bool isagentpwd = checkNum(ag_pwd);
                            if (isagentpwd)
                            {
                                xmlClient.CSTASetAgentState(new CSTADeviceID(txtbStationID.Text.Trim(), enDeviceIDType.deviceNumber), enReqAgentState.loggedOn, txtbAgentID.Text.Trim(), txtbAgentPassword.Text.Trim(), null, null);
                            }
                            else
                            {
                                MessageBox.Show("Invalid Agent Password", "Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                log.Error("Invalid agent Password entered");
                                txtbAgentPassword.Focus();
                            }
                        }
                        else
                        {
                            xmlClient.CSTASetAgentState(new CSTADeviceID(txtbStationID.Text.Trim(), enDeviceIDType.deviceNumber), enReqAgentState.loggedOn, txtbAgentID.Text.Trim(), txtbAgentPassword.Text.Trim(), null, null);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Agent ID not entered", "Agent ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Error("Agent ID not entered");
                    txtbAgentID.Focus();
                }
            }

            //Agent Logout
            if (agent_state == AgentState.agentLoggedOn || agent_state == AgentState.agentReady || agent_state == AgentState.agentNotReady || agent_state == AgentState.agentWorkingAfterCall)
            {
                xmlClient.CSTASetAgentState(new CSTADeviceID(txtbStationID.Text.Trim(), enDeviceIDType.deviceNumber), enReqAgentState.loggedOff, txtbAgentID.Text.Trim(), txtbAgentPassword.Text.Trim(), null, null);
            }
        }
       



        //Connection Established
        private void ConnectionEstablished()
        {
            lblAgentID.Enabled = true;
            lblAgentPassword.Enabled = true;
            lblStationID.Enabled = true;
            txtbStationID.Enabled = true;
            txtbAgentID.Enabled = true;
            txtbAgentPassword.Enabled = true;
            //btnMonitorStart.Enabled = true;
            btnConnect.Enabled = false;
            Program.globalCom.Connected = true;
            
            //this.Visible = false;
            //myToolTip.SetToolTip(btnConnect, "Disconnect from XML Server");
            //gbAgentDetails.Enabled = true;

        }

       

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
            //txtbAgentID.Clear();
            //txtbAgentPassword.Clear();
            //txtbStationID.Clear();
            //lblStation.Text = "";
            // btnConnect.Enabled = true;
            //myToolTip.SetToolTip(btnConnect, "Connect to XML Server");
            //gbAgentDetails.Enabled = false;
            //gbTelephony.Enabled = false;
        }

    }
}
