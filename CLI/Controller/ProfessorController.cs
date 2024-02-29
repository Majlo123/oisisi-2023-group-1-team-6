using CLI.Observer;
using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;

namespace CLI.Controller
{
    public class ProfessorController
    {
        private readonly ProfessorsDAO _professors;


        public ProfessorController()
        {
            _professors = new ProfessorsDAO();


        }
        public List<Professor> GetAllProfessors()
        {
            return _professors.GetAllProfessors();
        }
        /* public void Create(string surname, string name, DateOnly date, string street, int number, string city, string state, string phonenumber, string email, string id, string title, int workyear) { 

             Address address1 = new Address(street,number,city,state);
             Professor professor= new Professor(surname,name,date,address1,phonenumber,email,id,title,workyear);
             _professors.addProfessor(professor);
         }*/
        public List<Professor> GetProfessorsById(string id)
        {
            return _professors.GetProfessorsByID(id);
        }
        public void Add(Professor professor)
        {
            _professors.addProfessor(professor);
        }
        public void Update(Professor professor)
        {
            _professors.UpdateProfessor(professor);
        }
        public void Delete(string professorid)
        {
            _professors.removeProfessor(professorid);
        }
        public void Subscribe(IObserver observer)
        {
            _professors.ProfessorSubject.Subscribe(observer);
        }
        public void Save()
        {
            _professors.Save();
        }
        public Professor? getProfessorById(string professorid)
        {
            return _professors.GetProfessorById(professorid);
        }
    }
}
