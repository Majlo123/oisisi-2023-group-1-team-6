using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Model;
using StudentskaSluzba.Storage;
using CLI.Observer;


namespace StudentskaSluzba.DAO

{
    public class ProfessorsDAO
    {

        private readonly List<Professor> _professors;
        private readonly Storage<Professor> _storage;
        public Subject ProfessorSubject;
        public ProfessorsDAO()
        {
            _storage = new Storage<Professor>("professors.txt");
            _professors = _storage.Load();
            ProfessorSubject = new Subject();

        }
        public Professor? GetProfessorById(string id)
        {
            return _professors.Find(p => p.Id == id);
        }
        public void addProfessor(Professor professor)
        {
            bool exists = _professors.Contains(professor);
            if (exists) return;

            //professor.Id = GenerateId();
            _professors.Add(professor);
            _storage.Save(_professors);
            ProfessorSubject.NotifyObservers();
        }

        public Professor removeProfessor(string id)
        {
            Professor? professor = GetProfessorById(id);
            if (professor == null) return null;

            _professors.Remove(professor);
            _storage.Save(_professors);
            ProfessorSubject.NotifyObservers();
            return professor;

            /*Model.Index index = GetIndexById(id);
            if (index != null)
            {
                System.Console.WriteLine("There is no index with that Id, select another one!");
                return null;
            }
            _index.Remove(index);
            _storage.Save(_index);
            return index;*/
        }
        public Professor UpdateProfessor(Professor professor)
        {
            Professor? oldProfessor = GetProfessorById(professor.Id);
            if (oldProfessor == null) return null;
            oldProfessor.Surname = professor.Surname;
            oldProfessor.Name = professor.Name;
            oldProfessor.Date = professor.Date;
            oldProfessor.Address = professor.Address;
            oldProfessor.PhoneNumber = professor.PhoneNumber;
            oldProfessor.Email = professor.Email;
            oldProfessor.Id=professor.Id;
            oldProfessor.Title = professor.Title;
            oldProfessor.WorkYear = professor.WorkYear;
            oldProfessor.Subjects = professor.Subjects;

            _storage.Save(_professors);
            ProfessorSubject.NotifyObservers();

            return professor;

        }
        public List<Professor> GetAllProfessors()
        {
            return _professors;
        }

        public List<Professor> GetAllProfessors(int page, int pageSize)
        {
            IEnumerable < Professor > professor = _professors;

            professor = _professors.Skip((page - 1) * pageSize).Take(pageSize);

            return professor.ToList();
        }
    }
}
