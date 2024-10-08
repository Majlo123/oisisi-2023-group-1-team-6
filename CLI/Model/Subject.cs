﻿using StudentskaSluzba.Model;
using StudentskaSluzba.Serialization;
using System.Text;



namespace StudentskaSluzba.Model
{

    public class Subject : ISerializable
    {

        public int subjectId { get; set; }
        public string subjectName { get; set; }

        public int yearOfStudy { get; set; }

        public string professor_Id { get; set; }

        public int ESPBPoints { get; set; }

        public string semester { get; set; }

        public List<Student> StudentsPassed { get; set; }

        public List<Student> StudentsFailed { get; set; }


        public Subject()
        {
            subjectId = 0;
            subjectName = "";
            yearOfStudy = 0;
            ESPBPoints = 0;
            professor_Id = "";
            StudentsPassed = new List<Student>();
            StudentsFailed = new List<Student>();
        }
        public Subject(string subjectname)
        {

            subjectName = subjectname;

        }
        public Subject(int subjectid, string subjectname)
        {
            subjectName = subjectname;
            subjectId = subjectid;

        }
        public Subject(int subjectid, string subjectname, int yearofstudy, string Semester, string professor_id, int eSPBPoints)
        {
            subjectId = subjectid;
            subjectName = subjectname;
            yearOfStudy = yearofstudy;
            semester = Semester;
            professor_Id = professor_id;
            ESPBPoints = eSPBPoints;

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                subjectId.ToString(),
                subjectName,
                yearOfStudy.ToString(),
                semester,
                professor_Id,
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
            professor_Id = values[4];
            ESPBPoints = int.Parse(values[5]);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"SubjectId: {subjectId.ToString()}, ");
            sb.Append($"SubjectName: {subjectName}, ");
            sb.Append($"Years of study: {yearOfStudy.ToString()}, ");
            sb.Append($"Semester: {semester}");
            sb.Append($"Professor: {professor_Id}");
            sb.Append($"ESPB Points: {ESPBPoints.ToString()}, ");
            sb.Append($"Students who passed: ");
            sb.AppendJoin(", ", StudentsPassed.Select(Student => Student.Name));
            sb.Append($"Students who failed: ");
            sb.AppendJoin(", ", StudentsFailed.Select(Student => Student.Name));

            return sb.ToString();
        }

    }
}
