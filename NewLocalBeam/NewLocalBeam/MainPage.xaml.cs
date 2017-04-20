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

namespace NewLocalBeam
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            Model.configPage = new ConfigPage();
            Model.sensorHeightMainPage = new SensorHeightMainPage();
            Model.sensorHeight2MainPage = new SensorHeight2MainPage();

            Model.screenSizeMainPage = new ScreenSizeMainPage();
            Model.sensorHeightPage2 = new SensorHeightPage2();
            Model.sensorHeight2Page2 = new SensorHeight2Page2();

        }

        private void Main_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(Model.sensorHeightMainPage);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(Model.configPage);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            this.NavigationService.Navigate(Model.screenSizeMainPage);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(Model.sensorHeight2MainPage);
        }
    }
}
