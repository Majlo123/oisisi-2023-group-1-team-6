using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CLI.Model;
using StudentskaSluzba.Serialization;
namespace StudentskaSluzba.Model;


    public class Department : ISerializable
    {

        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentBoss {  get; set; }

        public List<Professor> Professors { get; set; }

        public Department()
        {
            DepartmentID = 0;
            DepartmentName = "";
            DepartmentBoss = "";
            Professors = new List<Professor>();
        }

        public Department(int depid, string depname, string depboss)
        {
            DepartmentID = depid;
            DepartmentName = depname;
            DepartmentBoss = depboss;
            Professors = new List<Professor>();
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                DepartmentID.ToString(), 
                DepartmentName,
                DepartmentBoss,
            };

            return csvValues;
        }

        public void FromCSV(string[] csvValues)
        {
            DepartmentID = int.Parse(csvValues[0]);
            DepartmentName = csvValues[2];
            DepartmentBoss = csvValues[3];
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Department ID: {DepartmentID}, ");
            sb.Append($"Department name: {DepartmentName}, ");
            sb.Append($"Department boss: {DepartmentBoss}, ");
            sb.Append("List of proffesors:");
            sb.AppendJoin(",proffesor:", Professors.Select(professor => professor.Id));
            return sb.ToString();
        }




    }

