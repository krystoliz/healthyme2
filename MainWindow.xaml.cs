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
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace HealthyMe3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=KRYSTOLIZ\SQLEXPRESS01;Initial Catalog=buatppbo;Integrated Security=True");

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();

            textBox1.Focus();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            String username, user_password;

            username = textBox1.Text;
            user_password = textBox2.Text;

            try
            {
                String querry = "SELECT * FROM UserInfo WHERE username = '" + textBox1.Text + "' AND password = '" + textBox2.Text + "'";
                SqlDataAdapter sda = new SqlDataAdapter(querry, conn);

                DataTable dtable = new DataTable();
                sda.Fill(dtable);

                if (dtable.Rows.Count > 0)
                {
                    username = textBox1.Text;
                    user_password = textBox2.Text;

                    //page needed to be loaded next;
                    Window1 form3 = new Window1();
                    form3.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid login details", "error");
                    textBox1.Clear();
                    textBox2.Clear();

                    //to focus username
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res;
            res = MessageBox.Show("Do you want to exit", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

    }
}
