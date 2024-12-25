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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
       
            private string connectionString = "DESKTOP-1838J82\\SQLEXPRESS"; // Replace with actual connection string

        public Window1()
        {
            InitializeComponent();
            LoadStudents(); // Call the method to load students into the ComboBox
        }

        // Method to load students into the ComboBox
        private void LoadStudents()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Open the connection
                    string query = "SELECT id, Name FROM Student"; // SQL query to fetch students

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable); // Fill the DataTable with results

                    // Set the ComboBox's data source and properties
                    StudentsComboBox.ItemsSource = dataTable.DefaultView;
                    StudentsComboBox.DisplayMemberPath = "Name"; // Display student names
                    StudentsComboBox.SelectedValuePath = "id";   // Use student IDs as values
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading students: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Event handler for ComboBox selection change
        private void StudentsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StudentsComboBox.SelectedValue != null)
            {
                int studentId = Convert.ToInt32(StudentsComboBox.SelectedValue); // Get the selected student ID
                LoadCoursesForStudent(studentId); // Load courses for the selected student
            }
        }

        // Method to load courses for a selected student
        private void LoadCoursesForStudent(int studentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Open the connection

                    string query = @"SELECT Course.Name AS CourseName, Course.Code 
                                     FROM StudentCourses 
                                     INNER JOIN Course ON StudentCourses.CourseID = Course.id 
                                     WHERE StudentCourses.StudentID = @StudentID";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@StudentID", studentId); // Add the parameter

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable); // Fill the DataTable with results

                    // Set the DataGrid's data source
                    CoursesDataGrid.ItemsSource = dataTable.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading courses: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
