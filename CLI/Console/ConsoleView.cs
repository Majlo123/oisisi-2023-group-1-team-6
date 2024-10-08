﻿using CLI.Console;
using StudentskaSluzba.DAO;

namespace StudentskaSluzba.Console
{
    public class ConsoleView
    {
        ConsoleViewStudent cvs;
        ConsoleViewProfessor cvp;
        ConsoleViewDepartment cvd;
        ConsoleViewSubject cvsub;
        ConsoleViewIndex cvi;
        ConsoleViewAddress cva;
        ConsoleViewGrade cvg;
        public ConsoleView()
        {
            cvs = new ConsoleViewStudent(new StudentsDAO());
            cvp = new ConsoleViewProfessor(new ProfessorsDAO());
            cvd = new ConsoleViewDepartment(new DepartmentsDAO());
            cvsub = new ConsoleViewSubject(new SubjectsDAO());
            cvi = new ConsoleViewIndex(new IndexDAO());
            cva = new ConsoleViewAddress(new AddressDAO());
            cvg = new ConsoleViewGrade(new GradesDAO());
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
                    IndexHandleMenuInput(userInput3);
                    break;
                case 5:
                    ShowGradeMenu();
                    int userInput4 = int.Parse(System.Console.ReadLine());
                    if (userInput4 == 0 || userInput4 == null) break;
                    GradeHandleMenuInput(userInput4);
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
                    AddressHandleMenuInput(userInput6);
                    break;
            }
        }

        private void StudentHandleMenuInput(int input)
        {
            switch (input)
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

        private void IndexHandleMenuInput(int input)
        {
            switch (input)
            {
                case 1:
                    cvi.showAllIndexess();
                    break;
                case 2:
                    cvi.AddIndex();
                    break;
                case 3:
                    cvi.UpdateIndex();
                    break;
                case 4:
                    cvi.removeIndex();
                    break;
                case 5:
                    RunMenu();
                    break;
            }

        }

        private void AddressHandleMenuInput(int input)
        {
            switch (input)
            {
                case 1:
                    cva.ShowAllAddresses();
                    break;
                case 2:
                    cva.AddAddress();
                    break;
                case 3:
                    cva.UpdateAddresses();
                    break;
                case 4:
                    cva.removeAddress();
                    break;
                case 0:
                    RunMenu();
                    break;
            }
        }
        private void GradeHandleMenuInput(int input)
        {
            switch (input)
            {
                case 1:
                    cvg.ShowAllGrades();
                    break;
                case 2:
                    cvg.AddGrade();
                    break;
                case 3:
                    cvg.UpdateGrade();
                    break;
                case 4:
                    cvg.removeGrade();
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

