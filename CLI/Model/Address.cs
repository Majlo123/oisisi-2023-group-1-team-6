using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Model;
using StudentskaSluzba.Serialization;

namespace StudentskaSluzba.Model
{

    public class Address : ISerializable
    {
        public int id { get; set; }
        public string Street { get; set; }

        public int Number { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public Address()
        {
            id = 0;
            Street = "";
            Number = 0;
            City = "";
            State = "";
        }

        public Address(int id1, string street, int number, string city, string state)
        {
            id = id1;
            Street = street;
            Number = number;
            City = city;
            State = state;

        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
            id.ToString(),
            Street,
            Number.ToString(),
            City,
            State,
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            id = int.Parse(values[0]);
            Street = values[1];
            Number = int.Parse(values[2]);
            City = values[3];
            State = values[4];

        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{id}");
            sb.Append($"|{Street}");
            sb.Append($"|{Number.ToString()}");
            sb.Append($"|{City}");
            sb.Append($"|{State}");


            return sb.ToString();
        }
    }

}

