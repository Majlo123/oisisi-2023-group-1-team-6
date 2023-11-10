using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Model;
using StudentskaSluzba.Storage;



namespace StudentskaSluzba.DAO
    
{
    public class StudentsDAO 
    {
        private readonly List<Student> _students;
        private readonly Storage<Student> _storage;

        public StudentsDAO() {
            _storage = new Storage<Student>("students.txt");
            _students = _storage.Load();
        
        }
       /* private int GenerateId()
        {
            if(_students.Count == 0) { return 0; }
            return _students[^1].Id;

        }*/
        private Student? GetStudentById(int id)
        {
            return _students.Find(s => s.Id == id);
        }
        public void addStudent(Student student)
        {
            bool exists = _students.Contains(student);
            if (exists) return;
            //student.Id = GenerateId(); 
            _students.Add(student);
            _storage.Save(_students);
        }

        public void removeStudent(int id)
        {
            Student? student = GetStudentById(id);
            if (student == null) return;

            _students.Remove(student);
            _storage.Save(_students);
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

            _storage.Save(_students);
            return student;

        }
        
        public List<Student> getAllStudents()
        {
            return _students;
        }
        public List<Student> GetAllStudents(int page, int pageSize)
        {
            IEnumerable<Student> students = _students;

            students = _students.Skip((page-1)*pageSize).Take(pageSize);

            return students.ToList();
        }
    }
}
