using CLI.Model;
using CLI.Observer;
using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Controller
{
    public class StudentController
    {
        private readonly StudentsDAO _students;


        public StudentController()
        {
            _students = new StudentsDAO();


        }
        public List<Student> GetAllStudents()
        {
            return _students.getAllStudents();
        }
        /* public void Create(string surname, string name, DateOnly date, string street, int number, string city, string state, string phonenumber, string email, string id, string title, int workyear) { 

             Address address1 = new Address(street,number,city,state);
             Professor professor= new Professor(surname,name,date,address1,phonenumber,email,id,title,workyear);
             _professors.addProfessor(professor);
         }*/
        public void Add(Student student)
        {
            _students.addStudent(student);
        }
        public void Update(Student student)
        {
            _students.UpdateStudent(student);
        }
        public void Delete(int studentid)
        {
            _students.removeStudent(studentid);
        }
        public void Subscribe(IObserver observer)
        {
            _students.StudentSubject.Subscribe(observer);
        }

        public Student? getStudentById(int studentid)
        {
            return _students.GetStudentById(studentid);

        }
    }
}
