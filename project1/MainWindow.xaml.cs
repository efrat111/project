using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;


namespace project1
{

    public partial class MainWindow : Window
    {
        List<string> recipesList;
        public MainWindow()
        {
            InitializeComponent();
            recipesList =new List<string>();
            Data.sqlCommand.CommandText = "SELECT recipeName FROM Recipes order by recipeName";
            Data.con.Open();
            string name = "";
            SqlDataReader reader = Data.sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                name = reader.GetString(0);
                recipesList.Add(name);
            }
            Data.con.Close();

            textBox1.TextChanged += new TextChangedEventHandler(textBox1_TextChanged);


        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox1.Text.Length==0)
            {
                lbSuggestion.Visibility = Visibility.Collapsed;
                lbSuggestion.ItemsSource = null;
                return;
            }

            string typedString = textBox1.Text;
            List<string> autoList = new List<string>();
            autoList.Clear();

            foreach (string item in recipesList)
            {
                if (item.StartsWith(typedString))
                    autoList.Add(item);
            }

            if (autoList.Count>0)
            {
                lbSuggestion.ItemsSource = autoList;
                lbSuggestion.Visibility = Visibility.Visible;
            }

            else
            {
                lbSuggestion.Visibility = Visibility.Collapsed;
                lbSuggestion.ItemsSource = null;
            }
        }
        
        private void lbSuggestion_SelectionChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (lbSuggestion.ItemsSource != null)
            {
                lbSuggestion.Visibility = Visibility.Collapsed;
                textBox1.TextChanged -= new TextChangedEventHandler(textBox1_TextChanged);
                if (lbSuggestion.SelectedIndex != -1)
                    textBox1.Text = lbSuggestion.SelectedItem.ToString();
                textBox1.TextChanged += new TextChangedEventHandler(textBox1_TextChanged);
            }
        }


        void window(string str)
        {
            //פתיחת חלון חדש ע"י לחיצה על כפתור
       
            this.Hide();
            Data.specificCategoryName = str;
            specific_Categories sc = new specific_Categories();
            sc.ShowDialog();
            this.ShowDialog();
        }

 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            enter_password ep = new enter_password();
            this.Hide();
            ep.ShowDialog();
            this.Show();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            window("דגים");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            window("סלטים");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            window("משקאות");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            window("בשרים");
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            window("ממרחים ומטבלים");
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            window("ללא גלוטן");
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            window("עופות");
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            window("לחם ומאפים");
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            window("צמחוניים");
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            window("פסטות");
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            window("עוגות ועוגיות");
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            window("טבעוניים");
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            window("פשטידות");
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            window("מרקים");
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            window("סוכרתיים");
        }
    }
}
