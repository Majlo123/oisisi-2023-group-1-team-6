using CLI.Controller;
using CLI.Model;
using StudentskaSluzba.Model;
using StudentskaSluzba.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLI.DAO
{
    public class ProfessorDepartmentDAO
    {
        private readonly Storage<ProfessorDepartment> _storage;
        private List<ProfessorDepartment> _professordepartment;
        private ProfessorController professorController = new ProfessorController();
        private List<Professor> professor_temp;
        public CLI.Observer.Subject ProfessorDepartmentSubject;
        public ProfessorDepartmentDAO()
        {
            _storage = new Storage<ProfessorDepartment>("professordepartment.txt");
            _professordepartment = _storage.Load();
            ProfessorDepartmentSubject = new CLI.Observer.Subject();
        }

        public int NextId()
        {
            if (_professordepartment.Count == 0)
            {
                return 0;
            }
            else
            {
                return _professordepartment.Max(s => s.id) + 1;
            }

        }
        public ProfessorDepartment? GetProfessorDepartmentById(int id)
        {
            return _professordepartment.Find(p => p.departmentId == id);
        }

        public void Add(string id_prof, int id_department)
        {
            int id = NextId();
            ProfessorDepartment profdepartment = new ProfessorDepartment(id_prof, id_department, id);
            _professordepartment.Add(profdepartment);
            _storage.Save(_professordepartment);
            ProfessorDepartmentSubject.NotifyObservers();
        }
        public ProfessorDepartment RemoveProfessorDepartment(int id)
        {
            ProfessorDepartment? professordepartment = GetProfessorDepartmentById(id);
            if (professordepartment == null) return null;
            _professordepartment.Remove(professordepartment);
            _storage.Save(_professordepartment);
            ProfessorDepartmentSubject.NotifyObservers();
            return professordepartment;
        }

        public ProfessorDepartment? UpdateProfessorDepartment(ProfessorDepartment ss)
        {
            ProfessorDepartment? oldprofessordepartment = GetProfessorDepartmentById(ss.id);
            if (oldprofessordepartment is null) { return null; }
            oldprofessordepartment.id = ss.id;
            oldprofessordepartment.professorId = ss.professorId;
            oldprofessordepartment.departmentId = ss.departmentId;


            _storage.Save(_professordepartment);
            ProfessorDepartmentSubject.NotifyObservers();
            return oldprofessordepartment;
        }


        public List<ProfessorDepartment> GetAll()
        {

            return _professordepartment;
        }

        public List<StudentskaSluzba.Model.Professor> GetAllById(int departmentid)
        {
            _professordepartment.Clear();
            _professordepartment = _storage.Load();
            professor_temp = professorController.GetAllProfessors();
            List<StudentskaSluzba.Model.Professor> professorlist = new List<StudentskaSluzba.Model.Professor>();
            List<ProfessorDepartment> professordepartment1 = GetAll();
            foreach (ProfessorDepartment st in professordepartment1)
            {
                if (st.departmentId == departmentid)
                {
                    Professor temp;
                    temp = professor_temp.Find(ss => ss.Id == st.professorId);
                    professorlist.Add(temp);
                }
            }
            return professorlist;
        }
    }
}
