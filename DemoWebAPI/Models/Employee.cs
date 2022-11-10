namespace DemoWebAPI.Models
{
    public sealed class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Password { get; set; }
        public string Role { get; set; } = "user";
    }
}
