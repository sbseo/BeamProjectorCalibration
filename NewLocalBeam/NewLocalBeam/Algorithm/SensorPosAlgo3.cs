using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLocalBeam.ModelFolder;
using System.Windows;

namespace NewLocalBeam.Algorithm
{

    /* In the case of the points located below 
        p[0]
        
        p[1]

        p[2]
    */

    class SensorPosAlgo3
    {

        private Point answerPoint;


        public SensorPosAlgo3()
        {

        }

        public Point AnswerPoint
        {
            get
            {
                return answerPoint;
            }

            set
            {
                answerPoint = value;
            }
        }

        public void FindSensorPosition(string direction, DeviceAngleInfo angleInfo)
        {

            if (direction == "right")
            {
                //계산시작(기본단위는 Radian으로 합시다)
                angleInfo.rightDevice_thetaR = angleInfo.rightDevice_thetaR2 - angleInfo.rightDevice_thetaR1;
                angleInfo.rightDevice_thetaL = angleInfo.rightDevice_thetaR3 - angleInfo.rightDevice_thetaR2;

                // a, b 변수 설정
                double a = angleInfo.p[1].Y - angleInfo.p[0].Y; double b = angleInfo.p[2].Y - angleInfo.p[1].Y;
                double A = angleInfo.rightDevice_thetaR + angleInfo.rightDevice_thetaL;

                // 공식대입
                angleInfo.rightDevice_thetaM = Math.Atan2((a * Math.Tan(angleInfo.rightDevice_thetaR)) + (b * Math.Tan(angleInfo.rightDevice_thetaR)) - (a * Math.Tan(A)), b * Math.Tan(A) * Math.Tan(angleInfo.rightDevice_thetaR));

                double x = a * (1 - Math.Tan(angleInfo.rightDevice_thetaM) * Math.Tan(angleInfo.rightDevice_thetaR)) / (Math.Tan(angleInfo.rightDevice_thetaR) * (Math.Tan(angleInfo.rightDevice_thetaM) * Math.Tan(angleInfo.rightDevice_thetaM) + 1));
                double y = Math.Tan(angleInfo.rightDevice_thetaM) * x;


                // 답 출력
                answerPoint.X = x + angleInfo.p[0].X; // x값 출력
                answerPoint.Y = y;

            }
            else if (direction == "left")
            {
                //계산시작(기본단위는 Radian으로 합시다)
                angleInfo.leftDevice_thetaR = angleInfo.leftDevice_thetaR2 - angleInfo.leftDevice_thetaR1;
                angleInfo.leftDevice_thetaL = angleInfo.leftDevice_thetaR3 - angleInfo.leftDevice_thetaR2;

                // a, b 변수 설정
                double a = angleInfo.p[1].Y - angleInfo.p[0].Y; double b = angleInfo.p[2].Y - angleInfo.p[1].Y;
                double A = angleInfo.leftDevice_thetaR + angleInfo.leftDevice_thetaL;


                // 공식대입
                angleInfo.leftDevice_thetaM = Math.Atan2((a * Math.Tan(angleInfo.leftDevice_thetaR)) + (b * Math.Tan(angleInfo.leftDevice_thetaR)) - (a * Math.Tan(A)), b * Math.Tan(A) * Math.Tan(angleInfo.leftDevice_thetaR));

                double x = a * (1 - Math.Tan(angleInfo.leftDevice_thetaM) * Math.Tan(angleInfo.leftDevice_thetaR)) / (Math.Tan(angleInfo.leftDevice_thetaR) * (Math.Tan(angleInfo.leftDevice_thetaM) * Math.Tan(angleInfo.leftDevice_thetaM) + 1));
                double y = Math.Tan(angleInfo.leftDevice_thetaM) * x;

                // 답 출력
                answerPoint.X = x + angleInfo.p[0].X; // x값 출력
                answerPoint.Y = y;
            }
        }
    }
}
