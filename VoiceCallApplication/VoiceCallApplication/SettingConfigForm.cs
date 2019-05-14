using AgileSoftware.Developer;
using AgileSoftware.Developer.XML;
using AgileSoftware.Developer.CSTA;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using AgileSoftware.Developer.Station;

namespace VoiceCallApplication
{
    
 

    public partial class SettingConfigForm : Form
    {
        ASXMLClient xmlClient;
        ASXMLStation xmlStation;
        
        enASXMLClientError err;
        public event EventHandler connectSuccessfully;

        

        ////Call varialbles
        //string incoming_call_id = null, make_call_id = null;
        //bool callTransfered = false, callConferenced = false, callAnswered = false;

        ////Timer variables
        //int hold_time = 0, time = 0, sec = 0, min = 0, hr = 0;


        ////Enumeration for Call type
        //enum CallType
        //{
        //    MakeCall,
        //    AnswerCall
        //};
        //CallType call_type = CallType.MakeCall;

        ////Enumeration to maintain Hold/Unhold Call State
        //enum Call_State
        //{
        //    Hold,
        //    UnHold
        //};
        //Call_State hold_state = Call_State.UnHold;

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

        ////Enumeration to maintain Consult type during Call Transfer or Call Conference
        //enum Consult_Type
        //{
        //    Blind,
        //    StartConsult,
        //    CompleteConsult
        //};
        //Consult_Type _type;

        ////Log File is generated that stores log entries of the application. It is stored in the same directory with the name: AgentLoginForm.log
        public static readonly ILog log = LogManager.GetLogger(typeof(SettingConfigForm));



        public SettingConfigForm()
        {
            

            xmlClient = Program.globalCom.xmlClient;
            //Initializes a new instance of AgileSoftware.Developer.ASXMLClient class. 
            xmlStation = Program.globalCom.xmlStation;
            //Initializes a new instance of AgileSoftware.Developer.ASXMLStation class. 
            InitializeComponent();
            
            buildEventHandlers();

            //Configures Log File
            XmlConfigurator.Configure();
            xmlClient.LogTraceToFile = true;
            xmlClient.TraceFilePathName = "XmlLogEntries.log";
            log.Info("Initialized Voice Handling Application");
            
            //Sets default values to ComboBox cmbType
            //cmbType.Items.Add("Blind");
            //cmbType.Items.Add("Consult");
            //cmbType.Text = cmbType.Items[0].ToString();
        }

        

        private void buildEventHandlers()
        {
            xmlClient.XMLEnumerateServicesReturned += new XMLEnumerateServicesReturnedEventHandler(xmlClient_XMLEnumerateServicesReturned);
            xmlClient.SocketConnected += new EventHandler(xmlClient_SocketConnected);
            xmlClient.StreamConnected += new EventHandler(xmlClient_StreamConnected);

            //xmlClient.CSTAGetAgentStateResponse += new CSTAGetAgentStateResponseEventHandler(xmlClient_CSTAGetAgentStateResponse);
            //xmlClient.CSTAAgentLoggedOn += new CSTAAgentLoggedOnEventHandler(xmlClient_CSTAAgentLoggedOn);

            
            


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

            xmlStation.MonitorStarted += new EventHandler(xmlStation_MonitorStarted);
            xmlStation.MonitorStopped += new EventHandler(xmlStation_MonitorStopped);

            //Program.globalCom.xmlClient = xmlClient;
            //Program.globalCom.xmlStation = xmlStation;

            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

        }


        void Application_ApplicationExit(object sender, EventArgs e)
        {
            log.Info("Exited Voice Handling Application.");
            Environment.Exit(0);
        }

