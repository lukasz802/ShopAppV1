using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ShopLib
{
    public class Client : INotifyPropertyChanged
    {
        public int ClientId { get; private set; }
        private string _firstName;
        private string _lastName;
        private string _code;
        private string _city;
        private string _street;
        private string _streetNumber;
        private string _flatNumber;
        private string _phoneNumber;
        private string _emailAddress;

        private ObservableCollection<Item> items = new ObservableCollection<Item>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }   
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public string Code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
                OnPropertyChanged("Code");
            }
        }

        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
                OnPropertyChanged("City");
            }
        }

        public string Street
        {
            get
            {
                return _street;
            }
            set
            {
                _street = value;
                OnPropertyChanged("Street");
            }
        }

        public string StreetNumber
        {
            get
            {
                return _streetNumber;
            }
            set
            {
                _streetNumber = value;
                OnPropertyChanged("StreetNumber");
            }
        }

        public string FlatNumber
        {
            get
            {
                return _flatNumber;
            }
            set
            {
                _flatNumber = value;
                OnPropertyChanged("FlatNumber");
            }
        }

        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }

        public string EmailAddress
        {
            get
            {
                return _emailAddress;
            }
            set
            {
                _emailAddress = value;
                OnPropertyChanged("EmailAddress");
            }
        }

        public ObservableCollection<Item> Items
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

        public void AddItem(Item item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }

        public void RemoveItemById(int id)
        {
            foreach (Item tmp in items)
            {
                if (tmp.ItemId == id)
                {
                    items.Remove(tmp);
                    return;
                }
            }
        }

        public Client(string firstName, string lastName, string code, string city, string street, string streetnumber,
            string flatnumber, string phonenumber, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Code = code;
            City = city;
            Street = street;
            StreetNumber = streetnumber;
            FlatNumber = flatnumber;
            EmailAddress = email;
            PhoneNumber = phonenumber;
        }
    }
}
