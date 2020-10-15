using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
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

namespace project1
{
    /// <summary>
    /// Interaction logic for specific_Categories.xaml
    /// </summary>
    public partial class specific_Categories : Window
    {
        public specific_Categories()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, MouseButtonEventArgs e)
        {
            MainWindow mw = new MainWindow();
            specific_Categories sc = new specific_Categories();
            mw.ShowDialog();
            this.Close(); 
        }

        string _prevText = string.Empty;
        private void search_TextChange(object sender, TextChangedEventArgs e)
        {

            foreach (var item in search.Items)
            {
                if (item.ToString().StartsWith(search.Text))
                {
                    _prevText = search.Text;
                    return;
                }
            }
            search.Text = _prevText;
        }
    }
}








