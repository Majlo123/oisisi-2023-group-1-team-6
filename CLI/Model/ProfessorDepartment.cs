using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model
{
    public class ProfessorDepartment : StudentskaSluzba.Serialization.ISerializable
    {

        public int id;

        public int departmentId;

        public string professorId;

        public ProfessorDepartment()
        {

        }

        public ProfessorDepartment(string id_professor, int id_department, int id)
        {
            this.professorId = id_professor;
            this.departmentId = id_department;
            this.id = id;
        }
        public void FromCSV(string[] values)
        {
            id = int.Parse(values[0]);
            professorId = values[1];
            departmentId = int.Parse(values[2]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                id.ToString(),
                professorId,
                departmentId.ToString()
            };
            return csvValues;
        }
    }
}
