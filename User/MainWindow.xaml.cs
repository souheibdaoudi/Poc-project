using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace User
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            Iremoting.Interface obj = (Iremoting.Interface)Activator.GetObject(typeof(Iremoting.Interface), "tcp://localhost:8085/obj");

            if (username.Text.Length == 0 && email.Text.Length == 0)
            {
                messageTextBox.Text = "fill your information";
            }
            else
            if (password.Password.Length == 0)
            {
                errorRectangle.Visibility = Visibility.Collapsed;

                messageTextBox.Text = "Enter password.";
                password.Focus();
            }
            else 
            if (passwordConfirm.Password.Length == 0)
            {
                messageTextBox.Text = "Enter Confirm password.";
                passwordConfirm.Focus();
            }
            if (!Regex.IsMatch(email.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                    messageTextBox.Text = "email invalide";
             }
            else
            if (password.Password != passwordConfirm.Password)
            {
                messageTextBox.Text = "Confirm password must be same as password.";
                passwordConfirm.Focus();
            }
            else
            if (dob1.SelectedDate> DateTime.Now)            {
                messageTextBox.Text = "invalide date of birth";
                passwordConfirm.Focus();
            }
            else
            if (gender.SelectedItem == null)
            {
                messageTextBox.Text = "you have to select you gender";

            }
            else
            if (gender.SelectedItem != null)
            {
                    messageTextBox.Text = "sign up valide !";
                    DateTime? date = dob1.SelectedDate;

                    string dob = date.Value.ToString();
                    string selectedgender = "";
                    ComboBoxItem cbi = (ComboBoxItem)gender.SelectedItem;
                    selectedgender = cbi.Content.ToString();
                    string password1 = password.Password.ToString();
                    obj.signup(username.Text, email.Text, password1, dob, selectedgender);

             }


            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
     
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void gender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Close();

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Login loginwindow = new Login();
            loginwindow.Show();
        }
    }
}
