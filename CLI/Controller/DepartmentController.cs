using CLI.Observer;
using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Controller
{
    public class DepartmentController
    {
        private readonly DepartmentsDAO _department;

        public DepartmentController()
        {
            _department = new DepartmentsDAO();
        }

        public List<Department> GetAllDepartments()
        {
            return _department.getAllDepartments();
        }
        public void Add(Department department)
        {
            _department.addDepartment(department);
        }
        public void Update(Department department)
        {
            _department.UpdateDepartment(department);
        }
        public void Delete(int departmentId)
        {
            _department.removeDepartment(departmentId);
        }
        public Department? getDepartmentById(int departmentid)
        {
            return _department.GetDepartmentById(departmentid);
        }

        public List<Professor> getProfessorsByDepartmentId(int id)
        {
            return _department.GetAllProfessorsByDepartmentId(id);
        }

        public void Subscribe(IObserver observer)
        {
            _department.DepartmentSubject.Subscribe(observer);
        }
    }
}
