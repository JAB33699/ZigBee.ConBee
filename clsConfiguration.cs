using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigBee.Utilities;

namespace ZigBee.ConBee
{
    public class Configuration : ZigBee.Utilities.Base
    {

        private string m_APIVersion = string.Empty;
        private string m_MACAddress = string.Empty;
        private string m_SWVersion = string.Empty;
        private string m_BridgeID = string.Empty;
        private string m_DataStoreVersion = string.Empty;
        private string m_DeviceName = string.Empty;
        private bool m_FactoryNew = false;
        private string m_ModelID = string.Empty;
        private string m_ReplaceBridgeID = string.Empty;
        private string m_StarterKitID = string.Empty;

        private bool m_DHCP = false;
        private string m_IPAddress = string.Empty;
        private string m_Gateway = string.Empty;
        private bool m_LinkButton = false;
        private DateTime m_LocalTime = DateTime.Now;
        
        private string m_NetMask = string.Empty;
        private string m_PANId = string.Empty;
        private bool m_PortalServices = false;
        private string m_ProxyAddress = string.Empty;
        private int m_ProxyPort = 0;
        //private Newtonsoft.Json.Linq.JObject m_SwupDate = null;
        
        private ZigBee.Utilities.Format.TimeFormat m_TimeFormat = Utilities.Format.TimeFormat.h12;
        private string m_TimeZone = string.Empty;
        private DateTime m_UTC = DateTime.UtcNow;
        private System.Guid m_UUID = System.Guid.Empty;
        private int m_ZigBeeChannel = 0;
        //private Newtonsoft.Json.Linq.JObject m_WhiteList = null;

        public string APIVersion
        {
            get
            {
                return m_APIVersion;
            }

            set
            {
                m_APIVersion = value;
            }
        }

        public bool DHCP
        {
            get
            {
                return m_DHCP;
            }

            set
            {
                m_DHCP = value;
            }
        }

        public string IPAddress
        {
            get
            {
                return m_IPAddress;
            }

            set
            {
                m_IPAddress = value;
            }
        }

        public string Gateway
        {
            get
            {
                return m_Gateway;
            }

            set
            {
                m_Gateway = value;
            }
        }

        public bool LinkButton
        {
            get
            {
                return m_LinkButton;
            }

            set
            {
                m_LinkButton = value;
            }
        }

        public DateTime LocalTime
        {
            get
            {
                return m_LocalTime;
            }

            set
            {
                m_LocalTime = value;
            }
        }

        public string MACAddress
        {
            get
            {
                return m_MACAddress;
            }

            set
            {
                m_MACAddress = value;
            }
        }

        public string NetMask
        {
            get
            {
                return m_NetMask;
            }

            set
            {
                m_NetMask = value;
            }
        }

        public string PANId
        {
            get
            {
                return m_PANId;
            }

            set
            {
                m_PANId = value;
            }
        }

        public string ProxyAddress
        {
            get
            {
                return m_ProxyAddress;
            }

            set
            {
                m_ProxyAddress = value;
            }
        }

        public bool PortalServices
        {
            get
            {
                return m_PortalServices;
            }

            set
            {
                m_PortalServices = value;
            }
        }

        public int ProxyPort
        {
            get
            {
                return m_ProxyPort;
            }

            set
            {
                m_ProxyPort = value;
            }
        }
        public string SWVersion
        {
            get
            {
                return m_SWVersion;
            }

            set
            {
                m_SWVersion = value;
            }
        }

        public Format.TimeFormat TimeFormat
        {
            get
            {
                return m_TimeFormat;
            }

            set
            {
                m_TimeFormat = value;
            }
        }

        public string TimeZone
        {
            get
            {
                return m_TimeZone;
            }

            set
            {
                m_TimeZone = value;
            }
        }

        public DateTime UTC
        {
            get
            {
                return m_UTC;
            }

            set
            {
                m_UTC = value;
            }
        }

        public Guid UUID
        {
            get
            {
                return m_UUID;
            }

            set
            {
                m_UUID = value;
            }
        }

        public int ZigBeeChannel
        {
            get
            {
                return m_ZigBeeChannel;
            }

            set
            {
                m_ZigBeeChannel = value;
            }
        }

        public string BridgeID
        {
            get
            {
                return m_BridgeID;
            }

            set
            {
                m_BridgeID = value;
            }
        }

