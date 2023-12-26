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
    public class SubjectController

    {
        private readonly SubjectsDAO _subjects;

        public SubjectController()
        {

            _subjects = new SubjectsDAO();

        }
        public List<CLI.Model.Subject> GetAllSubjects()
        {
            return _subjects.getAllSubjects();
        }

        public void Add(CLI.Model.Subject subject)
        {
            _subjects.addSubject(subject);
        }
        public void Update(CLI.Model.Subject subject)
        {
            _subjects.UpdateSubject(subject);
        }
        public void Delete(int subjectid)
        {
            _subjects.removeSubject(subjectid);
        }
        public void Subscribe(IObserver observer)
        {
            _subjects.SubjectSubject.Subscribe(observer);
        }
        public CLI.Model.Subject? getSubjectById(int subjectid) { 
            return _subjects.GetSubjectById(subjectid);
        }
    }
}
