using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigBee.Utilities;

namespace ZigBee.ConBee
{
    public class Group : ZigBee.Utilities.Control
    {
        private System.Collections.Generic.List<Light> m_Lights = null;
        private System.Collections.Generic.List<ZigBee.Utilities.State> m_States = null;
        private System.Collections.Generic.List<Scene> m_Scenes = null;
        private System.Collections.Generic.List<Action> m_Actions = null;

        //private System.Collections.Generic.List<> m_LightSequences = null;
        public List<State> States
        {
            get
            {
                return m_States;
            }

            set
            {
                m_States = value;
            }
        }

        public List<Scene> Scenes
        {
            get
            {
                return m_Scenes;
            }

            set
            {
                m_Scenes = value;
            }
        }

        public List<Action> Actions
        {
            get
            {
                return m_Actions;
            }

            set
            {
                m_Actions = value;
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

        public Group(ZigBee.Utilities.Settings.Settings Settings) : base(Settings)
        {
            Initialize();
        }

        private void Initialize()
        {
            Lights = new List<ConBee.Light>();
            m_Scenes = new List<ConBee.Scene>();
            m_States = new List<State>();
            Actions = new List<ConBee.Action>();

            m_Classname = "Group";

        }

        public override string ToString()
        {
            return m_Name;
        }
    }
}
