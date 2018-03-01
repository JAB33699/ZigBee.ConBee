using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigBee.ConBee
{
    public class ColorValue
    {
        private int m_X = 0;
        private int m_Y = 0;

        public int X
        {
            get
            {
                return m_X;
            }

            set
            {
                m_X = value;
            }
        }

        public int Y
        {
            get
            {
                return m_Y;
            }

            set
            {
                m_Y = value;
            }
        }

        public ColorValue()
        {
            Initialize();
        }

        private void Initialize()
        {

        }
    }
}
