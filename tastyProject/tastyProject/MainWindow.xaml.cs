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
            BL.openCategory(this, b.Content.ToString());
            likedRecipeNum.Content = BL.countTable("likedRecipes");
            this.Show();
        }

        private void ButtonClick_toAllSearches(object sender, RoutedEventArgs e)
        {
            Data.specificRecipeName = textBox1.Text;
            Data.toAllSearches = true;
            BL.recipes = BL.parameterForTable("SP_getRecipesBySearch", Data.specificRecipeName, "@recipeName");
            specific_Categories sc = new specific_Categories();
            this.Hide();
            sc.ShowDialog();
            textBox1.Text = "";
            likedRecipeNum.Content = BL.countTable("likedRecipes");
            this.Show();
            Data.toAllSearches = false;
        }

        private void Button_likedRecipes(object sender, RoutedEventArgs e)
        {
            BL.recipes = BL.getRecipesByUserId("SP_getRecipesByUserID", "likedRecipes");
            specific_Categories sc = new specific_Categories();
            this.Hide();
            sc.ShowDialog();
            likedRecipeNum.Content = BL.countTable("likedRecipes");
            this.Show();
        }

        private void Button_lastRecipes(object sender, RoutedEventArgs e)
        {
            BL.recipes = BL.getRecipesByUserId("SP_getRecipesByUserID", "lastSearches");
            specific_Categories sc = new specific_Categories();
            this.Hide();
            sc.ShowDialog();
            likedRecipeNum.Content = BL.countTable("likedRecipes");
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
