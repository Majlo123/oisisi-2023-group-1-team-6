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

        public Department InputGrade()
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

            return new Grade(id,student,subject1,date);
        }

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

        public void ShowAllDepartment()
        {
            PrintDepartment(_departmentsDao.getAllDepartments());
        }

        public void removeDepartment()
        {
            int DepartmentId = InputId();
            Department? removedDepartment = _departmentsDao.removeDepartment(DepartmentId);
            if (removedDepartment is null)
            {
                System.Console.WriteLine("Department not found");
                return;
            }

            System.Console.WriteLine("Department removed");
        }

        public void UpdateDepartments()
        {

            int id = InputId();
            DepartmentsDAO departmentsDAO = new DepartmentsDAO();
            Department departmentToAdd = departmentsDAO.GetDepartmentById(id);
            if (departmentToAdd == null)
            {
                System.Console.WriteLine("Department not found");
                return;
            }
            System.Console.WriteLine("Department found");
            Department department = InputDepartment();
            _departmentsDao.addDepartment(department);
            System.Console.WriteLine("Department updated");
        }

        public void AddDepartment()
        {
            Department department = InputDepartment();
            _departmentsDao.addDepartment(department);
            System.Console.WriteLine("Department added");
        }
        public void AddProfessorToDepartment()
        {
            int id = InputId1();
            ProfessorsDAO professorDAO = new ProfessorsDAO();
            Professor professorToAdd = professorDAO.GetProfessorById(id);
            if (professorToAdd != null)
            {
                int id1 = InputId();
                DepartmentsDAO departmentsDAO = new DepartmentsDAO();
                Department? departmentsToAdd = departmentsDAO.GetDepartmentById(id1);
                if (departmentsToAdd == null)
                {
                    System.Console.WriteLine("Department not found");

                }
                else
                {

                    departmentsToAdd.Professors.Add(professorToAdd);
                    System.Console.WriteLine($"Professor {professorToAdd.Name} added to the department.");
                }
            }
            else
            {
                System.Console.WriteLine($"Professor with ID {id} does not exist.");
            }
        }

    }
}
