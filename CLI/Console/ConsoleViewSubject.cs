using CLI.Model;
using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;

namespace StudentskaSluzba.Console
{
    public class ConsoleViewSubject
    {
        public readonly SubjectsDAO _subjectsDao;

        public ConsoleViewSubject(SubjectsDAO subjectsDao)
        {
            _subjectsDao = subjectsDao;
        }

        public void PrintSubject(List<Subject> subjects)
        {
            System.Console.WriteLine("Subjects: ");
            string header = " ";
            System.Console.WriteLine(header);
            foreach (Subject s in subjects)
            {
                System.Console.WriteLine(s);
            }
        }

        public Subject InputSubject()
        {
            System.Console.WriteLine("Enter subject Id: ");
            int id = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Enter Subject Name: ");
            string name = System.Console.ReadLine();
            while (name == "")
            {
                System.Console.WriteLine("Enter valid string: ");
                name = System.Console.ReadLine();
            }
            System.Console.WriteLine("Enter yearOfStudy: ");
            int year = ConsoleViewUtils.SafeInputInt();
            System.Console.WriteLine("In which semester is this subject(Summer or winter): ");
            string semester = System.Console.ReadLine();
            while (semester.ToLower() != "summer" && semester.ToLower() != "winter")
            {
                System.Console.WriteLine("Enter summer or winter: ");
                semester = System.Console.ReadLine();
            }
            System.Console.WriteLine("Enter proffesor id: ");
            string id1 = ConsoleViewUtils.SafeInputString();
            ProfessorsDAO professorDAO = new ProfessorsDAO();
            Professor professorToAdd = professorDAO.GetProfessorById(id1);
            while (professorToAdd == null)
            {

                System.Console.WriteLine("This proffesor doesn't exist: ");
                System.Console.WriteLine("Enter proffesor id: ");
                id1 = ConsoleViewUtils.SafeInputString();
                ProfessorsDAO professorDAO1 = new ProfessorsDAO();

                professorToAdd = professorDAO.GetProfessorById(id1);
            }
            Professor professor = new Professor(professorToAdd.Surname, professorToAdd.Name,
                professorToAdd.Date, professorToAdd.Address, professorToAdd.PhoneNumber,
                professorToAdd.Email, professorToAdd.Id, professorToAdd.Title, professorToAdd.WorkYear);


            System.Console.WriteLine("Enter ESPB points: ");
            int espb = ConsoleViewUtils.SafeInputInt();

            return new Subject(id, name, year, semester, id1, espb);
        }

        public int InputId()
        {
            System.Console.WriteLine("Enter subject id: ");
            int id = ConsoleViewUtils.SafeInputInt();
            return id;
        }

        public void ShowAllSubjects()
        {
            PrintSubject(_subjectsDao.getAllSubjects());
        }

        public void RemoveSubject()
        {
            int id = InputId();
            Subject? removedSubject = _subjectsDao.removeSubject(id);
            if (removedSubject is null)
            {
                System.Console.WriteLine("Subject not found");
                return;
            }

            System.Console.WriteLine("Subject removed");
        }

        public void UpdateSubject()
        {
            int id = InputId();
            Subject subject = InputSubject();
            subject.subjectId = id;
            Subject? updatedSubject = _subjectsDao.UpdateSubject(subject);
            if (updatedSubject == null)
            {
                System.Console.WriteLine("Subject not found");
                return;
            }
            System.Console.WriteLine("Subject updated");
        }

        public void AddSubject()
        {
            Subject subject = InputSubject();
            _subjectsDao.addSubject(subject);
            System.Console.WriteLine("Subject added");
        }

    }
}
