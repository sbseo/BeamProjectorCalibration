using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using NewLocalBeam.ModelFolder;

namespace NewLocalBeam
{

    // C# Tutorial - Circle Progress Bar | FoxLearn 이것도 구현합시다!!! 
    // https://www.youtube.com/watch?v=vegYbrNz1V8
    /// <summary>
    /// Interaction logic for SensorHeightPage2.xaml
    /// </summary>
    public partial class SensorHeightPage2 : Page
    {

        public const int ratio = 2; // 비율 설정
        public static double screenWidth;
        public static double screenHeight;
        public static double deviceLength;
        public static double deviceHeight;

        Point sensorLPoint;
        Point sensorRPoint;

        double sensorLx, sensorRx; // 센서 위치그림. 센서 x좌표.

//        // XAML에 DataBinding하는데 사용함
//        public static DependencyProperty MyProperty2Property =
//DependencyProperty.Register("MyProperty2", typeof(string), typeof(SensorHeightPage2), new UIPropertyMetadata(string.Empty));

//        public static DependencyProperty MyProperty1Property =
//            DependencyProperty.Register("MyProperty1", typeof(string), typeof(SensorHeightPage2), new UIPropertyMetadata(string.Empty));

//        public static DependencyProperty p1_xProperty =
//    DependencyProperty.Register("p1_x", typeof(string), typeof(SensorHeightPage2), new UIPropertyMetadata(string.Empty));

//        public static DependencyProperty p1_yProperty =
//DependencyProperty.Register("p1_y", typeof(string), typeof(SensorHeightPage2), new UIPropertyMetadata(string.Empty));

//        public static DependencyProperty p2_xProperty =
//DependencyProperty.Register("p2_x", typeof(string), typeof(SensorHeightPage2), new UIPropertyMetadata(string.Empty));

//        public static DependencyProperty p2_yProperty =
//DependencyProperty.Register("p2_y", typeof(string), typeof(SensorHeightPage2), new UIPropertyMetadata(string.Empty));

//        public static DependencyProperty p3_xProperty =
//DependencyProperty.Register("p3_x", typeof(string), typeof(SensorHeightPage2), new UIPropertyMetadata(string.Empty));

//        public static DependencyProperty p3_yProperty =
//DependencyProperty.Register("p3_y", typeof(string), typeof(SensorHeightPage2), new UIPropertyMetadata(string.Empty));

//        public static DependencyProperty p1_stringProperty =
//DependencyProperty.Register("p1_string", typeof(string), typeof(SensorHeightPage2), new UIPropertyMetadata(string.Empty));

//        public static DependencyProperty p2_stringProperty =
//DependencyProperty.Register("p2_string", typeof(string), typeof(SensorHeightPage2), new UIPropertyMetadata(string.Empty));

//        public static DependencyProperty p3_stringProperty =
//DependencyProperty.Register("p3_string", typeof(string), typeof(SensorHeightPage2), new UIPropertyMetadata(string.Empty));



        public SensorHeightPage2()
        {
            InitializeComponent();

            // 이벤트 추가
            Model.sensorHeightMainPage.ModelUpdateHappened += ModelUpdateHandler; // 앞에서 이벤트 발생하면 이거 실행
            this.KeepAlive = true;

        }




        // 이벤트 발생시 실행
        public void ModelUpdateHandler(object sender, EventArgs e)
        {
            //Do something here
            // DrawLine();
            // this.KeepAlive = true;

            InitializeValue();
            DrawScreen();
            DrawSensor();
            LocatePoints();
            MeasureAllAngles("left");
       // DrawArc(sensorLPoint, 50, 90, 0);
            FindSensorPosition("left");
         //   DrawArc(sensorLPoint, 50, 0, 45);
          //  DrawArc(sensorLPoint, 30, 45, 60);

            


            
            /* 객체로 저장함 */
            Points temp = new Points();
            temp.P1_x = Model.p1_x;
            temp.P2_x = Model.p2_x;
            temp.P3_x = Model.p3_x;
            temp.P1_y = Model.p1_y;
            temp.P2_y = Model.p2_y;
            temp.P3_y = Model.p3_y;
 
            Model.points = temp;

            DrawInfo();
  


            //  MeasureAllAngles("right");
            //  FindSensorPosition("right");
        }

