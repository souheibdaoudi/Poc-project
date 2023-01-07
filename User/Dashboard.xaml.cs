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
using System.Windows.Shapes;

namespace User
{
    /// <summary>
    /// Logique d'interaction pour Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
            
        }


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Hum_Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            Iremoting.SurveillanceSysteme obj2 = (Iremoting.SurveillanceSysteme)Activator.GetObject(typeof(Iremoting.SurveillanceSysteme), "tcp://localhost:8085/obj2");
            obj2.HumedityRecord(DateTime.Now);

        }

        private void Temp_Text(object sender, TextChangedEventArgs e)
        {
            Iremoting.SurveillanceSysteme obj2 = (Iremoting.SurveillanceSysteme)Activator.GetObject(typeof(Iremoting.SurveillanceSysteme), "tcp://localhost:8085/obj2");
            obj2.TempuratureRecord(DateTime.Now);
        }
    }
}
