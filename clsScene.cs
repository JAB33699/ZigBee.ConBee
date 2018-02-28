using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigBee.ConBee
{
    public class Scene : ZigBee.Utilities.Base
    {

        public Scene(ZigBee.Utilities.Settings.Settings Settings) : base(Settings)
        {
            Initialize();
        }

        private void Initialize()
        {
            m_Classname = "Scene";
        }

    }
}
