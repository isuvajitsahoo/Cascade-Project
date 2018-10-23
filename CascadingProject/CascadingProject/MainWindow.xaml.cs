using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace CascadingProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DataSet ds = new DataSet();
        ForeignKeyConstraint fk;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=ndamssql\sqlilearn; user id=sqluser; password=sqluser;initial catalog=training_19sep18_pune;");
            string query1 = "SELECT * FROM tblCustomers";
            string query2 = "SELECT * FROM tblOrders";

            SqlDataAdapter sdaC = new SqlDataAdapter(query1,con);
            SqlDataAdapter sdaO = new SqlDataAdapter(query2,con);

            sdaC.Fill(ds, "tblCustomers");
            sdaO.Fill(ds, "tblOrders");

            Cus.ItemsSource = ds.Tables[0].DefaultView;
            Ord.ItemsSource = ds.Tables["tblOrders"].DefaultView;

            DataColumn Parent = ds.Tables["tblCustomers"].Columns["CustId"];
            DataColumn Child = ds.Tables["tblOrders"].Columns["CustId"];

            fk = new ForeignKeyConstraint("FK_Customer_Order", Parent,Child);

            fk.DeleteRule = Rule.Cascade;
            fk.DeleteRule = Rule.Cascade;

            ds.Tables["tblOrders"].Constraints.Add(fk);

            con.Close();

        }
    }
}
