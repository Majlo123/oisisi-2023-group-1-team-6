using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Storage;
using StudentskaSluzba.Serialization;
using StudentskaSluzba.Model;

namespace CLI.Model
{
    class Grade : ISerializable
    {
        public Student StudentWhoPassed {  get; set; }

        public Subject subject { get; set; }

        public enum Grades { SIX=6, SEVEN=7, EIGHT=8, NINE=9, TEN=10};

        public double date { get; set; }

        public Grade()
        {
            Grades ocena = Grades.SIX;
            date = 0;
        }

        public Grade(Student studentWhoPassed, Subject subject, double date )
        {
            StudentWhoPassed = studentWhoPassed;
            this.subject = subject;
            this.date = date;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                StudentWhoPassed,
                subject.ToString(),
                date.ToString(),

            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            StudentWhoPassed = values[0];
            subject = values[1];
            date = int.Parse(values[2]);

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Student who passed: {StudentWhoPassed}, ");
            sb.Append($"Subject Name: {subject}, ");
            sb.Append($"Date: : {date.ToString()}, ");

            return sb.ToString();
        }
    }
}
