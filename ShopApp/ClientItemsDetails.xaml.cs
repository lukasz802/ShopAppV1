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
    /// Logika interakcji dla klasy ClientItemsDetails.xaml
    /// </summary>
    public partial class ClientItemsDetails : Window
    {
        public ClientItemsDetails()
        {
            InitializeComponent();
            mainwindowBar.MouseLeftButtonDown += (o, e) => DragMove();
        }
        
        private void AnulujButton_Click(object sender, RoutedEventArgs e)
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

        private void DodajButton_Click(object sender, RoutedEventArgs e)
        {
            AddItemWindow addItemWindow = new AddItemWindow();
            addItemWindow.Owner = this;
            addItemWindow.ShowDialog();
        }

        private void UsunButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy chcesz usunąć zaznaczone zamówienie z bazy?", "ShopApp", MessageBoxButton.YesNo, MessageBoxImage.Question,
MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                DataResources.RemoveItem(ItemsListBox.SelectedItem as Item);
                ModelData.DataResources.RelayClient.Items.Remove(ItemsListBox.SelectedItem as Item);
            }
        }

        private void ItemsListBox_KeyDown(object sender, KeyEventArgs e)
        {
            ListBox listBox = sender as ListBox;

            if (listBox.SelectedItems.Count != 0 && e.Key == Key.Delete)
            {
                UsunButton_Click(sender, new RoutedEventArgs());
            }
        }
    }
}
