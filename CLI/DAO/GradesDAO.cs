using CLI.Observer;
using StudentskaSluzba.Model;
using StudentskaSluzba.Storage;

namespace StudentskaSluzba.DAO
{
    public class GradesDAO
    {
        private readonly List<Grade> _grade;
        private readonly Storage<Grade> _storage;
        public CLI.Observer.Subject Subject;
        public StudentsDAO studentsDao;

        public GradesDAO()
        {
            _storage = new Storage<Grade>("grades.txt");
            _grade = _storage.Load();
            Subject = new CLI.Observer.Subject();
            studentsDao = new StudentsDAO();
        }
        public void Save()
        {
            _storage.Save(_grade);
        }
        public Grade? GetGradeById(int id)
        {
            return _grade.Find(g => g.Id == id);
        }
        public void addGrade(Grade grade)
        {
            bool exists = _grade.Contains(grade);
            if (exists) return;


            _grade.Add(grade);
            _storage.Save(_grade);
            Subject.NotifyObservers();
        }

        public List<Student> GetStudentBySubjectId(int subjectId)
        {
            List<Student> student_tmp = new List<Student>();
            foreach(Grade g in GetAllGrade())
            {
                if(g.subjectId == subjectId)
                {
                    Student stud = studentsDao.GetStudentById(g.studentId);
                    student_tmp.Add(stud);
                }
            }
            return student_tmp;
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
            oldGrade.date = grade.date;



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
