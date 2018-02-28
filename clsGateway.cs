using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigBee.Utilities;

namespace ZigBee.ConBee
{
    public class Gateway : ZigBee.Utilities.Control
    {
        private string m_InternalIPAddress = string.Empty;
        private string m_InternalPort = string.Empty;
        private Token m_Token = null;
        private System.Collections.Generic.List<Group> m_Groups = null;
        private System.Collections.Generic.List<Light> m_Lights = null;

        private Configuration m_Configuration = null;

        public string InternalIPAddress
        {
            get
            {
                return m_InternalIPAddress;
            }

            set
            {
                m_InternalIPAddress = value;
                if (m_Token != null)
                {
                    m_Token = new Token()
                    {
                        Authorized = true,
                        UserID = m_Settings.Password,
                        APIKey = m_Settings.APIKey,
                        InternalIPAddress = m_InternalIPAddress,
                        InternalPort = m_InternalPort,
                        Description = "OK"
                    };
                }
                m_Token.InternalIPAddress = m_InternalIPAddress;
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
                if (m_Token != null)
                {
                    m_Token = new Token()
                    {
                        Authorized = true,
                        UserID = m_Settings.Password,
                        APIKey = m_Settings.APIKey,
                        InternalIPAddress = m_InternalIPAddress,
                        InternalPort = m_InternalPort,
                        Description = "OK"
                    };
                }
                m_Token.InternalPort = m_InternalPort;
            }
        }

        public Configuration Configuration
        {
            get
            {
                return m_Configuration;
            }

            set
            {
                m_Configuration = value;
            }
        }

        public Token m_TokenToken
        {
            get
            {
                return m_Token;
            }

            set
            {
                m_Token = value;
            }
        }

        public List<Light> Lights
        {
            get
            {
                return m_Lights;
            }

            set
            {
                m_Lights = value;
            }
        }

        public List<Group> Groups
        {
            get
            {
                return m_Groups;
            }

            set
            {
                m_Groups = value;
            }
        }

        public Gateway(ZigBee.Utilities.Settings.Settings Settings) : base(Settings)
        {
            Initialize();
        }

        private void Initialize()
        {

            m_Token = new Token()
            {
                Authorized = true,
                UserID = m_Settings.Password,
                APIKey = m_Settings.APIKey,
                InternalIPAddress = m_InternalIPAddress,
                InternalPort = m_InternalPort,
                Description = "OK"
            };

            m_Configuration = new ConBee.Configuration(m_Settings);
            Groups = new List<ConBee.Group>()
            {

            };
            Lights = new List<ConBee.Light>()
            {

            };

            m_Classname = "Gateway";
        }

