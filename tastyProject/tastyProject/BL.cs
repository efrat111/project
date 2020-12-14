using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace tastyProject
{
    public static class BL
    {

        public static DataTable recipes = new DataTable();
        public static Window window;
        static List<SqlParameter> param = new List<SqlParameter>();
        static SqlParameter p;


        public static int countTable(string tableName)
        {
            param.Clear();
            p = new SqlParameter("@tableName", SqlDbType.NChar);
            p.Direction = ParameterDirection.Input;
            p.Value = tableName;
            param.Add(p);

            return (int)Dal.Scalar("SP_countTableRows", param);
        }


        public static int likedRecipe(string sp, int recipeCode)
        {
            param.Clear();
            p = new SqlParameter("@recipeCode", SqlDbType.Int);
            p.Direction = ParameterDirection.Input;
            p.Value = recipeCode;
            param.Add(p);
            p = new SqlParameter("@userID", SqlDbType.NChar);
            p.Direction = ParameterDirection.Input;
            p.Value = Data.userId;
            param.Add(p);
            if (string.Compare(sp, "SP_isRecipeLiked")==0)
                return (int)Dal.Scalar(sp, param);
            
            Dal.ExecuteNonQuery(sp, param);
            return -1;
        }

        public static DataTable getRecipesByUserId(string sp, string tableName)
        {
            param.Clear();
            p = new SqlParameter("@tableName", SqlDbType.NChar);
            p.Direction = ParameterDirection.Input;
            p.Value = tableName;
            param.Add(p);
            p = new SqlParameter("@userID", SqlDbType.NChar);
            p.Direction = ParameterDirection.Input;
            p.Value = Data.userId;
            param.Add(p);

            return Dal.getTable(sp, param);
        }

        public static void getRecipesNames(List<string> recipesNames)
        {
            param.Clear();
            p = new SqlParameter("@tableName", SqlDbType.NChar);
            p.Value = "Recipes";
            p.Direction = ParameterDirection.Input;
            param.Add(p);
            DataTable dt = Dal.getTable("SP_getTable", param);
            if (dt == null)
                return;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                recipesNames.Add(dt.Rows[i]["recipeName"].ToString());
            }
        }

        public static DataTable parameterForTable(string sp, int code, string parameterName)
        {
            param.Clear();
            SqlParameter p = new SqlParameter(parameterName, DbType.Int32);
            p.Direction = ParameterDirection.Input;
            p.Value = code;
            param.Add(p);
            return Dal.getTable(sp, param);
        }

        public static DataTable parameterForTable(string sp, string name, string parameterName)
        {
            param.Clear();
            SqlParameter p = new SqlParameter(parameterName, DbType.String);
            p.Direction = ParameterDirection.Input;
            p.Value = name;
            param.Add(p);
            return Dal.getTable(sp, param);
        }

        public static void openCategory(Window thisWindow, string buttonContent)
        {
            thisWindow.Hide();
            Data.specificCategoryName = buttonContent;
            specific_Categories sc = new specific_Categories();
            sc.ShowDialog();
        }

        public static int getCode(string sp, string categoryName)
        {
            List<SqlParameter> listParam = new List<SqlParameter>();
            SqlParameter p = new SqlParameter("@name", categoryName);
            p.DbType = DbType.String;
            p.Direction = ParameterDirection.Input;
            listParam.Add(p);
            return (int)Dal.Scalar(sp, listParam);
        }

        public static void recipesForWindow(Label label, Grid grid, TextBox textBox, Window thisWindow)
        {
            int recipesNumber = recipes.Rows.Count;
            label.Content = recipesNumber.ToString();
            window = thisWindow;

            StackPanel stackPnl;
            List<Button> buttons = new List<Button>();
            Button button;
            int rows, columns;
            int countRows = (recipesNumber % 3 == 0) ? (recipesNumber / 3) - 1 : recipesNumber / 3;
            GridLength length = new GridLength(250);
            RowDefinition row;

            for (rows = 0; rows <= countRows; rows++)
            {
                row = new RowDefinition();
                row.Height = length;
                grid.RowDefinitions.Add(row);

                for (columns = 0; columns < 3 && recipesNumber > 0; columns++)
                {
                    button = new Button();
                    textBox = new TextBox();
                    Image img = new Image();
                    stackPnl = new StackPanel();

                    DataRow recipe = recipes.Rows[recipesNumber - 1];
                    img.Source = new BitmapImage(new Uri(recipe["picture"].ToString()));
                    textBox.Text = recipe["recipeName"].ToString();
                    img.Stretch = Stretch.Fill;
                    textBox.FontSize = 20;
                    stackPnl.Children.Add(img);
                    button.Click += button_Click;
                    button.Margin = new Thickness(15, 20, 15, 40);
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
                    recipesNumber--;
                }
            }
        }

        public static void button_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            //save the recipe name
            Data.specificRecipeName = b.Name;
            specific_Recipe sr = new specific_Recipe();
            window.Hide();
            sr.ShowDialog();
            window.ShowDialog();
        }

        public static int specificRecipe(Image image, TextBox textBox1, TextBox textBox2)
        {
            DataRow myRecipe;
            int recipeRowNumber;

            if (Data.specificRecipeName.StartsWith("button"))
            {
                // i explanation //
                //example to understand the i ...... name: "button3" -> "3" -> String To Ascii ->  3=51 -> minus the ascii -> 51-48=3 -> minus 1 for arr of the table -> 3-1 -> 2
                recipeRowNumber = Data.specificRecipeName[Data.specificRecipeName.Length - 1] - 49;
                myRecipe = recipes.Rows[recipeRowNumber];
            }
            else
            {
                param.Clear();
                p = new SqlParameter("@tableName", SqlDbType.NChar);
                p.Value = "Recipes";
                p.Direction = ParameterDirection.Input;
                param.Add(p);
                recipes = Dal.getTable("SP_getTable", param);
                // i explanation //
                // i is code -> minus 1000 (the seed code) -<minus 1 for arr (starts from 0) //
                recipeRowNumber = (getCode("getRecipeCodeByName", Data.specificRecipeName)) - 1001; 
                myRecipe = recipes.Rows[recipeRowNumber]; 
            }

            textBox1.Text = Data.specificCategoryName + "\r" + myRecipe["recipeName"].ToString();
            image.Source = new BitmapImage(new Uri(myRecipe["picture"].ToString()));
            textBox2.Text = "";
            DataTable ingredients = parameterForTable("SP_getIngredients", (int)myRecipe["recipeCode"], "recipeCode");
            textBox2.Text = "מצרכים:" + "\r";
            foreach (DataRow row in ingredients.Rows)
            {
                textBox2.Text += row["amount"].ToString() + " " + row["ingredientName"].ToString() + "\r";
            }
            textBox2.Text += "אופן ההכנה:" + "\r" + myRecipe["preperation"];

            return (int)myRecipe["recipeCode"];
        }

        public static void LB_selecionChanged(Window thisWindow, ListBox lbSuggestion)
        {
            if (lbSuggestion.SelectedIndex >= 0)
            {
                Data.specificRecipeName = lbSuggestion.SelectedItem.ToString();
                specific_Recipe sr = new specific_Recipe();
                thisWindow.Hide();
                sr.ShowDialog();
                thisWindow.ShowDialog();
            }
        }

        public static void textbox_changed(TextBox tb, ListBox lbSuggestion, List<string> recipesList)
        {
            List<string> autoList = new List<string>();
            string name = "";

            if (tb.Text.Length == 0)
            {
                lbSuggestion.Visibility = Visibility.Collapsed;
                lbSuggestion.ItemsSource = null;
                return;
            }

            name = tb.Text;
            autoList.Clear();
            lbSuggestion.ItemsSource = null;

            foreach (string item in recipesList)
            {
                if (item.Contains(name))
                    autoList.Add(item);
            }

            if (autoList == null || autoList.Count <= 0)
            {
                lbSuggestion.Visibility = Visibility.Collapsed;
                lbSuggestion.ItemsSource = null;
            }

            else
            {
                lbSuggestion.ItemsSource = autoList;
                lbSuggestion.Visibility = Visibility.Visible;
            }
        }
    }
}
