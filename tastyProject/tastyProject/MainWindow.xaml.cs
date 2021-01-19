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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace tastyProject
{
    public partial class MainWindow : Window
    {
        List<string> recipesList = new List<string>();
        public MainWindow()
        {
            InitializeComponent();

            BL.getRecipesNames(recipesList);
            likedRecipeNum.Content = BL.countTable("likedRecipes");
            textBox1.TextChanged += new TextChangedEventHandler(textBox1_TextChanged);
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            BL.textbox_changed(textBox1, lbSuggestion, recipesList);
        }

        public void LB_selecionChanged(object sender, SelectionChangedEventArgs e)
        {
            BL.LB_selecionChanged(this, lbSuggestion);
        }

        private void Button_OpenCategory(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            Data.specificCategoryName = b.Content.ToString();
            BL.recipes = BL.parameterForTable("SP_getRecipesByCategory", BL.getCode("getCategoryCodeByName", Data.specificCategoryName), "@categoryCode");
            BL.openCategory(this, Data.specificCategoryName);
            likedRecipeNum.Content = BL.countTable("likedRecipes");
            this.Show();
        }

        private void ButtonClick_toAllSearches(object sender, RoutedEventArgs e)
        {
            Data.specificRecipeName = textBox1.Text;
            Data.pageName = "toAllSearchesPage";
            BL.recipes = BL.parameterForTable("SP_getRecipesBySearch", Data.specificRecipeName, "@recipeName");
            BL.openCategory(this, BL.recipes.Rows.Count+" - כל התוצאות");
            textBox1.Text = "";
            likedRecipeNum.Content = BL.countTable("likedRecipes");
            this.Show();
            Data.pageName = "";
        }

        private void Button_likedRecipes(object sender, RoutedEventArgs e)
        {
            BL.recipes = BL.getTable("SP_getRecipesByUserID", "likedRecipes");
            Data.pageName = "likedRecipesPage";
            BL.openCategory(this, "מתכונים שאהבת");
            likedRecipeNum.Content = BL.countTable("likedRecipes");
            Data.pageName = "";
            this.Show();
        }

        private void Button_lastRecipesEntered(object sender, RoutedEventArgs e)
        {
            Data.pageName = "lastEnteredRecipesPage";
            BL.recipes = BL.getTable("SP_getRecipesByUserID", "lastRecipesEntered");
            BL.openCategory(this, "מתכונים אחרונים שראית");
            Data.pageName = "";
            this.Show();
        }


        private void MenuItem_MouseEnter(object sender, MouseEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            mi.IsSubmenuOpen = true;
        }

        private void MenuItem_MouseLeave(object sender, MouseEventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            mi.IsSubmenuOpen = false;
        }

    }
}
