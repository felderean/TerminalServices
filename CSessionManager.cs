using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace eFormsBiz
{
    public class CSessionManager
    {
        // WTS_INFO_CLASS enumeration
        enum WTS_INFO_CLASS : ushort
        {
            WTSInitialProgram = 0,
            WTSApplicationName = 1,
            WTSWorkingDirectory = 2,
            WTSOEMId = 3,
            WTSSessionId = 4,
            WTSUserName = 5,
            WTSWinStationName = 6,
            WTSDomainName = 7,
            WTSConnectState = 8,
            WTSClientBuildNumber = 9,
            WTSClientName = 10,
            WTSClientDirectory = 11,
            WTSClientProductId = 12,
            WTSClientHardwareId = 13,
            WTSClientAddress = 14,
            WTSClientDisplay = 15,
            WTSClientProtocolType = 16,
            WTSIdleTime = 17,
            WTSLogonTime = 18,
            WTSIncomingBytes = 19,
            WTSOutgoingBytes = 20,
            WTSIncomingFrames = 21,
            WTSOutgoingFrames = 22
        }

        // WTS_CONNECTSTATE_CLASS enumeration
        enum WTS_CONNECTSTATE_CLASS {
            WTSActive,
            WTSConnected,
            WTSConnectQuery,
            WTSShadow,
            WTSDisconnected,
            WTSIdle,
            WTSListen,
            WTSReset,
            WTSDown,
            WTSInit
        }

        // WTS_SESSION_INFO structure
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        private struct WTS_SESSION_INFO
        {
            public Int32 SessionID; //'DWORD integer
            public String pWinStationName; // ' integer LPTSTR - Pointer to a null-terminated string containing the name of the WinStation for this session
            public WTS_CONNECTSTATE_CLASS State;
        }

        // Terminal Server API's
        [DllImport("wtsapi32.dll", SetLastError = true)]
        private static extern IntPtr WTSOpenServer(string pServerName);

        [DllImport("wtsapi32.dll", SetLastError = true)]
        private static extern int WTSEnumerateSessions(
                        System.IntPtr hServer,
                        int Reserved,
                        int Version,
                        ref System.IntPtr ppSessionInfo,
                        ref int pCount);

        [DllImport("Wtsapi32.dll")]
        private static extern bool WTSQuerySessionInformation(
        System.IntPtr hServer, int sessionId, int wtsInfoClass, out System.IntPtr ppBuffer, out uint pBytesReturned);

        [DllImport("wtsapi32.dll")]
        private static extern void WTSCloseServer(IntPtr hServer);

        [DllImport("wtsapi32.dll", SetLastError = true)]
        private static extern bool WTSLogoffSession(IntPtr hServer, int SessionId, bool bWait);

        [DllImport("wtsapi32.dll", ExactSpelling = true, SetLastError = false)]
        private static extern void WTSFreeMemory(IntPtr memory);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        // Constructor
        public CSessionManager()
        {
        }

        // OpenServer: pServerName = name of the server
        // return handle to the server
        public IntPtr OpenServer(string pServerName)
        {
            IntPtr ptrServer;
            ptrServer = WTSOpenServer(pServerName);

            return ptrServer;
        }

        // GetSessions: pServerName = name of the server
        // return all sessions for the server
        public List<CSessionInfo> GetSessions(string pServerName)
        {
            List<CSessionInfo> lSessions = new List<CSessionInfo>();

            if (pServerName == "")
                return lSessions;
            
            IntPtr iServer = OpenServer(pServerName);
            int rErrorCode = 0;

            if (iServer.ToInt32() > 0)
            {
                lSessions = GetSessions(iServer, ref rErrorCode);

                CSessionBuffer pSessionBuffer;
                pSessionBuffer = new CSessionBuffer();

                pSessionBuffer.SetSessions(lSessions);
            }

            return lSessions;
        }

        // GetSessions: pServerName = name of the server, rErrorCode = error code (reference)
        // return all sessions for the server
        private List<CSessionInfo> GetSessions(IntPtr ptrOpenedServer, ref int rErrorCode)
        {
            List<CSessionInfo> lSessionInfo = new List<CSessionInfo>();

            int nRetVal;

            IntPtr ppSessionInfo = IntPtr.Zero;
            int nCount = 0;

            if (ptrOpenedServer.ToInt32() <= 0)
                return lSessionInfo;

            try 
            { 
                nRetVal = WTSEnumerateSessions(ptrOpenedServer, 0, 1, ref ppSessionInfo, ref nCount);
                if (nRetVal != 0)
                {
                    WTS_SESSION_INFO[] sessionInfo;
                    sessionInfo = new WTS_SESSION_INFO[nCount];

                    Int64 current;
                    current = ppSessionInfo.ToInt64();

                    int DataSize = Marshal.SizeOf(new WTS_SESSION_INFO());

                    for (int i = 0; i < nCount; i++)
                    {
                        sessionInfo[i] = (WTS_SESSION_INFO)(Marshal.PtrToStructure(new IntPtr(current), (new WTS_SESSION_INFO()).GetType()));
                        current = current + DataSize;
                    }

                    WTSFreeMemory(ppSessionInfo);

                    IntPtr str;
                    uint nRet;
                    string sUserName = "";

                    for (int i = 0; i < nCount; i++)
                    {
                        CSessionInfo oSessionInfo = new CSessionInfo();
                        oSessionInfo.SessionID = sessionInfo[i].SessionID;
                        oSessionInfo.StationName = sessionInfo[i].pWinStationName;
                        oSessionInfo.ConnectionState = GetConnectionState(sessionInfo[i].State);

                        sUserName = "";
                        if (WTSQuerySessionInformation(ptrOpenedServer, oSessionInfo.SessionID, (int)WTS_INFO_CLASS.WTSUserName, out str, out nRet))
                        {
                            sUserName = Marshal.PtrToStringAnsi(str);
                            oSessionInfo.UserName = sUserName;
                        }

                        if (sUserName != "")
                            lSessionInfo.Add(oSessionInfo);
                    }                   
                }
            }
            catch (Exception e)
            {
                rErrorCode = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
            }

            return lSessionInfo;
        }

        // GetConnectionState: State = state of the session (enum)
        // return state of the session (string)
        private string GetConnectionState(WTS_CONNECTSTATE_CLASS State)
        {
            string RetVal;

            switch (State)
            {
                case WTS_CONNECTSTATE_CLASS.WTSActive:
                    RetVal = "Active";
                    break;
                case WTS_CONNECTSTATE_CLASS.WTSConnected:
                    RetVal = "Connected";
                    break;
                case WTS_CONNECTSTATE_CLASS.WTSConnectQuery:
                    RetVal = "Query";
                    break;
                case WTS_CONNECTSTATE_CLASS.WTSDisconnected:
                    RetVal = "Disconnected";
                    break;
                case WTS_CONNECTSTATE_CLASS.WTSDown:
                    RetVal = "Down";
                    break;
                case WTS_CONNECTSTATE_CLASS.WTSIdle:
                    RetVal = "Idle";
                    break;
                case WTS_CONNECTSTATE_CLASS.WTSInit:
                    RetVal = "Initializing.";
                    break;
                case WTS_CONNECTSTATE_CLASS.WTSListen:
                    RetVal = "Listen";
                    break;
                case WTS_CONNECTSTATE_CLASS.WTSReset:
                    RetVal = "reset";
                    break;
                case WTS_CONNECTSTATE_CLASS.WTSShadow:
                    RetVal = "Shadowing";
                    break;
                default:
                    RetVal = "Unknown connect state";
                    break;
            }

            return RetVal;
        }

    }

    // class CSessionInfo
    public class CSessionInfo
    {
        private int m_SessionID;
        private string m_StationName;
        private string m_ConnectionState;
        private string m_UserName;
        private string m_LogOff;

        public string WorkCenter;
        public string ServerName;

        // Constructor
        public CSessionInfo()
        {
            m_SessionID = 0;
            m_StationName = "";
            m_ConnectionState = "";
            m_UserName = "";
            m_LogOff = "Log Off";
        }

        // Properties
        public int SessionID
        {
            get
            {
                return m_SessionID;
            }
            set
            {
                m_SessionID = value;
            }
        }

        public string StationName
        {
            get
            {
                return m_StationName;
            }
            set
            {
                m_StationName = value;
            }
        }

        public string ConnectionState
        {
            get
            {
                return m_ConnectionState;
            }
            set
            {
                m_ConnectionState = value;
            }
        }

        public string UserName
        {
            get
            {
                return m_UserName;
            }
            set
            {
                m_UserName = value;
            }
        }

        public string LogOff
        {
            get
            {
                return m_LogOff;
            }
            set
            {
                m_LogOff = value;
            }
        }

    }
}