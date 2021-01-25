using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace tastyProject
{
    public partial class specific_Recipe : Window
    {
        int recipeCode;

        public specific_Recipe()
        {
            InitializeComponent();
            
            recipeCode = BL.specificRecipe(image, category, recipeName, ings, prepertion, amount, preperTm, time);

            if (BL.isRecipe("SP_isRecipeLiked", recipeCode)==1)
                like.Content = FindResource("afterLike");

            if (BL.isRecipe("SP_isRecipeExsist", recipeCode) == 1)
                BL.insertOrDelete("SP_deleteByRecipeCode", recipeCode, "lastRecipesEntered");
            BL.insertOrDelete("SP_insertIntoTable", recipeCode, "lastRecipesEntered");

            if (string.Compare(Data.pageName, "lastEnteredRecipesPage") == 0)
                BL.recipes= BL.getTable("SP_getRecipesByUserID", "lastRecipesEntered");

            if (BL.countTable("lastRecipesEntered") > 30)
                BL.deleteTop("SP_deleteTopLastRecipesEntered");

            StackPanel sp;
            Image img;
            Label lb;
            Border border;
            for (int i = 0; i < 3 && i < BL.recipes.Rows.Count; i++)
            {
                sp = new StackPanel();
                img = new Image();
                lb = new Label();
                border = new Border();

                img.Source = new BitmapImage(new Uri(BL.recipes.Rows[i]["picture"].ToString()));
                img.Stretch = Stretch.Fill;
                img.Height = 100;
                img.Margin = new Thickness(10, 10, 10, 0);
                //img.MouseDown
                lb.Content = BL.recipes.Rows[i]["recipeName"].ToString();
                lb.FontSize = 15;
                lb.Height = 30;
                lb.Margin = new Thickness(10, 0, 10, 0);
                lb.HorizontalContentAlignment = HorizontalAlignment.Center;
                lb.VerticalAlignment = VerticalAlignment.Bottom;

                var bc = new BrushConverter();
                border.BorderBrush = (Brush)bc.ConvertFrom("#c32026");
                
                border.BorderThickness = new Thickness(2);
                border.CornerRadius = new CornerRadius(3);
                sp.Width = 200;
                border.Margin = new Thickness(0, 10, 0, 10);
                border.Child = sp;

                sp.Height = 140;
                sp.Children.Add(img);
                sp.Children.Add(lb);
                rndList.Items[i] = border;
            }


        }

        private void Button_Like(object sender, RoutedEventArgs e)
        {
            if (like.Content == FindResource("beforeLike"))
            {
                like.Content = FindResource("afterLike");
                BL.insertOrDelete("SP_insertIntoTable", recipeCode, "likedRecipes");
            }

            else
            { 
                like.Content = FindResource("beforeLike");
                BL.insertOrDelete("SP_deleteByRecipeCode", recipeCode, "likedRecipes");
                if (string.Compare( Data.pageName, "likedRecipesPage")==0)
                    BL.recipes = BL.getTable("SP_getRecipesByUserID", "likedRecipes");
            }
        }
    }
}
