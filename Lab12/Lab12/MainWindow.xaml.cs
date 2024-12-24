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

namespace Lab12
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Address> Addresses { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            LoadAddresses();
            AddressComboBox.ItemsSource = Addresses;
        }
        private void LoadAddresses()
        {
            Addresses = new List<Address>
            {
                new Address { Name = "Home", Street = "123 Main St", City = "Anytown", State = "CA", ZipCode = "12345" },
                new Address { Name = "Work", Street = "456 Business Rd", City = "Businesstown", State = "CA", ZipCode = "67890" }
            };
        }

        private void AddressComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (AddressComboBox.SelectedItem is Address selectedAddress)
            {
                ShippingAddressTextBox.Text = selectedAddress.ToString();
            }
        }

        private void ChangeAddress_Click(object sender, RoutedEventArgs e)
        {
            if (AddressComboBox.SelectedItem is Address selectedAddress)
            {
                var newAddress = ShippingAddressTextBox.Text;
                if (MessageBox.Show("Do you want to change the existing address?", "Change Address", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    // Logic to change the existing address
                    // For simplicity, we will just update the selected address
                    var parts = newAddress.Split(',');
                    if (parts.Length == 5)
                    {
                        selectedAddress.Name = parts[0].Trim();
                        selectedAddress.Street = parts[1].Trim();
                        selectedAddress.City = parts[2].Trim();
                        selectedAddress.State = parts[3].Trim();
                        selectedAddress.ZipCode = parts[4].Trim();
                        AddressComboBox.Items.Refresh();
                    }
                }
            }
        }

        private void AddAddress_Click(object sender, RoutedEventArgs e)
        {
            var newAddress = ShippingAddressTextBox.Text;
            if (MessageBox.Show("Do you want to add this new address?", "Add Address", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var parts = newAddress.Split(',');
                if (parts.Length == 5)
                {
                    var newAddressObj = new Address
                    {
                        Name = parts[0].Trim(),
                        Street = parts[1].Trim(),
                        City = parts[2].Trim(),
                        State = parts[3].Trim(),
                        ZipCode = parts[4].Trim()
                    };
                    Addresses.Add(newAddressObj);
                    AddressComboBox.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("Please enter a valid address format: Name, Street, City, State, ZipCode");
                }
            }
        }

    }

    public class Address
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public override string ToString()
        {
            return $"{Name}, {Street}, {City}, {State}, {ZipCode}";
        }
    }


}
