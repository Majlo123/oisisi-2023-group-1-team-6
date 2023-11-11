using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Model;
using CLI.Model;
using StudentskaSluzba.Storage;



namespace StudentskaSluzba.DAO

{
    public class DepartmentsDAO
    {
        private readonly List<Department> _departments;
        private readonly Storage<Department> _storage;


        public DepartmentsDAO()
        {
            _storage = new Storage<Department>("departments.txt");
            _departments = _storage.Load();

        }
        
        private Department? GetDepartmentById(int id)
        {
            return _departments.Find(d => d.DepartmentID == id);
        }
        public void addDepartment(Department department)
        {
            bool exists = _departments.Contains(department);
            if (exists) return;
            //student.Id = GenerateId(); 
            _departments.Add(department);
            _storage.Save(_departments);
        }

        public void removeDepartments(int id)
        {
            Department? department = GetDepartmentById(id);
            if (department == null) return;

            _departments.Remove(department);
            _storage.Save(_departments);
        }
        public Department? UpdateDepartment(Department department)
        {
            Department? oldDepartment = GetDepartmentById(department.DepartmentID);
            if (oldDepartment == null) { return null; }
            oldDepartment.DepartmentID = department.DepartmentID;
            oldDepartment.DepartmentName = department.DepartmentName;
            oldDepartment.DepartmentBoss = department.DepartmentBoss;
            oldDepartment.Professors = department.Professors;



            _storage.Save(_departments);
            return department;

        }

        public List<Department> getAllDepartments()
        {
            return _departments;
        }
        public List<Department> GetAllDepartments(int page, int pageSize)
        {
            IEnumerable<Department> department = _departments;

            department = _departments.Skip((page - 1) * pageSize).Take(pageSize);

            return department.ToList();
        }
    }
}
