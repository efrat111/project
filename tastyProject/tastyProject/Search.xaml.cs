using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace tastyProject
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        public Search()
        {
            InitializeComponent();
            Data.grid = afterDrop;
            BL.addColumnsToGrid(mainGrd);
        }

        void basket_mouseDown(object sender, MouseButtonEventArgs e)
        {
            BL.getRecipes(this);
        }
    }
}