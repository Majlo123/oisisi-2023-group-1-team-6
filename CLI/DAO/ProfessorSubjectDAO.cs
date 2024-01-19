using CLI.Controller;
using CLI.Model;
using StudentskaSluzba.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLI.DAO
{
    public class ProfessorSubjectDAO
    {
        private readonly Storage<ProfessorSubject> _storage;
        private List<ProfessorSubject> _professorsubject;
        private SubjectController subjectcontroller = new SubjectController();
        private List<CLI.Model.Subject> subject_temp;
        public CLI.Observer.Subject ProfessorSubject;
        public ProfessorSubjectDAO()
        {
            _storage = new Storage<ProfessorSubject>("professorsubject.txt");
            _professorsubject = _storage.Load();
            ProfessorSubject = new CLI.Observer.Subject();
        }

        public int NextId()
        {
            if (_professorsubject.Count == 0)
            {
                return 0;
            }
            else
            {
                return _professorsubject.Max(s => s.id) + 1;
            }

        }
        public ProfessorSubject? GetProfessorSubjectById(int id)
        {
            return _professorsubject.Find(p => p.id_subject == id);
        }

        public void Add(string id_prof, int id_subject)
        {
            int id = NextId();
            ProfessorSubject profsubject = new ProfessorSubject(id_prof, id_subject, id);
            _professorsubject.Add(profsubject);
            _storage.Save(_professorsubject);
            ProfessorSubject.NotifyObservers();
        }
        public ProfessorSubject RemoveProfessorSubject(int id)
        {
            ProfessorSubject? professorsubject = GetProfessorSubjectById(id);
            if (professorsubject == null) return null;
            _professorsubject.Remove(professorsubject);
            _storage.Save(_professorsubject);
            ProfessorSubject.NotifyObservers();
            return professorsubject;
        }

        public ProfessorSubject? UpdateProfessorSubject(ProfessorSubject ss)
        {
            ProfessorSubject? oldprofessorsubject = GetProfessorSubjectById(ss.id);
            if (oldprofessorsubject is null) { return null; }
            oldprofessorsubject.id = ss.id;
            oldprofessorsubject.id_professor = ss.id_professor;
            oldprofessorsubject.id_subject = ss.id_subject;


            _storage.Save(_professorsubject);
            ProfessorSubject.NotifyObservers();
            return oldprofessorsubject;
        }


        public List<ProfessorSubject> GetAll()
        {

            return _professorsubject;
        }

        public List<CLI.Model.Subject> GetAllById(string professorid)
        {
            _professorsubject.Clear();
            _professorsubject = _storage.Load();
            subject_temp = subjectcontroller.GetAllSubjects();
            List<CLI.Model.Subject> subjectlist = new List<CLI.Model.Subject>();
            List<ProfessorSubject> professorsubject1 = GetAll();
            foreach (ProfessorSubject st in professorsubject1)
            {
                if (st.id_professor == professorid)
                {
                    CLI.Model.Subject temp;
                    temp = subject_temp.Find(ss => ss.subjectId == st.id_subject);
                    subjectlist.Add(temp);
                }
            }
            return subjectlist;
        }

    }
}
