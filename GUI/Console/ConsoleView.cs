using StudentskaSluzba.DAO;
using StudentskaSluzba.Console;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Model;
using StudentskaSluzba.Storage;
namespace StudentskaSluzba.Console
{
    class ConsoleView
    {

    
        private readonly StudentsDAO _studentsDao;

        public ConsoleView(StudentsDAO studentsDao)
        {
            _studentsDao = studentsDao;
        }

        private void PrintStudent(List<Student> vehicles)
        {
            System.Console.WriteLine("Students: ");
            string header = $"ID {"",12} | Name {"",21}";
            System.Console.WriteLine(header);
            foreach (Student s in vehicles)
            {
                System.Console.WriteLine(s);
            }
        }

        private Student InputStudent()
        {
            System.Console.WriteLine("Enter student Surname: ");
            string surname = System.Console.ReadLine() ?? string.Empty;

            System.Console.WriteLine("Enter ID: ");
            int id = ConsoleViewUtils.SafeInputInt();

            return new Student(surname, id);
        }

        private int InputId()
        {
            System.Console.WriteLine("Enter vehicle id: ");
            return ConsoleViewUtils.SafeInputInt();
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
                    ShowAllVehicles();
                    break;
                case "2":
                    AddVehicle();
                    break;
                case "3":
                    UpdateVehicle();
                    break;
                case "4":
                    RemoveVehicle();
                    break;
                case "5":
                    ShowAndSortVehicles();
                    break;
            }
        }

        private void ShowAllVehicles()
        {
            PrintVehicles(_vehiclesDao.GetAllVehicles());
        }

        private void RemoveVehicle()
        {
            int id = InputId();
            Vehicle? removedVehicle = _vehiclesDao.RemoveVehicle(id);
            if (removedVehicle is null)
            {
                System.Console.WriteLine("Vehicle not found");
                return;
            }

            System.Console.WriteLine("Vehicle removed");
        }

        private void UpdateVehicle()
        {
            int id = InputId();
            Vehicle vehicle = InputVehicle();
            vehicle.Id = id;
            Vehicle? updatedVehicle = _vehiclesDao.UpdateVehicle(vehicle);
            if (updatedVehicle == null)
            {
                System.Console.WriteLine("Vehicle not found");
                return;
            }

            System.Console.WriteLine("Vehicle updated");
        }

        private void AddStudent()
        {
            Student student = InputStudent();
            _studentsDao.AddStudent(student);
            System.Console.WriteLine("Vehicle added");
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

