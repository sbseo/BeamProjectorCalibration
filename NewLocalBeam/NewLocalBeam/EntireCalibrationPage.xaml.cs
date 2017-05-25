using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using NewLocalBeam.ModelFolder;
using NewLocalBeam.Algorithm;

namespace NewLocalBeam
{
    /// <summary>
    /// Interaction logic for EntireCalibration.xaml
    /// </summary>
    public partial class EntireCalibrationPage : Page
    {

        public const int ratio = 2; // 비율 설정
        public static double screenWidth;
        public static double screenHeight;
        public static double deviceLength;
        public static double deviceHeight;
        Points points;

        Point sensorLPoint;
        Point sensorRPoint;

        private DeviceAngleInfo angleInfo = new DeviceAngleInfo();
        public static Point[] DevicePosLeft = new Point[9];
        public static Point[] DevicePosRight = new Point[9];

        #region   For INPC Implemenation
        /* INotifyPropertyChanged 이걸로 적용함.
        https://www.codeproject.com/Articles/41817/Implementing-INotifyPropertyChanged */

        private ObservableCollection<Points> PointsList
        {
            get
            {
                return (ObservableCollection<Points>)
               GetValue(PointsListProperty);
            }
            set { SetValue(PointsListProperty, value); }
        }

        // Point를 저장하는 Depedency Proeprty. 이거를 통해서 View와 정보를 주고받음. 
        public static readonly DependencyProperty PointsListProperty =
DependencyProperty.Register("PointsList",
typeof(ObservableCollection<Points>), typeof(EntireCalibrationPage),
    new PropertyMetadata(new ObservableCollection<Points>()));
        #endregion

        public EntireCalibrationPage()
        {
            InitializeComponent();
            InitializeValue();
            MakePointsBinding();
        }

        public void ModelUpdateHandler(object sender, EventArgs e)
        {
            InitializeValue();
            DrawScreen();
            LocatePoints();
            InsertArray();
        }

        public void InsertArray()
        {
            SensorPosAlgo1 algo1 = new SensorPosAlgo1();
            SensorPosAlgo2 algo2 = new SensorPosAlgo2();
            SensorPosAlgo3 algo3 = new SensorPosAlgo3();

            angleInfo.MeasureAllAngles(sensorRPoint, sensorLPoint, p[0], p[1], p[2]);
            algo3.FindSensorPosition("left", angleInfo);
            DevicePosLeft[0] = algo3.AnswerPoint;
            algo3.FindSensorPosition("right", angleInfo);
            DevicePosRight[0] = algo3.AnswerPoint;

            angleInfo.MeasureAllAngles(sensorRPoint, sensorLPoint, p[1], p[2], p[3]);
            algo2.FindSensorPosition("left", angleInfo);
            DevicePosLeft[1] = algo2.AnswerPoint;
            algo2.FindSensorPosition("right", angleInfo);
            DevicePosRight[1] = algo2.AnswerPoint;

            angleInfo.MeasureAllAngles(sensorRPoint, sensorLPoint, p[2], p[3], p[4]);
            algo1.FindSensorPosition("left", angleInfo);
            DevicePosLeft[2] = algo1.AnswerPoint;
            algo1.FindSensorPosition("right", angleInfo);
            DevicePosRight[2] = algo1.AnswerPoint;

            angleInfo.MeasureAllAngles(sensorRPoint, sensorLPoint, p[3], p[4], p[5]);
            algo1.FindSensorPosition("left", angleInfo);
            DevicePosLeft[3] = algo1.AnswerPoint;
            algo1.FindSensorPosition("right", angleInfo);
            DevicePosRight[3] = algo1.AnswerPoint;

            angleInfo.MeasureAllAngles(sensorRPoint, sensorLPoint, p[4], p[5], p[6]);
            algo1.FindSensorPosition("left", angleInfo);
            DevicePosLeft[4] = algo1.AnswerPoint;
            algo1.FindSensorPosition("right", angleInfo);
            DevicePosRight[4] = algo1.AnswerPoint;

            angleInfo.MeasureAllAngles(sensorRPoint, sensorLPoint, p[5], p[6], p[7]);
            algo2.FindSensorPosition("left", angleInfo);
            DevicePosLeft[5] = algo2.AnswerPoint;
            algo2.FindSensorPosition("right", angleInfo);
            DevicePosRight[5] = algo2.AnswerPoint;

            angleInfo.MeasureAllAngles(sensorRPoint, sensorLPoint, p[6], p[7], p[8]);
            algo3.FindSensorPosition("left", angleInfo);
            DevicePosLeft[6] = algo3.AnswerPoint;
            algo3.FindSensorPosition("right", angleInfo);
            DevicePosLeft[6] = algo3.AnswerPoint;
        }

