namespace EFCoreTutorial.Data.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
