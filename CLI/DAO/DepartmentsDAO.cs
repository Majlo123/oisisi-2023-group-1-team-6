using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Observer;
using StudentskaSluzba.Model;
using StudentskaSluzba.Storage;



namespace StudentskaSluzba.DAO

{
    public class DepartmentsDAO
    {
        private readonly List<Department> _departments;
        private readonly Storage<Department> _storage;

        public Subject DepartmentSubject;
        public DepartmentsDAO()
        {
            _storage = new Storage<Department>("departments.txt");
            _departments = _storage.Load();
            DepartmentSubject = new Subject();
        }
        
        public Department? GetDepartmentById(int id)
        {
            return _departments.Find(d => d.DepartmentID == id);
        }
        public Department? addDepartment(Department department)
        {
            bool exists = _departments.Contains(department);
            if (exists) return null;
            //student.Id = GenerateId(); 
            _departments.Add(department);
            _storage.Save(_departments);
            DepartmentSubject.NotifyObservers();
            return department;
        }

        public Department? removeDepartment(int id)
        {
            Department? department = GetDepartmentById(id);
            if (department == null) return null;

            _departments.Remove(department);
            _storage.Save(_departments);
            DepartmentSubject.NotifyObservers();
            return department;
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
            DepartmentSubject.NotifyObservers();
            return oldDepartment;

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

        public List<Professor> GetAllProfessorsByDepartmentId(int id)
        {
            Department dep = GetDepartmentById(id);
            if (dep == null)
                return null;
            return dep.Professors;
        }
       
    }
       
}
