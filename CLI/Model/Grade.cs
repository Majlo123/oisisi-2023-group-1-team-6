using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Storage;
using StudentskaSluzba.Serialization;
using StudentskaSluzba.Storage;
using StudentskaSluzba.Model;

public enum Grades { SIX = 6, SEVEN = 7, EIGHT = 8, NINE = 9, TEN = 10 };

namespace CLI.Model
{
    public class Grade : ISerializable
    {
        public Student StudentWhoPassed {  get; set; }

        public Subject subject { get; set; }

        public Grades grades { get; set; }

        public DateTime date { get; set; }


        public Grade(Student studentWhoPassed, Subject subject, DateTime date, Grades grade )
        {
            this.StudentWhoPassed = studentWhoPassed;
            this.subject = subject;
            this.date = date;
            grades = grade;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                StudentWhoPassed.ToString(),
                subject.ToString(),
                date.ToString(),
                grades.ToString()

            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            StudentWhoPassed = values[0];
            subject = values[1];//pitacemo
            date = int.Parse(values[2]);//pitacemo
            grades = (Grades)int.Parse(values[3]);

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Student who passed: {StudentWhoPassed}, ");
            sb.Append($"Subject Name: {subject}, ");
            sb.Append($"Date:  {date.ToString()}, ");
            sb.Append($"Grade: {grades}");

            return sb.ToString();
        }
    }
}
