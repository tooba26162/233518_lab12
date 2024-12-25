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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace WpfApp10
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        private string connectionString = "DESKTOP-1838J82\\SQLEXPRESS"; // Replace with your connection string

        public Window2()
        {
            InitializeComponent();
            LoadCoursesNotAssigned();
        }

        private void LoadCoursesNotAssigned()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"SELECT * 
                                     FROM Course 
                                     WHERE id NOT IN (SELECT CourseID FROM StudentCourses)";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    CoursesNotAssignedDataGrid.ItemsSource = dataTable.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading courses: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CoursesNotAssignedDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

