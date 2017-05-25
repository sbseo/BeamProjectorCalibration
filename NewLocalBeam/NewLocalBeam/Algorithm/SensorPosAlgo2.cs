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
        p[0]                               p[0]
        
        p[1]      p[2]          p[2]       p[1]      
    */

    class SensorPosAlgo2
    {

        private Point answerPoint;


        public SensorPosAlgo2()
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
            Boolean isRightSided = false;

            if (direction == "right")
            {
                //계산시작(기본단위는 Radian)
                angleInfo.rightDevice_thetaR = Math.Abs(angleInfo.rightDevice_thetaR2 - angleInfo.rightDevice_thetaR1);
                angleInfo.rightDevice_thetaL = Math.Abs(angleInfo.rightDevice_thetaR3 - angleInfo.rightDevice_thetaR2);

                if (angleInfo.p[1].X < angleInfo.p[2].X)
                {
                }
                /*               p1 [p0]
                    p3 [p2]      p2 [p1]
                꼴일경우... */
                else
                {
                    isRightSided = true;
                }


                // a, b 변수 설정
                double a = Math.Abs(angleInfo.p[1].Y - angleInfo.p[0].Y);
                double b = Math.Abs(angleInfo.p[2].X - angleInfo.p[1].X);
                double thetaA = angleInfo.rightDevice_thetaR + angleInfo.rightDevice_thetaL;

                // 공식대입
                angleInfo.rightDevice_thetaM = Math.Atan2((b * Math.Tan(thetaA) - (a * Math.Tan(thetaA) * Math.Tan(angleInfo.rightDevice_thetaL)) - (b * Math.Tan(angleInfo.rightDevice_thetaL))), a * Math.Tan(angleInfo.rightDevice_thetaL));

                double y = ((a * Math.Tan(thetaA)) + (a * Math.Tan(angleInfo.rightDevice_thetaM)) - (Math.Tan(thetaA) * Math.Tan(angleInfo.rightDevice_thetaM) * b) + b) / (Math.Tan(thetaA) * (1 + Math.Tan(angleInfo.rightDevice_thetaM) * Math.Tan(angleInfo.rightDevice_thetaM)));
                double x = y * Math.Tan(angleInfo.rightDevice_thetaM);


                // 답 출력
                answerPoint.X = (isRightSided == true) ? angleInfo.p[2].X - x : angleInfo.p[2].X + x; // x값 출력
                answerPoint.Y = y;

            }
            else if (direction == "left")
            {

                //계산시작(기본단위는 Radian)
                angleInfo.leftDevice_thetaR = Math.Abs(angleInfo.leftDevice_thetaR2 - angleInfo.leftDevice_thetaR1);
                angleInfo.leftDevice_thetaL = Math.Abs(angleInfo.leftDevice_thetaR3 - angleInfo.leftDevice_thetaR2);

                /* p1 [p0]
                   p2 [p1]     p3 [p2]
                꼴일경우... */
                if (angleInfo.p[1].X < angleInfo.p[2].X)
                {


                }
                /*               p1 [p0]
                    p3 [p2]      p2 [p1]
                꼴일경우... */
                else
                {
                    isRightSided = true;
                }

                // a, b 변수 설정
                double a = Math.Abs(angleInfo.p[1].Y - angleInfo.p[0].Y);
                double b = Math.Abs(angleInfo.p[2].X - angleInfo.p[1].X);
                double thetaA = angleInfo.leftDevice_thetaR + angleInfo.leftDevice_thetaL;

                //공식대입 4월 23일버전
                angleInfo.leftDevice_thetaM = Math.Atan2((b * Math.Tan(thetaA) - (a * Math.Tan(thetaA) * Math.Tan(angleInfo.leftDevice_thetaL)) - (b * Math.Tan(angleInfo.leftDevice_thetaL))), a * Math.Tan(angleInfo.leftDevice_thetaL));
                double y = ((a * Math.Tan(thetaA)) + (a * Math.Tan(angleInfo.leftDevice_thetaM)) - (Math.Tan(thetaA) * Math.Tan(angleInfo.leftDevice_thetaM) * b) + b) / (Math.Tan(thetaA) * (1 + Math.Tan(angleInfo.leftDevice_thetaM) * Math.Tan(angleInfo.leftDevice_thetaM)));
                double x = Math.Tan(angleInfo.leftDevice_thetaM) * y;

                // 답 출력
                answerPoint.X = (isRightSided == true) ? angleInfo.p[2].X - x : angleInfo.p[2].X + x; // x값 출력
                answerPoint.Y = y;
            }
        }
    }
}
