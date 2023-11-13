using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using CLI.Model;
using System.Xml.Linq;
using StudentskaSluzba.Serialization;
using StudentskaSluzba.Model;



namespace CLI.Model
{

    public class Subject : ISerializable
    {

        public int subjectId { get; set; }
        public string subjectName { get; set; }

        public int yearOfStudy {  get; set; }

        public Professor professor {  get; set; }

        public int ESPBPoints { get; set; }

        public string semester { get; set; }

        public List<Student> StudentsPassed { get; set; }

        public List<Student> StudentsFailed {  get; set; }


        public Subject() {
            subjectId = 0;
            subjectName = "";
            yearOfStudy = 0;
            ESPBPoints = 0;
            StudentsPassed = new List<Student>();
            StudentsFailed = new List<Student>();
        }
        public Subject(string subjectname)
        {
            
            subjectName = subjectname;

        }
        public Subject(int subjectid, string subjectname, int yearofstudy,string Semester, Professor Professor, int eSPBPoints)
        {
            subjectId = subjectid;
            subjectName = subjectname;
            yearOfStudy = yearofstudy;
            semester = Semester;
            professor = Professor;
            ESPBPoints = eSPBPoints;
            StudentsPassed = new List<Student>();
            StudentsFailed = new List<Student>();
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                subjectId.ToString(),
                subjectName,
                yearOfStudy.ToString(),
                semester,
                professor.Name,
                professor.Surname,
                ESPBPoints.ToString()

            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            subjectId = int.Parse(values[0]);
            subjectName = values[1];
            yearOfStudy = int.Parse(values[2]);
            semester = (values[3]);
            Address address = new Address
            {
                Street = values[7],
                Number = int.Parse(values[8]),
                City = values[9],
                State = values[10]
            };
            Professor professorToAdd = new Professor
            {
                Surname = values[4], // Surname
                Name = values[5], // Name
                Date = DateOnly.ParseExact(values[6], "M/d/yyyy"), // Date
                Address = address,   // Address
                PhoneNumber = values[11], // PhoneNumber
                Email = values[12], // Email
                Id = int.Parse(values[13]), // Id
                Title = values[14], // Title
                WorkYear = int.Parse(values[15]) // WorkYear
            };
            ESPBPoints = int.Parse(values[16]);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"SubjectId: {subjectId.ToString()}, ");
            sb.Append($"SubjectName: {subjectName}, ");
            sb.Append($"Years of study: {yearOfStudy.ToString()}, ");
            sb.Append($"Semester: {semester}");
            //ne radi kad se ugasi terminal pa upali ponovo bilo sta osim prikaza
            //sb.AppendLine($"Professor: {professor.Name.ToString()} {professor.Surname.ToString()}, ");
            sb.Append($"ESPB Points: {ESPBPoints.ToString()}, ");
            sb.Append($"Students who passed: ");
            sb.AppendJoin(", ", StudentsPassed.Select(Student => Student.Name));
            sb.Append($"Students who failed: ");
            sb.AppendJoin(", ", StudentsFailed.Select(Student => Student.Name));

            return sb.ToString();
        }

    }
}
