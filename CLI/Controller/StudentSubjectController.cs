using CLI.DAO;
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
    public class StudentSubjectController
    {
        private readonly StudentSubjectDAO _students;
       

        public StudentSubjectController()
        {
            _students = new StudentSubjectDAO();

            
        }
        public List<StudentSubject> GetAllSubjectByStudent()
        {
            return _students.GetAll();
        }
        
        public void Add(int id_student,int id_subject)
        {
            _students.Add(id_student,id_subject);
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

        public StudentSubject? getAllSubjectsById(int studentid)
        {
            return _students.GetStudentSubjectById(studentid);

        }
        public List<CLI.Model.Subject> GetAllSubjectsById(int id_student) { 
            return _students.GetAllById(id_student);
        }
    }
}
