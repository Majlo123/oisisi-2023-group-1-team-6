using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CLI.Model;
using StudentskaSluzba.Serialization;
namespace StudentskaSluzba.Model;

    public class Grade : ISerializable
    {
        public int Id { get; set; }
        public Student StudentWhoPassed {  get; set; }

        public Subject subject { get; set; }

        public string grades { get; set; }

        public DateOnly date { get; set; }

        public Grade()
        {
            grades= "";
            Id = 0;
            
        }
        public Grade(int id,Student studentWhoPassed, Subject Subject, DateOnly Date, string grade )
        {
            Id = id;
            StudentWhoPassed = studentWhoPassed;
            subject = Subject;
            date = Date;
            grades = grade;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                StudentWhoPassed.Surname,
                StudentWhoPassed.Name,
                subject.subjectName,
                date.ToString(),
                grades

            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Student student = new Student(values[1], values[2]);
            Subject subject = new Subject(values[3]);
            date = DateOnly.ParseExact(values[4], "M/d/yyyy");
            grades = values[5];

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"ID: {Id.ToString()}, ");
            sb.Append($"Student who passed: {StudentWhoPassed.Name}, ");
            sb.Append($"Subject Name: {subject.subjectName}, ");
            sb.Append($"Date:  {date.ToString()}, ");
            sb.Append($"Grade: {grades}");

            return sb.ToString();
        }
    }

