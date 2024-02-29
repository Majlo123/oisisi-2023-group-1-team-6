using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;

namespace StudentskaSluzba.Console
{
    public class ConsoleViewStudent
    {
        public readonly StudentsDAO _studentsDao;

        public ConsoleViewStudent(StudentsDAO studentsDao)
        {
            _studentsDao = studentsDao;
        }

        public void PrintStudent(List<Student> students)
        {
            System.Console.WriteLine("Students: ");
            string header = " ";
            System.Console.WriteLine(header);
            foreach (Student s in students)
            {
                System.Console.WriteLine(s);
            }
        }

        public Student InputStudent()
        {
            System.Console.WriteLine("Enter student Surname: ");
            string surname = System.Console.ReadLine(); ;
            while (surname == "")
            {
                System.Console.WriteLine("Enter valid string: ");
                surname = System.Console.ReadLine();
            }
            System.Console.WriteLine("Enter Name: ");
            string name = System.Console.ReadLine();
            while (surname == "")
            {
                System.Console.WriteLine("Enter valid string: ");
                surname = System.Console.ReadLine();
            }
            System.Console.WriteLine("Enter id: ");
            int id = ConsoleViewUtils.SafeInputInt();


            System.Console.WriteLine("Enter date of birth (in the format MM/dd/yyyy): ");
            DateOnly date = ConsoleViewUtils.SafeInputDateTime();

            System.Console.WriteLine("Enter address(id): ");
            int id1 = ConsoleViewUtils.SafeInputInt();
            System.Console.WriteLine("Enter address(state): ");
            string state = System.Console.ReadLine();
            System.Console.WriteLine("Enter address(city): ");
            string city = System.Console.ReadLine();
            System.Console.WriteLine("Enter address(street): ");

            string street = System.Console.ReadLine();
            System.Console.WriteLine("Enter address(number): ");
            int number = ConsoleViewUtils.SafeInputInt();
            Address address = new Address(id1, street, number, city, state);

            System.Console.WriteLine("Enter phone number: ");
            string phone = System.Console.ReadLine();

            System.Console.WriteLine("Enter email: ");
            string email = System.Console.ReadLine();

            System.Console.WriteLine("Enter id of index: ");
            int idIndex = ConsoleViewUtils.SafeInputInt();
            System.Console.WriteLine("Enter abbreviation of major: ");
            string abb = System.Console.ReadLine();
            System.Console.WriteLine("Enter mark of major: ");
            int mark = ConsoleViewUtils.SafeInputInt();
            System.Console.WriteLine("Enter year of major: ");
            int year = ConsoleViewUtils.SafeInputInt();
            Model.Index index = new Model.Index(idIndex, abb, mark, year);

            System.Console.WriteLine("Enter year of study: ");
            int yearstudy = ConsoleViewUtils.SafeInputInt();

            System.Console.WriteLine("Enter status(B or S): ");
            string status = System.Console.ReadLine();
            while (status != "B" && status != "S" && status != "b" && status != "s")
            {
                System.Console.WriteLine("Enter valid string: ");
                status = System.Console.ReadLine();
            }

            System.Console.WriteLine("Enter average grade: ");
            float avg = ConsoleViewUtils.SafeInputFloat();


            return new Student(surname, name, id, date, address, phone, email, index, yearstudy, status, avg);
        }

        public int InputId()
        {
            System.Console.WriteLine("Enter student id: ");
            int id = ConsoleViewUtils.SafeInputInt();
            return id;
        }

        public void ShowAllStudents()
        {
            PrintStudent(_studentsDao.getAllStudents());
        }

        public void RemoveStudent()
        {
            int id = InputId();
            Student? removedStudent = _studentsDao.removeStudent(id);
            if (removedStudent is null)
            {
                System.Console.WriteLine("Student not found");
                return;
            }

            System.Console.WriteLine("Student removed");
        }

        public void UpdateStudents()
        {
            int id = InputId();
            Student student = InputStudent();
            student.Id = id;
            Student? updatedStudent = _studentsDao.UpdateStudent(student);
            if (updatedStudent == null)
            {
                System.Console.WriteLine("Student not found");
                return;
            }
            System.Console.WriteLine("Student updated");
        }

        public void AddStudent()
        {
            Student student = InputStudent();
            _studentsDao.addStudent(student);
            System.Console.WriteLine("Student added");
        }

    }
}
