using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Model;
using StudentskaSluzba.Serialization;
namespace StudentskaSluzba.Model;
    public class Index : ISerializable
    {
        public string AbbreviationOfMajor { get; set; }

        public int MarkOfMajor { get; set; }

        public int YearOfEnrollment { get; set; }

        public int IdIndex { get; set; }

        public Index() {
            AbbreviationOfMajor = "";
            MarkOfMajor = 0;
            YearOfEnrollment = 0;
            IdIndex = 0;
        }

        public Index(int id, string abb, int mark, int year)
        {
            IdIndex = id;
            AbbreviationOfMajor = abb;
            MarkOfMajor = mark;
            YearOfEnrollment = year;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                IdIndex.ToString(),
                AbbreviationOfMajor,
                MarkOfMajor.ToString(),
                YearOfEnrollment.ToString(),

            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            IdIndex = int.Parse(values[0]);
            AbbreviationOfMajor = values[1];
            MarkOfMajor = int.Parse(values[2]);
            YearOfEnrollment = int.Parse(values[3]);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{IdIndex}");
            sb.Append($"|{AbbreviationOfMajor}");
            sb.Append($"|{MarkOfMajor.ToString()}");
            sb.Append($"|{YearOfEnrollment.ToString()}"); ;

            return sb.ToString();
        }

    };


