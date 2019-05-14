using AgileSoftware.Developer;
using AgileSoftware.Developer.CSTA;
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

        //Log File is generated that stores log entries of the application. It is stored in the same directory with the name: AgentLoginForm.log
        public static readonly ILog log = LogManager.GetLogger(typeof(SettingConfigForm));


        public VoiceCallForm()
        {
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
               
            //buildEventHandlers();

           
        }
        private Form CheckExists(Type ftype)
        {
            FormCollection fc = Application.OpenForms;
            foreach (Form f in fc)
                if (f.GetType() == ftype)
                    return f;
            return null;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Form frm = this.CheckExists(typeof(SettingConfigForm));

            if (frm != null) frm.Activate();
            else
            {
                f = new SettingConfigForm();
                //f.MdiParent = this;
                buildEventHandlers();   
                f.ShowDialog();
                
            }
            MessageBox.Show(xmlClient.ServerIP + "-" + xmlClient.ServerPort);
            MessageBox.Show(xmlStation.StationDN);
        }


        void f_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible && Program.globalCom.Connected == true) {
                btnLogout.Enabled = true;
            }
        }

        private void buildEventHandlers()
        {
            xmlClient = Program.globalCom.xmlClient;
            
            xmlStation = Program.globalCom.xmlStation;
           
            f.VisibleChanged += new EventHandler(f_VisibleChanged);
            //if(f!=null) f.connectSuccessfully += new EventHandler(activeLogout);

            //xmlClient.XMLEnumerateServicesReturned += new XMLEnumerateServicesReturnedEventHandler(Program.globalCom.xmlClient.XMLEnumerateServicesReturned);
            //xmlClient.SocketConnected += new EventHandler(xmlClient_SocketConnected);
            //xmlClient.StreamConnected += new EventHandler(xmlClient_StreamConnected);

            xmlClient.CSTAGetAgentStateResponse += new CSTAGetAgentStateResponseEventHandler(xmlClient_CSTAGetAgentStateResponse);
            xmlClient.CSTAAgentLoggedOn += new CSTAAgentLoggedOnEventHandler(xmlClient_CSTAAgentLoggedOn);

            //StationAgentList 
            IEnumerator<StationAgentList> agentList = xmlStation.StationAgents.Cast<StationAgentList>().GetEnumerator();


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
            
        }

        void Application_ApplicationExit(object sender, EventArgs e)
        {
            log.Info("Exited Voice Handling Application.");
            Environment.Exit(0);
        }

       


    }
}
