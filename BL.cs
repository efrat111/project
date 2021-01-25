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
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Markup;

namespace tastyProject
{
    public static class BL
    {

        public static DataTable recipes = new DataTable();
        static Window window;
        static List<SqlParameter> param = new List<SqlParameter>();
        static SqlParameter p;
        static string image="";
        static int basketIngredientsNum=0;
        static List<string> basketIngredientsNames = new List<string>();
        static List<ingridientsImages> list = new List<ingridientsImages>();

        //Dal Functions

        public static int countTable(string tableName)
        {
            param.Clear();
            p = new SqlParameter("@tableName", SqlDbType.NChar);
            p.Direction = ParameterDirection.Input;
            p.Value = tableName;
            param.Add(p);

            return (int)Dal.Scalar("SP_countTableRows", param);
        }

        public static void insertOrDelete(string sp, int recipeCode, string tableName)
        {
            param.Clear();
            p = new SqlParameter("@tableName", SqlDbType.NChar);
            p.Direction = ParameterDirection.Input;
            p.Value = tableName;
            param.Add(p);
            p = new SqlParameter("@recipeCode", SqlDbType.Int);
            p.Direction = ParameterDirection.Input;
            p.Value = recipeCode;
            param.Add(p);
            p = new SqlParameter("@userID", SqlDbType.NChar);
            p.Direction = ParameterDirection.Input;
            p.Value = Data.userId;
            param.Add(p);
            Dal.ExecuteNonQuery(sp, param);
        }

        public static void deleteTop(string sp)
        {
            param.Clear();
            p = new SqlParameter("@userID", SqlDbType.NChar);
            p.Direction = ParameterDirection.Input;
            p.Value = Data.userId;
            param.Add(p);
            Dal.ExecuteNonQuery(sp, param);
        }

        public static int isRecipe(string sp, int recipeCode)
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
            return (int)Dal.Scalar(sp, param);
        }

        public static DataTable getTable(string sp, string tableName)
        {
            param.Clear();
            p = new SqlParameter("@tableName", SqlDbType.NChar);
            p.Direction = ParameterDirection.Input;
            p.Value = tableName;
            param.Add(p);
            if (string.Compare(sp, "SP_getTable") !=0)
            {
                p = new SqlParameter("@userID", SqlDbType.NChar);
                p.Direction = ParameterDirection.Input;
                p.Value = Data.userId;
                param.Add(p);
            }
            
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
            p = new SqlParameter(parameterName, DbType.Int32);
            p.Direction = ParameterDirection.Input;
            p.Value = code;
            param.Add(p);
            return Dal.getTable(sp, param);
        }

        public static DataTable parameterForTable(string sp, string name, string parameterName)
        {
            param.Clear();
            p = new SqlParameter(parameterName, DbType.String);
            p.Direction = ParameterDirection.Input;
            p.Value = name;
            param.Add(p);
            return Dal.getTable(sp, param);
        }

        public static int getCode(string sp, string name)
        {
            param.Clear();
            p = new SqlParameter("@name", name);
            p.DbType = DbType.String;
            p.Direction = ParameterDirection.Input;
            param.Add(p);
            return (int)Dal.Scalar(sp, param);
        }

        public static void listToTable(List<SearchRecipesNode> myList)
        {
            param.Clear();
            recipes.Clear();

            recipes.Columns.Add("recipeCode", typeof(int));
            recipes.Columns.Add("categoryCode", typeof(int));
            recipes.Columns.Add("preperation", typeof(string));
            recipes.Columns.Add("picture", typeof(string));
            recipes.Columns.Add("recipeName", typeof(string));

            DataTable recipe;
            DataRow newRow;

            foreach (SearchRecipesNode item in myList)
            {
                p = new SqlParameter("@recipeCode", DbType.Int32);
                p.Direction = ParameterDirection.Input;
                p.Value = item.RecipeCode;
                param.Add(p);

                newRow = recipes.NewRow();
                recipe = Dal.getTable("SP_getRecipesByCode", param);
                newRow["recipeCode"] = recipe.Rows[0]["recipeCode"];
                newRow["recipeName"] = recipe.Rows[0]["recipeName"];
                newRow["preperation"] = recipe.Rows[0]["preperation"];
                newRow["picture"] = recipe.Rows[0]["picture"];
                newRow["categoryCode"] = recipe.Rows[0]["categoryCode"];

                recipes.Rows.Add(newRow);

                param.Clear();
            }
        }


