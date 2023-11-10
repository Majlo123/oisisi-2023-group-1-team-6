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

    public long PhoneNumber { get; set; }

    public string Email { get; set; }

    public int Id { get; set; }


    public string Title { get; set; }

    public int WorkYear { get; set; }

    public List<Subject> Subjects { get; set; }


    public Professor()
    {
        Subjects = new List<Subject>();
    }

    public Professor(string surname, string name, DateOnly date, Address address, long phonenumber, string email, int id, string title, int workyear)
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
            PhoneNumber.ToString(),
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
        Date = DateOnly.ParseExact(values[2], "dd-MM-yyyy");
        Address.Street = values[3];
        Address.Number = int.Parse(values[4]);
        Address.City = values[5];
        Address.State = values[6];
        PhoneNumber = int.Parse(values[7]);
        Email = values[8];
        Id = int.Parse(values[9]);
        Title = values[10];
        WorkYear=int.Parse(values[11]);
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"SURNAME: {Surname}, ");
        sb.Append($"NAME: {Name}, ");
        sb.Append($"DATE OF BIRTH: {Date}, ");
        sb.Append($"ADDRESS: {Address}, ");
        sb.Append($"PHONE NUMBER: {PhoneNumber.ToString()}, ");
        sb.Append($"EMAIL: {Email}, ");
        sb.Append($"ID: {Id.ToString()}, ");
        sb.Append($"TITLE: {Title}, ");
        sb.Append($"WORK YEAR: {WorkYear.ToString()}, ");
        sb.Append("SUBJECTS:");
        sb.AppendJoin(", ", Subjects.Select(subject => subject.subjectName));

        return sb.ToString();
    }
}

