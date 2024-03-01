using CLI.DAO;
using CLI.Model;
using CLI.Observer;
using StudentskaSluzba.Model;

namespace CLI.Controller
{
    public class StudentSubjectController
    {
        private readonly StudentSubjectDAO _students;


        public StudentSubjectController()
        {
            _students = new StudentSubjectDAO();


        }
        public List<StudentSubject> GetAllStudentSubject()
        {
            return _students.GetAll();
        }

        public void Add(int id_student, int id_subject)
        {
            _students.Add(id_student, id_subject);
        }

        public void Update(StudentSubject student)
        {
            _students.UpdateStudentSubject(student);
        }
        public void Delete(int id)
        {
            _students.RemoveStudentSubject(id);
        }
        public void Subscribe(IObserver observer)
        {
            _students.StudentSubject.Subscribe(observer);
        }

        public StudentSubject? getStudentSubjectsById(int studentid)
        {
            return _students.GetStudentSubjectById(studentid);

        }

        public List<Student> getAllStudentsBySubjectId(int subjectid)
        {
            return _students.GetAllStudentsBySubjectId(subjectid);
        }
        public List<StudentskaSluzba.Model.Subject> GetAllSubjectsById(int id_student)
        {
            return _students.GetAllById(id_student);
        }
    }
}
