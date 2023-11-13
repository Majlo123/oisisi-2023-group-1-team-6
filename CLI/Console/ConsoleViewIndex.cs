using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentskaSluzba.Console
{
    class ConsoleViewIndex
    {
        public readonly IndexDAO _indexDAO;

        public ConsoleViewIndex(IndexDAO indexDAO)
        {
            _indexDAO = indexDAO;
        }

        public void PrintIndex(List<Model.Index> indexess)
        {
            System.Console.WriteLine("Indexes: ");
            string header = " ";
            System.Console.WriteLine(header);
            foreach (Model.Index i in indexess)
            {
                System.Console.WriteLine(i);
            }
        }

        public Model.Index InputIndex()
        {
            System.Console.WriteLine("Enter id: ");
            int id = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Enter abbreviation: ");
            string abb = ConsoleViewUtils.SafeInputString();

            System.Console.WriteLine("Enter mark: ");
            int mark = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Enter year of enrollment: ");
            int year = ConsoleViewUtils.SafeInputInt();

            return new Model.Index(id, abb, mark, year);
        }

        public int InputId()
        {
            System.Console.WriteLine("Enter index id: ");
            int id = ConsoleViewUtils.SafeInputInt();
            return id;
        }

        public void showAllIndexess()
        {
            PrintIndex(_indexDAO.GetAllIndexes());
        }

        public void removeIndex()
        {
            int id = InputId();
            Model.Index? removedIndex = _indexDAO.removeIndex(id);
            if (removedIndex is null)
            {
                System.Console.WriteLine("Index not found");
                return;
            }

            System.Console.WriteLine("Index removed");
        }

        public void UpdateIndex()
        {
            int id = InputId();
            Model.Index index = InputIndex();
            index.IdIndex = id;
            Model.Index? updatedIndex = _indexDAO.UpdateIndex(index);
            if (updatedIndex == null)
            {
                System.Console.WriteLine("Index not found");
                return;
            }

            System.Console.WriteLine("Index updated");
        }

        public void AddIndex()
        {
            Model.Index index = InputIndex();
            _indexDAO.addIndex(index);
            System.Console.WriteLine("Index added");
        }
    }
}
