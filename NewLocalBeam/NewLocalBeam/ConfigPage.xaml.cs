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
    /// Interaction logic for ConfigPage.xaml
    /// </summary>
    public partial class ConfigPage : Page
    {
        public ConfigPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // 정보입력
            Model.X = Convert.ToDouble(txt_X.Text);
            Model.Y = Convert.ToDouble(txt_Y.Text);
            Model.H = Convert.ToDouble(txt_H.Text);
            Model.D = Convert.ToDouble(txt_D.Text);
            Model.d = Convert.ToDouble(txt_d.Text);
            Model.h = Convert.ToDouble(txt_h.Text);

            // 페이지 이동
            this.NavigationService.Navigate(Model.mainPage);
        }

    }
}
