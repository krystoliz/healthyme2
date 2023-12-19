using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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
using System.Windows.Shapes;

namespace HealthyMe3
{
    /// <summary>
    /// Interaction logic for Journal.xaml
    /// </summary>
    public partial class Journal : Window
    {
        public Journal()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""DatabaseJournal.mdf"";Integrated Security=True";
        private void Save_Journal_Click(object sender, RoutedEventArgs e)
        {
           
            string Entry1 = entry1.Text;
            string Entry2 = entry2.Text;
            string Entry3 = entry3.Text;
            string Entry4 = entry4.Text;

            

            if (!string.IsNullOrEmpty(entry1.Text) && !string.IsNullOrEmpty(entry2.Text) && !string.IsNullOrEmpty(entry3.Text) && !string.IsNullOrEmpty(entry4.Text))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //insert data into database
                    string insertQuery = "INSERT INTO dbo.Journal (things you did, how you felt, things youre grateful for, new goals) VALUES (@things you did, @how you felt, @things youre grateful for, @new goals)";
                    SqlCommand command = new SqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@things you did", Entry1);
                    command.Parameters.AddWithValue("@how you felt", Entry2);
                    command.Parameters.AddWithValue("@things youre grateful for", Entry3);
                    command.Parameters.AddWithValue("@new goals", Entry4);

                    try
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data inserted successfully!");
                            // Clear input fields or perform any other action after successful insertion
                            entry1.Text = "";
                            entry2.Text = "";
                            entry3.Text = "";
                            entry4.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Failed to insert data!");
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill today's journal");
            }
        }

        private void Open_Data_Copy_Click(object sender, RoutedEventArgs e)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Assuming 'Users' is the table name and you want to display all its data
                string query = "SELECT * FROM Users";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                
            }
        }
    }
}
