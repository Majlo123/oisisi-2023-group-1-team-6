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


namespace CLI.Model
{

    class Subject : ISerializable
    {

        public int subjectId { get; set; }
        public string subjectName { get; set; }
        public enum Semester { Summer , Winter };

        public int yearOfStudy {  get; set; }

        public string professor {  get; set; }

        public int ESPBPoints { get; set; }

        public List<Student> StudentsPassed { get; set; }

        public List<Student> StudentsFailed {  get; set; }


        public Subject() {
            subjectId = 0;
            subjectName = "";
            yearOfStudy = 0;
            professor = "";
            ESPBPoints = 0;
            StudentsPassed = new List<Student>();
            StudentsFailed = new List<Student>();
        }

        public Subject(int subjectId, string subjectName, int yearOfStudy, string professor, int eSPBPoints, List<Student> studentsPassed, List<Student> studentsFailed)
        {
            subjectId = subjectId;
            subjectName = subjectName;
            yearOfStudy = yearOfStudy;
            professor = professor;
            ESPBPoints = eSPBPoints;
            StudentsPassed = studentsPassed;
            StudentsFailed = studentsFailed;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                subjectId.ToString(),
                subjectName,
                yearOfStudy.ToString(),
                professor,
                ESPBPoints.ToString()
                //#pitaj za enum

            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            subjectId = int.Parse(values[0]);
            subjectName = values[1];
            yearOfStudy = int.Parse(values[2]);
            professor = values[3];
            ESPBPoints = int.Parse(values[4]);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"SubjectId: {subjectId.ToString()}, ");
            sb.Append($"SubjectName: {subjectName}, ");
            sb.Append($"Years of study: {yearOfStudy.ToString()}, ");
            sb.Append($"Professor: {professor}, ");
            sb.Append($"ESPB Points: {ESPBPoints.ToString()}, ");
            sb.Append($"Students who passed: ");
            sb.AppendJoin(", ", StudentsPassed.Select(Student => Student.Subjects));
            sb.Append($"Students who failed: ");
            sb.AppendJoin(", ", StudentsFailed.Select(Student => Student.Subjects));

            return sb.ToString();
        }

    }
}
