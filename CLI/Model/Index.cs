using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Serialization;

namespace CLI.Model
{
    class Index : ISerializable
    {
        public string AbbreviationOfMajor { get; set; }

        public int MarkOfMajor { get; set; }

        public int YearOfEnrollment { get; set; }

        public Index() {
            AbbreviationOfMajor = "";
            MarkOfMajor = 0;
            YearOfEnrollment = 0;
        }

        public Index(string abb, int mark, int year)
        {
            AbbreviationOfMajor = abb;
            MarkOfMajor = mark;
            YearOfEnrollment = year;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                AbbreviationOfMajor,
                MarkOfMajor.ToString(),
                YearOfEnrollment.ToString(),

            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            AbbreviationOfMajor = values[0];
            MarkOfMajor = int.Parse(values[1]);
            YearOfEnrollment = int.Parse(values[2]);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Abbreviation of major: {AbbreviationOfMajor}, ");
            sb.Append($"Mark of major: {MarkOfMajor.ToString()}, ");
            sb.Append($"Years of enrollment: {YearOfEnrollment.ToString()}, ");

            return sb.ToString();
        }

    };

}
