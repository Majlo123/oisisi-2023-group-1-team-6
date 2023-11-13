using StudentskaSluzba.Model;
using StudentskaSluzba.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentskaSluzba.DAO
{
    public class IndexDAO
    {
        private readonly List<Model.Index> _index;
        private readonly Storage<Model.Index> _storage;

        public IndexDAO()
        {
            _storage = new Storage<Model.Index>("index.txt");
            _index = _storage.Load();
        }

        public Model.Index? GetIndexById(int id)
        {
            return _index.Find(p => p.IdIndex == id);
        }
        public void addIndex(Model.Index index)
        {
            bool exists = _index.Contains(index);
            if (exists)
            {
                System.Console.WriteLine("Index already exists, please enter another one!");
                return;
            }

            //professor.Id = GenerateId();
            _index.Add(index);
            _storage.Save(_index);
        }

        public Model.Index removeIndex(int id)
        {
            Model.Index index = GetIndexById(id);
            if (index == null)
            {
                System.Console.WriteLine("There is no index with that Id, select another one!");
                return null;
            }
            _index.Remove(index);
            _storage.Save(_index);
            return index;
        }
        public Model.Index UpdateIndex(Model.Index index)
        {
            Model.Index oldIndex = GetIndexById(index.IdIndex);
            if (oldIndex == null)
            {
                System.Console.WriteLine("Index doesnt exist, please pick another one!");
                return null;
            }
            oldIndex.IdIndex = index.IdIndex;
            oldIndex.AbbreviationOfMajor = index.AbbreviationOfMajor;
            oldIndex.MarkOfMajor = index.MarkOfMajor;
            oldIndex.YearOfEnrollment = index.YearOfEnrollment;


            _storage.Save(_index);
            return oldIndex;

        }

        public List<Model.Index> GetAllIndexes()
        {
            return _index;
        }

        public List<Model.Index> GetAllIndexes(int page, int pageSize)
        {
            IEnumerable<Model.Index> indexes = _index;

            indexes = _index.Skip((page - 1) * pageSize).Take(pageSize);

            return indexes.ToList();
        }
    }
}
