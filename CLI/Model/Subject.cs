﻿using System;
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

enum Semester { Summer, Winter };

namespace CLI.Model
{

    class Subject : ISerializable
    {

        public int subjectId { get; set; }
        public string subjectName { get; set; }

        public int yearOfStudy {  get; set; }

        public string professor {  get; set; }

        public int ESPBPoints { get; set; }

        public Semester semester { get; set; }

        public List<Student> StudentsPassed { get; set; }

        public List<Student> StudentsFailed {  get; set; }


        public Subject() {
            subjectId = 0;
            subjectName = "";
            yearOfStudy = 0;
            professor = "";
            semester.ToString();
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
            semester.ToString();
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
                semester.ToString(),
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
            semester = (Semester)int.Parse(values[3]);
            professor = values[4];
            ESPBPoints = int.Parse(values[5]);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"SubjectId: {subjectId.ToString()}, ");
            sb.Append($"SubjectName: {subjectName}, ");
            sb.Append($"Years of study: {yearOfStudy.ToString()}, ");
            sb.Append($"Professor: {professor}, ");
            sb.Append($"Semester: {semester.ToString()}");
            sb.Append($"ESPB Points: {ESPBPoints.ToString()}, ");
            sb.Append($"Students who passed: ");
            sb.AppendJoin(", ", StudentsPassed.Select(Student => Student.Name));
            sb.Append($"Students who failed: ");
            sb.AppendJoin(", ", StudentsFailed.Select(Student => Student.Name));

            return sb.ToString();
        }

    }
}
