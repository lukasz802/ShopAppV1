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
using ShopLib;
using ModelData;
using System.Text.RegularExpressions;

namespace ShopApp
{
    /// <summary>
    /// Logika interakcji dla klasy AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        public AddClientWindow()
        {
            InitializeComponent();
            mainwindowBar.MouseLeftButtonDown += (o, e) => DragMove();            
        }
        
        private void InitialEdit()
        {
            if (clientWindowTitleLabel.Content.ToString() == "Edytuj klienta")
            {
                Client client = DataResources.RelayClient;
                this.telefonTextBox.Text = client.PhoneNumber;
                this.imieTextBox.Text = client.FirstName;
                this.nazwiskoTextBox.Text = client.LastName;
                this.emailTextBox.Text = client.EmailAddress;
                this.miastoTextBox.Text = client.City;
                this.kodTextBox.Text = client.Code;
                this.ulicaTextBox.Text = client.Street;
                this.nrdomuTextBox.Text = client.StreetNumber;
                this.nrmieszkaniaTextBox.Text = client.FlatNumber;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void AnulujButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DodajButton_Click(object sender, RoutedEventArgs e)
        {
            if (clientWindowTitleLabel.Content.ToString() != "Edytuj klienta")
            {
                Client client = new Client(imieTextBox.Text, nazwiskoTextBox.Text, kodTextBox.Text, miastoTextBox.Text, ulicaTextBox.Text,
                nrdomuTextBox.Text, nrmieszkaniaTextBox.Text, telefonTextBox.Text, emailTextBox.Text);
                ModelData.DataResources.AddClient(client);
            }
            else
            {
                Client client = DataResources.RelayClient;              
                client.PhoneNumber = this.telefonTextBox.Text;
                client.FirstName = this.imieTextBox.Text;
                client.LastName = this.nazwiskoTextBox.Text;
                client.EmailAddress = this.emailTextBox.Text;
                client.City = this.miastoTextBox.Text;
                client.Code = this.kodTextBox.Text;
                client.Street = this.ulicaTextBox.Text;
                client.StreetNumber = this.nrdomuTextBox.Text;
                client.FlatNumber = this.nrmieszkaniaTextBox.Text;             
            }

            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitialEdit();
        }

        private static readonly Regex _regex = new Regex("[^0-9]");
        private static readonly Regex _regex_address = new Regex("[^0-9-]");
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        private static bool AddressIsTextAllowed(string text)
        {
            return !_regex_address.IsMatch(text);
        }

        private void AdresTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            e.Handled = !AddressIsTextAllowed(e.Text);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            e.Handled = !IsTextAllowed(e.Text);
        }
    }
}
