using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;
using System.Globalization;

namespace ShopLib
{
    public enum ItemType
    {
        Chemia = 0,
        Elektronika = 1,
        Komunikacja = 2,
        Rozrywka = 3,
        Spożywcze = 4,
        Inne = 5
    }

    public class Item : INotifyPropertyChanged
    {
        static int nextId;
        public int ItemId { get; private set; }

        private string _productName { get; set; }
        private decimal _unitPrice;
        private uint _amount;
        private ItemType _type;
        private Client client;

        public decimal Subtotal => UnitPrice * Amount;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ItemType ItemType
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                OnPropertyChanged("ItemType");
            }
        }

        public decimal UnitPrice
        {
            get
            {
                return _unitPrice;
            }
            set
            {
                if (value > 0.01M)
                {
                    _unitPrice = value;
                }
                else
                {
                    value = 0.01M;
                    _unitPrice = value;
                }
                OnPropertyChanged("UnitPrice");
            }
        }

        public uint Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                if (value > 1)
                {
                    _amount = value;
                }
                else
                {
                    value = 1;
                    _amount = value;
                }
                OnPropertyChanged("Amount");
            }
        }

        public string ProductName
        {
            get
            {
                return _productName;
            }
            set
            {
                _productName = value;
                OnPropertyChanged("ProductName");
            }
        }

        public Item()
        {
            ItemId = GenerateId();
        }

        public Item(string name, decimal price, ItemType type)
        {
            ItemId = GenerateId();
            ProductName = name;
            UnitPrice = price;
            ItemType = type;
            Amount = 1;
        }

        public Item(string name, decimal price, uint amount, ItemType type)
        {
            ItemId = GenerateId();
            ProductName = name;
            UnitPrice = price;
            ItemType = type;
            Amount = amount;
        }

        public Item(string name, decimal price, uint amount, ItemType type, Client client)
        {
            ItemId = GenerateId();
            ProductName = name;
            UnitPrice = price;
            ItemType = type;
            Amount = amount;
            Client = client;
        }

        public Client Client
        {
            get
            {
                return client;
            }
            set
            {
                client = value;
                OnPropertyChanged("Client");
            }
        }

        protected static int GenerateId()
        {
            // http://stackoverflow.com/a/9262259/1442833
            return Interlocked.Increment(ref nextId);
        }
    }
}