        public string DataStoreVersion
        {
            get
            {
                return m_DataStoreVersion;
            }

            set
            {
                m_DataStoreVersion = value;
            }
        }

        public string DeviceName
        {
            get
            {
                return m_DeviceName;
            }

            set
            {
                m_DeviceName = value;
            }
        }

        public bool FactoryNew
        {
            get
            {
                return m_FactoryNew;
            }

            set
            {
                m_FactoryNew = value;
            }
        }

        public string ModelID
        {
            get
            {
                return m_ModelID;
            }

            set
            {
                m_ModelID = value;
            }
        }

        public string ReplaceBridgeID
        {
            get
            {
                return m_ReplaceBridgeID;
            }

            set
            {
                m_ReplaceBridgeID = value;
            }
        }

        public string StarterKitID
        {
            get
            {
                return m_StarterKitID;
            }

            set
            {
                m_StarterKitID = value;
            }
        }

        public Configuration(ZigBee.Utilities.Settings.Settings Settings) : base(Settings)
        {
            Initialize();
        }

        private void Initialize()
        {

        }

        public async Task<bool> GetConfiguration(Token Token)
        {
            m_Succeed = false;
            if (Token != null)
            {
                if (Token.Authorized == true)
                {
                    System.Net.Http.HttpClient m_HttpClient = new System.Net.Http.HttpClient();
                    m_HttpClient.DefaultRequestHeaders.Add("X-HeaderKey", "HeaderValue");
                    m_HttpClient.DefaultRequestHeaders.Referrer = new Uri(m_Settings.ServiceURL);
                    var m_URL = "http://" + Token.InternalIPAddress + ":" + Token.InternalPort + "/api/" + Token.APIKey + "/config";

                    System.Net.Http.HttpResponseMessage m_HttpResponseMessage = await m_HttpClient.GetAsync(m_URL);

                    if (m_HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string m_Response = await m_HttpResponseMessage.Content.ReadAsStringAsync();

                        System.Web.Script.Serialization.JavaScriptSerializer m_JsSerializer =
                                new System.Web.Script.Serialization.JavaScriptSerializer();
                        var m_Result = m_JsSerializer.DeserializeObject(m_Response);

                        System.Collections.Generic.Dictionary<string, object> m_GWConfiguration = 
                            (System.Collections.Generic.Dictionary<string, object>)m_Result;

                        m_APIVersion = m_GWConfiguration["apiversion"].ToString();
                        m_MACAddress = m_GWConfiguration["mac"].ToString();
                        m_SWVersion = m_GWConfiguration["swversion"].ToString();
                        m_Name = m_GWConfiguration["name"].ToString();
                        m_BridgeID = m_GWConfiguration["bridgeid"].ToString();
                        m_DataStoreVersion = m_GWConfiguration["datastoreversion"].ToString();
                        m_DeviceName = m_GWConfiguration["devicename"].ToString();
                        if (m_GWConfiguration["devicename"].ToString().ToLower() == "false")
                        {
                            m_FactoryNew = false;
                        }
                        else
                        {
                            m_FactoryNew = true;
                        }
                        m_ModelID = m_GWConfiguration["modelid"].ToString();
                        m_StarterKitID = m_GWConfiguration["starterkitid"].ToString();
                        if (m_GWConfiguration["starterkitid"].ToString() == "null")
                        {
                            m_ReplaceBridgeID = "null";
                        }
                        else
                        {
                            m_ReplaceBridgeID = m_GWConfiguration["starterkitid"].ToString();
                        }

                        //m_DHCP = false;
                        //m_IPAddress = m_JToken.SelectToken("ipaddress").ToString();
                        //m_Gateway = m_JToken.SelectToken("gateway").ToString();
                        //m_LinkButton = false;
                        //m_LocalTime = DateTime.Now;

                        //m_NetMask = string.Empty;
                        //m_PANId = string.Empty;
                        //m_PortalServices = false;
                        //m_ProxyAddress = string.Empty;
                        //m_ProxyPort = 0;
                        //m_SwupDate = null;

                        //m_TimeFormat = Utilities.Format.TimeFormat.h12;
                        //m_TimeZone = string.Empty;
                        //m_UTC = DateTime.UtcNow;
                        //m_UUID = System.Guid.Empty;
                        //m_ZigBeeChannel = 0;
                        //m_WhiteList = null;
                    }

                    m_Succeed = true;
                }
            }
            return m_Succeed;
        }


    }
}
