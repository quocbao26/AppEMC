//This class is used for Agent Login. 
//Agent can also change between states.

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
        public void connectAgent()
        {
            //Agent Login
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
                                try
                                {
                                    xmlClient.CSTASetAgentState(new CSTADeviceID(txtbStationID.Text.Trim(), enDeviceIDType.deviceNumber), enReqAgentState.loggedOn, txtbAgentID.Text.Trim(), txtbAgentPassword.Text.Trim(), null, null);
                                    Program.globalCom.agentID = ag_id;
                                    Program.globalCom.agentPassword = ag_pwd;
                                }
                                catch(Exception e)
                                {
                                    MessageBox.Show(e.ToString());
                                }
                               
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

        void xmlClient_CSTAAgentLoggedOff(object sender, CSTAAgentLoggedOffEventArgs arg)
        {
            //rtbStatus.Text = "Agent Logged Off";
            log.Info("Agent " + arg.AgentID + " Logged Off");
            agent_state = AgentState.agentLoggedOff;
            AgentLoggedOff();
        }

        //Agent Logged Off
        private void AgentLoggedOff()
        {
            Image img = Image.FromFile(@"..\..\Resources\images\login.png");
            //btnConnect.Image = img;
            myToolTip.SetToolTip(btnConnect, "Agent Login");
            //btnAvailable.Enabled = false;
            //btnACW.Enabled = false;
            //btnAux.Enabled = false;
        }

        //Agent Login
        private void AgentLoggenOn()
        {
            Image img = Image.FromFile(@"..\..\Resources\images\logout.png");
            //btnAvailable.Enabled = true;
            //btnAux.Enabled = true;
            //btnACW.Enabled = true;
            //btnLogin.Image = img;
            myToolTip.SetToolTip(btnConnect, "Agent Logout");
        }

        void xmlClient_CSTAAgentLoggedOn(object sender, CSTAAgentLoggedOnEventArgs arg)
        {
            //rtbStatus.Text = "Agent Logged On";
            log.Info("Agent  " + arg.AgentID + " Logged On");
            agent_state = AgentState.agentLoggedOn;
            AgentLoggenOn();
            xmlClient.CSTAGetAgentState(new CSTADeviceID(txtbStationID.Text.Trim(), enDeviceIDType.deviceNumber));
        }



        //Agent Not Ready
        private void AgentNotReady()
        {
            Image img = Image.FromFile(@"..\..\Resources\images\logout.png");
            //btnLogin.Image = img;
            myToolTip.SetToolTip(btnConnect, "Agent Logout");
            //btnAux.Enabled = false;
            //btnACW.Enabled = true;
            //btnAvailable.Enabled = true;
        }

        //Agent Ready
        private void AgentReady()
        {
            Image img = Image.FromFile(@"..\..\Resources\images\logout.png");
            //btnLogin.Image = img;
            myToolTip.SetToolTip(btnConnect, "Agent Logout");
            //btnAvailable.Enabled = false;
            //btnAux.Enabled = true;
            //btnACW.Enabled = true;
        }

        //Agent Working After Call
        private void AgentWorkingAfterCall()
        {
            Image img = Image.FromFile(@"..\..\Resources\images\logout.png");
            //btnLogin.Image = img;
            myToolTip.SetToolTip(btnConnect, "Agent Logout");
            //btnACW.Enabled = false;
            //btnAux.Enabled = true;
            //btnAvailable.Enabled = true;
        }

        void xmlClient_CSTAAgentWorkingAfterCall(object sender, CSTAAgentWorkingAfterCallEventArgs arg)
        {
            //rtbStatus.Text = "Agent in ACW state";
            log.Info("Agent State : ACW");
            agent_state = AgentState.agentWorkingAfterCall;
            AgentWorkingAfterCall();
        }

        void xmlClient_CSTAAgentNotReady(object sender, CSTAAgentNotReadyEventArgs arg)
        {
            //rtbStatus.Text = "Agent in Aux Sate";
            log.Info("Agent State : Aux");
            agent_state = AgentState.agentNotReady;
            AgentNotReady();
        }

        void xmlClient_CSTAAgentReady(object sender, CSTAAgentReadyEventArgs arg)
        {
          //KeyDown=  rtbStatus.Text = "Agent is in Available State";
            log.Info("Agent State : Available");
            agent_state = AgentState.agentReady;
            AgentReady();
        }

        //Sets/Channges Agent state to Available.
        private void btnAvailable_Click(object sender, EventArgs e)
        {
            xmlClient.CSTASetAgentState(new CSTADeviceID(txtbStationID.Text.Trim(), enDeviceIDType.deviceNumber), enReqAgentState.ready, txtbAgentID.Text.Trim(), txtbAgentPassword.Text.Trim(), null, null);
        }

        private void btnAux_Click(object sender, EventArgs e)
        {
            xmlClient.CSTASetAgentState(new CSTADeviceID(txtbStationID.Text.Trim(), enDeviceIDType.deviceNumber), enReqAgentState.notReady, txtbAgentID.Text.Trim(), txtbAgentPassword.Text.Trim(), null, null);
        }

        private void btnACW_Click(object sender, EventArgs e)
        {
            xmlClient.CSTASetAgentState(new CSTADeviceID(txtbStationID.Text.Trim(), enDeviceIDType.deviceNumber), enReqAgentState.workingAfterCall, txtbAgentID.Text.Trim(), txtbAgentPassword.Text.Trim(), null, null);
        }
    }
}
