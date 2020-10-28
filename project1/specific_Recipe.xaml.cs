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
using System.Data;

namespace project1
{
    /// <summary>
    /// Interaction logic for specific_Recipe.xaml
    /// </summary>
    public partial class specific_Recipe : Window
    {
        public specific_Recipe()
        {
            InitializeComponent();
            int? value = 0;
            Data.tastyDataSet.Recipes.Clear();
            Data.query.getCategoryCodeByName(Data.specificCategoryName, ref value);
            Data.sqlCommand.CommandText = "select * from recipes where categoryCode = " + value.GetValueOrDefault();
            Data.adapter.SelectCommand = Data.sqlCommand;
            Data.adapter.Fill(Data.tastyDataSet.Recipes);
            // i explanation //
            //example to understand the i -> name: "button3" -> "3" -> String To Ascii ->  51 -> minus the ascii -> 51-48=3 -> minus 1 for arr of the table -> 3-1 -> 2
            int i = Data.specificRecipeName[Data.specificRecipeName.Length - 1]-49;
            TastyDataSet.RecipesRow myRecipe = Data.tastyDataSet.Recipes[i];
            textBox1.Text = Data.specificCategoryName + "\r" + myRecipe.recipeName;
            image.Source = new BitmapImage(new Uri(myRecipe.picture));
            textBox2.Text = "";
            Data.sqlCommand.CommandText = "select ingredientName, amount from ( ingredient i inner join Recipe_Ingredient ri ON ri.ingredientCode = i.ingredientCode ) where recipeCode = " + myRecipe.recipeCode;
            DataTable table = new DataTable("myTable");
            table.Columns.Add("ingredientName", typeof(string));
            table.Columns.Add("amount", typeof(string));

            Data.adapter.SelectCommand = Data.sqlCommand;
            Data.adapter.Fill(table);
            textBox2.Text = "מצרכים:" + "\r";
            foreach (DataRow row in table.Rows)
            {
                textBox2.Text += row["amount"].ToString() + " " + row["ingredientName"].ToString()  + "\r";
            }
            textBox2.Text += "אופן ההכנה:" + "\r" + myRecipe.preperation;
        }
    }
}