        public void DrawInfo()
        {
            /* Data Binding은 이걸로 하자 
            http://stackoverflow.com/questions/666856/access-codebehind-variable-in-xaml

            위 출처에서 2번째꼐 잘됨... 
            */

            // 왼쪽센서 - p1 선 긋기
            Line myLine = new Line();
            myLine.X1 = sensorLPoint.X + sensorBorder/2; myLine.Y1 = sensorLPoint.Y + sensorBorder/2;
            myLine.X2 = Model.p1_x + sensorBorder/2 ; myLine.Y2 = Model.p1_y + sensorBorder/2;
            myLine.Stroke = new SolidColorBrush(Colors.Black);
            canvas.Children.Add(myLine);

            // 왼쪽센서 - p2 선 긋기
            myLine = new Line();
            myLine.X1 = sensorLPoint.X + sensorBorder / 2; myLine.Y1 = sensorLPoint.Y + sensorBorder / 2;
            myLine.X2 = Model.p2_x + sensorBorder / 2; myLine.Y2 = Model.p2_y + sensorBorder / 2;
            myLine.Stroke = new SolidColorBrush(Colors.Black);
            canvas.Children.Add(myLine);

            // 왼쪽센서 - p3 선 긋기
            myLine = new Line();
            myLine.X1 = sensorLPoint.X + sensorBorder / 2; myLine.Y1 = sensorLPoint.Y + sensorBorder / 2;
            myLine.X2 = Model.p3_x + sensorBorder / 2; myLine.Y2 = Model.p3_y + sensorBorder / 2;
            myLine.Stroke = new SolidColorBrush(Colors.Black);
            canvas.Children.Add(myLine);


            // 좌표 위치 표시하기
            Label lblP1 = new Label();
            Canvas.SetLeft(lblP1, Model.p1_x);
            Canvas.SetTop(lblP1, Model.p1_y + 5);
            lblP1.Content = "(" + Model.p1_x + ", " + Model.p1_y + ")";
            canvas.Children.Add(lblP1);

            Label lblP2 = new Label();
            Canvas.SetLeft(lblP2, Model.p2_x);
            Canvas.SetTop(lblP2, Model.p2_y + 5);
            lblP2.Content = "(" + Model.p2_x + ", " + Model.p2_y + ")";
            canvas.Children.Add(lblP2);

            Label lblP3 = new Label();
            Canvas.SetLeft(lblP3, Model.p3_x);
            Canvas.SetTop(lblP3, Model.p3_y + 5);
            lblP3.Content = "(" + Model.p3_x + ", " + Model.p3_y + ")";
            canvas.Children.Add(lblP3);

            Label lblSensorLeft = new Label();
            Canvas.SetLeft(lblSensorLeft, sensorLPoint.X + 20);
            Canvas.SetTop(lblSensorLeft, sensorLPoint.Y);
            lblSensorLeft.Content = "(" + sensorLPoint.X+ ", " + sensorLPoint.Y + ")";
            lblSensorLeft.Foreground = new SolidColorBrush(Colors.DarkRed);
            canvas.Children.Add(lblSensorLeft);

            // 각도 표시하기
            Label lblThetaR = new Label();
            Canvas.SetLeft(lblThetaR, (sensorLPoint.X + Model.p1_x) * 2 / 3);
            Canvas.SetTop(lblThetaR, sensorLPoint.Y + 10);
            lblThetaR.Content = Math.Round(RadianToDegree(leftDevice_thetaR), 3) + "°";
            lblThetaR.Foreground = new SolidColorBrush(Colors.DarkRed);
            canvas.Children.Add(lblThetaR);


            Label lblThetaL = new Label();
            Canvas.SetLeft(lblThetaL, (sensorLPoint.X + Model.p3_x) * 1 / 2);
            Canvas.SetTop(lblThetaL, sensorLPoint.Y + 10);
            lblThetaL.Content = Math.Round(RadianToDegree(leftDevice_thetaL), 3) + "°";
            lblThetaL.Foreground = new SolidColorBrush(Colors.DarkRed);
            canvas.Children.Add(lblThetaL);


            // Set MyProperty1 and 2
            //this.MyProperty1 = "Hello Hello Hello";
            //this.MyProperty2 = "World";

            //this.p1_x = Convert.ToString(Model.p1_x);
            //this.p2_x = Convert.ToString(Model.p2_x);
            //this.p3_x = Convert.ToString(Model.p3_x);
            //this.p1_y = Convert.ToString(Model.p1_y + 5);
            //this.p2_y = Convert.ToString(Model.p2_y + 5);
            //this.p3_y = Convert.ToString(Model.p3_y + 5);
            //this.p1_string = "(" + Model.p1_x + " " + Model.p1_y + ")";
            //this.p2_string = "(" + Model.p2_x + " " + Model.p2_y + ")";
            //this.p3_string = "(" + Model.p3_x + " " + Model.p3_y + ")";

        }


