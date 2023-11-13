using StudentskaSluzba.Model;
using StudentskaSluzba.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentskaSluzba.DAO
{
    class AddressDAO
    {
        private readonly List<Address> _address;
        private readonly Storage<Address> _storage;

        public AddressDAO()
        {
            _storage = new Storage<Address>("address.txt");
            _address = _storage.Load();
        }
    }
}
