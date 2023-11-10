using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CLI.Model;
using StudentskaSluzba.Serialization;
namespace StudentskaSluzba.Model;
public enum finance { B, S }
public class Student : ISerializable
{
    public string Surname { get; set; }

    public string Name { get; set; }

    public DateOnly Date { get; set; }

    public Address Address { get; set; }

    public long PhoneNumber { get; set; }

    public string Email { get; set; }

    public Index Id { get; set; }

    public int YearOfStudy { get; set; }

    public finance Status;

    public float AvarageGrade { get; set; }

    public List<Subject> PassedSubjects { get; set; }

    public List<Subject> FailedSubjects { get; set; }


    public Student()
    {
        PassedSubjects = new List<Subject>();
        FailedSubjects = new List<Subject>();
    }

    public Student(string surname, string name, DateOnly date, Address address, long phonenumber, string email, Index id, int yearofstudy, finance status, float avaragegrade)
    {
        Surname = surname;
        Name = name;
        Date = date;
        Address = address;
        PhoneNumber = phonenumber;
        Email = email;
        Id = id;
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
            Date.ToString(),
            Address.ToString(),
            PhoneNumber.ToString(),
            Email,
            Id.ToString(),
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
        Date = DateOnly.ParseExact(values[2], "dd-MM-yyyy");
        Address.Street = values[3];
        Address.Number = int.Parse(values[4]);
        Address.City = values[5];
        Address.State = values[6];
        PhoneNumber = int.Parse(values[7]);
        Email = values[8];
        Id.AbbreviationOfMajor = values[9];
        Id.MarkOfMajor = int.Parse(values[10]);
        Id.YearOfEnrollment = int.Parse(values[11]);
        YearOfStudy = int.Parse(values[12]);
        Status = (finance)int.Parse(values[13]);
        AvarageGrade = int.Parse(values[14]);
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"SURNAME: {Surname}, ");
        sb.Append($"NAME: {Name}, ");
        sb.Append($"DATE OF BIRTH: {Date.ToString()}, ");
        sb.Append($"ADDRESS: {Address.ToString()}, ");
        sb.Append($"PHONE NUMBER: {PhoneNumber.ToString()}, ");
        sb.Append($"EMAIL: {Email}, ");
        sb.Append($"INDEX NUMBER: {Id.ToString()}, ");
        sb.Append($"YEAR OF STUDY: {YearOfStudy.ToString()}, ");
        sb.Append($"STUDDY YEAR STATUS: {Status.ToString()}, ");
        sb.Append($"AVERAGE GRADE: {AvarageGrade.ToString()}, ");
        sb.Append("SUBJECTS PASSED:");
        sb.AppendJoin(", ", PassedSubjects.Select(subject => subject.subjectName));
        sb.Append($"SUBJECTS FAILED:");
        sb.AppendJoin(", ", FailedSubjects.Select(subject => subject.subjectName));//ovo se primenjuje posle pravljenja klase subject


        return sb.ToString();
    }

    
}
