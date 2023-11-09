using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CLI.Model;
using StudentskaSluzba.Serialization;
namespace StudentskaSluzba.Model;

class Student : ISerializable
{
    public string Surname { get; set; }

    public string Name { get; set; }

    public string Date {  get; set; }

    //public Address Address { get; set; }

    public long PhoneNumber {  get; set; }

    public string Email { get; set; }
    
    public int Id { get; set; }

    public int YearoOfStudy {  get; set; }

    public enum Status { B, S};

    public float AvarageGrade { get; set; }

    public List<Subject> Subjects { get; set; }


    public Student()
    {
        Subjects = new List<Subject>();
    }












    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Name
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Name = values[1];
    }
}
