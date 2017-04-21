using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLocalBeam.ModelFolder
{
    class Model
    {

        public static int windowWidth = 1024;
        public static int windowHeight = 768;

        /* XAML 페이지 관리 */
        public static MainPage mainPage;
        public static SensorHeightMainPage sensorHeightMainPage;
        public static SensorHeightPage2 screenHeightPage2;

        public static SensorHeight2MainPage sensorHeight2MainPage;
        public static SensorHeight2Page2 sensorHeight2Page2;

        public static ConfigPage configPage;
        public static ScreenSizeMainPage screenSizeMainPage;
        public static SensorHeightPage2 sensorHeightPage2;

        public static Points points;

        /* 센서 환경설정 */
        //길이
        public static double X; // 화면가로길이
        public static double Y; // 화면세로길이
        public static double H; // 센서와 화면간의 높이
        public static double h; // 센서와 반사판 사이의 거리
        public static double D; // 센서 중앙과 반사판 끝까지의 거리
        public static double d; // 센서와 센서 사이의 거리

        // 비율
        public static double RATIO_FULLWIDTH;
        public static double RATIO_WIDTH;
        public static double RATIO_FULLHEIGHT;
        public static double RATIO_HEIGHT;

        // 정보 임시 저장
        public static double p1_x;
        public static double p2_x;
        public static double p3_x;

        /* 수평 맞추기2 */
        public static double p1_y;
        public static double p2_y;
        public static double p3_y;

    }
}
