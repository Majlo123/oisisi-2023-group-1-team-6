using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Storage;
using StudentskaSluzba.Model;
using StudentskaSluzba.DAO;

namespace StudentskaSluzba.Console
{
    public class ConsoleViewAddress
    {
        public readonly AddressDAO _addressDao;

        public ConsoleViewAddress(AddressDAO addressDao)
        {
            _addressDao = addressDao;
        }

        private Address temp;
        public void addAdresa()
        {
            Address adresa = InputAddress();
            _addressDao.addAddress(adresa);
            System.Console.WriteLine(adresa);
            temp = adresa;
        }

        public int getIdAddress()
        {
            return temp.id;
        }

        public void PrintAddress(List<Address> address)
        {
            System.Console.WriteLine("Address: ");
            string header = " ";
            System.Console.WriteLine(header);
            foreach (Address a in address)
            {
                System.Console.WriteLine(a);
            }

        }

        public Address InputAddress()
        {
            System.Console.WriteLine("Enter Address ID: ");
            int id = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Enter address(state): ");
            string state = ConsoleViewUtils.SafeInputString();
            System.Console.WriteLine("Enter address(city)");
            string city = ConsoleViewUtils.SafeInputString();
            System.Console.WriteLine("Enter address(street)");
            string street = ConsoleViewUtils.SafeInputString();
            System.Console.WriteLine("Enter address(number): ");
            int number = ConsoleViewUtils.SafeInputInt();


            return new Address(id, street, number, city, state);
        }

        public int InputId()
        {
            System.Console.WriteLine("Enter Address id: ");
            int id = ConsoleViewUtils.SafeInputInt();
            return id;
        }

        public void ShowAllAddresses()
        {
            PrintAddress(_addressDao.getAllAddresses());
        }

        public void removeAddress()
        {
            int AddressId = InputId();
            Address? removedAddress = _addressDao.removeAddress(AddressId);
            if (removedAddress is null)
            {
                System.Console.WriteLine("Address not found");
                return;
            }

            System.Console.WriteLine("Address removed");
        }

        public void UpdateAddresses()
        {

            int id = InputId();
            Address address = InputAddress();
            address.id = id;
            Address? updatedAddress = _addressDao.UpdateAddress(address);
            if (updatedAddress == null)
            {
                System.Console.WriteLine("Address not found");
                return;
            }

            System.Console.WriteLine("Address updated");
        }

        public void AddAddress()
        {
            Address address = InputAddress();
            _addressDao.addAddress(address);
            System.Console.WriteLine("Address added");
        }
    }
}
