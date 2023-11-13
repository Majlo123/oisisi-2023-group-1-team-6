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

        /*public Grade InputGrade()
        {
            System.Console.WriteLine("Enter Grade ID: ");
            int id = ConsoleViewUtils.SafeInputInt();
            System.Console.WriteLine("Enter Student ID which you want to grade: ");
            int id1= ConsoleViewUtils.SafeInputInt();
            System.Console.WriteLine("Enter Student name which you want to grade: ");
            string name = System.Console.ReadLine();
            while (name == "")
            {
                System.Console.WriteLine("Enter valid name: ");
                name = System.Console.ReadLine();
            }
            System.Console.WriteLine("Enter Student surname which you want to grade: ");
            string surname = System.Console.ReadLine();
            while (surname == "")
            {
                System.Console.WriteLine("Enter valid surname: ");
                surname = System.Console.ReadLine();
            }
            Student student = new Student(id,surname,name);
            System.Console.WriteLine("Enter subject name which you want to grade: ");
            string subject = System.Console.ReadLine();
            Subject subject1 = new Subject(subject);
            while (subject == "")
            {
                System.Console.WriteLine("Enter valid subject: ");
                subject = System.Console.ReadLine();
            }

            System.Console.WriteLine("Enter date of the exam");
            DateOnly date = ConsoleViewUtils.SafeInputDateTime();
            System.Console.WriteLine("Enter grade(must be from 6 to 10): ");
            string grade = System.Console.ReadLine();
            while (grade != "6"&& grade != "7"&&grade != "8" && grade != "9" && grade != "10"
                && grade.ToLower() != "six" && grade.ToLower() != "seven" && grade.ToLower() != "eight"
                && grade.ToLower() != "nine" && grade.ToLower() != "ten")
            {
                System.Console.WriteLine("Enter valid subject: ");
                subject = System.Console.ReadLine();
            }

            return new Grade(id,subject,subject1,date,grade);
        }*/

        public int InputId()
        {
            System.Console.WriteLine("Enter Department id: ");
            int id = ConsoleViewUtils.SafeInputInt();
            return id;
        }
        public int InputId1()
        {
            System.Console.WriteLine("Enter Proffesor id: ");
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

        /*public void UpdateGrade()
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
        }*/

        /*public void AddGrade()
        {
            Grade grade = InputGrade();
            _gradesDao.addGrade(grade);
            System.Console.WriteLine("Grade added");
        }*/
        

    }
}
