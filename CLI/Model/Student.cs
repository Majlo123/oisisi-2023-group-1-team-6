using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CLI.Model;
using StudentskaSluzba.Serialization;
namespace StudentskaSluzba.Model;
public class Student : ISerializable
{
    public string Surname { get; set; }

    public string Name { get; set; }

    public int Id {  get; set; }

    public DateOnly Date { get; set; }

    public Address Address { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    public Index Index { get; set; }

    public int YearOfStudy { get; set; }

    public string Status { get; set; }

    public int AvarageGrade { get; set; }

    public List<Subject> PassedSubjects { get; set; }

    public List<Subject> FailedSubjects { get; set; }


    public Student()
    {
        PassedSubjects = new List<Subject>();
        FailedSubjects = new List<Subject>();
    }

    public Student(string surname, string name,int id, DateOnly date, Address address, string phonenumber, string email, Index index, int yearofstudy, string status, int avaragegrade)
    {
        Surname = surname;
        Name = name;
        Id = id;
        Date = date;
        Address = address;
        PhoneNumber = phonenumber;
        Email = email;
        Index = index;
        YearOfStudy = yearofstudy;
        Status = status;
        AvarageGrade = avaragegrade;
        PassedSubjects = new List<Subject>();
        FailedSubjects = new List<Subject>();
    }
    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Surname,
            Name,
            Id.ToString(),
            Date.ToString(),
            Address.ToString(),
            PhoneNumber,
            Email,
            Index.ToString(),
            YearOfStudy.ToString(),
            Status.ToString(),
            AvarageGrade.ToString(),
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        
        Surname = values[0];
        Name = values[1];
        Id = int.Parse(values[2]);
        Date = DateOnly.ParseExact(values[3], "M/dd/yyyy");
        PhoneNumber = values[4];
        Email = values[5];
        YearOfStudy = int.Parse(values[6]);
        Status = values[7];
        AvarageGrade = int.Parse(values[8]);
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"SURNAME: {Surname}, ");
        sb.Append($"NAME: {Name}, ");
        sb.Append($"ID: {Id.ToString()}, ");
        sb.Append($"DATE OF BIRTH: {Date.ToString()}, ");
        sb.Append($"PHONE NUMBER: {PhoneNumber.ToString()}, ");
        sb.Append($"EMAIL: {Email}, ");
        sb.Append($"YEAR OF STUDY: {YearOfStudy.ToString()}, ");
        sb.Append($"STUDDY YEAR STATUS: {Status}, ");
        sb.Append($"AVERAGE GRADE: {AvarageGrade.ToString()}, ");
        sb.Append("SUBJECTS PASSED:");
        sb.AppendJoin(", ", PassedSubjects.Select(subject => subject.subjectName));
        sb.Append($"SUBJECTS FAILED:");
        sb.AppendJoin(", ", FailedSubjects.Select(subject => subject.subjectName));//ovo se primenjuje posle pravljenja klase subject


        return sb.ToString();
    }

    
}
