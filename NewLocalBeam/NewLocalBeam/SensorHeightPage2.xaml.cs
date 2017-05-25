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
using System.Windows.Controls.Primitives;
using System.Collections.ObjectModel;

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
        Points points;
        Point sensorLPoint;
        Point sensorRPoint;

        double sensorLx, sensorRx; // 센서 위치그림. 센서 x좌표.

        public SensorHeightPage2()
        {
            InitializeComponent();
            InitializeValue();
            MakePointsBinding();

            // 이벤트 추가
            //Model.sensorHeightMainPage.ModelUpdateHappened += ModelUpdateHandler; // 앞에서 이벤트 발생하면 이거 실행
            this.KeepAlive = true;

        }

        #region   For INPC Implemenation
        /* INotifyPropertyChanged 이걸로 적용함.
        https://www.codeproject.com/Articles/41817/Implementing-INotifyPropertyChanged */

        private ObservableCollection<Points> PointList
        {
            get
            {
                return (ObservableCollection<Points>)
               GetValue(PointListProperty);
            }
            set { SetValue(PointListProperty, value); }
        }

        // Point를 저장하는 Depedency Proeprty. 이거를 통해서 View와 정보를 주고받음. 
        public static readonly DependencyProperty PointListProperty =
DependencyProperty.Register("PointList",
typeof(ObservableCollection<Points>), typeof(SensorHeightPage2),
    new PropertyMetadata(new ObservableCollection<Points>()));


        // 객체가 아니라 간단하게 하나만 Binding할떄는 이 코드 사용!
        public double myPointLx
        {
            get { return (double)GetValue(myPointLxProperty); }
            set { SetValue(myPointLxProperty, value); }
        }

        public static readonly DependencyProperty myPointLxProperty =
    DependencyProperty.Register("myPointLx", typeof(double), typeof(SensorHeightPage2), new PropertyMetadata(null));

        public double myPointRx
        {
            get { return (double)GetValue(myPointRxProperty); }
            set { SetValue(myPointRxProperty, value); }
        }

        public static readonly DependencyProperty myPointRxProperty =
    DependencyProperty.Register("myPointRx", typeof(double), typeof(SensorHeightPage2), new PropertyMetadata(null));

        public double myPointLy
        {
            get { return (double)GetValue(myPointLyProperty); }
            set { SetValue(myPointLyProperty, value); }
        }

        public static readonly DependencyProperty myPointLyProperty =
    DependencyProperty.Register("myPointLy", typeof(double), typeof(SensorHeightPage2), new PropertyMetadata(null));
   
        public double myPointRy
        { 
            get { return (double)GetValue(myPointRyProperty); }
            set { SetValue(myPointRyProperty, value); }
        }

        public static readonly DependencyProperty myPointRyProperty =
    DependencyProperty.Register("myPointRY", typeof(double), typeof(SensorHeightPage2), new PropertyMetadata(null));





        #endregion

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
            MeasureAllAngles("right");
            // DrawArc(sensorLPoint, 50, 90, 0);
            FindSensorPosition("left");
            FindSensorPosition("right");
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

        // http://stackoverflow.com/questions/1481130/wpf-binding-to-local-variable 
        // 바인딩 하는법
        public void MakePointsBinding()
        {
            PointList = new ObservableCollection<Points>();
            points = new Points();
            points.P1_x = 100;
            points.P1_y = 100;
            points.P2_x = 200;
            points.P3_x = 400;

            //  InitializeValue();

            PointList.Add(points);
            points.ModelUpdateHappened += ModelUpdateHandler;
        }

        Line myLine1;
        Line myLine2;
        Line myLine3;
        Label lblP1;
        Label lblP2;
        Label lblP3;
        Label lblSensorLeft;
        Label lblSensorRight;


        public void DrawInfo()
        {
            /* Data Binding은 이걸로 하자 
            http://stackoverflow.com/questions/666856/access-codebehind-variable-in-xaml

            위 출처에서 2번째꼐 잘됨... 
            */

            // 왼쪽센서 - p1 선 긋기
            myLine1 = new Line();
            myLine1.X1 = myPointLx + sensorBorder/2; myLine1.Y1 = myPointLy + sensorBorder/2;
            myLine1.X2 = PointList[0].P1_x + sensorBorder/2 ; myLine1.Y2 = PointList[0].P1_y + sensorBorder/2;
            myLine1.Stroke = new SolidColorBrush(Colors.Black);
            canvas.Children.Add(myLine1);

            // 왼쪽센서 - p2 선 긋기
            myLine2 = new Line();
            myLine2.X1 = myPointLx + sensorBorder / 2; myLine2.Y1 = myPointLy + sensorBorder / 2;
            myLine2.X2 = PointList[0].P2_x + sensorBorder / 2; myLine2.Y2 = PointList[0].P1_y + sensorBorder / 2;
            myLine2.Stroke = new SolidColorBrush(Colors.Black);
            canvas.Children.Add(myLine2);

            // 왼쪽센서 - p3 선 긋기
            myLine3 = new Line();
            myLine3.X1 = myPointLx + sensorBorder / 2; myLine3.Y1 = myPointLy + sensorBorder / 2;
            myLine3.X2 = PointList[0].P3_x + sensorBorder / 2; myLine3.Y2 = PointList[0].P1_y + sensorBorder / 2;
            myLine3.Stroke = new SolidColorBrush(Colors.Black);
            canvas.Children.Add(myLine3);

            // 좌표 위치 표시하기
            lblP1 = new Label();
            Canvas.SetLeft(lblP1, PointList[0].P1_x);
            Canvas.SetTop(lblP1, PointList[0].P1_y + 5);
            lblP1.Content = "P1 (" + PointList[0].P1_x  + ", " + PointList[0].P1_y + ")";
            canvas.Children.Add(lblP1);

            lblP2 = new Label();
            Canvas.SetLeft(lblP2, PointList[0].P2_x);
            Canvas.SetTop(lblP2, PointList[0].P1_y + 5);
            lblP2.Content = "P2 (" + PointList[0].P2_x + ", " + PointList[0].P1_y + ")";
            canvas.Children.Add(lblP2);

            lblP3 = new Label();
            Canvas.SetLeft(lblP3, PointList[0].P3_x);
            Canvas.SetTop(lblP3, PointList[0].P1_y + 5);
            lblP3.Content = "P3 (" + PointList[0].P3_x + ", " + PointList[0].P1_y + ")";
            canvas.Children.Add(lblP3);

            lblSensorLeft = new Label();
            Canvas.SetLeft(lblSensorLeft, myPointLx);
            Canvas.SetTop(lblSensorLeft, myPointLy + 5);
            lblSensorLeft.Content = "SensorLeft (" + myPointLx + ", " + myPointLy + ")";
            lblSensorLeft.Foreground = new SolidColorBrush(Colors.DarkRed);
            canvas.Children.Add(lblSensorLeft);

            lblSensorRight= new Label();
            Canvas.SetLeft(lblSensorRight, myPointRx);
            Canvas.SetTop(lblSensorRight, myPointRy + 5);
            lblSensorRight.Content = "SensorRight (" + myPointRx + ", " + myPointRy + ")";
            lblSensorRight.Foreground = new SolidColorBrush(Colors.DarkRed);
            canvas.Children.Add(lblSensorRight);

            // 각도 표시하기
            //Label lblThetaR = new Label();
            //Canvas.SetLeft(lblThetaR, (sensorLPoint.X + Model.p1_x) * 2 / 3);
            //Canvas.SetTop(lblThetaR, sensorLPoint.Y + 10);
            //lblThetaR.Content = Math.Round(RadianToDegree(leftDevice_thetaR), 3) + "°";
            //lblThetaR.Foreground = new SolidColorBrush(Colors.DarkRed);
            //canvas.Children.Add(lblThetaR);


            //Label lblThetaL = new Label();
            //Canvas.SetLeft(lblThetaL, (sensorLPoint.X + Model.p3_x) * 1 / 2);
            //Canvas.SetTop(lblThetaL, sensorLPoint.Y + 10);
            //lblThetaL.Content = Math.Round(RadianToDegree(leftDevice_thetaL), 3) + "°";
            //lblThetaL.Foreground = new SolidColorBrush(Colors.DarkRed);
            //canvas.Children.Add(lblThetaL);

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

            // Databinding
            myPointLx = sensorLPoint.X;
            myPointRx = sensorRPoint.X;
            myPointLy = sensorLPoint.Y;
            myPointRy = sensorRPoint.Y;

            // 그림판에서 장치들 제거함.
            canvas.Children.Remove(screen);
            canvas.Children.Remove(sensorL);
            canvas.Children.Remove(sensorR);
            canvas.Children.Remove(myLine1);
            canvas.Children.Remove(myLine2);
            canvas.Children.Remove(myLine3);
            canvas.Children.Remove(lblP1);
            canvas.Children.Remove(lblP2);
            canvas.Children.Remove(lblP3);
            canvas.Children.Remove(lblSensorLeft);
            canvas.Children.Remove(lblSensorRight);
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


                // 답 출력
                txt_RDthetaM.Text = Convert.ToString(RadianToDegree(rightDevice_thetaM));
                txt_RDx.Text = Convert.ToString(x + p[1].X); // x값 출력
                txt_RDy.Text = Convert.ToString(y);

                txtB_RFinal.Text = "(" + txt_LDx.Text + " , " + Math.Round(p[0].Y - y, 4) + ")";

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
                txt_LDx.Text = Convert.ToString(x + p[1].X); // x값 출력
                txt_LDy.Text = Convert.ToString(y);

                txtB_LFinal.Text = "(" + txt_LDx.Text + " , " + Math.Round(p[0].Y - y, 4) + ")";

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

        Rectangle screen;

        /* 사각형(빔화면) 그림 */
        public void DrawScreen()
        {
            screen = new Rectangle();
            screen.Width = screenWidth; // Model.X / ratio;
            screen.Height = screenHeight; //  Model.Y / ratio;

            Canvas.SetLeft(screen, 0);
            Canvas.SetTop(screen, deviceHeight + 5); // 센서 지름만큼 추가해야함. 수정필요.

            screen.Stroke = new SolidColorBrush(Colors.Black);
            canvas.Children.Add(screen);
        }

        public static int sensorBorder = 10;

        Ellipse sensorL;
        Ellipse sensorR;

        /* 센서 그림 */
        public void DrawSensor()
        {

            // 왼쪽센서
            sensorL = new Ellipse();
            sensorL.Width = sensorBorder;
            sensorL.Height = sensorBorder;

            Canvas.SetLeft(sensorL, sensorLx);
            Canvas.SetTop(sensorL, 0);

            sensorL.Fill = Brushes.DarkRed;
            canvas.Children.Add(sensorL);

            // 오른쪽센서
            sensorR = new Ellipse();
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
                        p[i].X = PointList[0].P1_x;
                        p[i].Y = PointList[0].P1_y;
                        break;
                    case 1:
                        p[i].X = PointList[0].P2_x;
                        p[i].Y = PointList[0].P1_y;
                        break;
                    case 2:
                        p[i].X = PointList[0].P3_x;
                        p[i].Y = PointList[0].P1_y;
                        break;
                }
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

            // canvas.Children.Clear();
            this.NavigationService.GoBack();

        }

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        // Drang And Drop
        void onDragDelta(object sender, DragDeltaEventArgs e)
        {
            // Canvas.SetLeft(myThumb, Canvas.GetLeft(myThumb) + e.HorizontalChange);
           //  Canvas.SetTop(myThumb, Canvas.GetTop(myThumb) + e.VerticalChange);
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
