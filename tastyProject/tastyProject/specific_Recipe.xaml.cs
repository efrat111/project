using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace tastyProject
{
    public partial class specific_Recipe : Window
    {
        int recipeCode;

        public specific_Recipe()
        {
            InitializeComponent();
            
            recipeCode = BL.specificRecipe(image, textBox1, textBox2);

            if (BL.isRecipe("SP_isRecipeLiked", recipeCode)==1)
                like.Content = FindResource("afterLike");

            if (BL.isRecipe("SP_isRecipeExsist", recipeCode) == 1)
                BL.insertOrDelete("SP_deleteByRecipeCode", recipeCode, "lastRecipesEntered");
            BL.insertOrDelete("SP_insertIntoTable", recipeCode, "lastRecipesEntered");

            if (string.Compare(Data.pageName, "lastEnteredRecipesPage") == 0)
                BL.recipes= BL.getTable("SP_getRecipesByUserID", "lastRecipesEntered");

            if (BL.countTable("lastRecipesEntered") > 30)
                BL.deleteTop("SP_deleteTopLastRecipesEntered");
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
