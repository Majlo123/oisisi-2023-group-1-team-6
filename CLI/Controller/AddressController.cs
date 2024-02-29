using CLI.Observer;
using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;

namespace CLI.Controller
{
    public class AddressController
    {

        private readonly AddressDAO _adress;

        public AddressController()
        {

            _adress = new AddressDAO();

        }
        public List<Address> GetAllAddress()
        {
            return _adress.getAllAddresses();
        }

        public void Add(Address address)
        {
            _adress.addAddress(address);
        }
        public void Update(Address address)
        {
            _adress.UpdateAddress(address);
        }
        public void Delete(int addressid)
        {
            _adress.removeAddress(addressid);
        }
        public Address? getAddressById(int addressid)
        {
            return _adress.GetAddressById(addressid);

        }
        public void Subscribe(IObserver observer)
        {
            _adress.AddressSubject.Subscribe(observer);
        }


    }
}
