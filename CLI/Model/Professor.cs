using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Model;
using StudentskaSluzba.Serialization;
namespace StudentskaSluzba.Model;

    public  class Professor : ISerializable
    {
    public string Surname { get; set; }

    public string Name { get; set; }

    public DateOnly Date { get; set; }

    public Address Address { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public int Id { get; set; }


    public string Title { get; set; }

    public int WorkYear { get; set; }

    public List<Subject> Subjects { get; set; }


    public Professor()
    {
        Subjects = new List<Subject>();
    }

    
    public Professor(string surname, string name, DateOnly date, Address address, string phonenumber, string email, int id, string title, int workyear)
    {
        Surname = surname;
        Name = name;
        Date = date;
        Address = address;
        PhoneNumber = phonenumber;
        Email = email;
        Id = id;
        Title = title;
        WorkYear = workyear;
        Subjects = new List<Subject>();
    }
    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Surname,
            Name,
            Date.ToString(),
            Address.ToString(),
            PhoneNumber,
            Email,
            Id.ToString(),
            Title,
            WorkYear.ToString(),
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Surname = values[0];
        Name = values[1];
        Date = DateOnly.ParseExact(values[2], "M/d/yyyy");
        Address = new Address(int.Parse(values[3]), values[4], int.Parse(values[5]), values[6], values[7]);
        PhoneNumber = values[8];
        Email = values[9];
        Id = int.Parse(values[10]);
        Title = values[11];
        WorkYear=int.Parse(values[12]);
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"SURNAME: {Surname}, ");
        sb.Append($"NAME: {Name}, ");
        sb.Append($"DATE OF BIRTH: {Date}, ");
        sb.Append($"ADDRESS: {Address.ToString()}, ");
        sb.Append($"PHONE NUMBER: {PhoneNumber}, ");
        sb.Append($"EMAIL: {Email}, ");
        sb.Append($"ID: {Id.ToString()}, ");
        sb.Append($"TITLE: {Title}, ");
        sb.Append($"WORK YEAR: {WorkYear.ToString()}, ");
        sb.Append("SUBJECTS:");
        sb.AppendJoin(", ", Subjects.Select(subject => subject.subjectName));

        return sb.ToString();
    }
}

