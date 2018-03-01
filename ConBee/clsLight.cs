using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigBee.ConBee
{
    public class Light : ZigBee.Utilities.Control
    {

        public Light(ZigBee.Utilities.Settings.Settings Settings) : base(Settings)
        {
            Initialize();
        }


        private void Initialize()
        {
            m_Classname = "Light";
        }

        public override string ToString()
        {
            return m_Name;
        }
    }

}
