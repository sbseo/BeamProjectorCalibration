using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using NewLocalBeam.ModelFolder;

namespace NewLocalBeam
{
    /// <summary>
    /// Interaction logic for SensorHeight2Page2.xaml
    /// </summary>
    public partial class SensorHeight2Page2 : Page
    {

        public const int ratio = 2; // 비율 설정
        public static double screenWidth;
        public static double screenHeight;
        public static double deviceLength;
        public static double deviceHeight;

        Point sensorLPoint;
        Point sensorRPoint;

        double sensorLx, sensorRx; // 센서 위치그림. 센서 x좌표.

        public SensorHeight2Page2()
        {
            InitializeComponent();

            // 이벤트 추가
            Model.sensorHeight2MainPage.ModelUpdateHappened += ModelUpdateHandler; // 앞에서 이벤트 발생하면 이거 실행
        }


        // 이벤트 발생시 실행
        public void ModelUpdateHandler(object sender, EventArgs e)
        {
            //Do something here
            // DrawLine();
            InitializeValue();
            DrawScreen();
            DrawSensor();
            LocatePoints();
            MeasureAllAngles("left");
            FindSensorPosition2("left");

            MeasureAllAngles("right");
            FindSensorPosition2("right");
        }

        /* 변수 초기화. 센서환경설정을 그림그리기 적합하게 설정함 */
        public void InitializeValue()
        {
            screenWidth = Model.X / ratio;
            screenHeight = Model.Y / ratio;
            deviceLength = Model.d / ratio;
            deviceHeight = Model.H / ratio;

            // 숫자가 지금 안맞음. 센서 위치.
            sensorLPoint = new Point();
            sensorRPoint = new Point();

            sensorLx = ((screenWidth + deviceLength) / 2);
            sensorRx = ((screenWidth - deviceLength) / 2);

            sensorLPoint.X = sensorLx; sensorLPoint.Y = 0;
            sensorRPoint.X = sensorRx; sensorRPoint.Y = 0;
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

        /* p1, p2, p3좌표 각도 구하기 */
        public double leftDevice_thetaR1;
        public double leftDevice_thetaR2;
        public double leftDevice_thetaR3;
        public double leftDevice_thetaL; // 삼각형에서 왼쪽각도..
        public double leftDevice_thetaR; // 삼각형에서 오른쪽각도..

        public double rightDevice_thetaR;
        public double rightDevice_thetaL;


        public double leftDevice_thetaM;
        public double rightDevice_thetaM;

        public double rightDevice_thetaR1;
        public double rightDevice_thetaR2;
        public double rightDevice_thetaR3;


        public void MeasureAllAngles(string direction)
        {

            if (direction == "right")
            {

                // 정보입력 (Radian)
                rightDevice_thetaR1 = CalculateAngle(sensorRPoint, p[0]);
                rightDevice_thetaR2 = CalculateAngle(sensorRPoint, p[1]);
                rightDevice_thetaR3 = CalculateAngle(sensorRPoint, p[2]);

                // Console.WriteLine(sensorLPoint);

                // 정보출력
                txt_RDangleR1.Text = Convert.ToString(RadianToDegree(rightDevice_thetaR1));
                txt_RDangleR2.Text = Convert.ToString(RadianToDegree(rightDevice_thetaR2));
                txt_RDangleR3.Text = Convert.ToString(RadianToDegree(rightDevice_thetaR3));

                txt_RDsensorX.Text = Convert.ToString(sensorRPoint.X);
                txt_RDsensorY.Text = Convert.ToString(sensorRPoint.Y);

            }
            else if (direction == "left")
            {

                // 정보입력 (Radian)
                leftDevice_thetaR1 = CalculateAngle(sensorLPoint, p[0]);
                leftDevice_thetaR2 = CalculateAngle(sensorLPoint, p[1]);
                leftDevice_thetaR3 = CalculateAngle(sensorLPoint, p[2]);

                // Console.WriteLine(sensorLPoint);

                // 정보출력
                txt_LDangleR1.Text = Convert.ToString(RadianToDegree(leftDevice_thetaR1));
                txt_LDangleR2.Text = Convert.ToString(RadianToDegree(leftDevice_thetaR2));
                txt_LDangleR3.Text = Convert.ToString(RadianToDegree(leftDevice_thetaR3));

                txt_LDsensorX.Text = Convert.ToString(sensorLPoint.X);
                txt_LDsensorY.Text = Convert.ToString(sensorLPoint.Y);

            }
        }

        public async void FindSensorPosition2(string direction)
        {

            Boolean isRightSided = false;

            if (direction == "right")
            {
                //계산시작(기본단위는 Radian으로 합시다)
                rightDevice_thetaR = Math.Abs(rightDevice_thetaR2 - rightDevice_thetaR1);
                rightDevice_thetaL = Math.Abs(rightDevice_thetaR3 - rightDevice_thetaR2);

                if (p[1].X < p[2].X)
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
                double a = Math.Abs(p[1].Y - p[0].Y);
                double b = Math.Abs(p[2].X - p[1].X);
                double thetaA = rightDevice_thetaR + rightDevice_thetaL;

                // 공식대입
                rightDevice_thetaM = Math.Atan2((b * Math.Tan(thetaA) - (a * Math.Tan(thetaA) * Math.Tan(rightDevice_thetaL)) - (b * Math.Tan(rightDevice_thetaL))), a * Math.Tan(rightDevice_thetaL));

                double y = ((a * Math.Tan(thetaA)) + (a * Math.Tan(rightDevice_thetaM)) - (Math.Tan(thetaA) * Math.Tan(rightDevice_thetaM) * b) + b) / (Math.Tan(thetaA) * (1 + Math.Tan(rightDevice_thetaM) * Math.Tan(rightDevice_thetaM)));
                double x = y * Math.Tan(rightDevice_thetaM);


                // 답 출력
                txt_RDthetaM.Text = Convert.ToString(RadianToDegree(rightDevice_thetaM));
                txt_RDx.Text = (isRightSided == true) ? Convert.ToString(p[2].X - x) : Convert.ToString(p[2].X + x); // x값 출력
                txt_RDy.Text = Convert.ToString(y);

            }
            else if (direction == "left")
            {

                //계산시작(기본단위는 Radian으로 합시다)
                leftDevice_thetaR = Math.Abs(leftDevice_thetaR2 - leftDevice_thetaR1);
                leftDevice_thetaL = Math.Abs(leftDevice_thetaR3 - leftDevice_thetaR2);

                /* p1 [p0]
                   p2 [p1]     p3 [p2]
                꼴일경우... */
                if (p[1].X < p[2].X)
                {
                //    leftDevice_thetaR = Math.Abs(leftDevice_thetaR2 - leftDevice_thetaR1);
                //    leftDevice_thetaL = Math.Abs(leftDevice_thetaR3 - leftDevice_thetaR2);
                }
                /*               p1 [p0]
                    p3 [p2]      p2 [p1]
                꼴일경우... */
                else
                {
                    isRightSided = true;
                   // leftDevice_thetaR = Math.Abs(leftDevice_thetaR1 - leftDevice_thetaR2);
                  //  leftDevice_thetaL = Math.Abs(leftDevice_thetaR2 - leftDevice_thetaR3);
                }

                // a, b 변수 설정
                double a = Math.Abs(p[1].Y - p[0].Y);
                double b = Math.Abs(p[2].X - p[1].X);
                double thetaA = leftDevice_thetaR + leftDevice_thetaL;

                // 공식대입
                // double y = (a * A + a * M + b + b * A * M) / (A * (1 + M * M));
                //leftDevice_thetaM = Math.Atan2((b * Math.Tan(thetaA)) - (a * Math.Tan(thetaA) * Math.Tan(leftDevice_thetaL)) - Math.Tan(leftDevice_thetaL), (2 * b * Math.Tan(thetaA) * Math.Tan(leftDevice_thetaL)) + (a * Math.Tan(leftDevice_thetaL)));
                //double y = b / ( Math.Tan(leftDevice_thetaL + leftDevice_thetaM) * Math.Tan(leftDevice_thetaM));
                //double x = y * Math.Tan(leftDevice_thetaM);

                //공식대입 4월 23일버전
                leftDevice_thetaM = Math.Atan2((b * Math.Tan(thetaA) - (a * Math.Tan(thetaA) * Math.Tan(leftDevice_thetaL)) - (b * Math.Tan(leftDevice_thetaL))), a * Math.Tan(leftDevice_thetaL));
                double y = ((a * Math.Tan(thetaA)) + (a * Math.Tan(leftDevice_thetaM)) - (Math.Tan(thetaA) * Math.Tan(leftDevice_thetaM) * b) + b) / (Math.Tan(thetaA) * (1 + Math.Tan(leftDevice_thetaM) * Math.Tan(leftDevice_thetaM)));
               // double y = (b * (1 - (Math.Tan(leftDevice_thetaL) * Math.Tan(leftDevice_thetaM)))) / (Math.Tan(leftDevice_thetaL) * (1 + Math.Tan(leftDevice_thetaM) * Math.Tan(leftDevice_thetaM)));
                double x = Math.Tan(leftDevice_thetaM) * y;

           
               // Console.WriteLine("1번식: {0}, 2번식: {1}", y, y2);


                // 답 출력
                txt_LDthetaM.Text = Convert.ToString(RadianToDegree(leftDevice_thetaM));
                txt_LDx.Text = (isRightSided == true) ? Convert.ToString(p[2].X - x) : Convert.ToString(p[2].X + x); // x값 출력
                txt_LDy.Text = Convert.ToString(y);


                // leftDevice_thetaL + leftDevice_thetaM > 0 && leftDevice_thetaL + leftDevice_thetaM < Math.PI / 2
                if (leftDevice_thetaR2 + leftDevice_thetaL + leftDevice_thetaM > Math.PI / 2 - 0.1 && leftDevice_thetaR2 + leftDevice_thetaL + leftDevice_thetaM < Math.PI / 2 + 0.1)
                {

                }
                else // 잘못된 값이면 출력
                {
                    // Display message box
                    var metroWindow = (Application.Current.MainWindow as MetroWindow);
                    await metroWindow.ShowMessageAsync("실패", "LEFT DEVICE Calibration에 실패하였습니다. 값을 조정해주세요");
                }

            }

        }

        private Task ShowMessageAsync(string v1, string v2)
        {
            throw new NotImplementedException();
        }


        /* 각도보정 */
        // 여기서부터 하면 됨!!!
        //public void CalibrateAngle()
        //{
        //    double calL = Math.Atan2(Model.h, (Model.X + Model.d) / 2);
        //    double calR = Math.Atan2(Model.h, (Model.X - Model.d) / 2);
        //    this.setThetaL(this.getThetaL(true) + calL, true);
        //    this.setThetaR(this.getThetaR(true) + calR, true);
        //}

        //public double getThetaL(bool isRadian = false)
        //{
        //    return isRadian ? thetaL : (thetaL * 180) / Math.PI;
        //}
        //public void setThetaL(double theta, bool isRadian = false)
        //{
        //    this.thetaL = isRadian ? theta : (theta * Math.PI) / 180;
        //}
        //public double getThetaR(bool isRadian = false)
        //{
        //    return isRadian ? thetaR : (thetaR * 180) / Math.PI;
        //}
        //public void setThetaR(double theta, bool isRadian = false)
        //{
        //    this.thetaR = (isRadian ? theta : (theta * Math.PI) / 180);
        //}



        /* 사각형(빔화면) 그림 */
        public void DrawScreen()
        {
            Rectangle screen = new Rectangle();
            screen.Width = screenWidth; // Model.X / ratio;
            screen.Height = screenHeight; //  Model.Y / ratio;

            Canvas.SetLeft(screen, 0);
            Canvas.SetTop(screen, deviceHeight + 5); // 센서 지름만큼 추가해야함. 수정필요.

            screen.Stroke = new SolidColorBrush(Colors.Black);
            canvas.Children.Add(screen);
        }

        /* 센서 그림 */
        public void DrawSensor()
        {

            // 왼쪽센서
            Ellipse sensorL = new Ellipse();
            sensorL.Width = 10;
            sensorL.Height = 10;

            Canvas.SetLeft(sensorL, sensorLx);
            Canvas.SetTop(sensorL, 0);

            sensorL.Fill = Brushes.DarkRed;
            canvas.Children.Add(sensorL);

            // 오른쪽센서
            Ellipse sensorR = new Ellipse();
            sensorR.Width = 10;
            sensorR.Height = 10;
            Canvas.SetLeft(sensorR, sensorRx);
            Canvas.SetTop(sensorR, 0);
            sensorR.Fill = Brushes.DarkRed;
            canvas.Children.Add(sensorR);
        }

        /* p1, p2, p3 좌표 그림 */
        public static Point[] p;
        public void LocatePoints()
        {
            // 좌표를 생성하고 정보를 입력함
            // p[0] = p1 , p[1] = p2, p[2] = p3
            p = new Point[3];
            for (int i = 0; i < 3; i++)
            {
                switch (i)
                {
                    case 0:
                        p[i].X = Model.p1_x;
                        p[i].Y = Model.p1_y;
                        break;
                    case 1:
                        p[i].X = Model.p2_x;
                        p[i].Y = Model.p2_y;
                        break;
                    case 2:
                        p[i].X = Model.p3_x;
                        p[i].Y = Model.p3_y;
                        break;
                }
            }

            // 원을 만들고 그림
            for (int i = 0; i < 3; i++)
            {
                Ellipse ep = new Ellipse();
                ep.Width = 10;
                ep.Height = 10;
                ep.SetValue(Canvas.LeftProperty, p[i].X);
                ep.SetValue(Canvas.TopProperty, p[i].Y);
                ep.Fill = Brushes.Black;

                canvas.Children.Add(ep);
            }
        }

        public void DrawLine()
        {

            Line myLine = new Line();
            myLine.X1 = 0;
            myLine.Y1 = 0;
            myLine.X2 = 500;
            myLine.Y2 = 0;
            myLine.Stroke = new SolidColorBrush(Colors.Black);
            canvas.Children.Add(myLine);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            canvas.Children.Clear();
            this.NavigationService.Navigate(Model.sensorHeight2MainPage);
        }

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

    }

   
}
