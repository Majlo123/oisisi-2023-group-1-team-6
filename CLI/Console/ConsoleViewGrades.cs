using StudentskaSluzba.DAO;
using StudentskaSluzba.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Model;
using StudentskaSluzba.Storage;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using CLI.Model;

namespace StudentskaSluzba.Console
{
    public class ConsoleViewGrade
    {
        public readonly GradesDAO _gradesDao;

        public ConsoleViewGrade(GradesDAO gradesDao)
        {
            _gradesDao = gradesDao;
        }

        public void PrintGrades(List<Grade> grades)
        {
            System.Console.WriteLine("Grades: ");
            string header = " ";
            System.Console.WriteLine(header);
            foreach (Grade g in grades)
            {
                System.Console.WriteLine(g);
            }

        }

        public Grade InputGrade()
        {
            System.Console.WriteLine("Enter Grade ID: ");
            int id = ConsoleViewUtils.SafeInputInt();
            System.Console.WriteLine("Enter student id which you want to grade: ");
            int id1 = ConsoleViewUtils.SafeInputInt();
            StudentsDAO studentsDAO = new StudentsDAO();
            Student studentToAdd = studentsDAO.GetStudentById(id1);
            while (studentToAdd == null)
            {
                System.Console.WriteLine("This student doesn't exist: ");
                System.Console.WriteLine("Enter student id: ");
                id1 = ConsoleViewUtils.SafeInputInt();
                StudentsDAO studentsDAO1 = new StudentsDAO();
                studentToAdd = studentsDAO.GetStudentById(id1);
            }
            
            
            Student student = new Student(studentToAdd.Surname,studentToAdd.Name, studentToAdd.Id,
                studentToAdd.Date, studentToAdd.Address, studentToAdd.PhoneNumber, studentToAdd.Email,
                studentToAdd.Index, studentToAdd.YearOfStudy, studentToAdd.Status, studentToAdd.AvarageGrade);
            System.Console.WriteLine("Enter subject id which you want to give grade to student: ");
            int id2 = ConsoleViewUtils.SafeInputInt();
            SubjectsDAO subjectDAO = new SubjectsDAO();
            Subject subjectToAdd = subjectDAO.GetSubjectById(id2);
            while (subjectToAdd == null)
            {
                System.Console.WriteLine("This subject doesn't exist: ");
                System.Console.WriteLine("Enter subject id: ");
                id2 = ConsoleViewUtils.SafeInputInt();
                SubjectsDAO subjectDAO1 = new SubjectsDAO();
                subjectToAdd = subjectDAO.GetSubjectById(id2);
            }
<<<<<<< Updated upstream
            Subject subject = new Subject(subjectToAdd.subjectId, subjectToAdd.subjectName, subjectToAdd.yearOfStudy,
                subjectToAdd.semester, subjectToAdd.professor, subjectToAdd.ESPBPoints);
=======
            Student student = new Student(id,surname,name);
            System.Console.WriteLine("Enter subject name which you want to grade: ");
            string subject = System.Console.ReadLine();
            
            while (subject == "")
            {
                System.Console.WriteLine("Enter valid subject: ");
                subject = System.Console.ReadLine();
            }
            Subject subject1 = new Subject();
>>>>>>> Stashed changes
            System.Console.WriteLine("Enter date of the exam");
            DateOnly date = ConsoleViewUtils.SafeInputDateTime();
            System.Console.WriteLine("Enter grade(must be from 6 to 10): ");
            string grade = System.Console.ReadLine();
            while (grade != "6"&& grade != "7"&&grade != "8" && grade != "9" && grade != "10"
                && grade.ToLower() != "six" && grade.ToLower() != "seven" && grade.ToLower() != "eight"
                && grade.ToLower() != "nine" && grade.ToLower() != "ten")
            {
                System.Console.WriteLine("Enter valid grade: ");
<<<<<<< Updated upstream
                grade = System.Console.ReadLine();
            }

            return new Grade(id,student,subject,date,grade);
=======
               grade = System.Console.ReadLine();
            }

            return new Grade(id,student,subject1,date,grade);
>>>>>>> Stashed changes
        }

        public int InputId()
        {
<<<<<<< Updated upstream
            System.Console.WriteLine("Enter grade id: ");
=======
            System.Console.WriteLine("Enter Grade id: ");
>>>>>>> Stashed changes
            int id = ConsoleViewUtils.SafeInputInt();
            return id;
        }
        

        public void ShowAllGrades()
        {
            PrintGrades(_gradesDao.GetAllGrade());
        }

        public void removeGrade()
        {
            int GradeId = InputId();
<<<<<<< Updated upstream
            Grade? removedGrade = _gradesDao.removeGrade(GradeId);
            if (removedGrade is null)
            {
                System.Console.WriteLine("Grade not found");
                return;
            }

            System.Console.WriteLine("Grade removed");
=======
            Grade? removedDepartment = _gradesDao.removeGrade(GradeId);
            if (removedDepartment is null)
            {
                System.Console.WriteLine("Grade from student not found");
                return;
            }

            System.Console.WriteLine("Grade from student removed");
>>>>>>> Stashed changes
        }

        public void UpdateGrade()
        {

            int id = InputId();
<<<<<<< Updated upstream
            Grade grade = InputGrade();
            grade.Id = id;
            Grade updatedGrade = _gradesDao.UpdateGrade(grade);
            if (updatedGrade == null)
            {
                System.Console.WriteLine("Grade not found");
                return;
            }

=======
            GradesDAO GradesDAO = new GradesDAO();
            Grade gradesToAdd = GradesDAO.GetGradeById(id);
            if (gradesToAdd == null)
            {
                System.Console.WriteLine("Grade from student not found");
                return;
            }
            System.Console.WriteLine("Grade from student found");
            Grade grade = InputGrade();
            _gradesDao.addGrade(grade);
>>>>>>> Stashed changes
            System.Console.WriteLine("Grade updated");
        }

        public void AddGrade()
        {
            Grade grade = InputGrade();
            _gradesDao.addGrade(grade);
            System.Console.WriteLine("Grade added");
        }
        
<<<<<<< Updated upstream
=======
        
>>>>>>> Stashed changes

    }
}
