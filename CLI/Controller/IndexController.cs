using CLI.Observer;
using StudentskaSluzba.DAO;
using Index = StudentskaSluzba.Model.Index;

namespace CLI.Controller
{
    public class IndexController
    {
        private readonly IndexDAO _index;

        public IndexController()
        {

            _index = new IndexDAO();

        }
        public List<Index> GetAllIndexes()
        {
            return _index.GetAllIndexes();
        }

        public void Add(Index index)
        {
            _index.addIndex(index);
        }
        public void Update(Index index)
        {
            _index.UpdateIndex(index);
        }
        public void Delete(int addressid)
        {
            _index.removeIndex(addressid);
        }
        public void Subscribe(IObserver observer)
        {
            _index.IndexSubject.Subscribe(observer);
        }

        public Index? getIndexById(int addressid)
        {
            return _index.GetIndexById(addressid);

        }
    }
}
