namespace TheComicsTestMVC.Models
{
    public class Story
    {
        public int Id { get; set; }
        public string Title { get; set; }    
        public string Description { get; set; }
        public List<Character> Characters { get; set; }
    }
}
