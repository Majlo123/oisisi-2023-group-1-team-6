using CLI.Controller;
using CLI.Model;
using CLI.Observer;
using StudentskaSluzba.Model;
using StudentskaSluzba.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.DAO
{
    public class StudentSubjectDAO
    {
       

        private readonly Storage<StudentSubject> _storage;
        private List<StudentSubject> _studentsubject;
        private SubjectController subjectcontroller = new SubjectController();
        private List<CLI.Model.Subject> subject_temp;
        public CLI.Observer.Subject StudentSubject;
        public StudentSubjectDAO()
        {
            _storage = new Storage<StudentSubject>("studentsubject.txt");
            _studentsubject = _storage.Load();
           StudentSubject=new CLI.Observer.Subject();
        }

        public int NextId()
        {
            if (_studentsubject.Count == 0)
            {
                return 0;
            }
            else
            {
                return _studentsubject.Max(s => s.id) + 1;
            }

        }
        public StudentSubject? GetStudentSubjectById(int id)
        {
            return _studentsubject.Find(s => s.id_subject == id);
        }

        public void Add(int id_stud, int id_subject)
        {
            int id = NextId();
            StudentSubject studsubject = new StudentSubject(id_stud, id_subject, id);
            _studentsubject.Add(studsubject);
            _storage.Save(_studentsubject);
            StudentSubject.NotifyObservers();
        }
        public StudentSubject RemoveStudentSubject(int id)
        {
           StudentSubject? studentsubject=GetStudentSubjectById(id);
            if (studentsubject == null) return null;
            _studentsubject.Remove(studentsubject);
            _storage.Save(_studentsubject);
            StudentSubject.NotifyObservers();
            return studentsubject;
        }

        public StudentSubject? UpdateStudentSubject(StudentSubject ss)
        {
            StudentSubject? oldstudentsubject = GetStudentSubjectById(ss.id);
            if (oldstudentsubject is null) { return null; }
            oldstudentsubject.id = ss.id;
            oldstudentsubject.id_student = ss.id_student;
            oldstudentsubject.id_subject = ss.id_subject;


            _storage.Save(_studentsubject);
            StudentSubject.NotifyObservers();
            return oldstudentsubject;
        }


        public List<StudentSubject> GetAll()
        {

            return _studentsubject;
        }

        public List<CLI.Model.Subject> GetAllById(int studentid)
        {
            _studentsubject.Clear();
            _studentsubject = _storage.Load();
            subject_temp = subjectcontroller.GetAllSubjects();
            List<CLI.Model.Subject> subjectlist = new List<CLI.Model.Subject>();
            List<StudentSubject> studentsubject1 = GetAll();
            foreach (StudentSubject st in studentsubject1)
            {
                if (st.id_student == studentid)
                {
                    CLI.Model.Subject temp;
                    temp = subject_temp.Find(ss => ss.subjectId == st.id_subject);
                    subjectlist.Add(temp);
                }
            }
            return subjectlist;
        }
        
    }
}