        private void clearEventHandlers()
        {
            //xmlClient.XMLEnumerateServicesReturned -= new XMLEnumerateServicesReturnedEventHandler(xmlClient_XMLEnumerateServicesReturned);
            //xmlClient.SocketConnected -= new EventHandler(xmlClient_SocketConnected);
            //xmlClient.StreamConnected -= new EventHandler(xmlClient_StreamConnected);

            //xmlClient.CSTAGetAgentStateResponse -= new CSTAGetAgentStateResponseEventHandler(xmlClient_CSTAGetAgentStateResponse);
            //xmlClient.CSTAAgentLoggedOn -= new CSTAAgentLoggedOnEventHandler(xmlClient_CSTAAgentLoggedOn);
            //xmlClient.CSTAAgentLoggedOff -= new CSTAAgentLoggedOffEventHandler(xmlClient_CSTAAgentLoggedOff);
            //xmlClient.CSTAAgentReady -= new CSTAAgentReadyEventHandler(xmlClient_CSTAAgentReady);
            //xmlClient.CSTAAgentNotReady -= new CSTAAgentNotReadyEventHandler(xmlClient_CSTAAgentNotReady);
            //xmlClient.CSTAAgentWorkingAfterCall -= new CSTAAgentWorkingAfterCallEventHandler(xmlClient_CSTAAgentWorkingAfterCall);

            //xmlClient.CSTADelivered -= new CSTADeliveredEventHandler(xmlClient_CSTADelivered);
            //xmlClient.CSTAEstablished -= new CSTAEstablishedEventHandler(xmlClient_CSTAEstablished);
            //xmlClient.CSTAAnswerCallResponse -= new CSTABasicResponseEventHandler(xmlClient_CSTAAnswerCallResponse);
            //xmlClient.CSTAMakeCallResponse -= new CSTAMakeCallResponseEventHandler(xmlClient_CSTAMakeCallResponse);
            //xmlClient.CSTAConnectionCleared -= new CSTAConnectionClearedEventHandler(xmlClient_CSTAConnectionCleared);
            //xmlClient.CSTAHeld -= new CSTAHeldEventHandler(xmlClient_CSTAHeld);
            //xmlClient.CSTAHoldCallResponse -= new CSTABasicResponseEventHandler(xmlClient_CSTAHoldCallResponse);
            //xmlClient.CSTARetrieveCallResponse -= new CSTABasicResponseEventHandler(xmlClient_CSTARetrieveCallResponse);
            //xmlClient.CSTARetrieved -= new CSTARetrievedEventHandler(xmlClient_CSTARetrieved);
            //xmlClient.CSTATransferCallResponse -= new CSTATransferCallResponseEventHandler(xmlClient_CSTATransferCallResponse);
            //xmlClient.CSTATransfered -= new CSTATransferedEventHandler(xmlClient_CSTATransfered);
            //xmlClient.CSTAConferenceCallResponse -= new CSTAConferenceCallResponseEventHandler(xmlClient_CSTAConferenceCallResponse);
            //xmlClient.CSTAConferenced -= new CSTAConferencedEventHandler(xmlClient_CSTAConferenced);

            //xmlStation.MonitorStarted -= new EventHandler(xmlStation_MonitorStarted);
            //xmlStation.MonitorStopped -= new EventHandler(xmlStation_MonitorStopped);

            //Application.ApplicationExit -= new EventHandler(Application_ApplicationExit);
        }

        //This function is used to validate the IP address. Returns true if IP is valid otherwise false.
        public static bool checkIP(string _ip)
        {
            Regex ip = new Regex(@"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b");
            MatchCollection result = ip.Matches(_ip);
            if (result.Count > 0)
            {
                string[] ipParts = _ip.Split('.');
                int ipPortionVal;
                foreach (string ipPortion in ipParts)
                {
                    bool isInt = Int32.TryParse(ipPortion, out ipPortionVal);
                    if (isInt)
                    {
                        if (!(ipPortionVal > -1 && ipPortionVal < 256))
                            return false;
                    }
                    else
                        return false;
                }
                return true;
            }
            else
                return false;
        }

        private void txtbServerIP_TextChanged(object sender, EventArgs e)
        {

        }

        //This function is used to check whether the input value entered is Numeric. Ex. The port number or the Agent ID must always be Numeric.
        // Returns true if string is Numeric otherwise false.
        public static bool checkNum(string _no)
        {
            int no;
            bool isNum = int.TryParse(_no, out no);
            if (isNum)
                return true;
            else
                return false;
        }

        //Clears Textbox to accept Station ID
        //void txtbTransferCall_Click(object sender, EventArgs e)
        //{
        //    txtbTransferCall.Clear();
        //    txtbTransferCall.Click -= new EventHandler(txtbTransferCall_Click);
        //    txtbTransferCall.Click += new EventHandler(txtbTransferCall_Click);
        //}

        ////Clears TextBox to accept Station ID
        //void txtbDialNumber_Click(object sender, EventArgs e)
        //{
        //    txtbDialNumber.Clear();
        //    btnPlaceCall.Enabled = true;
        //    txtbDialNumber.Click -= new EventHandler(txtbDialNumber_Click);
        //    txtbDialNumber.Click += new EventHandler(txtbDialNumber_Click);
        //}

        //private void BuildTextBoxEvents()
        //{
        //    //TextBox events
        //    txtbDialNumber.Click += new EventHandler(txtbDialNumber_Click);
        //    txtbTransferCall.Click += new EventHandler(txtbTransferCall_Click);
        //}



        //private void holdTimer_Tick(object sender, EventArgs e)
        //{
        //    hold_time = hold_time + 1;
        //    sec = hold_time % 60;
        //    min = ((hold_time - sec) / 60) % 60;
        //    hr = ((hold_time - (sec + (min * 60))) / 3600) % 60;
        //    lblHoldTimer.Text = hr.ToString() + ":" + min.ToString() + ":" + sec.ToString();
        //}

        //private void callTimer_Tick(object sender, EventArgs e)
        //{
        //    time = time + 1;
        //    sec = time % 60;
        //    min = ((time - sec) / 60) % 60;
        //    hr = ((time - (sec + (min * 60))) / 3600) % 60;
        //    lblCallTimer.Text = hr.ToString() + ":" + min.ToString() + ":" + sec.ToString();
        //}

        //Selects the type of Transfer Call or Conference Call
        //private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cmbType.SelectedItem.Equals("Blind"))
        //    {
        //        _type = Consult_Type.Blind;
        //    }
        //    if (cmbType.SelectedItem.Equals("Consult"))
        //    {
        //        _type = Consult_Type.StartConsult;
        //    }
        //}
    }
}