        public void MakePointsBinding()
        {
            PointsList = new ObservableCollection<Points>();
            points = new Points();
            points.ModelUpdateHappened += ModelUpdateHandler;
            points.P1_x = screenWidth * 1 / 6;
            points.P1_y = screenHeight * 1 / 4;
            points.P2_x = screenWidth * 1 / 6;
            points.P2_y = screenHeight * 2 / 4;
            points.P3_x = screenWidth * 1 / 6;
            points.P3_y = screenHeight * 3 / 4;
            points.P4_x = screenWidth * 2 / 6;
            points.P4_y = screenHeight * 3 / 4;
            points.P5_x = screenWidth * 3 / 6;
            points.P5_y = screenHeight * 3 / 4;
            points.P6_x = screenWidth * 4 / 6;
            points.P6_y = screenHeight * 3 / 4;
            points.P7_x = screenWidth * 5 / 6;
            points.P7_y = screenHeight * 3 / 4;
            points.P8_x = screenWidth * 5 / 6;
            points.P8_y = screenHeight * 2 / 4;
            points.P9_x = screenWidth * 5 / 6;
            points.P9_y = screenHeight * 1 / 4;
            PointsList.Add(points);
        }


        /* 변수 초기화. 센서환경설정을 그림그리기 적합하게 설정함 */
        public void InitializeValue()
        {
            // 여기를 전체 화면 크기로 바꾸어주면 되는듯
            screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            deviceLength = Model.d;
            deviceHeight = Model.H;

            // 숫자가 지금 안맞음. 센서 위치.
            sensorLPoint = new Point();
            sensorRPoint = new Point();

            sensorLPoint.X = ((screenWidth + deviceLength) / 2); sensorLPoint.Y = 0;
            sensorRPoint.X = ((screenWidth - deviceLength) / 2); sensorRPoint.Y = 0;
        }

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

        /* p1, p2, p3 좌표 그림 */
        public static Point[] p;
        public void LocatePoints()
        {
            // 좌표를 생성하고 정보를 입력함
            // p[0] = p1 , p[1] = p2, p[2] = p3
            p = new Point[9];
            for (int i = 0; i < 8; i++)
            {
                switch (i)
                {
                    case 0:
                        p[i].X = PointsList[0].P1_x;
                        p[i].Y = PointsList[0].P1_y;
                        break;
                    case 1:
                        p[i].X = PointsList[0].P2_x;
                        p[i].Y = PointsList[0].P2_y;
                        break;
                    case 2:
                        p[i].X = PointsList[0].P3_x;
                        p[i].Y = PointsList[0].P3_y;
                        break;
                    case 3:
                        p[i].X = PointsList[0].P4_x;
                        p[i].Y = PointsList[0].P4_y;
                        break;
                    case 4:
                        p[i].X = PointsList[0].P5_x;
                        p[i].Y = PointsList[0].P5_y;
                        break;
                    case 5:
                        p[i].X = PointsList[0].P6_x;
                        p[i].Y = PointsList[0].P6_y;
                        break;
                    case 6:
                        p[i].X = PointsList[0].P7_x;
                        p[i].Y = PointsList[0].P7_y;
                        break;
                    case 7:
                        p[i].X = PointsList[0].P8_x;
                        p[i].Y = PointsList[0].P8_y;
                        break;
                    case 8:
                        p[i].X = PointsList[0].P9_x;
                        p[i].Y = PointsList[0].P9_y;
                        break;
                }
            }
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
