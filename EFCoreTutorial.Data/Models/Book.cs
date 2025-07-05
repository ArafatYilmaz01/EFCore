namespace EFCoreTutorial.Data.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Author { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int StudentId { get; set; }

        public Student Student { get; set; } = null!;
    }
}
