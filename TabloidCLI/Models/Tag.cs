namespace TabloidCLI.Models
{

    public class Tag
    {

        public int Id { get; set; }


        public string Name { get; set; }


        public Author author { get; set; }


        public Blog blog { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}