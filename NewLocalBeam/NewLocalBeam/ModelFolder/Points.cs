using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLocalBeam.ModelFolder
{
    class Points : INotifyPropertyChanged
    {
        /* 수평 맞추기 */
        private double p1_x;
        private double p2_x;
        private double p3_x;

        /* 수평 맞추기2 */
        private double p1_y;
        private double p2_y;
        private double p3_y;

        private string p1_string;
        private string p2_string;
        private string p3_string;

        public event PropertyChangedEventHandler PropertyChanged;

        internal interface INotifyProperyChanged
        {
        }
        public Points()
        {

        }
       
        private void OnPropertyChanged()
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(null));
            }
        }

        public double P1_x
        {
            get
            {
                return p1_x;
            }

            set
            {
                p1_x = value;
            }
        }

        public double P2_x
        {
            get
            {
                return p2_x;
            }

            set
            {
                p2_x = value;
            }
        }

        public double P3_x
        {
            get
            {
                return p3_x;
            }

            set
            {
                p3_x = value;
            }
        }

        public double P1_y
        {
            get
            {
                return p1_y;
            }

            set
            {
                p1_y = value;
            }
        }

        public double P2_y
        {
            get
            {
                return p2_y;
            }

            set
            {
                p2_y = value;
            }
        }

        public double P3_y
        {
            get
            {
                return p3_y;
            }

            set
            {
                p3_y = value;
            }
        }

        public string P1_string
        {
            get
            {
                return "(" + P1_x + "," + P1_y + ")";
            }

            set
            {
                p1_string = value;
                OnPropertyChanged();
            }
        }

        public string P2_string
        {
            get
            {
                return "(" + P2_x + "," + P2_y + ")";
            }

            set
            {
                p2_string = value;
            }
        }

        public string P3_string
        {
            get
            {
                return "(" + P3_x + "," + P3_y + ")";
            }

            set
            {
                p3_string = value;
            }
        }
    }


}
