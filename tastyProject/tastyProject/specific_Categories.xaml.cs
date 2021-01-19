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


namespace tastyProject
{
    public partial class specific_Categories : Window
    {
        List<string> recipesList=new List<string>();

        public specific_Categories()
        {
            InitializeComponent();
            BL.getRecipesNames(recipesList);
            textBox1.TextChanged += new TextChangedEventHandler(textBox1_TextChanged);

            if (Data.pageName == "")
                pageName.Content = Data.specificCategoryName;

            BL.recipesForWindow(label1, grid, this);

        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            BL.textbox_changed(textBox1, lbSuggestion, recipesList);
        }

        private void LB_selecionChanged(object sender, SelectionChangedEventArgs e)
        {
            BL.LB_selecionChanged(this, lbSuggestion);
        }

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            Data.specificRecipeName = textBox1.Text;
            BL.recipes = BL.parameterForTable("SP_getRecipesBySearch", Data.specificRecipeName, "@recipeName");
            BL.recipesForWindow(label1, grid, this);
        }
    }
}