        //public string MyProperty1
        //{
        //    get { return (string)GetValue(MyProperty1Property); }
        //    set { SetCurrentValue(MyProperty1Property, value); }
        //}

        //public string MyProperty2
        //{
        //    get { return (string)GetValue(MyProperty2Property); }
        //    set { SetCurrentValue(MyProperty2Property, value); }
        //}

        //public string p1_x
        //{
        //    get { return (string)GetValue(p1_xProperty); }
        //    set { SetCurrentValue(p1_xProperty, value); }
        //}

        //public string p1_y
        //{
        //    get { return (string)GetValue(p1_yProperty); }
        //    set { SetCurrentValue(p1_yProperty, value); }
        //}

        //public string p2_x
        //{
        //    get { return (string)GetValue(p2_xProperty); }
        //    set { SetCurrentValue(p2_xProperty, value); }
        //}

        //public string p2_y
        //{
        //    get { return (string)GetValue(p2_yProperty); }
        //    set { SetCurrentValue(p2_yProperty, value); }
        //}

        //public string p3_x
        //{
        //    get { return (string)GetValue(p3_xProperty); }
        //    set { SetCurrentValue(p3_xProperty, value); }
        //}

        //public string p3_y
        //{
        //    get { return (string)GetValue(p3_yProperty); }
        //    set { SetCurrentValue(p3_yProperty, value); }
        //}

        //public string p1_string
        //{
        //    get { return (string)GetValue(p1_stringProperty); }
        //    set { SetCurrentValue(p1_stringProperty, value); }
        //}

        //public string p2_string
        //{
        //    get { return (string)GetValue(p2_stringProperty); }
        //    set { SetCurrentValue(p2_stringProperty, value); }
        //}

        //public string p3_string
        //{
        //    get { return (string)GetValue(p3_stringProperty); }
        //    set { SetCurrentValue(p3_stringProperty, value); }
        //}


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

