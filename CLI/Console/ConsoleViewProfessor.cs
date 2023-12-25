using CLI.Model;
using StudentskaSluzba;
using StudentskaSluzba.Console;
using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Console
{
    public class ConsoleViewProfessor
    {
        public readonly ProfessorsDAO _professorsDao;

        public ConsoleViewProfessor(ProfessorsDAO professorsDao)
        {
            _professorsDao = professorsDao;
        }

        public void PrintProfessor(List<Professor> professors)
        {
            System.Console.WriteLine("Professors: ");
            string header = " ";
            System.Console.WriteLine(header);
            foreach (Professor p in professors)
            {
                System.Console.WriteLine(p);
            }
        }

        public Professor InputProfessor()
        {
            System.Console.WriteLine("Enter proffesor Surname: ");
            string surname = System.Console.ReadLine(); ;
            while (surname == "")
            {
                System.Console.WriteLine("Enter valid string: ");
                surname = System.Console.ReadLine();
            }
            System.Console.WriteLine("Enter proffesor Name: ");
            string name = System.Console.ReadLine();
            while (surname == "")
            {
                System.Console.WriteLine("Enter valid string: ");
                surname = System.Console.ReadLine();
            }

            System.Console.WriteLine("Enter date of birth (in the format MM/dd/yyyy): ");
            DateOnly date = ConsoleViewUtils.SafeInputDateTime();

            System.Console.WriteLine("Enter address id: ");
            int id = ConsoleViewUtils.SafeInputInt();
            System.Console.WriteLine("Enter address(state): ");
            string state = ConsoleViewUtils.SafeInputString();
            System.Console.WriteLine("Enter address(city): ");
            string city = ConsoleViewUtils.SafeInputString();
            System.Console.WriteLine("Enter address(street): ");
            string street = ConsoleViewUtils.SafeInputString(); ;
            System.Console.WriteLine("Enter address(number): ");
            int number = ConsoleViewUtils.SafeInputInt();
            Address address = new Address(id, street,number,city,state);

            System.Console.WriteLine("Enter phone number: ");
            string phone = ConsoleViewUtils.SafeInputString(); ;

            System.Console.WriteLine("Enter email: ");
            string email = ConsoleViewUtils.SafeInputString();

            System.Console.WriteLine("Enter id: ");
            string id1 = ConsoleViewUtils.SafeInputString();

            System.Console.WriteLine("Enter title: ");
            string title = ConsoleViewUtils.SafeInputString(); ;

            System.Console.WriteLine("Enter years of working: ");
            int years = ConsoleViewUtils.SafeInputInt();

            Professor professor = new Professor(surname, name, date, address, phone, email, id1, title, years);

            System.Console.WriteLine("Enter subjects he teaches.When done, press 0: ");
            string subjectName = "";
            while(subjectName != "")
            {
                subjectName = ConsoleViewUtils.SafeInputString();
            }
            /*string[] lines = File.ReadAllLines("subjects.txt");
            foreach (string line in lines)
            {
                string[] subjectDetails = line.Split('|'); 

                string subjectName1 = subjectDetails[1].Trim(); 

                if (name == subjectName)
                {
                    string subjectId = subjectDetails[0].Trim(); // Assuming the code is the second attribute
                    string description = subjectDetails[2].Trim(); // Assuming the description is the third attribute

                    // Create a new Subject object and add it to the list
                    subjects.Add(new Subject(name, code, description,));
                }
            }*/
            return new Professor(surname, name, date, address, phone, email, id1, title, years);
        }

        public string InputId()
        {
            System.Console.WriteLine("Enter professor id: ");
            string id = ConsoleViewUtils.SafeInputString();
            return id;
        }

        public void showAllProffesors()
        {
            PrintProfessor(_professorsDao.GetAllProfessors());
        }

        public void removeProfessor()
        {
            string id = InputId();
            Professor? removedProfessor = _professorsDao.removeProfessor(id);
            if (removedProfessor is null)
            {
                System.Console.WriteLine("Professor not found");
                return;
            }

            System.Console.WriteLine("Professor removed");
        }

        public void UpdateProfessor()
        {
            string id = InputId();
            Professor professor = InputProfessor();
            professor.Id = id;
            Professor? updatedProfessor = _professorsDao.UpdateProfessor(professor);
            if (updatedProfessor == null)
            {
                System.Console.WriteLine("Professor not found");
                return;
            }

            System.Console.WriteLine("Professor updated");


        }

        public void AddProfessor()
        {
            Professor professor = InputProfessor();
            _professorsDao.addProfessor(professor);
            System.Console.WriteLine("Professor added");
        }
    }
}
    

