using StudentskaSluzba.Serialization;
using System.Text;
namespace StudentskaSluzba.Model;

public class Grade : ISerializable
{
    public int Id { get; set; }
    public int studentId { get; set; }

    public int subjectId { get; set; }

    public double grades { get; set; }

    public DateOnly date { get; set; }

    public Grade()
    {
        grades = 0;
        Id = 0;
        studentId = 0;
        subjectId = 0;
    }
    public Grade(int studentid, int subjectid, DateOnly Date, double grade)
    {
        studentId = studentid;
        subjectId = subjectid;
        date = Date;
        grades = grade;
    }
    public Grade(DateOnly Date, double grade)
    {

        date = Date;
        grades = grade;
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
                Id.ToString(),
                studentId.ToString(),
                subjectId.ToString(),
                date.ToString(),
                grades.ToString()

            };

        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        studentId = int.Parse(values[1]);
        subjectId = int.Parse(values[2]);
        date = DateOnly.ParseExact(values[3], "M/d/yyyy");
        grades = int.Parse(values[4]);

    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"ID: {Id.ToString()}, ");

        sb.Append($"Student who passed: {studentId}, ");




        sb.Append($"Subject: {subjectId}, ");


        sb.Append($"Date:  {date.ToString()}, ");
        sb.Append($"Grade: {grades}");

        return sb.ToString();
    }
}

