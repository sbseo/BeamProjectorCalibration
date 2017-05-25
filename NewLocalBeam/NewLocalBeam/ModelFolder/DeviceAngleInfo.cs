using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NewLocalBeam.ModelFolder
{
    class DeviceAngleInfo
    {
        public double leftDevice_thetaR1;
        public double leftDevice_thetaR2;
        public double leftDevice_thetaR3;
        public double leftDevice_thetaL;
        public double leftDevice_thetaR;

        public double rightDevice_thetaR;
        public double rightDevice_thetaL;

        public double leftDevice_thetaM;
        public double rightDevice_thetaM;
        public double rightDevice_TanThetaM;

        public double rightDevice_thetaR1;
        public double rightDevice_thetaR2;
        public double rightDevice_thetaR3;

        public Point[] p;

        public DeviceAngleInfo()
        {

        }
        
        public void PointsToArray(Point a, Point b, Point c)
        {
            p[0] = a;
            p[1] = b;
            p[2] = c;
        }

        public void MeasureAllAngles(Point sensorRPoint, Point sensorLPoint, Point a, Point b, Point c)
        {

            // 입력받은 세 좌표도 필드에 저장함.
            PointsToArray(a, b, c);
            
            // 정보입력 (Radian)
            rightDevice_thetaR1 = CalculateAngle(sensorRPoint, a);
            rightDevice_thetaR2 = CalculateAngle(sensorRPoint, b);
            rightDevice_thetaR3 = CalculateAngle(sensorRPoint, c);

            // 정보입력 (Radian)
            leftDevice_thetaR1 = CalculateAngle(sensorLPoint, a);
            leftDevice_thetaR2 = CalculateAngle(sensorLPoint, b);
            leftDevice_thetaR3 = CalculateAngle(sensorLPoint, c);
        }

        /* 각도계산 */
        public double CalculateAngle(Point p1, Point p2)
        {
            double xDiff = p1.X - p2.X;
            double yDiff = p1.Y - p2.Y;
            double radian = -1 * Math.Atan2(yDiff, xDiff);
            // double degree = radian * (180 / Math.PI);

            return radian;
        }

        public double LeftDevice_thetaR1
        {
            get
            {
                return leftDevice_thetaR1;
            }

            set
            {
                leftDevice_thetaR1 = value;
            }
        }

        public double LeftDevice_thetaR2
        {
            get
            {
                return leftDevice_thetaR2;
            }

            set
            {
                leftDevice_thetaR2 = value;
            }
        }

        public double LeftDevice_thetaR3
        {
            get
            {
                return leftDevice_thetaR3;
            }

            set
            {
                leftDevice_thetaR3 = value;
            }
        }

        public double LeftDevice_thetaL
        {
            get
            {
                return leftDevice_thetaL;
            }

            set
            {
                leftDevice_thetaL = value;
            }
        }

        public double LeftDevice_thetaR
        {
            get
            {
                return leftDevice_thetaR;
            }

            set
            {
                leftDevice_thetaR = value;
            }
        }

        public double RightDevice_thetaR
        {
            get
            {
                return rightDevice_thetaR;
            }

            set
            {
                rightDevice_thetaR = value;
            }
        }

        public double RightDevice_thetaL
        {
            get
            {
                return rightDevice_thetaL;
            }

            set
            {
                rightDevice_thetaL = value;
            }
        }

        public double LeftDevice_thetaM
        {
            get
            {
                return leftDevice_thetaM;
            }

            set
            {
                leftDevice_thetaM = value;
            }
        }

        public double RightDevice_thetaM
        {
            get
            {
                return rightDevice_thetaM;
            }

            set
            {
                rightDevice_thetaM = value;
            }
        }

        public double RightDevice_TanThetaM
        {
            get
            {
                return rightDevice_TanThetaM;
            }

            set
            {
                rightDevice_TanThetaM = value;
            }
        }

        public double RightDevice_thetaR1
        {
            get
            {
                return rightDevice_thetaR1;
            }

            set
            {
                rightDevice_thetaR1 = value;
            }
        }

        public double RightDevice_thetaR2
        {
            get
            {
                return rightDevice_thetaR2;
            }

            set
            {
                rightDevice_thetaR2 = value;
            }
        }

        public double RightDevice_thetaR3
        {
            get
            {
                return rightDevice_thetaR3;
            }

            set
            {
                rightDevice_thetaR3 = value;
            }
        }
    }
}
