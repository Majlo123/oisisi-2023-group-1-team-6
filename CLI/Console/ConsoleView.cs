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
using CLI.Console;

namespace StudentskaSluzba.Console
{
    public class ConsoleView 
    {
        ConsoleViewStudent cvs;
        ConsoleViewProfessor cvp;
        ConsoleViewDepartment cvd;
        ConsoleViewSubject cvsub;
        public ConsoleView()
        {
            cvs = new ConsoleViewStudent(new StudentsDAO());
            cvp = new ConsoleViewProfessor(new ProfessorsDAO());
            cvd= new ConsoleViewDepartment(new DepartmentsDAO());
            cvsub= new ConsoleViewSubject(new SubjectsDAO());
        }
        public void RunMenu()
        {
            while (true)
            {
                ShowMenu();
                int userInput = int.Parse(System.Console.ReadLine());
                if (userInput == 0 || userInput == null) break;
                HandleMenuInput(userInput);
            }
        }

        
        private void HandleMenuInput(int input)
        {
            switch (input)
            {
                case 1:
                    StudentsShowMenu();
                    int userInput = int.Parse(System.Console.ReadLine());
                    if (userInput == 0 || userInput == null) break;
                    StudentHandleMenuInput(userInput);
                    break;
                case 2:
                    ProfessorShowMenu();
                    int userInput1 = int.Parse(System.Console.ReadLine());
                    if (userInput1 == 0 || userInput1 == null) break;
                    ProfessorHandleMenuInput(userInput1);
                    break;
                case 3:
                    SubjectShowMenu();
                    int userInput2 = int.Parse(System.Console.ReadLine());
                    if (userInput2 == 0 || userInput2 == null) break;
                    SubjectHandleMenuInput(userInput2);
                    break;
                case 4:
                    IndexShowMenu();
                    int userInput3 = int.Parse(System.Console.ReadLine());
                    if (userInput3 == 0 || userInput3 == null) break;
                    StudentHandleMenuInput(userInput3);
                    break;
                case 5:
                    ShowGradeMenu();
                    int userInput4 = int.Parse(System.Console.ReadLine());
                    if (userInput4 == 0 || userInput4 == null) break;
                    StudentHandleMenuInput(userInput4);
                    break;
                case 6:
                    ShowDeparmentMenu();
                    int userInput5 = int.Parse(System.Console.ReadLine());
                    if (userInput5 == 0 || userInput5 == null) break;
                    DepartmentHandleMenuInput(userInput5);
                    break;
                case 7:
                    ShowAddressMenu();
                    int userInput6 = int.Parse(System.Console.ReadLine());
                    if (userInput6 == 0 || userInput6 == null) break;
                    StudentHandleMenuInput(userInput6);
                    break;
            }
        }

        private void StudentHandleMenuInput(int input)
        {
            switch(input)
            {
                case 1:
                    cvs.ShowAllStudents();
                    break;
                case 2:
                    cvs.AddStudent();
                    break;
                case 3:
                    cvs.UpdateStudents();
                    break;
                case 4:
                    cvs.RemoveStudent();
                    break;
                case 5:
                    RunMenu();
                    break;
            }
        }

        private void ProfessorHandleMenuInput(int input)
        {
            switch (input)
            {
                case 1:
                    cvp.showAllProffesors();
                    break;
                case 2:
                    cvp.AddProfessor();
                    break;
                case 3:
                    cvp.UpdateProfessor();
                    break;
                case 4:
                    cvp.removeProfessor();
                    break;
                case 5:
                    RunMenu();
                    break;

            }
        }
        private void DepartmentHandleMenuInput(int input)
        {
            switch (input)
            {
                case 1:
                    cvd.ShowAllDepartment();
                    break;
                case 2:
                    cvd.AddDepartment();
                    break;
                case 3:
                    cvd.UpdateDepartments();
                    break;
                case 4:
                    cvd.removeDepartment();
                    break;
                case 5:
                    cvd.AddProfessorToDepartment();
                    break;
                case 6:
                    RunMenu();
                    break;
            }
        }
        private void SubjectHandleMenuInput(int input)
        {
            switch (input)
            {
                case 1:
                    cvsub.ShowAllSubjects();
                    break;
                case 2:
                    cvsub.AddSubject();
                    break;
                case 3:
                    cvsub.UpdateSubject();
                    break;
                case 4:
                    cvsub.RemoveSubject();
                    break;
                case 5:
                    RunMenu();
                    break;

            }
        }
        private void ProfessorShowMenu()
        {
            System.Console.WriteLine("\nChoose an option: ");
            System.Console.WriteLine("1: Show All professors: ");
            System.Console.WriteLine("2: Add professor: ");
            System.Console.WriteLine("3: Update professor: ");
            System.Console.WriteLine("4: Remove professor: ");
            System.Console.WriteLine("5: Go back");
        }
       
        private void StudentsShowMenu()
        {
            System.Console.WriteLine("\nChoose an option: ");
            System.Console.WriteLine("1: Show All students");
            System.Console.WriteLine("2: Add student");
            System.Console.WriteLine("3: Update student");
            System.Console.WriteLine("4: Remove student");
            System.Console.WriteLine("0: Go back");
        }

        private void SubjectShowMenu()
        {
            System.Console.WriteLine("\nChoose an option: ");
            System.Console.WriteLine("1: Show All subjects");
            System.Console.WriteLine("2: Add subject");
            System.Console.WriteLine("3: Update subject");
            System.Console.WriteLine("4: Remove subject");
            System.Console.WriteLine("0: Go back");
        }

        private void IndexShowMenu()
        {
            System.Console.WriteLine("\nChoose an option: ");
            System.Console.WriteLine("1: Show All indexes");
            System.Console.WriteLine("2: Add index");
            System.Console.WriteLine("3: Update index");
            System.Console.WriteLine("4: Remove index");
            System.Console.WriteLine("0: Go back");
        }

        private void ShowAddressMenu()
        {
            System.Console.WriteLine("\nChoose an option: ");
            System.Console.WriteLine("1: Show All addresses");
            System.Console.WriteLine("2: Add address");
            System.Console.WriteLine("3: Update address");
            System.Console.WriteLine("4: Remove address");
            System.Console.WriteLine("0: Go back");
        }

        private void ShowDeparmentMenu()
        {
            System.Console.WriteLine("\nChoose an option: ");
            System.Console.WriteLine("1: Show All departments");
            System.Console.WriteLine("2: Add department");
            System.Console.WriteLine("3: Update department");
            System.Console.WriteLine("4: Remove department");
            System.Console.WriteLine("5: Add professor to department");
            System.Console.WriteLine("0: Go back");
        }

        private void ShowGradeMenu()
        {
            System.Console.WriteLine("\nChoose an option: ");
            System.Console.WriteLine("1: Show All grade");
            System.Console.WriteLine("2: Add grade");
            System.Console.WriteLine("3: Update grade");
            System.Console.WriteLine("4: Remove grade");
            System.Console.WriteLine("0: Go back");
        }

        private void ShowMenu()
        {
            System.Console.WriteLine("\nChoose an option: ");
            System.Console.WriteLine("1: Student: ");
            System.Console.WriteLine("2: Professor: ");
            System.Console.WriteLine("3: Subject: ");
            System.Console.WriteLine("4: Index: ");
            System.Console.WriteLine("5: Grade: ");
            System.Console.WriteLine("6: Department: ");
            System.Console.WriteLine("7: Address: ");
            System.Console.WriteLine("0: Close");
        }
    }
}

