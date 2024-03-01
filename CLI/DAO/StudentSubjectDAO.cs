using CLI.Controller;
using CLI.Model;
using StudentskaSluzba.Model;
using StudentskaSluzba.Storage;

namespace CLI.DAO
{
    public class StudentSubjectDAO
    {


        private readonly Storage<StudentSubject> _storage;
        private List<StudentSubject> _studentsubject;
        private SubjectController subjectcontroller = new SubjectController();
        private StudentController studentController = new StudentController();
        private List<Subject> subject_temp;
        private List<Student> student_temp;
        public CLI.Observer.Subject StudentSubject;
        public StudentSubjectDAO()
        {
            _storage = new Storage<StudentSubject>("studentsubject.txt");
            _studentsubject = _storage.Load();
            StudentSubject = new CLI.Observer.Subject();
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
            StudentSubject? studentsubject = GetStudentSubjectById(id);
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

        public List<Student> GetAllStudentsBySubjectId(int subjectId)
        {
            _studentsubject.Clear();
            _studentsubject = _storage.Load();
            student_temp = studentController.GetAllStudents();
            List<Student> studentList = new List<Student>();
            List<StudentSubject> studentSubject1 = GetAll();
            foreach(StudentSubject st in studentSubject1)
            {
                if(st.id_subject == subjectId)
                {
                    Student temp;
                    temp = student_temp.Find(ss => ss.Id == st.id_student);
                    studentList.Add(temp);
                }
            }
            return studentList;
        }

        public List<Subject> GetAllById(int studentid)
        {
            _studentsubject.Clear();
            _studentsubject = _storage.Load();
            subject_temp = subjectcontroller.GetAllSubjects();
            List<Subject> subjectlist = new List<Subject>();
            List<StudentSubject> studentsubject1 = GetAll();
            foreach (StudentSubject st in studentsubject1)
            {
                if (st.id_student == studentid)
                {
                    Subject temp;
                    temp = subject_temp.Find(ss => ss.subjectId == st.id_subject);
                    subjectlist.Add(temp);
                }
            }
            return subjectlist;
        }

    }
}