        //Tasty Function
        public static void openCategory(Window thisWindow, string pageName)
        {
            thisWindow.Hide();
            specific_Categories sc = new specific_Categories();
            sc.pageName.Content = pageName;
            sc.ShowDialog();
        }       

        public static void recipesForWindow(Label label, Grid grid, Window thisWindow)
        {
            int recipesNumber = recipes.Rows.Count;
            label.Content = recipesNumber.ToString();
            window = thisWindow;

            TextBox textBox;
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
                    textBox = new TextBox();

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
                    textBox.VerticalAlignment = VerticalAlignment.Bottom;

                    grid.Children.Add(button);
                    grid.Children.Add(textBox);
                    buttons.Add(button);
                    recipesNumber--;
                }
            }
        }


        public static void addColumnsToGrid(Grid mainGrd)
        {
            int i = 0;
            int j;
            ColumnDefinition column;
            List<Label> labels = mainGrd.Children.OfType<Label>().ToList();

            foreach (Grid item in mainGrd.Children.OfType<Grid>().ToList())
            {
                item.Width = Double.NaN;
                item.Margin = new Thickness(0, 10, 0, 0);
                for (j = 0; j < 18; j++)
                {
                    column = new ColumnDefinition();
                    column.Width = new GridLength(70, GridUnitType.Star);
                    column.MinWidth = 40;
                    item.ColumnDefinitions.Add(column);
                }
                ingredientsForWindow(item, labels[i].Content.ToString(), i);
                i++;
            }
        }

        public static void ingredientsForWindow(Grid grid, string ingredientCategory, int gridNumber)
        {

            grid.Margin = new Thickness(0, 35, 0, 0);
            DataTable ingredients = parameterForTable("SP_getIngredientsByCategory", ingredientCategory, "@ingredientName");
            int ingredientsNumber = ingredients.Rows.Count;
            TextBlock tb;
            Ellipse ellipse;
            ImageBrush imgBrush;
            StackPanel stkPnl;
            int rows, columns;
            int countRows = (ingredientsNumber % 18 == 0) ? (ingredientsNumber / 18) - 1 : ingredientsNumber / 18;
            GridLength length = new GridLength(100);
            RowDefinition row;
            string ingredientName = "";
            string IdName = "";
            string fileLocation = "";
            char[] charsToTrim = { '.', ',', '\'', '-', '(', ')', ' ' };

            for (rows = 0; rows <= countRows; rows++)
            {
                row = new RowDefinition();
                row.Height = length;
                grid.RowDefinitions.Add(row);

                for (columns = 17; columns >= 0 && ingredientsNumber > 0; columns--)
                {
                    tb = new TextBlock();
                    stkPnl = new StackPanel();
                    ellipse = new Ellipse();
                    imgBrush = new ImageBrush();
                    DataRow ingredient = ingredients.Rows[ingredientsNumber - 1];
                    ingredientName = ingredient["ingredientName"].ToString();
                    fileLocation = System.IO.Path.Combine(Environment.CurrentDirectory + @"..\..\..\images\search\" + ingredientCategory + "\\", ingredientName + ".jpg");
                    imgBrush.ImageSource = new BitmapImage(new Uri(fileLocation));
                    imgBrush.Stretch = Stretch.Fill;

                    ellipse.Fill = imgBrush;
                    ellipse.VerticalAlignment = VerticalAlignment.Top;
                    ellipse.Height = 50;
                    ellipse.MinHeight = 30;
                    ellipse.Margin = new Thickness(5,0,5,0);
                    IdName = new String(ingredientName.Where(Char.IsLetter).ToArray());
                    ellipse.Name = IdName;
                    ellipse.MouseLeftButtonDown += MouseLeftButtonDown;
                    ellipse.SetValue(Grid.ColumnProperty, columns);
                    ellipse.SetValue(Grid.RowProperty, rows);

                    tb.Margin=new Thickness(5,50,5,0);
                    tb.TextAlignment = TextAlignment.Center;
                    tb.Text = ingredientName;
                    tb.TextWrapping = TextWrapping.Wrap;
                    tb.SetValue(Grid.ColumnProperty, columns);
                    tb.SetValue(Grid.RowProperty, rows);

                    grid.Children.Add(ellipse);
                    grid.Children.Add(tb);

                    ingridientsImages item = new ingridientsImages(IdName, fileLocation);
                    list.Add(item);
                    ingredientsNumber--;
                    
                }
            }
        }

        public static void button_Click(object sender, RoutedEventArgs e)
        {
            int num = countTable("likedRecipes"); //this int is for likedRecipes page - if theres changes
            Button b = (Button)sender;
            //save the recipe name
            Data.specificRecipeName = b.Name;
            specific_Recipe sr = new specific_Recipe();
            window.Hide();
            sr.ShowDialog();
            if (num > countTable("likedRecipes"))
            {
                window.Close();
                specific_Categories sc = new specific_Categories();
                sc.pageName.Content = "מתכונים שאהבת";
                recipesForWindow(sc.label1, sc.grid, sc);
                sc.ShowDialog();
            }
            else
            { 
                if (string.Compare(Data.pageName, "lastEnteredRecipesPage" )==0)
                {
                    window.Close();
                    specific_Categories sc = new specific_Categories();
                    sc.pageName.Content = "מתכונים אחרונים";
                    recipesForWindow(sc.label1, sc.grid, sc);
                    sc.ShowDialog();
                }
                else
                    window.ShowDialog();
            }
        }

        public static int specificRecipe(Image image, TextBox category, TextBox recipeName, TextBlock ings, TextBlock preperation, Label amount, Label preperTm, Label time)
        {
            DataRow myRecipe;
            int recipeRowNumber;

            if (Data.specificRecipeName.StartsWith("button"))
            {
                // explanation //
                //example: name: "button3" -> "3" -> String To Ascii ->  3=51 -> minus the ascii -> 51-48=3 -> minus 1 for arr of the table -> 3-1 -> 2
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
                // explanation //
                // minus 1000 (the seed code) - minus 1 for arr (starts from 0) //
                recipeRowNumber = (getCode("SP_getRecipeCodeByName", Data.specificRecipeName)) - 1001; 
                myRecipe = recipes.Rows[recipeRowNumber]; 
            }

            if (Data.specificCategoryName=="")
            {
                param.Clear();
                p = new SqlParameter("@categoryCode", typeof(int));
                p.Value = myRecipe["categoryCode"];
                p.Direction = ParameterDirection.Input;
                param.Add(p);
                Data.specificCategoryName = Dal.Scalar("SP_getCategoryNameByCode", param).ToString().Trim();
            }
            category.Text = Data.specificCategoryName + " >>> " + myRecipe["recipeName"].ToString();
            recipeName.Text= myRecipe["recipeName"].ToString();
            image.Source = new BitmapImage(new Uri(myRecipe["picture"].ToString()));
            ings.Text = "";
            preperation.Text = "";
            DataTable ingredients = parameterForTable("SP_getIngredientsByRecipe", (int)myRecipe["recipeCode"], "recipeCode");

            foreach (DataRow row in ingredients.Rows)
            {
                ings.Text += row["amount"].ToString().Trim() + " " + row["ingredientName"].ToString() + "\r";
            }
            preperation.Text = myRecipe["preperation"].ToString()+"\r";

            amount.Content = myRecipe["amount"].ToString();
            time.Content = myRecipe["preparationTime"].ToString();
            preperTm.Content = myRecipe["Baking/CookingTime"].ToString();


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

        public static void getRecipes(Window thisWindow)
        {
            List<SearchRecipesNode> recipesForUser = new List<SearchRecipesNode>();
            int ingredientsInRecipe = 0, recipeCode;
            DataTable recipes = BL.parameterForTable("SP_getTable", "Recipes", "@tableName");
            DataTable ingredients;
            SearchRecipesNode node;
            for (int i = 0; i < recipes.Rows.Count; i++)
            {
                recipeCode = BL.getCode("SP_getRecipeCodeByName", recipes.Rows[i]["recipeName"].ToString());
                ingredients = BL.parameterForTable("SP_getIngredientsByRecipe", recipeCode, "@recipeCode");

                foreach (DataRow ingredient in ingredients.Rows)
                {
                    foreach (var item in basketIngredientsNames)
                    {
                        if (string.Compare(item, ingredient["ingredientName"].ToString()) == 0)
                            ingredientsInRecipe++;
                    }
                    if (ingredientsInRecipe > 0)
                    {
                        node = new SearchRecipesNode(recipeCode, ingredientsInRecipe);
                        recipesForUser.Add(node);
                        ingredientsInRecipe = 0;
                    }
                }
            }
            if (recipesForUser.Count > 0)
            {
                Quick_Sort(recipesForUser, 0, recipesForUser.Count - 1);
                BL.listToTable(recipesForUser);
                BL.openCategory(thisWindow, "מתכונים של החיפוש");
                thisWindow.Show();
            }
            else
            {
                MessageBox.Show("אין תוצאות");
            }

        }

        public static void Quick_Sort(List<SearchRecipesNode> arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);

                if (pivot > 1)
                {
                    Quick_Sort(arr, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    Quick_Sort(arr, pivot + 1, right);
                }
            }
        }

        public static int Partition(List<SearchRecipesNode> arr, int left, int right)
        {
            int pivot = arr[left].SameIngredientsNum;
            while (true)
            {

                while (arr[left].SameIngredientsNum < pivot)
                {
                    left++;
                }

                while (arr[right].SameIngredientsNum > pivot)
                {
                    right--;
                }

                if (left < right)
                {
                    if (arr[left].SameIngredientsNum == arr[right].SameIngredientsNum) return right;

                    int temp = arr[left].SameIngredientsNum;
                    arr[left].SameIngredientsNum = arr[right].SameIngredientsNum;
                    arr[right].SameIngredientsNum = temp;
                }
                else
                    return right;
            }
        }

        public static void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse ellipse = sender as Ellipse;
            foreach (var item in list)
            {
                if (string.Compare(ellipse.Name, item.Code) == 0)
                {
                    image = item.Image;
                    break;
                }
            }

            DragDrop.DoDragDrop((DependencyObject)sender, e.Source, DragDropEffects.Copy);

            GridLength length = new GridLength(60);
            RowDefinition row;

            if (basketIngredientsNum‏ % 3 == 0)
            {
                row = new RowDefinition();
                row.Height = length;
                Data.grid.RowDefinitions.Add(row);
            }

            int countRows = Data.grid.RowDefinitions.Count - 1;
            int columns = basketIngredientsNum‏ % 3;

            basketIngredientsNames.Add((image.Substring(image.LastIndexOf('\\') + 1)).Replace(".jpg", ""));
            basketIngredientsNum‏++;

            Ellipse newE = XamlReader.Parse(XamlWriter.Save(ellipse)) as Ellipse;
            newE.Margin = new Thickness(5);
            newE.Height = 45;
            newE.SetValue(Grid.ColumnProperty, columns);
            newE.SetValue(Grid.RowProperty, countRows);
            Data.grid.Children.Add(newE);
        }

    }
}
