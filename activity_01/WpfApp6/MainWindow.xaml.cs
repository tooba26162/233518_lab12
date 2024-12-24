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

namespace WpfApp6
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
        public DateTime CurrentTime
        {
            get { return (DateTime)GetValue(CurrentTimePropertyProperty); }
            set { SetValue(CurrentTimePropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentTime. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentTimePropertyProperty =
            DependencyProperty.Register("CurrentTime", typeof(DateTime), typeof(MainWindow), new FrameworkPropertyMetadata(
                    DateTime.Now,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnCurrentTimeChanged,
                    CoerceCurrentTime));

        private static void OnCurrentTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = d as MainWindow;
            if (window != null)
            {
                Console.WriteLine($"CurrentTime changed from {e.OldValue} to {e.NewValue}");
            }
        }

        private static object CoerceCurrentTime(DependencyObject d, object value)
        {
            DateTime newValue = (DateTime)value;

            if (newValue > DateTime.Now)
            {
                return DateTime.Now;
            }

            return value;
        }

        private static bool ValidateCurrentTime(object value)
        {
            DateTime newValue = (DateTime)value;

            return newValue <= DateTime.Now;
        }

        private void UpdateTime_Click(object sender, RoutedEventArgs e)
        {
            DateTime newTime = DateTime.Now;

            if (ValidateCurrentTime(newTime))
            {
                CurrentTime = newTime;
            }
            else
            {
                MessageBox.Show("The time cannot be in the future.");
            }
        }
    }
}
