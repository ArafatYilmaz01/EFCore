namespace EFCoreTutorial.Data.Models
{
    public class StudentAddress
    {
        public int Id { get; set; }

        public string City { get; set; } = string.Empty;

        public string District { get; set; } = string.Empty;

        public string FullAddress { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public Student? Student { get; set; }
    }
}