        public void DrawArc(Point center, double radius, double start_angle, double end_angle)
        {
            Path arc_path = new Path();
            arc_path.Stroke = Brushes.DarkRed;
            arc_path.StrokeThickness = 3;
            Canvas.SetLeft(arc_path, 0);
            Canvas.SetTop(arc_path, 0);

            start_angle = ((start_angle % (Math.PI * 2)) + Math.PI * 2) % (Math.PI * 2);
            end_angle = ((end_angle % (Math.PI * 2)) + Math.PI * 2) % (Math.PI * 2);
            if (end_angle < start_angle)
            {
                double temp_angle = end_angle;
                end_angle = start_angle;
                start_angle = temp_angle;
            }
            double angle_diff = end_angle - start_angle;
            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure();
            ArcSegment arcSegment = new ArcSegment();
            arcSegment.IsLargeArc = angle_diff >= Math.PI;
            //Set start of arc
            pathFigure.StartPoint = new Point(center.X + radius * Math.Cos(start_angle), center.Y + radius * Math.Sin(start_angle));
            //set end point of arc.
            arcSegment.Point = new Point(center.X + radius * Math.Cos(end_angle), center.Y + radius * Math.Sin(end_angle));
            arcSegment.Size = new Size(radius, radius);
            arcSegment.SweepDirection = SweepDirection.Clockwise;

            pathFigure.Segments.Add(arcSegment);
            pathGeometry.Figures.Add(pathFigure);
            arc_path.Data = pathGeometry;
            canvas.Children.Add(arc_path);
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
        public double rightDevice_TanThetaM;

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

        public async void FindSensorPosition(string direction)
        {

            if (direction == "right")
            {
                //계산시작(기본단위는 Radian으로 합시다)
                rightDevice_thetaR = rightDevice_thetaR2 - rightDevice_thetaR1;
                rightDevice_thetaL = rightDevice_thetaR3 - rightDevice_thetaR2;

                // a, b 변수 설정
                double a = p[1].X - p[0].X; double b = p[2].X - p[1].X;

                // 공식대입
                rightDevice_thetaM = Math.Atan2((a * Math.Tan(rightDevice_thetaL) - b * Math.Tan(rightDevice_thetaR)), ((a + b) * Math.Tan(rightDevice_thetaR) * Math.Tan(rightDevice_thetaL)));

                double y = a / (Math.Tan(rightDevice_thetaR + rightDevice_thetaM) - Math.Tan(rightDevice_thetaM));
                double x = y * Math.Tan(rightDevice_thetaM);

                // x이용해서 센서의 x좌표 찾기. p2위치와 센서 위치에 따라 양수 음수 구별.
                // x = (p[1].X > x) ? p[1].X + x : p[1].X - x;   

                for (int i = -90; i <= 90; i += 10)
                {
                    double rad = DegreeToRadian(i);
                    Console.WriteLine(Math.Tan(rad));
                }


                // 답 출력
                txt_RDthetaM.Text = Convert.ToString(RadianToDegree(rightDevice_thetaM));
                txt_RDx.Text = Convert.ToString(x); // x값 출력
                txt_RDy.Text = Convert.ToString(y);

            }
            else if (direction == "left")
            {

                //계산시작(기본단위는 Radian으로 합시다)
                leftDevice_thetaR = leftDevice_thetaR2 - leftDevice_thetaR1;
                leftDevice_thetaL = leftDevice_thetaR3 - leftDevice_thetaR2;

                // a, b 변수 설정
                double a = p[1].X - p[0].X; double b = p[2].X - p[1].X;

                // 공식대입
                leftDevice_thetaM = Math.Atan2((a * Math.Tan(leftDevice_thetaL) - b * Math.Tan(leftDevice_thetaR)), ((a + b) * Math.Tan(leftDevice_thetaR) * Math.Tan(leftDevice_thetaL)));

                double y = a / (Math.Tan(leftDevice_thetaR + leftDevice_thetaM) - Math.Tan(leftDevice_thetaM));
                double x = y * Math.Tan(leftDevice_thetaM);


                // 답 출력
                txt_LDthetaM.Text = Convert.ToString(RadianToDegree(leftDevice_thetaM));
                txt_LDx.Text = Convert.ToString(x); // x값 출력
                txt_LDy.Text = Convert.ToString(y);


                // leftDevice_thetaR + leftDevice_thetaM < Math.PI / 2 && leftDevice_thetaR + leftDevice_thetaM > 0
                if (leftDevice_thetaR1 + leftDevice_thetaM + leftDevice_thetaR < Math.PI / 2 + 0.01 && leftDevice_thetaR1 + leftDevice_thetaM + leftDevice_thetaR > Math.PI / 2 - 0.01)
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

        public static int sensorBorder = 10;

        /* 센서 그림 */
        public void DrawSensor()
        {

            // 왼쪽센서
            Ellipse sensorL = new Ellipse();
            sensorL.Width = sensorBorder;
            sensorL.Height = sensorBorder;

            Canvas.SetLeft(sensorL, sensorLx);
            Canvas.SetTop(sensorL, 0);

            sensorL.Fill = Brushes.DarkRed;
            canvas.Children.Add(sensorL);

            // 오른쪽센서
            Ellipse sensorR = new Ellipse();
            sensorR.Width = sensorBorder;
            sensorR.Height = sensorBorder;
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
                ep.SetCurrentValue(Canvas.LeftProperty, p[i].X);
                ep.SetCurrentValue(Canvas.TopProperty, p[i].Y);
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
            this.NavigationService.Navigate(Model.sensorHeightMainPage);
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


// 안됨..
public static class ExtensionMethods
{
    private static Action EmptyDelegate = delegate () { };

    public static void Refresh(this UIElement uiElement)
    {
        uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
    }
}