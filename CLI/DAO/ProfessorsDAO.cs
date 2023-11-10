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
        private int GenerateId()
        {
            if (_professors.Count == 0) { return 0; }
            return _professors[^1].Id;

        }
        private Professor? GetProfessorById(int id)
        {
            return _professors.Find(p => p.Id == id);
        }
        public Professor addStudent(Professor professor)
        {
            professor.Id = GenerateId();
            _professors.Add(professor);
            _storage.Save(_professors);
            return professor;
        }
        public Professor? UpdateProfessor(Professor professor)
        {
            Professor? oldProfessor = GetProfessorById(professor.Id);
            if (oldProfessor == null) { return null; }
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



            return professor;

        }
        public List<Professor> GetAllProfessors()
        {
            return _professors;
        }
    }
}
