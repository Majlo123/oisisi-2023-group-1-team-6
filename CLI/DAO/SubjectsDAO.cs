using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Model;
using StudentskaSluzba.Model;
using StudentskaSluzba.Storage;
using CLI.Observer;


namespace StudentskaSluzba.DAO

{
    public class SubjectsDAO
    {
        private readonly List<CLI.Model.Subject> _subjects;
        private readonly Storage<CLI.Model.Subject> _storage;
        public CLI.Observer.Subject SubjectSubject;
        public SubjectsDAO()
        {
            _storage = new Storage<CLI.Model.Subject>("subject.txt");
            _subjects = _storage.Load();
            SubjectSubject=new CLI.Observer.Subject();
        }
        /* private int GenerateId()
         {
             if(_students.Count == 0) { return 0; }
             return _students[^1].Id;

         }*/
        public CLI.Model.Subject? GetSubjectById(int id)
        {
            return _subjects.Find(s => s.subjectId == id);
        }
        public void addSubject(CLI.Model.Subject subject)
        {
            bool exists = _subjects.Contains(subject);
            if (exists) return;
            //student.Id = GenerateId(); 
            _subjects.Add(subject);
            _storage.Save(_subjects);
            SubjectSubject.NotifyObservers();
        }

        public CLI.Model.Subject removeSubject(int id)
        {
            CLI.Model.Subject? subject = GetSubjectById(id);
            if (subject == null) return null;

            _subjects.Remove(subject);
            _storage.Save(_subjects);
            SubjectSubject.NotifyObservers();
            return subject;
        }
        public CLI.Model.Subject? UpdateSubject(CLI.Model.Subject subject)
        {
            CLI.Model.Subject? oldSubject = GetSubjectById(subject.subjectId);
            if (oldSubject == null) { return null; }
            oldSubject.subjectId = subject.subjectId;
            oldSubject.subjectName = subject.subjectName;
            oldSubject.yearOfStudy = subject.yearOfStudy;
            oldSubject.professor = subject.professor;
            oldSubject.ESPBPoints = subject.ESPBPoints;
            oldSubject.semester = subject.semester;
            oldSubject.StudentsPassed = subject.StudentsPassed;
            oldSubject.StudentsFailed = subject.StudentsFailed;

            _storage.Save(_subjects);
            SubjectSubject.NotifyObservers();
            return subject;

        }

        public List<CLI.Model.Subject> getAllSubjects()
        {
            return _subjects;
        }
        public List<CLI.Model.Subject> GetAllSubjects(int page, int pageSize)
        {
            IEnumerable<CLI.Model.Subject> subject = _subjects;

            subject = _subjects.Skip((page - 1) * pageSize).Take(pageSize);

            return subject.ToList();
        }
    }
}
