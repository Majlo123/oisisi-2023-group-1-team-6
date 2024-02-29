using CLI.Observer;
using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;

namespace CLI.Controller
{
    public class GradeController
    {
        private readonly GradesDAO _grades;

        public GradeController()
        {

            _grades = new GradesDAO();

        }
        public List<Grade> GetAllSubjects()
        {
            return _grades.GetAllGrade();
        }
        public List<Grade> GetAllGradesByStudent(int studentId)
        {
            return _grades.GetGradesByIdStudent(studentId);
        }
        public void Add(Grade subject)
        {
            _grades.addGrade(subject);
        }
        public void Update(Grade subject)
        {
            _grades.UpdateGrade(subject);
        }
        public void Delete(int subjectid)
        {
            _grades.removeGrade(subjectid);
        }
        public void Subscribe(IObserver observer)
        {
            _grades.Subject.Subscribe(observer);
        }
        public Grade? getSubjectById(int subjectid)
        {
            return _grades.GetGradeById(subjectid);
        }
        public void Save()
        {
            _grades.Save();
        }

    }
}

