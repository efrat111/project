using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using project1.TastyDataSetTableAdapters;

namespace project1
{
    public class Data
    {
        public static TastyDataSet tastyDataSet = new TastyDataSet();
        public static QueriesTableAdapter query = new QueriesTableAdapter();
        public static string conToTasty = ConfigurationManager.ConnectionStrings["TastyConnectionString"].ConnectionString;
        public static SqlConnection con = new SqlConnection(conToTasty);
        public static SqlCommand sqlCommand = new SqlCommand("", con);
        public static SqlDataAdapter adapter = new SqlDataAdapter("",con);
        
        public static string specificCategoryName="";
        public static string specificRecipeName="";

    }
}
