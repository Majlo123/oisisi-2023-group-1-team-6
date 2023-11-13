using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Model;
using StudentskaSluzba.Storage;

namespace StudentskaSluzba.DAO
{
    public class AddressDAO
    {
        private readonly List<Address> _address;
        private readonly Storage<Address> _storage;

        public AddressDAO()
        {
            _storage = new Storage<Address>("address.txt");
            _address = _storage.Load();
        }

        public Address? GetAddressById(int id)
        {
            return _address.Find(a => a.id == id);
        }

        public Address addAddress(Address address)
        {
            bool exists = _address.Contains(address);
            if (exists)
            {
                System.Console.WriteLine("Address already exists. Enter another one!");
                return null;
            }//student.Id = GenerateId(); 
            _address.Add(address);
            _storage.Save(_address);
            return address;
        }

        public Address? removeAddress(int id)
        {
            Address? address = GetAddressById(id);
            if (address == null)
            {
                System.Console.WriteLine("There isn't an address with that id.Please enter another one!");
                return null;
            }
            _address.Remove(address);
            _storage.Save(_address);
            return address;
        }

        public Address? UpdateAddress(Address address)
        {
            Address oldAddress = GetAddressById(address.id);
            if (oldAddress is null) {
                System.Console.WriteLine("There is no address with that id. Enter another one!");
                return null; 
            }
            oldAddress.id = address.id;
            oldAddress.Street = address.Street;
            oldAddress.Number = address.Number;
            oldAddress.City = address.City;
            oldAddress.State = address.State;

            _storage.Save(_address);
            return oldAddress;

        }

        public List<Address> getAllAddresses()
        {
            return _address;
        }
        public List<Address> GetAllDepartments(int page, int pageSize)
        {
            IEnumerable<Address> address = _address;

            address = _address.Skip((page - 1) * pageSize).Take(pageSize);

            return address.ToList();
        }
    }
}
