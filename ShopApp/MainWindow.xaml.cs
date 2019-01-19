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
using ShopLib;
using ShopApp;
using ModelData;
using System.Collections.ObjectModel;

namespace ShopApp
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            mainwindowBar.MouseLeftButtonDown += (o, e) => DragMove();
            listBoxKlienci.DataContext = ModelData.DataResources.Clients;
            listBoxZamowienia.DataContext = ModelData.DataResources.Items;
        }

        private void RejestrujKlienta(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClientWindow = new AddClientWindow();
            addClientWindow.Owner = this;
            addClientWindow.ShowDialog();
        }

        private void UsunKlienta(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy chcesz usunąć zaznaczonego klienta z bazy?" + "\nOperacja ta spowoduje dodatkowo usunięcie wszystkich zamówień przypisanych do klienta.", "ShopApp", MessageBoxButton.YesNo, MessageBoxImage.Question,
                MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                var temp = (from Item item in DataResources.Items
                            where item.Client == listBoxKlienci.SelectedItem as Client
                            select item).ToList();

                foreach (Item item in temp)
                {
                    DataResources.RemoveItem(item);
                }

                DataResources.RemoveClient(listBoxKlienci.SelectedItem as Client);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ControlButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Minimized;
            }
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender==clientsButton)
            {
                mainTabControl.SelectedIndex = 0;
                clientsButton.IsChecked = true;
            }
            else if (sender == ordersButton)
            {
                mainTabControl.SelectedIndex = 1;
                clientsButton.IsChecked = false;
            }
            else if (sender == settingsButton)
            {
                mainTabControl.SelectedIndex = 2;
                clientsButton.IsChecked = false;
            }
            else
            {
                mainTabControl.SelectedIndex = 3;
                clientsButton.IsChecked = false;
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MenuButton_Click(clientsButton, new RoutedEventArgs());
        }

        private void MainMenuColorChanged(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button.Parent == menucolorWrapPanel)
            {                
                Application.Current.Resources["menuColor"] = button.Background;
            }
            else if (button.Parent == activemenucolorWrapPanel)
            {
                Application.Current.Resources["activecontrolColor"] = button.Background;
            }
        }
        
        private void MainFontColorChanged(object sender, RoutedEventArgs e)
        {
           Button button = sender as Button;
           Application.Current.Resources["mainFontFamily"] = button.FontFamily;
        }

        private void ButtonEdytujKlienta_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxKlienci.SelectedItems.Count != 0)
            {
                AddClientWindow editClientWindow = new AddClientWindow();
                ModelData.DataResources.RelayClient = listBoxKlienci.SelectedItem as Client;
                editClientWindow.clientWindowTitleLabel.Content = "Edytuj klienta";
                editClientWindow.dodajButton.Content = "Zapisz";
                editClientWindow.dodajButton.ToolTip = "Zapisz zmiany";
                editClientWindow.clientWindowImage.Source = new BitmapImage(new Uri("Icons/editusersImage.png", UriKind.Relative));
                editClientWindow.clientWindowImage.Margin = new Thickness(0, 0, 0, 5);
                editClientWindow.Owner = this;
                editClientWindow.ShowDialog();
            }
        }

        private void UsunZamowienie(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy chcesz usunąć zaznaczone zamówienie z bazy?", "ShopApp", MessageBoxButton.YesNo, MessageBoxImage.Question,
            MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                ModelData.DataResources.RelayClient = (listBoxZamowienia.SelectedItem as Item).Client;
                ModelData.DataResources.RelayClient.Items.Remove(listBoxZamowienia.SelectedItem as Item);
                DataResources.RemoveItem(listBoxZamowienia.SelectedItem as Item);
            }
        }

        private void ButtonEdytujZamowienie_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxZamowienia.SelectedItems.Count != 0)
            {
                AddItemWindow editItemWindow = new AddItemWindow();
                ModelData.DataResources.RelayItem = listBoxZamowienia.SelectedItem as Item;
                editItemWindow.itemWindowTitleLabel.Content = "Edytuj zamówienie";
                editItemWindow.dodajButton.Content = "Zapisz";
                editItemWindow.dodajButton.ToolTip = "Zapisz zmiany";
                editItemWindow.clientWindowImage.Source = new BitmapImage(new Uri("Icons/editorderImage.png", UriKind.Relative));
                editItemWindow.Owner = this;
                editItemWindow.ShowDialog();
            }
        }

        private void ListBoxZamowienia_KeyDown(object sender, KeyEventArgs e)
        {
            ListBox listBox = sender as ListBox;

            if (listBox.SelectedItems.Count !=0 && e.Key == Key.Delete)
            {
                UsunZamowienie(sender, new RoutedEventArgs());
            }
        }

        private void ListBoxKlienci_KeyDown(object sender, KeyEventArgs e)
        {
            ListBox listBox = sender as ListBox;

            if (listBox.SelectedItems.Count != 0 && e.Key == Key.Delete)
            {
                UsunKlienta(sender, new RoutedEventArgs());
            }
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            ClientItemsDetails detailsItemWindow = new ClientItemsDetails();
            DataResources.RelayClient = listBoxKlienci.SelectedItem as Client;
            detailsItemWindow.ItemsListBox.DataContext = DataResources.RelayClient.Items;
            detailsItemWindow.Owner = this;
            detailsItemWindow.ShowDialog();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            OrderInfoWindow orderItemWindow = new OrderInfoWindow();
            DataResources.RelayItem = listBoxZamowienia.SelectedItem as Item;
            orderItemWindow.imieLabel.Content = DataResources.RelayItem.Client.FirstName;
            orderItemWindow.nazwiskoLabel.Content = DataResources.RelayItem.Client.LastName;
            orderItemWindow.ulicaLabel.Content = DataResources.RelayItem.Client.Street;
            orderItemWindow.kodLabel.Content = DataResources.RelayItem.Client.Code;
            orderItemWindow.miastoLabel.Content = DataResources.RelayItem.Client.City;
            orderItemWindow.numerulicyLabel.Content = DataResources.RelayItem.Client.StreetNumber;
            orderItemWindow.numermieszkaniaLabel.Content = DataResources.RelayItem.Client.FlatNumber;
            orderItemWindow.telefonLabel.Content = DataResources.RelayItem.Client.PhoneNumber;
            orderItemWindow.emailLabel.Content = DataResources.RelayItem.Client.EmailAddress;
            orderItemWindow.Owner = this;
            orderItemWindow.ShowDialog();
        }
    }
}
