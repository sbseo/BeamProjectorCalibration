using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLocalBeam.ModelFolder
{
    // https://www.codeproject.com/Articles/41817/Implementing-INotifyPropertyChanged 
    // INPC 사용법
    class Points : INotifyPropertyChanged
    {
        /* 수평 맞추기 */
        private double _p1_x;
        private double _p2_x;
        private double _p3_x;

        /* 수평 맞추기2 */
        private double _p1_y;
        private double _p2_y;
        private double _p3_y;

        private string _p1_string;
        private string _p2_string;
        private string _p3_string;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler ModelUpdateHappened;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
                /* 이벤트 발생 알림 */
                //Fire the event - notifying all subscribers
                if (this.ModelUpdateHappened != null)
                    ModelUpdateHappened(this, null);
            }
        }

        public double P1_x
        {
            get
            {
                return _p1_x;
            }

            set
            {
                _p1_x = value;
                OnPropertyChanged(new PropertyChangedEventArgs("P1_x"));
            }
        }

        public double P2_x
        {
            get
            {
                return _p2_x;
            }

            set
            {
                _p2_x = value;
                OnPropertyChanged(new PropertyChangedEventArgs("P2_x"));
            }
        }

        public double P3_x
        {
            get
            {
                return _p3_x;
            }

            set
            {
                _p3_x = value;
                OnPropertyChanged(new PropertyChangedEventArgs("P3_x"));
            }
        }

        public double P1_y
        {
            get
            {
                return _p1_y;
            }

            set
            {
                _p1_y = value;
                OnPropertyChanged(new PropertyChangedEventArgs("P1_y"));
            }
        }

        public double P2_y
        {
            get
            {
                return _p2_y;
            }

            set
            {
                _p2_y = value;
                OnPropertyChanged(new PropertyChangedEventArgs("P2_y"));
            }
        }

        public double P3_y
        {
            get
            {
                return _p3_y;
            }

            set
            {
                _p3_y = value;
                OnPropertyChanged(new PropertyChangedEventArgs("P3_y"));
            }
        }

        internal interface INotifyProperyChanged
        {
        }
        public Points()
        {

        }

        }
    }
