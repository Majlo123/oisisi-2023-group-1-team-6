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
        public int studentId {  get; set; }

        public int subjectId { get; set; }

        public string grades { get; set; }

        public DateOnly date { get; set; }

        public Grade()
        {
            grades= "";
            Id = 0;
            studentId = 0;
            subjectId = 0;
        }
        public Grade(int id,int studentid, int subjectid, DateOnly Date, string grade )
        {
            Id = id;
            studentId=studentid;
            subjectId = subjectid;
            date = Date;
            grades = grade;
        }
    public Grade(DateOnly Date, string grade)
    {

        date = Date;
        grades = grade;
    }

    public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                studentId.ToString(),
                subjectId.ToString(),
                date.ToString(),
                grades

            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            studentId = int.Parse(values[1]);
            subjectId = int.Parse(values[2]);
            date = DateOnly.ParseExact(values[3], "M/d/yyyy");
            grades = values[4];

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"ID: {Id.ToString()}, ");
            
            sb.Append($"Student who passed: {studentId}, ");




        sb.Append($"Subject: {subjectId}, ");
            
            
        sb.Append($"Date:  {date.ToString()}, ");
            sb.Append($"Grade: {grades}");

            return sb.ToString();
        }
    }

