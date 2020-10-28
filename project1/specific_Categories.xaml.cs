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
using System.Data.SqlClient;
using project1.TastyDataSetTableAdapters;

namespace project1
{
    /// <summary>
    /// Interaction logic for specific_Categories.xaml
    /// </summary>
    public partial class specific_Categories : Window
    {
        List< Button > buttons;
        Button button;
        StackPanel stackPnl;
        TextBox textBox;

        int? value = 0;
        

        public specific_Categories()
        {
            InitializeComponent();
            
            int categoryCode;

            label1.Content = Data.specificCategoryName;
            Data.query.getCategoryCodeByName(Data.specificCategoryName, ref value);
            categoryCode = value.GetValueOrDefault();
            label2.Content = categoryCode.ToString();
            Data.query.countRecipesByCategory(value.GetValueOrDefault(), ref value);
            int recipesNumber= value.GetValueOrDefault();
            label2.Content = recipesNumber.ToString();

            GridLength length= new GridLength(250);
            RowDefinition row;
            
            int rows, columns;
            int countRows= (recipesNumber % 3 == 0)? (recipesNumber / 3) -1 : recipesNumber / 3;

            Data.sqlCommand.CommandText = "SELECT picture, recipeName FROM Recipes where categoryCode = " + categoryCode;
            Data.adapter.SelectCommand=Data.sqlCommand;
            Data.tastyDataSet.Recipes.Clear();
            Data.adapter.Fill(Data.tastyDataSet.Recipes);
            buttons = new List<Button>();

            for (rows = 0; rows <= countRows; rows++)
            {
                row = new RowDefinition();
                row.Height = length;
                grid.RowDefinitions.Add(row);
                
                for (columns = 0; columns < 3 && recipesNumber>0; columns++)
                {
                    button = new Button();
                    textBox = new TextBox();
                    Image img = new Image();
                    stackPnl = new StackPanel();
                    
                    TastyDataSet.RecipesRow recipe = Data.tastyDataSet.Recipes[recipesNumber - 1];
                    img.Source = new BitmapImage(new Uri(recipe.picture));
                    textBox.Text = recipe.recipeName;
                    img.Stretch = Stretch.Fill;
                    textBox.FontSize = 20;
                    stackPnl.Children.Add(img);
                    button.SizeChanged += new SizeChangedEventHandler(this.button_SizeChanged);
                    button.Click += this.button_Click;
                    button.Margin = new Thickness( 15,20,15,40);
                    button.Content = stackPnl;
                    button.Name = "button" + recipesNumber;
                    button.SetValue(Grid.ColumnProperty, columns);
                    button.SetValue(Grid.RowProperty, rows);
                    textBox.SetValue(Grid.ColumnProperty, columns);
                    textBox.SetValue(Grid.RowProperty, rows);
                    textBox.Margin = new Thickness(15, 200, 15, 0);
                    textBox.TextAlignment = TextAlignment.Center;
                    grid.Children.Add(button);
                    textBox.VerticalAlignment = VerticalAlignment.Bottom;
                    grid.Children.Add(textBox);
                    buttons.Add(button);
                    recipesNumber --;
                }
            }
        }

        private void button_SizeChanged(object sender, System.EventArgs e)
        {
            foreach (Button button in buttons)
            {
                stackPnl.Width = button.Width;
                stackPnl.Height = button.Height;
                textBox.Width = button.Width - 20;
            }
        }
        private void Button_Click(object sender, MouseButtonEventArgs e)
        {
            this.Close(); 
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            //save the recipe name
            Data.specificRecipeName = b.Name;
            specific_Recipe er = new specific_Recipe();
            this.Hide();
            er.ShowDialog();
            this.ShowDialog();
        }

        string duplicate(string str)
        {
            for (int i = 0; i<str.Length ; i++)
            {
                if (str[i] == (char)92)
                    str.Remove(i);
            }
            return str;
        }

    }
}








