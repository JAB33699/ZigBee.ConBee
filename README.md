
# ZigBee.ConBee Gateway
Notwendige usings:
System.Web

**Gateways im Netz ermitteln**
Um alle Gateways im Netz zu ermitteln nutze bitte folgende Methode.

```C#
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
```

**Methoden eines Gateways**

Alle Lichter eines Gateways ermitteln.

```C#
        public async Task<ZigBee.Utilities.Response> GetAllLights()
        {
            m_Succeed = false;
            ZigBee.Utilities.Response m_Response = new Utilities.Response()
            {
                StatusCode = Utilities.Constants.StatusCode.NotDefined,
                Description = string.Empty
            };
            m_Lights = new List<ConBee.Light>()
            {

            };
            if (m_Token != null)
            {
                if (m_Token.Authorized == true)
                {
                    System.Net.Http.HttpClient m_HttpClient = new System.Net.Http.HttpClient();
                    m_HttpClient.DefaultRequestHeaders.Add("X-HeaderKey", "HeaderValue");
                    m_HttpClient.DefaultRequestHeaders.Referrer = new Uri(m_Settings.ServiceURL);

                    var m_URL = String.Format("http://{0}/api/{1}/lights", m_Token.InternalIPAddress, m_Token.UserID);
                    System.Net.Http.HttpResponseMessage m_HttpResponseMessage = await m_HttpClient.GetAsync(m_URL);

                    string m_HTTPResponse = await m_HttpResponseMessage.Content.ReadAsStringAsync();

                    if (m_HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        System.Web.Script.Serialization.JavaScriptSerializer m_JsSerializer =
                              new System.Web.Script.Serialization.JavaScriptSerializer();
                        var result = m_JsSerializer.DeserializeObject(m_HTTPResponse);
                        Dictionary<string, object> obj2 = new Dictionary<string, object>();
                        obj2 = (Dictionary<string, object>)(result);

                        foreach(KeyValuePair<string, object> m_LightObject in obj2)
                        {
                            if (m_Succeed == false)
                            {
                                Dictionary<string, object> m_Values = (Dictionary < string, object> )m_LightObject.Value;
                                Dictionary<string, object> m_StateValues = (Dictionary<string, object>)m_Values["state"];

                                ZigBee.ConBee.Light m_Light = new Light(m_Settings)
                                {
                                    Name = m_Values["name"].ToString(),
                                    ManufacturerName = m_Values["manufacturername"].ToString(),
                                    ID = m_LightObject.Key,
                                    MacAddress = m_Values["uniqueid"].ToString(),
                                    Etag = m_Values["etag"].ToString(),
                                    UniqueID = m_Values["uniqueid"].ToString(),
                                    SwVersion = m_Values["swversion"].ToString(),
                                    Type = m_Values["type"].ToString(),
                                    HasColor =
                                        CADBeckerTools.JAB.Conversion.GetBool(m_Values["hascolor"].ToString()),

                                    State = new Utilities.State(m_Settings)
                                    {
                                        Alert = m_StateValues["alert"].ToString(),
                                        Brightness =
                                            CADBeckerTools.JAB.Conversion.GetInt(m_StateValues["bri"].ToString()),
                                        Reachable = CADBeckerTools.JAB.Conversion.GetBool(m_StateValues["reachable"].ToString()),
                                        On = CADBeckerTools.JAB.Conversion.GetBool(m_StateValues["on"].ToString())
                                    }
                                };

                                m_Lights.Add(m_Light);
                            }

                        }
                        m_Response = new Utilities.Response()
                        {
                            StatusCode = Utilities.Constants.StatusCode.OK,
                            Description = string.Empty
                        };
                        m_Succeed = true;
                    }
                }
            }
            return m_Response;
        }
```
