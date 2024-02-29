using CLI.DAO;
using CLI.Model;
using CLI.Observer;
using StudentskaSluzba.Model;

namespace CLI.Controller
{
    public class ProfessorDepartmentController
    {
        private readonly ProfessorDepartmentDAO _professors;


        public ProfessorDepartmentController()
        {
            _professors = new ProfessorDepartmentDAO();


        }
        public List<ProfessorDepartment> GetAllProfessorDepartments()
        {
            return _professors.GetAll();
        }

        public void Add(string id_professor, int id_subject)
        {
            _professors.Add(id_professor, id_subject);
        }

        public void Update(ProfessorDepartment professor)
        {
            _professors.UpdateProfessorDepartment(professor);
        }
        public void Delete(int id)
        {
            _professors.RemoveProfessorDepartment(id);
        }
        public void Subscribe(IObserver observer)
        {
            _professors.ProfessorDepartmentSubject.Subscribe(observer);
        }

        public ProfessorDepartment? getProfessorDepartmentById(int departmentid)
        {
            return _professors.GetProfessorDepartmentById(departmentid);

        }
        public List<Professor> GetAllProfessorsById(int id_department)
        {
            return _professors.GetAllById(id_department);
        }
    }
}
