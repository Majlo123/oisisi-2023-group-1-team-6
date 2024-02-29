using CLI.DAO;
using CLI.Model;
using CLI.Observer;

namespace CLI.Controller
{
    public class ProfessorSubjectController
    {
        private readonly ProfessorSubjectDAO _professors;


        public ProfessorSubjectController()
        {
            _professors = new ProfessorSubjectDAO();


        }
        public List<ProfessorSubject> GetAllSubjectByProfessor()
        {
            return _professors.GetAll();
        }

        public void Add(string id_professor, int id_subject)
        {
            _professors.Add(id_professor, id_subject);
        }

        public void Update(ProfessorSubject professor)
        {
            _professors.UpdateProfessorSubject(professor);
        }
        public void Delete(int id)
        {
            _professors.RemoveProfessorSubject(id);
        }
        public void Subscribe(IObserver observer)
        {
            _professors.ProfessorSubject.Subscribe(observer);
        }

        public ProfessorSubject? getAllSubjectsById(int professorid)
        {
            return _professors.GetProfessorSubjectById(professorid);

        }
        public List<CLI.Model.Subject> GetAllSubjectsById(string id_professor)
        {
            return _professors.GetAllById(id_professor);
        }
    }
}