        public async Task<Token> CreateAPIKey()
        {
            m_Succeed = false;
            m_Token = new ConBee.Token()
            {
                Authorized = false
            };
            System.Net.Http.HttpClient m_HttpClient = new System.Net.Http.HttpClient();
            m_HttpClient.DefaultRequestHeaders.Add("X-HeaderKey", "HeaderValue");
            m_HttpClient.DefaultRequestHeaders.Referrer = new Uri(m_Settings.ServiceURL);

            var m_URL = "http://" + m_InternalIPAddress + ":" + m_InternalPort + "/api";

            System.Web.Script.Serialization.JavaScriptSerializer m_JsSerializer =
                new System.Web.Script.Serialization.JavaScriptSerializer();

            System.Net.Http.StringContent m_StringContent = 
                new System.Net.Http.StringContent(m_JsSerializer.Serialize(new { devicetype = m_Settings.APIKey, username = m_Settings.Password }));

            System.Net.Http.HttpResponseMessage m_HttpResponseMessage = await m_HttpClient.PostAsync(m_URL, m_StringContent);
            string m_Response = await m_HttpResponseMessage.Content.ReadAsStringAsync();

            if (m_HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {

                var m_Result = m_JsSerializer.DeserializeObject(m_Response);
                System.Object[] m_APIKeyArray = (System.Object[])m_Result;

                System.Collections.Generic.Dictionary<string, object> m_APIKeyConfiguration =
                        (System.Collections.Generic.Dictionary<string, object>)m_APIKeyArray[0];

                try
                {
                    System.Collections.Generic.Dictionary<string, object> m_APIKey =
                        (System.Collections.Generic.Dictionary<string, object>)m_APIKeyConfiguration["success"];
                    m_Token = new Token()
                    {
                        Authorized = true,
                        UserID = m_APIKey["username"].ToString(),
                        APIKey = m_Settings.APIKey,
                        InternalIPAddress = m_InternalIPAddress,
                        InternalPort = m_InternalPort,
                        Description = "OK",
                        HttpResponseMessage = m_HttpResponseMessage
                    };
                }
                catch
                {

                }
                m_Succeed = true;
            }

            return m_Token;
        }

        public async Task GetData()
        {
            await GetAllLights();
            await GetGroups();
        }

        public async Task<bool> GetConfiguration()
        {
            m_Succeed = false;
            if (m_Token.Authorized == true)
            {
                m_Succeed = await Configuration.GetConfiguration(m_Token);
            }
            return m_Succeed;
        }

        public async Task<ZigBee.Utilities.Response> Update()
        {
            m_Succeed = false;
            ZigBee.Utilities.Response m_Response = new Utilities.Response()
            {
                StatusCode = Utilities.Constants.StatusCode.NotDefined,
                Description = string.Empty
            };
            if (m_Token.Authorized == true)
            {
                System.Net.Http.HttpClient m_HttpClient = new System.Net.Http.HttpClient();
                m_HttpClient.DefaultRequestHeaders.Add("X-HeaderKey", "HeaderValue");
                m_HttpClient.DefaultRequestHeaders.Referrer = new Uri(m_Settings.ServiceURL);

                var m_URL = "http://" + m_InternalIPAddress + ":" + m_InternalPort + "/api";
                System.Web.Script.Serialization.JavaScriptSerializer m_JsSerializer =
                        new System.Web.Script.Serialization.JavaScriptSerializer();
                System.Net.Http.StringContent m_StringContent =
                    new System.Net.Http.StringContent(m_JsSerializer.Serialize(new { devicetype = m_Settings.APIKey, username = m_Settings.Password }));

                System.Net.Http.HttpResponseMessage m_HttpResponseMessage =
                    await m_HttpClient.PostAsync(m_URL, m_StringContent);
                string m_HttpResponse = await m_HttpResponseMessage.Content.ReadAsStringAsync();

                if (m_HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    m_Response.StatusCode = Utilities.Constants.StatusCode.OK;
                    
                }
            }
            return m_Response;
        }

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

        public async Task<ZigBee.Utilities.Response> GetGroups()
        {
            ZigBee.Utilities.Response m_Response = new Utilities.Response()
            {
                StatusCode = Utilities.Constants.StatusCode.NotDefined,
                Description = string.Empty
            };
            ///api/<apikey>/groups
            if (m_Token != null)
            {
                if (m_Token.Authorized == true)
                {
                    System.Net.Http.HttpClient m_HttpClient = new System.Net.Http.HttpClient();
                    m_HttpClient.DefaultRequestHeaders.Add("X-HeaderKey", "HeaderValue");
                    m_HttpClient.DefaultRequestHeaders.Referrer = new Uri(m_Settings.ServiceURL);

                    var m_URL = String.Format("http://{0}/api/{1}/groups", m_Token.InternalIPAddress, m_Token.UserID);
                    System.Net.Http.HttpResponseMessage m_HttpResponseMessage = await m_HttpClient.GetAsync(m_URL);

                    string m_HTTPResponse = await m_HttpResponseMessage.Content.ReadAsStringAsync();

                    if (m_HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        System.Web.Script.Serialization.JavaScriptSerializer m_JsSerializer =
                              new System.Web.Script.Serialization.JavaScriptSerializer();
                        var result = m_JsSerializer.DeserializeObject(m_HTTPResponse);
                        Dictionary<string, object> m_Object = new Dictionary<string, object>();
                        m_Object = (Dictionary<string, object>)(result);

                        foreach (KeyValuePair<string, object> m_GroupObject in m_Object)
                        {
                            Dictionary<string, object> m_Values = (Dictionary<string, object>)m_GroupObject.Value;
                            ZigBee.ConBee.Group m_Group = new Group(m_Settings)
                            {
                                Name = m_Values["name"].ToString(),
                                ID = m_Values["id"].ToString(),
                                Type = m_Values["type"].ToString(),
                                Etag = m_Values["etag"].ToString(),
                                Hidden = CADBeckerTools.JAB.Conversion.GetBool(m_Values["hidden"].ToString()),
                                ZigBeeClass = m_Values["class"].ToString(),
                                States = new List<State>(),
                                
                            };


                            object[] m_LightValues = (object[])m_Values["lights"];
                            foreach (object m_LightObject in m_LightValues)
                            {
                                Light m_Light = new Light(m_Settings)
                                {
                                    ID = m_LightObject.ToString()
                                };
                                m_Group.Lights.Add(m_Light);

                            }

                            object[] m_SceneValues = (object[])m_Values["scenes"];
                            foreach (object m_SceneObject in m_SceneValues)
                            {
                                

                                ZigBee.ConBee.Scene m_Scene = new Scene(m_Settings)
                                {
                                    
                                };
                                m_Group.Scenes.Add(m_Scene);

                            }

                            Dictionary<string, object> m_ActionValues = (Dictionary<string, object>)m_Values["action"];
                            object[] m_XY = (object[])m_ActionValues["xy"];
                            Action m_Action = new ConBee.Action(m_Settings)
                            {
                                Bri = CADBeckerTools.JAB.Conversion.GetInt(m_ActionValues["bri"].ToString()),
                                ColorMode = m_ActionValues["colormode"].ToString(),
                                Ct = CADBeckerTools.JAB.Conversion.GetInt(m_ActionValues["ct"].ToString()),
                                Hue = CADBeckerTools.JAB.Conversion.GetInt(m_ActionValues["hue"].ToString()),
                                On = CADBeckerTools.JAB.Conversion.GetBool(m_ActionValues["on"].ToString()),
                                Sat = CADBeckerTools.JAB.Conversion.GetInt(m_ActionValues["sat"].ToString())
                            };
                            m_Action.ColorValue = new ConBee.ColorValue()
                            {
                                X = CADBeckerTools.JAB.Conversion.GetInt(m_XY[0].ToString()),
                                Y = CADBeckerTools.JAB.Conversion.GetInt(m_XY[1].ToString())
                            };

                            m_Group.Actions.Add(m_Action);

                            object[] m_DeviceMembershipValues = (object[])m_Values["devicemembership"];
                            foreach (object m_DeviceMembershipObject in m_DeviceMembershipValues)
                            {


                            }

                             object[] m_LightSequenceValues = (object[])m_Values["lightsequence"];
                            foreach (object m_LightSequenceObject in m_LightSequenceValues)
                            {


                            }

                            object[] m_MultiDeviceIdsValues = (object[])m_Values["multideviceids"];
                            foreach (object m_MultiDeviceIdsObject in m_MultiDeviceIdsValues)
                            {


                            }
                            Groups.Add(m_Group);
                        }
                    }
                }
            }

            return m_Response;
        }

        public async Task<ZigBee.Utilities.Response> GetGroupLight(Group Group)
        {
            ZigBee.Utilities.Response m_Response = new Utilities.Response()
            {
                StatusCode = Utilities.Constants.StatusCode.NotDefined,
                Description = string.Empty
            };

            if (m_Token != null)
            {
                if (m_Token.Authorized == true)
                {

                }
            }

            return m_Response;
        }


        public override string ToString()
        {
            return m_Name;
        }


    }

