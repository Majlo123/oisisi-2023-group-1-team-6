using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Model;
using StudentskaSluzba.Serialization;
namespace StudentskaSluzba.Model;

    public class Address : ISerializable
    {
    public string Street { get; set; }

    public int Number { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    

    public Address(string street,int number,string city,string state)
    { 
        Street=street;
        Number=number;
        City=city;
        State=state;

    }
    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Street,
            Number.ToString(),
            City,
            State,
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Street= values[0];
        Number = int.Parse(values[1]);
        City = values[2];
        State = values[3];
        
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"{Street}");
        sb.Append($"|{Number.ToString()}");
        sb.Append($"|{City}");
        sb.Append($"|{State}");


        return sb.ToString();
    }
}

