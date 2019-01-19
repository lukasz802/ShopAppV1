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

namespace ShopApp
{
    /// <summary>
    /// Logika interakcji dla klasy OrderInfoWindow.xaml
    /// </summary>
    public partial class OrderInfoWindow : Window
    {
        public OrderInfoWindow()
        {
            InitializeComponent();
            mainwindowBar.MouseLeftButtonDown += (o, e) => DragMove();
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

        private void ZmienButton_Click(object sender, RoutedEventArgs e)
        {
            OrderClientDetails orderClientDetails = new OrderClientDetails();
            orderClientDetails.ClientsListBox.DataContext = DataResources.Clients;
            orderClientDetails.Owner = this;
            orderClientDetails.ShowDialog();
        }
    }
}
