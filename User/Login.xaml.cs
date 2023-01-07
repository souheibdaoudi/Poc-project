using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string password1 = password.Password.ToString();
            if (!Regex.IsMatch(email.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errorMessage.Text = "email invalide";
            }
            else if (password.Password.Length == 0)
            {

                errorMessage.Text = "Enter password.";
                password.Focus();
            }
            else {
                Iremoting.Interface obj = (Iremoting.Interface)Activator.GetObject(typeof(Iremoting.Interface), "tcp://localhost:8085/obj");
                obj.login(email.Text, password1);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
