using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace tastyProject
{
    public partial class specific_Recipe : Window
    {
        int recipeCode;
        public specific_Recipe()
        {
            InitializeComponent();
            recipeCode = BL.specificRecipe(image, textBox1, textBox2);

            if (BL.likedRecipe("SP_isRecipeLiked", recipeCode)==1)
                like.Content = FindResource("afterLike");
        }

        private void Button_Like(object sender, RoutedEventArgs e)
        {
            if (like.Content == FindResource("beforeLike"))
            {
                like.Content = FindResource("afterLike");
                BL.likedRecipe("SP_insertLikedRecipe", recipeCode);
            }

            else
            { 
                like.Content = FindResource("beforeLike");
                BL.likedRecipe("SP_deleteLikedRecipe", recipeCode);
            }
        }
    }
}
