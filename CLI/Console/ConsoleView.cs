using StudentskaSluzba.DAO;
using StudentskaSluzba.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Model;
using StudentskaSluzba.DAO;
using StudentskaSluzba.Storage;
namespace StudentskaSluzba.Console
{
    public class ConsoleView
    {

    
        private readonly StudentsDAO _studentsDao;

        public ConsoleView(StudentsDAO studentsDao)
        {
            _studentsDao = studentsDao;
        }

        private void PrintStudent(List<Student> students)
        {
            System.Console.WriteLine("Students: ");
            string header = $"Surname {"",12} | Name {"",21} | Date {"",15} | Address {"",30} | Phone number {"",20} | Email {"",20} | Index {"",20} | Year {"",2} | Status {"",2} | Average {"",5} | Passed subjects {"",30} | Failed subjects {"",30}";
            System.Console.WriteLine(header);
            foreach (Student s in students)
            {
                System.Console.WriteLine(s);
            }
        }

        private Student InputStudent()
        {
            System.Console.WriteLine("Enter student Surname: ");
            string surname = System.Console.ReadLine(); ;
            while (surname =="")
            {
                System.Console.WriteLine("Enter valid string: ");
                surname = System.Console.ReadLine();
            }
            System.Console.WriteLine("Enter Name: ");
            string name = System.Console.ReadLine();
            while (surname == "")
            {
                System.Console.WriteLine("Enter valid string: ");
                surname = System.Console.ReadLine();
            }
            System.Console.WriteLine("Enter id: ");
            int id1=0;
            int id=ConsoleViewUtils.SafeInputInt(id1);
            

            System.Console.WriteLine("Enter date of birth: ");
            DateOnly date = DateOnly.ParseExact(System.Console.ReadLine(), "dd-MM-yyyy");

            System.Console.WriteLine("Enter address(state): ");
            string state = System.Console.ReadLine();
            System.Console.WriteLine("Enter address(city): ");
            string city = System.Console.ReadLine();
            System.Console.WriteLine("Enter address(street): ");
            string street = System.Console.ReadLine();
            System.Console.WriteLine("Enter address(number): ");
            int number1 = 0;
            int number=ConsoleViewUtils.SafeInputInt(number1);
            Address address = new Address(street, number, city, state);

            System.Console.WriteLine("Enter phone number: ");
            string phone = System.Console.ReadLine();

            System.Console.WriteLine("Enter email: ");
            string email = System.Console.ReadLine();

            System.Console.WriteLine("Enter abbreviation of major: ");
            string abb = System.Console.ReadLine();
            System.Console.WriteLine("Enter mark of major: ");
            int mark1 = 0;
            int mark=ConsoleViewUtils.SafeInputInt(mark1);
            System.Console.WriteLine("Enter year of major: ");
            int year1 = 0;
            int year=ConsoleViewUtils.SafeInputInt(year1);
            Model.Index index = new Model.Index(abb, mark, year);

            System.Console.WriteLine("Enter year of study: ");
            int yearstudy1 = 0;
            int yearstudy=ConsoleViewUtils.SafeInputInt(yearstudy1);

            System.Console.WriteLine("Enter status(B or S): ");
            string status = System.Console.ReadLine();
            while(status != "B" && status != "S" && status != "b" && status != "s")
            {
                System.Console.WriteLine("Enter valid string: ");
                status = System.Console.ReadLine();
            }

            System.Console.WriteLine("Enter average grade: ");
            int avg1 = 0;
            int avg=ConsoleViewUtils.SafeInputInt(avg1);


            return new Student(surname, name, id, date, address, phone, email, index, yearstudy, status,avg);
        }

        private int InputId()
        {
            System.Console.WriteLine("Enter student id: ");
            int id1 = 0;
            int id= ConsoleViewUtils.SafeInputInt(id1);
            return id;
        }
       
        public void RunMenu()
        {
            while (true)
            {
                ShowMenu();
                string userInput = System.Console.ReadLine() ?? "0";
                if (userInput == "0") break;
                HandleMenuInput(userInput);
            }
        }


        private void HandleMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    ShowAllStudents();
                    break;
                case "2":
                    AddStudent();
                    break;
                case "3":
                    UpdateStudents();
                    break;
                case "4":
                    RemoveStudent();
                    break;
            }
        }

        private void ShowAllStudents()
        {
            PrintStudent(_studentsDao.getAllStudents());
        }

        private void RemoveStudent()
        {
            int id = InputId();
            Student? removedVehicle = _studentsDao.removeStudent(id);
            if (removedVehicle is null)
            {
                System.Console.WriteLine("Student not found");
                return;
            }

            System.Console.WriteLine("Student removed");
        }

        private void UpdateStudents()
        {
            int id = InputId();
            Student student = InputStudent();
            student.Id = id;
            Student? updatedStudent = _studentsDao.UpdateStudent(student);
            if (updatedStudent == null)
            {
                System.Console.WriteLine("Student not found");
                return;
            }

            System.Console.WriteLine("Student updated");
        }

        private void AddStudent()
        {
            Student student = InputStudent();
            _studentsDao.addStudent(student);
            System.Console.WriteLine("Student added");
        }

       
        private void ShowMenu()
        {
            System.Console.WriteLine("\nChoose an option: ");
            System.Console.WriteLine("1: Show All students");
            System.Console.WriteLine("2: Add student");
            System.Console.WriteLine("3: Update student");
            System.Console.WriteLine("4: Remove student");
            System.Console.WriteLine("0: Close");
        }
    }
}

