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
            System.Console.WriteLine("Enter student id: ");
            int id1 = ConsoleViewUtils.SafeInputInt();
            StudentsDAO studentsDAO = new StudentsDAO();
            Student studentToAdd = studentsDAO.GetStudentById(id1);
            while (studentToAdd == null)
            {
                System.Console.WriteLine("This student doesn't exist: ");
                System.Console.WriteLine("Enter student id: ");
                id1 = ConsoleViewUtils.SafeInputInt();
                StudentsDAO studentsDAO1 = new StudentsDAO();
                studentToAdd = studentsDAO.GetStudentById(id);
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
            Subject subject = new Subject(subjectToAdd.subjectId, subjectToAdd.subjectName, subjectToAdd.yearOfStudy,
                subjectToAdd.semester, subjectToAdd.professor, subjectToAdd.ESPBPoints);
            System.Console.WriteLine("Enter date of exam (in the format MM/dd/yyyy): ");
            DateOnly date = ConsoleViewUtils.SafeInputDateTime();
            System.Console.WriteLine("Enter grade(must be from 6 to 10): ");
            string grade = System.Console.ReadLine();
            while (grade != "6"&& grade != "7"&&grade != "8" && grade != "9" && grade != "10"
                && grade.ToLower() != "six" && grade.ToLower() != "seven" && grade.ToLower() != "eight"
                && grade.ToLower() != "nine" && grade.ToLower() != "ten")
            {
                System.Console.WriteLine("Enter valid grade: ");
                grade = System.Console.ReadLine();
            }

            return new Grade(id,student,subject,date,grade);
        }

        public int InputId()
        {
            System.Console.WriteLine("Enter grade id: ");
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

            Grade? removedGrade = _gradesDao.removeGrade(GradeId);
            if (removedGrade is null)
            {
                System.Console.WriteLine("Grade not found");
                return;
            }

            System.Console.WriteLine("Grade removed");
        }

        public void UpdateGrade()
        {

            int id = InputId();
            Grade grade = InputGrade();
            grade.Id = id;
            Grade updatedGrade = _gradesDao.UpdateGrade(grade);
            if (updatedGrade == null)
            {
                System.Console.WriteLine("Grade not found");
                return;
            }

            System.Console.WriteLine("Grade updated");
        }

        public void AddGrade()
        {
            Grade grade = InputGrade();
            _gradesDao.addGrade(grade);
            System.Console.WriteLine("Grade added");
        }
        

    }
}
