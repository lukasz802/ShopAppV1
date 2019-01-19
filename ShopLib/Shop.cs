using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLib
{
    public class Shop
    {
        private List<Client> clients = new List<Client>();

        public List<Client> Clients
        {
            get
            {
                // Copy of original "clients"
                return new List<Client>(clients);
            }
        }

        public Client RegisterClient(string firstName, string lastName, string code, string city, string street,
            string streetnumber, string flatnumber, string phonenumber, string email)
        {
            Client client = new Client(firstName, lastName, code, city, street, streetnumber, flatnumber, phonenumber, email);
            clients.Add(client);
            return client;
        }

        public void RemoveClient(Client client)
        {
            clients.Remove(client);
        }

        public void AddItem(Client client, Item item)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            client.AddItem(item);
        }

        public void RemoveItem(Client client, Item item)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            client.RemoveItem(item);
        }

        public void RemoveItemById(Client client, int itemId)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            client.RemoveItemById(itemId);
        }

        public decimal TotalPriceFor(Client client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            decimal total = 0.0m;
            foreach (Item item in client.Items)
            {
                total += item.Subtotal;
            }
            return total;
        }
    }
}
