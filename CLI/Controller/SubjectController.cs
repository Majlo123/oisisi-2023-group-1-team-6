using CLI.Observer;
using CLI.Model;
using StudentskaSluzba.DAO;
using Subject = StudentskaSluzba.Model.Subject;
namespace CLI.Controller
{
    public class SubjectController

    {
        private readonly SubjectsDAO _subjects;

        public SubjectController()
        {

            _subjects = new SubjectsDAO();

        }
        public List<Subject> GetAllSubjects()
        {
            return _subjects.getAllSubjects();
        }

        public void Add(Subject subject)
        {
            _subjects.addSubject(subject);
        }
        public void Update(Subject subject)
        {
            _subjects.UpdateSubject(subject);
        }
        public void Delete(int subjectid)
        {
            _subjects.removeSubject(subjectid);
        }
        public void Subscribe(IObserver observer)
        {
            _subjects.SubjectSubject.Subscribe(observer);
        }
        public Subject? getSubjectById(int subjectid)
        {
            return _subjects.GetSubjectById(subjectid);
        }

        public Subject? GetSubjectByName(string name)
        {
            return _subjects.GetSubjectByName(name);
        }
        public void Save()
        {
            _subjects.Save();
        }
        public void DeleteProfessor(int professor_id)
        {
            _subjects.removeProfessorfromSubject(professor_id);
        }
    }
}
