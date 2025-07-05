namespace EFCoreTutorial.Data.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public int Number { get; set; }

        public int? AddressId { get; set; }  // Nullable olarak önerilir

        public StudentAddress? Address { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
