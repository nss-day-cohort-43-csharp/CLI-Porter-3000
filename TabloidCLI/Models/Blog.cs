using System.Collections.Generic;

namespace TabloidCLI.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();

        //overriding what the blog class displays
        public override string ToString()
        {
            return $"{Title} ({Url})";
        }
    }
}