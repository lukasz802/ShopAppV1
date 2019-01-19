using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopLib;

namespace ModelData
{
    public class DataResources
    {
        private static ObservableCollection<Client> clients = new ObservableCollection<Client>();
        private static ObservableCollection<Item> items = new ObservableCollection<Item>();
        private static Client tempclient = null;
        private static Item tempitem = null;

        public static Client RelayClient
        {
            get
            {
                return tempclient;
            }
            set
            {
                tempclient = value;
            }
        }

        public static Item RelayItem
        {
            get
            {
                return tempitem;
            }
            set
            {
                tempitem = value;
            }
        }

        public static ObservableCollection<Client> Clients
        {
            get
            {
                return clients;
            }
            set
            {
                clients = value;
            }
        }

        public static ObservableCollection<Item> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
            }
        }

        public static void AddItem(Item item)
        {
            items.Add(item);
        }

        public static void AddClient(Client client)
        {
            clients.Add(client);
        }

        public static void RemoveItem(Item item)
        {
            items.Remove(item);
        }

        public static void RemoveClient(Client client)
        {
            clients.Remove(client);
        }
    }
}
