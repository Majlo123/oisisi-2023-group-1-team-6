using StudentskaSluzba.Model;
using StudentskaSluzba.Storage;


namespace StudentskaSluzba.DAO

{
    public class SubjectsDAO
    {
        private readonly List<Subject> _subjects;
        private readonly Storage<Subject> _storage;
        public CLI.Observer.Subject SubjectSubject;
        public SubjectsDAO()
        {
            _storage = new Storage<Subject>("subject.txt");
            _subjects = _storage.Load();
            SubjectSubject = new CLI.Observer.Subject();
        }
        /* private int GenerateId()
         {
             if(_students.Count == 0) { return 0; }
             return _students[^1].Id;

         }*/
        public void Save()
        {
            _storage.Save(_subjects);
        }
        public Subject? GetSubjectById(int id)
        {
            return _subjects.Find(s => s.subjectId == id);
        }

        public StudentskaSluzba.Model.Subject? GetSubjectByName(string name)
        {
            return _subjects.Find(s => s.subjectName == name);
        }
        public void addSubject(Subject subject)
        {
            bool exists = _subjects.Contains(subject);
            if (exists) return;
            //student.Id = GenerateId(); 
            _subjects.Add(subject);
            _storage.Save(_subjects);
            SubjectSubject.NotifyObservers();
        }

        public Subject removeSubject(int id)
        {
            Subject? subject = GetSubjectById(id);
            if (subject == null) return null;

            _subjects.Remove(subject);
            _storage.Save(_subjects);
            SubjectSubject.NotifyObservers();
            return subject;
        }
        public Subject removeProfessorfromSubject(int subjectId)
        {
            Subject? subject = GetSubjectById(subjectId);
            if (subject == null) return null;


            subject.professor_Id = "";

            _storage.Save(_subjects);
            SubjectSubject.NotifyObservers();

            return subject;
        }

        public Subject? UpdateSubject(Subject subject)
        {
            Subject? oldSubject = GetSubjectById(subject.subjectId);
            if (oldSubject == null) { return null; }
            oldSubject.subjectId = subject.subjectId;
            oldSubject.subjectName = subject.subjectName;
            oldSubject.yearOfStudy = subject.yearOfStudy;
            oldSubject.professor_Id = subject.professor_Id;
            oldSubject.ESPBPoints = subject.ESPBPoints;
            oldSubject.semester = subject.semester;
            oldSubject.StudentsPassed = subject.StudentsPassed;
            oldSubject.StudentsFailed = subject.StudentsFailed;

            _storage.Save(_subjects);
            SubjectSubject.NotifyObservers();
            return subject;

        }

        public List<Subject> getAllSubjects()
        {
            return _subjects;
        }
        public List<Subject> GetAllSubjects(int page, int pageSize)
        {
            IEnumerable<Subject> subject = _subjects;

            subject = _subjects.Skip((page - 1) * pageSize).Take(pageSize);

            return subject.ToList();
        }
    }
}
