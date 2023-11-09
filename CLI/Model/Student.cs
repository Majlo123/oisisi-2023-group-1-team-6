using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CLI.Model;
using StudentskaSluzba.Serialization;
namespace StudentskaSluzba.Model;
enum finance {B,S}
class Student : ISerializable
{
    public string Surname { get; set; }

    public string Name { get; set; }

    public string Date {  get; set; }

    public string Address { get; set; }

    public long PhoneNumber {  get; set; }

    public string Email { get; set; }
    
    public int Id { get; set; }

    public int YearOfStudy {  get; set; }

    public finance Status;

    public float AvarageGrade { get; set; }

    public List<Subject> Subjects { get; set; }


    public Student()
    {
        Subjects = new List<Subject>();
    }

    public Student(string surname,string name,string date,string address,long phonenumber,string email,int id,int yearofstudy,finance status,float avaragegrade)
    {
        Surname=surname;
        Name=name;
        Date=date;
        Address=address;
        PhoneNumber=phonenumber;
        Email=email;
        Id=id;
        YearOfStudy = yearofstudy;
        Status=status;
        AvarageGrade = avaragegrade;
        Subjects=new List<Subject>();
    }
    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Surname,
            Name,
            Date,
            Address,
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
        Date = values[2];
        Address = values[3];
        PhoneNumber = int.Parse(values[4]);
        Email = values[5];
        Id = int.Parse(values[6]);
        YearOfStudy= int.Parse(values[7]);
        Status = (finance)int.Parse(values[8]);
        AvarageGrade= int.Parse(values[9]);
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
        sb.Append($"INDEX NUMBER: {Id.ToString()}, ");
        sb.Append($"YEAR OF STUDY: {YearOfStudy.ToString()}, ");
        sb.Append($"STUDDY YEAR STATUS: {Status.ToString()}, ");
        sb.Append($"AVARAGE GRADE: {AvarageGrade.ToString()}, ");
        sb.Append("SUBJECTS:");
        //sb.AppendJoin(", ", Subjects.Select(subject => subject.Name)); ovo se primenjuje posle pravljenja klase subject
       
        return sb.ToString();
    }
}
