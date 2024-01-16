using CLI.Observer;
using StudentskaSluzba.Model;
using StudentskaSluzba.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentskaSluzba.DAO
{
    public class GradesDAO
    {
        private readonly List<Grade> _grade;
        private readonly Storage<Grade> _storage;
        public Subject Subject;

        public GradesDAO()
        {
            _storage = new Storage<Grade>("grades.txt");
            _grade = _storage.Load();
            Subject=new Subject();
        }
        public void Save()
        {
            _storage.Save(_grade);
        }
        public Grade? GetGradeById(int id)
        {
            return _grade.Find(g=> g.Id == id);
        }
        public void addGrade(Grade grade)
        {
            bool exists = _grade.Contains(grade);
            if (exists) return;
            
            
            _grade.Add(grade);
            _storage.Save(_grade);
            Subject.NotifyObservers();
        }
        public List<Grade> GetGradesByIdStudent(int id)
        {
            List<Grade> grades_tmp = new List<Grade>();
            foreach (Grade g in _grade)
            {
                if (g.studentId == id)
                {
                    grades_tmp.Add(g);
                }
            }
            return grades_tmp;
        }
        public Grade removeGrade(int id)
        {
            Grade grade = GetGradeById(id);
            if (grade == null)
            {
                System.Console.WriteLine("There is no grade for this subject, select another one!");
                return null;
            }
            _grade.Remove(grade);
            _storage.Save(_grade);
            Subject.NotifyObservers();
            return grade;
        }
        public Grade UpdateGrade(Grade grade)
        {
            Grade oldGrade = GetGradeById(grade.Id);
            if (oldGrade == null)
            {
                System.Console.WriteLine("There is no grade for this subject, select another one!");
                return null;
            }
            oldGrade.Id = grade.Id;
            oldGrade.studentId = grade.studentId;
            oldGrade.subjectId = grade.subjectId;
            oldGrade.grades = grade.grades;
            oldGrade.date  = grade.date;
           


            _storage.Save(_grade);
            Subject.NotifyObservers();
            return oldGrade;

        }

        public List<Grade> GetAllGrade()
        {
            return _grade;
        }

        public List<Grade> GetAllGrades(int page, int pageSize)
        {
            IEnumerable<Grade> grades = _grade;

            grades = _grade.Skip((page - 1) * pageSize).Take(pageSize);

            return grades.ToList();
        }
    }
}