    //internal static class Extensions
    //{
    //    internal static string[] GetNamesOfProperties(this JToken obj)
    //    {
    //        var names = new List<string>();
    //        foreach (JProperty prop in obj)
    //        {
    //            names.Add(prop.Name);
    //        }
    //        return names.ToArray();
    //    }

    //    internal static string[] GetNameFieldOfProperties(this JToken obj)
    //    {
    //        var names = new List<string>();
    //        foreach (JObject prop in obj.Values())
    //        {
    //            names.Add(prop.GetValue("name").ToString());
    //        }
    //        return names.ToArray();
    //    }

    //    internal static async Task<string> GetUrl(this string uri)
    //    {
    //        using (var hc = new System.Net.Http.HttpClient())
    //        {
    //            // Get the response: if it's valid return the JSON string else null

    //            var res = await hc.GetAsync(uri);
    //            return
    //            res.IsSuccessStatusCode ?
    //            await res.Content.ReadAsStringAsync() :
    //            null;
    //        }
    //    }

    //    internal static bool RgbFromCie(
    //    double x, double y, out double r, out double g, out double b
    //    )
    //    {
    //        // Convert the colour from CIE to RGB

    //        //var xy = new Colourful.xyYColor(x, y, y);
    //        //var converter = new Colourful.Conversion.ColourfulConverter();
    //        //var col = converter.ToRGB(xy);
    //        //r = col.R * 255;
    //        //g = col.G * 255;
    //        //b = col.B * 255;
    //        r = 255;
    //        g = 255;
    //        b = 255;

    //        return true;
    //    }
    //}
}
