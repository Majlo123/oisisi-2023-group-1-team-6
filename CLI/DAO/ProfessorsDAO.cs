using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Model;
using StudentskaSluzba.Storage;



namespace GUI.DAO

{
    public class ProfessorsDAO
    {
        private readonly List<Professor> _professors;
        private readonly Storage<Professor> _storage;

        public ProfessorsDAO()
        {
            _storage = new Storage<Professor>("professors.txt");
            _professors = _storage.Load();

        }
        private Professor? GetProfessorById(int id)
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
        }

        public void removeProfessor(int id)
        {
            Professor? professor = GetProfessorById(id);
            if (professor != null) return;

            _professors.Remove(professor);
            _storage.Save(_professors);
        }
        public void UpdateProfessor(Professor professor)
        {
            Professor? oldProfessor = GetProfessorById(professor.Id);
            if (oldProfessor == null) return;
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
