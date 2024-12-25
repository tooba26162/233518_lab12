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
using System.Data;

namespace WpfApp10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString = "DESKTOP-1838J82\\SQLEXPRESS";

        public MainWindow()
        {
            InitializeComponent();
            LoadCourses();
        }
        private void LoadCourses()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Course", conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                CoursesDataGrid.ItemsSource = dt.DefaultView;
            }
        }

        private void AddCourseButton_Click(object sender, RoutedEventArgs e)
        {
            string code = CodeTextBox.Text;
            string name = NameTextBox.Text;

            if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(name))
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Course (Code, Name) VALUES (@Code, @Name)", conn);
                    cmd.Parameters.AddWithValue("@Code", code);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.ExecuteNonQuery();
                }
                LoadCourses();
            }
        }

        private void CoursesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
  
}
