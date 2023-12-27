using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Observer;
using StudentskaSluzba.Model;
using StudentskaSluzba.Storage;



namespace StudentskaSluzba.DAO
    
{
    public class StudentsDAO 
    {
        private readonly List<Student> _students;
        private readonly Storage<Student> _storage;

        public Subject StudentSubject;

        public StudentsDAO() {
            _storage = new Storage<Student>("students.txt");
            _students = _storage.Load();
            StudentSubject = new Subject();
        }
        private int GenerateId()
        {
            if(_students.Count == 0) { return 0; }
            return _students[^1].Id;

        }
        public Student? GetStudentById(int id)
        {
            return _students.Find(s => s.Id == id);
        }
        public Student addStudent(Student student)
        {
            bool exists = _students.Contains(student);
            if (exists) return null;
            //student.Id = GenerateId(); 
            _students.Add(student);
            _storage.Save(_students);
            StudentSubject.NotifyObservers();
            return student;
        }
        public void Save()
        {
            _storage.Save(_students);
        }
        public Student removeStudent(int id)
        {
            Student? student = GetStudentById(id);
            if (student == null) return null;

            _students.Remove(student);
            _storage.Save(_students);
            StudentSubject.NotifyObservers();
            return student;
        }
        public Student? UpdateStudent(Student student)
        {
            Student? oldStudent = GetStudentById(student.Id);
            if(oldStudent is null) { return null; }
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
            StudentSubject.NotifyObservers();
            return oldStudent;

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
