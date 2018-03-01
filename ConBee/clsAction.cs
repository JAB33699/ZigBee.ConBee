using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigBee.ConBee
{
    public class Action : ZigBee.Utilities.Base
    {
        private int m_Bri = 0;
        private string m_ColorMode = string.Empty;
        private int m_Ct = 0;
        private string m_Effect = string.Empty;
        private int m_Hue = 0;
        private bool m_On = false;
        private int m_Sat = 0;
        private ColorValue m_ColorValue = null;


        public int Bri
        {
            get
            {
                return m_Bri;
            }

            set
            {
                m_Bri = value;
            }
        }

        public string ColorMode
        {
            get
            {
                return m_ColorMode;
            }

            set
            {
                m_ColorMode = value;
            }
        }

        public int Ct
        {
            get
            {
                return m_Ct;
            }

            set
            {
                m_Ct = value;
            }
        }

        public string Effect
        {
            get
            {
                return m_Effect;
            }

            set
            {
                m_Effect = value;
            }
        }

        public int Hue
        {
            get
            {
                return m_Hue;
            }

            set
            {
                m_Hue = value;
            }
        }

        public bool On
        {
            get
            {
                return m_On;
            }

            set
            {
                m_On = value;
            }
        }

        public int Sat
        {
            get
            {
                return m_Sat;
            }

            set
            {
                m_Sat = value;
            }
        }

        public ColorValue ColorValue
        {
            get
            {
                return m_ColorValue;
            }

            set
            {
                m_ColorValue = value;
            }
        }

        public Action(ZigBee.Utilities.Settings.Settings Settings) : base(Settings)
        {
            Initialize();
        }

        private void Initialize()
        {
            m_ColorValue = new ConBee.ColorValue();
        }
    }
}
