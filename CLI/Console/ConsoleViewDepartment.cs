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


namespace StudentskaSluzba.Console
{
    public class ConsoleViewDepartment
    {
        public readonly DepartmentsDAO _departmentsDao;

        public ConsoleViewDepartment(DepartmentsDAO departmentsDao)
        {
            _departmentsDao = departmentsDao;
        }

        public void PrintDepartment(List<Department> departments)
        {
            System.Console.WriteLine("Departments: ");
            string header = " ";
            System.Console.WriteLine(header);
            foreach (Department d in departments)
            {
                System.Console.WriteLine(d);
            }
            
        }
        
        public Department InputDepartment()
        {
            System.Console.WriteLine("Enter Department ID: ");
            int id = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Enter Department name: ");
            string name = System.Console.ReadLine();
            while (name == "")
            {
                System.Console.WriteLine("Enter valid string: ");
                name = System.Console.ReadLine();
            }
            System.Console.WriteLine("Enter Department Boss");
            string boss= System.Console.ReadLine();


            return new Department(id, name, boss);
        }

        public int InputId()
        {
            System.Console.WriteLine("Enter Department id: ");
            int id = ConsoleViewUtils.SafeInputInt();
            return id;
        }
        public string InputId1()
        {
            System.Console.WriteLine("Enter Proffesor id: ");
            string id = System.Console.ReadLine();
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
            Department department = InputDepartment();
            department.DepartmentID = id;
            Department updatedDepartment = _departmentsDao.UpdateDepartment(department);
            if (updatedDepartment == null)
            {
                System.Console.WriteLine("Department not found");
                return;
            }

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
            string id = InputId1();
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
