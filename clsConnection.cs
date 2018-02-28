using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace ZigBee.ConBee
{
    public class Connection : ZigBee.Utilities.Base
    {
        private System.Collections.Generic.List<Gateway> m_Gateways = null;

        public Connection(ZigBee.Utilities.Settings.Settings Settings) : base(Settings)
        {
            Initalize();
        }

        private void Initalize()
        {
            m_Gateways = new List<Gateway>();
        }

        public async Task<List<Gateway>> GetGateways()
        {
            m_Gateways = new List<Gateway>();

            System.Net.Http.HttpClient m_HttpClient = new System.Net.Http.HttpClient();
            m_HttpClient.DefaultRequestHeaders.Add("X-HeaderKey", "HeaderValue");
            m_HttpClient.DefaultRequestHeaders.Referrer = new Uri(m_Settings.ServiceURL);

            var m_URL = String.Format(m_Settings.ServiceURL);

            System.Net.Http.HttpResponseMessage m_HttpResponseMessage = await m_HttpClient.GetAsync(m_URL);

            if (m_HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string m_Response = await m_HttpResponseMessage.Content.ReadAsStringAsync();
                System.Web.Script.Serialization.JavaScriptSerializer m_JsSerializer =
                            new System.Web.Script.Serialization.JavaScriptSerializer();
                var m_Result = m_JsSerializer.DeserializeObject(m_Response);
                System.Object[] m_ObjectArray = (System.Object[])m_Result;

                foreach (System.Object m_GWObject in m_ObjectArray)
                {
                    System.Collections.Generic.Dictionary<string, object> m_GWDictionary = 
                        (System.Collections.Generic.Dictionary<string, object>)m_GWObject;

                    ZigBee.ConBee.Gateway m_Gateway = new ConBee.Gateway(m_Settings)
                    {
                        ID = m_GWDictionary["id"].ToString(),
                        MacAddress = m_GWDictionary["macaddress"].ToString(),
                        InternalIPAddress = m_GWDictionary["internalipaddress"].ToString(),
                        InternalPort = m_GWDictionary["internalport"].ToString(),
                        Name = m_GWDictionary["name"].ToString()
                    };
                    m_Gateways.Add(m_Gateway);
                }
            }
            return m_Gateways;
        }

    }
}
