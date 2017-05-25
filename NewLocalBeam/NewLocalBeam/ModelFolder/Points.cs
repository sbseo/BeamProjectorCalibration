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
        #region #Properties

        /* 수평 맞추기 */
        private double _p1_x;
        private double _p2_x;
        private double _p3_x;
        private double _p4_x;
        private double _p5_x;
        private double _p6_x;
        private double _p7_x;
        private double _p8_x;
        private double _p9_x;

        /* 수평 맞추기2 */
        private double _p1_y;
        private double _p2_y;
        private double _p3_y;
        private double _p4_y;
        private double _p5_y;
        private double _p6_y;
        private double _p7_y;
        private double _p8_y;
        private double _p9_y;

        /* Sensor 위치 */
        private double _sensorLx;
        private double _sensorLy;
        private double _sensorRx;
        private double _sensorRy;

        #endregion

        #region INPC Implemenation
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

        internal interface INotifyProperyChanged
        {
        }
        #endregion

        #region GetSet
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

        public double P4_x
        {
            get
            {
                return _p4_x;
            }

            set
            {
                _p4_x = value;
                OnPropertyChanged(new PropertyChangedEventArgs("P4_x"));
            }
        }

        public double P5_x
        {
            get
            {
                return _p5_x;
            }

            set
            {
                _p5_x = value;
                OnPropertyChanged(new PropertyChangedEventArgs("P5_x"));
            }
        }

        public double P6_x
        {
            get
            {
                return _p6_x;
            }

            set
            {
                _p6_x = value;
                OnPropertyChanged(new PropertyChangedEventArgs("P6_x"));
            }
        }

        public double P7_x
        {
            get
            {
                return _p7_x;
            }

            set
            {
                _p7_x = value;
                OnPropertyChanged(new PropertyChangedEventArgs("P7_x"));
            }
        }

        public double P8_x
        {
            get
            {
                return _p8_x;
            }

            set
            {
                _p8_x = value;
                OnPropertyChanged(new PropertyChangedEventArgs("P8_x"));
            }
        }

        public double P9_x
        {
            get
            {
                return _p9_x;
            }

            set
            {
                _p9_x = value;
                OnPropertyChanged(new PropertyChangedEventArgs("P9_x"));
            }
        }

        public double P4_y
        {
            get
            {
                return _p4_y;
            }

            set
            {
                _p4_y = value;
                OnPropertyChanged(new PropertyChangedEventArgs("P4_y"));
            }
        }

        public double P5_y
        {
            get
            {
                return _p5_y;
            }

            set
            {
                _p5_y = value;
                OnPropertyChanged(new PropertyChangedEventArgs("P5_y"));
            }
        }

        public double P6_y
        {
            get
            {
                return _p6_y;
            }

            set
            {
                _p6_y = value;
                OnPropertyChanged(new PropertyChangedEventArgs("P6_y"));
            }
        }

        public double P7_y
        {
            get
            {
                return _p7_y;
            }

            set
            {
                _p7_y = value;
                OnPropertyChanged(new PropertyChangedEventArgs("P7_y"));
            }
        }

        public double P8_y
        {
            get
            {
                return _p8_y;
            }

            set
            {
                _p8_y = value;
                OnPropertyChanged(new PropertyChangedEventArgs("P8_y"));
            }
        }

        public double P9_y
        {
            get
            {
                return _p9_y;
            }

            set
            {
                _p9_y = value;
                OnPropertyChanged(new PropertyChangedEventArgs("P9_y"));
            }
        }

        public double SensorLx
        {
            get
            {
                return _sensorLx;
            }

            set
            {
                _sensorLx = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SensorLx"));
            }
        }

        public double SensorLy
        {
            get
            {
                return _sensorLy;
            }

            set
            {
                _sensorLy = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SensorLy"));
            }
        }

        public double SensorRx
        {
            get
            {
                return _sensorRx;
            }

            set
            {
                _sensorRx = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SensorRx"));
            }
        }

        public double SensorRy
        {
            get
            {
                return _sensorRy;
            }

            set
            {
                _sensorRy = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SensorRy"));
            }
        }
        #endregion

        public Points()
        {

        }
    }
}
