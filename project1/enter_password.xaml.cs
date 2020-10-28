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

namespace project1
{
    /// <summary>
    /// Interaction logic for enter_password.xaml
    /// </summary>
    /// 
    public partial class enter_password : Window
    {
        public enter_password()
        {
            InitializeComponent();
        }

        private void exist_user_Click(object sender, RoutedEventArgs e)
        {
            lableNewUser.Visibility = Visibility.Collapsed;//הסתרת כפתור
            lableNewPassword.Visibility = Visibility.Collapsed;
            lableMail.Visibility = Visibility.Collapsed;
            TBMail.Visibility = Visibility.Collapsed;
            TBNewPassword.Visibility = Visibility.Collapsed;
            TBNewUser.Visibility = Visibility.Collapsed;
            EnterDetails.Visibility = Visibility.Collapsed;

            combobox1.Visibility = Visibility.Visible;
            password.Visibility = Visibility.Visible;
            lableExistUser.Visibility = Visibility.Visible;
            lableExitPassword.Visibility = Visibility.Visible;
            ButtonPassword.Visibility = Visibility.Visible;


        }

        private void new_user_Click(object sender, RoutedEventArgs e)
        {
            lableExistUser.Visibility = Visibility.Collapsed;//הסתרת ספתור
            lableExitPassword.Visibility = Visibility.Collapsed;
            password.Visibility = Visibility.Collapsed;
            combobox1.Visibility = Visibility.Collapsed;
            exist_user.Visibility = Visibility.Collapsed;
            ButtonPassword.Visibility = Visibility.Collapsed;

            lableNewUser.Visibility = Visibility.Visible;//הצגת כפתור שהוסתר
            lableNewPassword.Visibility = Visibility.Visible;
            lableMail.Visibility = Visibility.Visible;
            TBMail.Visibility = Visibility.Visible;
            TBNewPassword.Visibility = Visibility.Visible;
            TBNewUser.Visibility = Visibility.Visible;
            EnterDetails.Visibility = Visibility.Visible;
            haveUser.Visibility = Visibility.Visible;

            if(new_user.Visibility==Visibility.Visible)
            {
                new_user.Visibility = Visibility.Collapsed;
            }
        }

        private void have_user_Click(object sender, RoutedEventArgs e)
        {
            lableNewUser.Visibility = Visibility.Collapsed;//הסתרת כפתור
            lableNewPassword.Visibility = Visibility.Collapsed;
            lableMail.Visibility = Visibility.Collapsed;
            TBMail.Visibility = Visibility.Collapsed;
            TBNewPassword.Visibility = Visibility.Collapsed;
            TBNewUser.Visibility = Visibility.Collapsed;
            EnterDetails.Visibility = Visibility.Collapsed;
            ButtonPassword.Visibility = Visibility.Collapsed;
            haveUser.Visibility = Visibility.Collapsed;

            combobox1.Visibility = Visibility.Visible;
            password.Visibility = Visibility.Visible;
            lableExistUser.Visibility = Visibility.Visible;
            lableExitPassword.Visibility = Visibility.Visible;
            ButtonPassword.Visibility = Visibility.Visible;
            exist_user.Visibility = Visibility.Visible;
            new_user.Visibility = Visibility.Visible;





        }





    }
}
