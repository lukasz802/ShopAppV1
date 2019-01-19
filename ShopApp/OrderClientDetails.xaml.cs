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
using ModelData;
using ShopLib;

namespace ShopApp
{
    /// <summary>
    /// Logika interakcji dla klasy OrderClientDetails.xaml
    /// </summary>
    public partial class OrderClientDetails : Window
    {
        public OrderClientDetails()
        {
            InitializeComponent();
            mainwindowBar.MouseLeftButtonDown += (o, e) => DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void WybierzButton_Click(object sender, RoutedEventArgs e)
        {            
            DataResources.RelayItem.Client = ClientsListBox.SelectedItem as Client;

            OrderInfoWindow window = (OrderInfoWindow)this.Owner;
            window.imieLabel.Content = DataResources.RelayItem.Client.FirstName;
            window.nazwiskoLabel.Content = DataResources.RelayItem.Client.LastName;
            window.ulicaLabel.Content = DataResources.RelayItem.Client.Street;
            window.kodLabel.Content = DataResources.RelayItem.Client.Code;
            window.miastoLabel.Content = DataResources.RelayItem.Client.City;
            window.numerulicyLabel.Content = DataResources.RelayItem.Client.StreetNumber;
            window.numermieszkaniaLabel.Content = DataResources.RelayItem.Client.FlatNumber;
            window.telefonLabel.Content = DataResources.RelayItem.Client.PhoneNumber;
            window.emailLabel.Content = DataResources.RelayItem.Client.EmailAddress;

            var temp = (from Client client in DataResources.Clients
                        where client.Items.Contains(DataResources.RelayItem)
                        select client).First();
            temp.RemoveItem(DataResources.RelayItem);

            DataResources.RelayClient = ClientsListBox.SelectedItem as Client;
            DataResources.RelayClient.Items.Add(DataResources.RelayItem);

            this.Close();
        }
    }
}
