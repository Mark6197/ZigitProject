namespace Domain.Entities
{
    public class User:Entity
    {
        public long Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public PersonalDetails PersonalDetails { get; set; }
    }

    public class PersonalDetails
    {
        public long UserId { get; set; }
        public string Team { get; set; }
        public string Avatar { get; set; }
        public DateTime JoinedAt { get; set; }
        public string Name { get; set; }
    }
}
