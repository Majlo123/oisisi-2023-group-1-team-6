using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Controls;
using StudentskaSluzba.Model;
using StudentskaSluzba.Storage;



namespace CLI.DAO
    
{
    public class StudentsDAO 
    {
        private readonly List<Student> _students;
        private readonly Storage<Student> _storage;

        public StudentsDAO() {
            _storage = new Storage<Student>("students.txt");
            _students = _storage.Load();
        
        }
        private int GenerateId()
        {
            if(_students.Count == 0) { return 0; }
            return _students[^1].Id;

        }
        private Student? GetStudentById(int id)
        {
            return _students.Find(s => s.Id == id);
        }
        public Student addStudent(Student student)
        {
            student.Id = GenerateId(); 
            _students.Add(student);
            _storage.Save(_students);
            return student;
        }
        public Student? UpdateStudent(Student student)
        {
            Student? oldStudent = GetStudentById(student.Id);
            if(oldStudent == null) { return null; }
            oldStudent.Surname = student.Surname;
            oldStudent.Name= student.Name;
            oldStudent.Date= student.Date;
            oldStudent.Address= student.Address;
            oldStudent.PhoneNumber= student.PhoneNumber;
            oldStudent.Email= student.Email;
            oldStudent.YearOfStudy= student.YearOfStudy;
            oldStudent.Status= student.Status; 
            oldStudent.AvarageGrade= student.AvarageGrade;
            oldStudent.PassedSubjects= student.PassedSubjects;
            oldStudent.FailedSubjects= student.FailedSubjects;

            return student;

        }
        public List<Student> GetAllStudents()
        {
            return _students;
        }
    }
}
