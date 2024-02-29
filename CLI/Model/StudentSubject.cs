using StudentskaSluzba.Serialization;

namespace CLI.Model
{
    public class StudentSubject : ISerializable
    {
        public int id { get; set; }
        public int id_student { get; set; }

        public int id_subject { get; set; }

        public StudentSubject()
        {


        }
        public StudentSubject(int id_student, int id_subject, int id)
        {
            this.id_student = id_student;
            this.id_subject = id_subject;
            this.id = id;
        }
        public void FromCSV(string[] values)
        {
            id = int.Parse(values[0]);
            id_student = int.Parse(values[1]);
            id_subject = int.Parse(values[2]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                id.ToString(),
                id_student.ToString(),
                id_subject.ToString()
            };
            return csvValues;
        }
    }
}
