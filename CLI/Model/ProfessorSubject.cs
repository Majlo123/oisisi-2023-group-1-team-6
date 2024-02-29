namespace CLI.Model
{
    public class ProfessorSubject : StudentskaSluzba.Serialization.ISerializable
    {
        public int id { get; set; }
        public string id_professor { get; set; }

        public int id_subject { get; set; }

        public ProfessorSubject()
        {


        }
        public ProfessorSubject(string id_professor, int id_subject, int id)
        {
            this.id_professor = id_professor;
            this.id_subject = id_subject;
            this.id = id;
        }
        public void FromCSV(string[] values)
        {
            id = int.Parse(values[0]);
            id_professor = (values[1]);
            id_subject = int.Parse(values[2]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                id.ToString(),
                id_professor.ToString(),
                id_subject.ToString()
            };
            return csvValues;
        }


    }
}
