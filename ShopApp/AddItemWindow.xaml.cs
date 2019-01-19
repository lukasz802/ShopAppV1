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
    /// Logika interakcji dla klasy AddItemWindow.xaml
    /// </summary>
    public partial class AddItemWindow : Window
    {
        public AddItemWindow()
        {
            InitializeComponent();
            mainwindowBar.MouseLeftButtonDown += (o, e) => DragMove();
        }

        private void InitialEdit()
        {
            if (itemWindowTitleLabel.Content.ToString() == "Edytuj zamówienie")
            {
                Item item = DataResources.RelayItem;
                this.nazwaTextBox.Text = item.ProductName;
                this.iloscTextBox.Text = Convert.ToString(item.Amount);
                this.typComboBox.SelectedIndex = Convert.ToInt32(item.ItemType);
                this.cenaTextBox.Text = Convert.ToString(item.UnitPrice);
            }
        }

        private void DodajButton_Click(object sender, RoutedEventArgs e)
        {
            if (itemWindowTitleLabel.Content.ToString() != "Edytuj zamówienie")
            {
                Item item = new Item(nazwaTextBox.Text, Convert.ToDecimal(cenaTextBox.Text), Convert.ToUInt32(iloscTextBox.Text), (ItemType)typComboBox.SelectedIndex, DataResources.RelayClient);
                ModelData.DataResources.AddItem(item);
                DataResources.RelayClient.AddItem(item);
            }
            else
            {
                Item item = DataResources.RelayItem;
                item.ProductName = this.nazwaTextBox.Text;
                item.Amount = Convert.ToUInt32(this.iloscTextBox.Text);
                item.ItemType = (ItemType)typComboBox.SelectedValue;
                item.UnitPrice = Convert.ToDecimal(this.cenaTextBox.Text);
            }

            this.Close();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitialEdit();
        }

        private void IloscTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox.Text == "" || Convert.ToInt32(textBox.Text) < 1)
            {
                textBox.Text = "1";
            }
        }

        private void CenaTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            try
            {
                if (textBox.Text == "" || Convert.ToDecimal(textBox.Text) < 0.01M)
                {
                    textBox.Text = "0.01";
                }
                else
                {
                    textBox.Text = Convert.ToString(Math.Round(Convert.ToDouble(textBox.Text), 2));
                }
            }                        
            catch (Exception ex)
            {
                textBox.Text = "10";
            }
        }

        private static readonly Regex _regex_amount = new Regex("[^0-9]");
        private static readonly Regex _regex_price = new Regex("[^0-9,]");
        private static bool AmountIsTextAllowed(string text)
        {
            return !_regex_amount.IsMatch(text);
        }

        private static bool PriceIsTextAllowed(string text)
        {
            return !_regex_price.IsMatch(text);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender == iloscTextBox)
            {
                e.Handled = !AmountIsTextAllowed(e.Text);
            }
            else
            {
                e.Handled = !PriceIsTextAllowed(e.Text);
            }
        }
    }
}
