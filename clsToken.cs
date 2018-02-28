using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ZigBee.ConBee
{
    public class Token
    {
        private bool m_Authorized = false;
        private string m_UserID = string.Empty;
        private string m_APIKey = string.Empty;
        private string m_Description = string.Empty;
        private string m_InternalIPAddress = string.Empty;
        private string m_InternalPort = string.Empty;
        private System.Net.Http.HttpResponseMessage m_HttpResponseMessage = null;

        public string AuthToken
        {
            get
            {
                string m_AuthToken = string.Empty;
                byte[] m_BytesToEncode = Encoding.UTF8.GetBytes("username:" + m_UserID);
                //byte[] m_BytesToEncode = Encoding.UTF8.GetBytes(m_UserID);
                m_AuthToken = Convert.ToBase64String(m_BytesToEncode);

                return m_AuthToken;
            }
        }
        public string UserID
        {
            get
            {
                return m_UserID;
            }

            set
            {
                m_UserID = value;
            }
        }

        public string APIKey
        {
            get
            {
                return m_APIKey;
            }

            set
            {
                m_APIKey = value;
            }
        }

        public bool Authorized
        {
            get
            {
                if (m_InternalIPAddress.Length == 0 || m_InternalPort.Length == 0)
                {
                    m_Authorized = false;
                }
                return m_Authorized;
            }

            set
            {
                m_Authorized = value;
            }
        }

        public string Description
        {
            get
            {
                return m_Description;
            }

            set
            {
                m_Description = value;
            }
        }

        public string InternalIPAddress
        {
            get
            {
                return m_InternalIPAddress;
            }

            set
            {
                m_InternalIPAddress = value;
            }
        }

        public string InternalPort
        {
            get
            {
                return m_InternalPort;
            }

            set
            {
                m_InternalPort = value;
            }
        }

        public HttpResponseMessage HttpResponseMessage
        {
            get
            {
                return m_HttpResponseMessage;
            }

            set
            {
                m_HttpResponseMessage = value;
            }
        }

        public Token()
        {

        }

    }
}
