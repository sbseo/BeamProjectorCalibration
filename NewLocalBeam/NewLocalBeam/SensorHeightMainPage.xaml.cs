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
using NewLocalBeam.ModelFolder;

namespace NewLocalBeam
{
    /// <summary>
    /// Interaction logic for SensorHeightMainPage.xaml
    /// </summary>
    public partial class SensorHeightMainPage : Page
    {
        public event EventHandler ModelUpdateHappened;

        public SensorHeightMainPage()
        {
            InitializeComponent();

            this.KeepAlive = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(Model.mainPage);
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {

            /* 정보입력 */
            Model.p1_y = Convert.ToDouble(txtBox_y.Text);
            Model.p2_y = Convert.ToDouble(txtBox_y.Text);
            Model.p3_y = Convert.ToDouble(txtBox_y.Text);
            Model.p1_x = Convert.ToDouble(txt_p1x.Text);
            Model.p2_x = Convert.ToDouble(txt_p2x.Text);
            Model.p3_x = Convert.ToDouble(txt_p3x.Text);

            /* 이벤트 발생 알림 */
            //Fire the event - notifying all subscribers
            if (this.ModelUpdateHappened != null)
                ModelUpdateHappened(this, null);

            /* 페이지 이동 */
            this.NavigationService.Navigate(Model.sensorHeightPage2);
        }
    }
}
