using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Model;
using StudentskaSluzba.Storage;



namespace GUI.DAO
    
{
    class StudentsDAO 
    {
        private readonly List<Student> _students;
        private readonly Storage<Student> _storage;

        public StudentsDAO() {
            _storage = new Storage<Student>("students.txt");
            _students = _storage.Load();
        
        }
        private int GenerateId()
        {
            if(_students.Count == 0) { return 0; }
            return _students[^1].Id;

        }

    }
}
