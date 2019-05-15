using AgileSoftware.Developer;
using AgileSoftware.Developer.CSTA;
using AgileSoftware.Developer.CSTA.PrivateData;
using AgileSoftware.Developer.Station;
using AgileSoftware.Developer.XML;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoiceCallApplication
{
    public partial class VoiceCallForm : Form
    {

        ASXMLClient xmlClient;
        ASXMLStation xmlStation;
        enASXMLClientError err;
        SettingConfigForm f = null;
        Communication comm;
        
        
        //Call varialbles
        string incoming_call_id = null, make_call_id = null;
        bool callTransfered = false, callConferenced = false, callAnswered = false;

        //Timer variables
        int hold_time = 0, time = 0, sec = 0, min = 0, hr = 0;


        //Enumeration for Call type
        enum CallType
        {
            MakeCall,
            AnswerCall
        };
        CallType call_type = CallType.MakeCall;

        //Enumeration to maintain Hold/Unhold Call State
        enum Call_State
        {
            Hold,
            UnHold
        };
        Call_State hold_state = Call_State.UnHold;
        
        //Enumeration to maintain Connection State
        enum Connection
        {
            Connect,
            Disconnect
        };

        Connection connectionState = Connection.Disconnect;

        //Enumeration to maintain Agent State
        enum AgentState
        {
            agentReady,
            agentNotReady,
            agentLoggedOn,
            agentLoggedOff,
            agentWorkingAfterCall
        };

        AgentState agent_state = AgentState.agentLoggedOff;

        //Enumeration to maintain Monitor State
        enum MonitorState
        {
            MonitorStart,
            MonitorStop
        };
        MonitorState monitor_state = MonitorState.MonitorStop;

        //Enumeration to maintain Consult type during Call Transfer or Call Conference
        enum Consult_Type
        {
            Blind,
            StartConsult,
            CompleteConsult
        };
        Consult_Type _type;

        enum enCallState
        {
            Idle = 0,
            Connecting = 1,
            Alerting = 2,
            Connected = 3,
            Hold = 4,
            Queued = 5,
            Failed = 6
        }


        private void btnAux_Click(object sender, EventArgs e)
        {

        }

        //Log File is generated that stores log entries of the application. It is stored in the same directory with the name: AgentLoginForm.log
        public static readonly ILog log = LogManager.GetLogger(typeof(SettingConfigForm));


        public VoiceCallForm()
        {
            // prevent user from changing form size
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            xmlClient = new ASXMLClient();
            
            //Initializes a new instance of AgileSoftware.Developer.ASXMLClient class. 
            xmlStation = new ASXMLStation();
           
            //Initializes a new instance of AgileSoftware.Developer.ASXMLStation class. 
            InitializeComponent();
            if (Program.globalCom.Connected)
            {
                btnLogout.Enabled = true;
                //Configures Log File
                XmlConfigurator.Configure();
                xmlClient.LogTraceToFile = true;
                xmlClient.TraceFilePathName = "XmlLogEntries.log";
                log.Info("Initialized Voice Handling Application");
            }
               
            buildEventHandlers();

           
        }

        private void btnAvailable_Click(object sender, EventArgs e)
        {
            xmlStation.AgentSetState(enReqAgentState.ready, comm.agentID, comm.agentPassword, enWorkMode.WM_AUTO_IN,0);

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            disconnect();
            txtStatus.Text = "Logged Off";
            btnAvailable.Enabled = false;
            btnHold.Enabled = false;
            lblStation.Text = "";
            btnDrop.Enabled = false;
            btnPlaceCall.Enabled = false;
            btnAux.Enabled = false;
            txtbDialNumber.ReadOnly = true;
            callTimer.Stop();
            holdTimer.Stop();
            lblCallTimer.Text = "";
            lblHoldTimer.Text = "";
            txtIncoming.Clear();
            
        }

        private Form CheckExists(Type ftype)
        {
            FormCollection fc = Application.OpenForms;
            foreach (Form f in fc)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }

        public static bool checkNum(string _no)
        {
            int no;
            bool isNum = int.TryParse(_no, out no);
            if (isNum)
                return true;
            else
                return false;
        }

        private void btnPlaceCall_Click(object sender, EventArgs e)
        {
            //Make Call
            if (call_type == CallType.MakeCall)
            {
                if (txtbDialNumber.Text.Length <= 0)
                {
                    MessageBox.Show("Enter valid Station ID", "Station ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    bool is_Station = checkNum(txtbDialNumber.Text.Trim());
                    if (is_Station)
                    {
                        string CallToNumber = txtbDialNumber.Text.Trim();
                        xmlStation.CallDial(CallToNumber, "");
                    }
                    else
                    {
                        MessageBox.Show("Invalid Station ID.", "Station ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            //Answer Call
            if (call_type == CallType.AnswerCall)
            {
                xmlStation.CallAnswer();
            }
        }

        

        private void btnDrop_Click(object sender, EventArgs e)
        {
            xmlStation.CallRelease();
        }

        private void btnHold_Click(object sender, EventArgs e)
        {
            StationCall objStationCall = xmlStation.GetCallByCallID(incoming_call_id);
            if (objStationCall != null)
            {
                if ((int)objStationCall.CallState == (int)enCallState.Connecting || (int)objStationCall.CallState == (int)enCallState.Alerting || (int)objStationCall.CallState == (int)enCallState.Connected)
                {
                    callAnswered = false;
                    xmlStation.CallHold(objStationCall);
                }
                if ((int)objStationCall.CallState == (int)enCallState.Hold)
                {
                    xmlStation.CallUnhold();
                    holdTimer.Stop();
                    log.Info("--End hold between: " + objStationCall.CallerDN + " and " + objStationCall.CalledDN + " Duration: " + lblHoldTimer.Text);

                }
            }
        }

        private void holdTimer_Tick(object sender, EventArgs e)
        {
            hold_time = hold_time + 1;
            sec = hold_time % 60;
            min = ((hold_time - sec) / 60) % 60;
            hr = ((hold_time - (sec + (min * 60))) / 3600) % 60;
            lblHoldTimer.Text = hr.ToString() + ":" + min.ToString() + ":" + sec.ToString();
        }

        private void callTimer_Tick(object sender, EventArgs e)
        {
            time = time + 1;
            sec = time % 60;
            min = ((time - sec) / 60) % 60;
            hr = ((time - (sec + (min * 60))) / 3600) % 60;
            lblCallTimer.Text = hr.ToString() + ":" + min.ToString() + ":" + sec.ToString();
        }

       

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Program.globalCom.xmlClient = xmlClient;
            Program.globalCom.xmlStation = xmlStation;
            Form frm = this.CheckExists(typeof(SettingConfigForm));

            if (frm != null)
            {
                frm.Visible = true;
            }
            else
            {
                f = new SettingConfigForm();
                f.VisibleChanged += Frm_VisibleChanged; 
                //f.MdiParent = this;
                //buildEventHandlers();
                f.ShowDialog();

            }
            //MessageBox.Show(xmlClient.ServerIP + "-" + xmlClient.ServerPort);
            //MessageBox.Show(xmlStation.StationDN);
        }





        private void Frm_VisibleChanged(object sender, EventArgs e)
        {
            if (!f.Visible && Program.globalCom.Connected == true) {
                btnLogout.Enabled = true;
                xmlStation.AgentGetState();
                comm = Program.globalCom;
                //xmlClient.CSTAGetAgentState(new CSTADeviceID(xmlStation.StationDN, enDeviceIDType.deviceNumber));
            }
        }

        private void buildEventHandlers()
        {
            //xmlClient = Program.globalCom.xmlClient;
            
            //xmlStation = Program.globalCom.xmlStation;

            //IEnumerator<StationAgentList> agentList = xmlStation.StationAgents.Cast<StationAgentList>().GetEnumerator();
            //while (agentList.MoveNext())
            //{
            //    StationAgent agent = agentList.Current.Cast<StationAgent>().First();
            //    string st = agent.AgentName;
            //}

            //if(f!=null) f.connectSuccessfully += new EventHandler(activeLogout);

            //xmlClient.XMLEnumerateServicesReturned += new XMLEnumerateServicesReturnedEventHandler(Program.globalCom.xmlClient.XMLEnumerateServicesReturned);
            //xmlClient.SocketConnected += new EventHandler(xmlClient_SocketConnected);
            //xmlClient.StreamConnected += new EventHandler(xmlClient_StreamConnected);

            //StationAgentList
            //IEnumerator<StationAgentList> agentList = xmlStation.StationAgents.Cast<StationAgentList>().GetEnumerator();

            xmlClient.CSTAGetAgentStateResponse += new CSTAGetAgentStateResponseEventHandler(xmlClient_CSTAGetAgentStateResponse);
            xmlStation.AgentGetStateReturn += XmlStation_AgentGetStateReturn;
            //xmlClient.CSTAAgentLoggedOn += new CSTAAgentLoggedOnEventHandler(xmlClient_CSTAAgentLoggedOn);


            xmlStation.StationCallChanged += XmlStation_StationCallChanged;
            
            

            //xmlClient.CSTAAgentLoggedOff += new CSTAAgentLoggedOffEventHandler(xmlClient_CSTAAgentLoggedOff);
            //xmlClient.CSTAAgentReady += new CSTAAgentReadyEventHandler(xmlClient_CSTAAgentReady);
            //xmlClient.CSTAAgentNotReady += new CSTAAgentNotReadyEventHandler(xmlClient_CSTAAgentNotReady);
            //xmlClient.CSTAAgentWorkingAfterCall += new CSTAAgentWorkingAfterCallEventHandler(xmlClient_CSTAAgentWorkingAfterCall);

            //xmlClient.CSTADelivered += new CSTADeliveredEventHandler(xmlClient_CSTADelivered);
            //xmlClient.CSTAEstablished += new CSTAEstablishedEventHandler(xmlClient_CSTAEstablished);
            //xmlClient.CSTAAnswerCallResponse += new CSTABasicResponseEventHandler(xmlClient_CSTAAnswerCallResponse);
            //xmlClient.CSTAMakeCallResponse += new CSTAMakeCallResponseEventHandler(xmlClient_CSTAMakeCallResponse);
            //xmlClient.CSTAConnectionCleared += new CSTAConnectionClearedEventHandler(xmlClient_CSTAConnectionCleared);
            //xmlClient.CSTAHeld += new CSTAHeldEventHandler(xmlClient_CSTAHeld);
            //xmlClient.CSTAHoldCallResponse += new CSTABasicResponseEventHandler(xmlClient_CSTAHoldCallResponse);
            //xmlClient.CSTARetrieveCallResponse += new CSTABasicResponseEventHandler(xmlClient_CSTARetrieveCallResponse);
            //xmlClient.CSTARetrieved += new CSTARetrievedEventHandler(xmlClient_CSTARetrieved);
            //xmlClient.CSTATransferCallResponse += new CSTATransferCallResponseEventHandler(xmlClient_CSTATransferCallResponse);
            //xmlClient.CSTATransfered += new CSTATransferedEventHandler(xmlClient_CSTATransfered);
            //xmlClient.CSTAConferenceCallResponse += new CSTAConferenceCallResponseEventHandler(xmlClient_CSTAConferenceCallResponse);
            //xmlClient.CSTAConferenced += new CSTAConferencedEventHandler(xmlClient_CSTAConferenced);

            //xmlStation.MonitorStarted += new EventHandler(xmlStation_MonitorStarted);
            //xmlStation.MonitorStopped += new EventHandler(xmlStation_MonitorStopped);

            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
          //  AppDomain.CurrentDomain.ProcessExit += new EventHandler(Application_ApplicationExit);

        }

        private void XmlStation_StationCallChanged(object sender, StationCallChangedEventArgs arg)
        {
            /* string station = arg.StationCall.CallConnection.DeviceID.ID;*/ // may hien tai
            //string a = arg.StationCall.CallAppearance.ToString();
            string b = arg.StationCall.CallerDN; // nguoi goi
            string c = arg.StationCall.CalledDN; // nguoi nghe
            string d = arg.StationCall.CallState.ToString();
            //string w = arg.StationCall.CallEvents[0].ToString();
            string e = arg.StationCall.CallDirection.ToString(); // huong goi vao
            //string r = arg.StationCall.CallMembers[0].ToString();

            //co cuoc goi toi
            switch ((int)arg.StationCall.CallState)
            {
                // thong bao
                case (int)enCallState.Alerting:
                    incoming_call_id = arg.StationCall.CallConnection.CallID.ToString();
                    txtIncoming.Text = "Incoming: " + arg.StationCall.CallerDN;
                    txtStatus.Text = "Ringing";
                    log.Info("--Call Recieved from: " + arg.StationCall.CallerDN + " to " + arg.StationCall.CalledDN);
                    call_type = CallType.AnswerCall;


                    btnPlaceCall.Enabled = true;
                    btnDrop.Enabled = true;
                    btnHold.Enabled = false;
                    btnTransfer.Enabled = false;
                    btnConference.Enabled = false;
                    break;

                // bat may
                case (int)enCallState.Connected:
                    txtStatus.Text = "Connected";
                    log.Info("--Having conversation between: " + arg.StationCall.CallerDN + " and " + arg.StationCall.CalledDN);

                    callTimer.Start();
                    btnPlaceCall.Enabled = false;
                    btnDrop.Enabled = true;
                    btnHold.Enabled = true;
                    btnTransfer.Enabled = true;
                    btnConference.Enabled = true;
                    break;

                // cup may
                case (int)enCallState.Idle:
                    log.Info("--End conversation between: " + arg.StationCall.CallerDN + " and " + arg.StationCall.CalledDN + "\n Duration: " + lblCallTimer.Text);
                    xmlStation.AgentSetState(enReqAgentState.ready, comm.agentID, comm.agentPassword, enWorkMode.WM_AUTO_IN, 0);
                    break;

                // goi di
                case (int)enCallState.Connecting:
                    incoming_call_id = arg.StationCall.CallConnection.CallID.ToString();
                    txtStatus.Text = "Connecting";
                    log.Info("--Is connecting with: " + arg.StationCall.CalledDN);


                    btnPlaceCall.Enabled = false;
                    btnDrop.Enabled = true;
                    btnHold.Enabled = false;
                    btnTransfer.Enabled = false;
                    btnConference.Enabled = false;
                    txtIncoming.Text = "Out-going: " + arg.StationCall.CalledDN;
                    txtbDialNumber.Clear();
                    break;
                // hold
                case (int)enCallState.Hold:
                    holdTimer.Start();
                    callTimer.Stop();
                    txtStatus.Text = "Holding";
                    log.Info("--Hold conversation between: " + arg.StationCall.CallerDN + " and " + arg.StationCall.CalledDN);
                    break;
            }
            
            


        }

        void clearEventHandlers()
        {
            xmlStation.AgentGetStateReturn -= XmlStation_AgentGetStateReturn;
            Application.ApplicationExit -= new EventHandler(Application_ApplicationExit);
           // AppDomain.CurrentDomain.ProcessExit -= new EventHandler(Application_ApplicationExit);
        }
        

        void Application_ApplicationExit(object sender, EventArgs e)
        {
            log.Info("Exited Voice Handling Application.");
            disconnect();
            clearEventHandlers();
            Environment.Exit(0);
        }

        void disconnect()
        {
            //Logs off Agent if Agent is in Logged in state.
            if (agent_state == AgentState.agentLoggedOn || agent_state == AgentState.agentReady || agent_state == AgentState.agentNotReady || agent_state == AgentState.agentWorkingAfterCall)
            {
                xmlClient.CSTASetAgentState(new CSTADeviceID(Program.globalCom.stationDN.Trim(), enDeviceIDType.deviceNumber), enReqAgentState.loggedOff, Program.globalCom.agentID.Trim(), Program.globalCom.agentPassword.Trim(), null, null);
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
                
                ConnectionDisconnected();
                log.Info("Exited AgentLoginApplication");
            }
            //else
            //    rtbStatus.Text = err.ToString();
        }




    }
}
