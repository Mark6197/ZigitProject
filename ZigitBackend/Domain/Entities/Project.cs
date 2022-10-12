namespace Domain.Entities
{
    public class Project : Entity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public Guid ProjectGuid { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int DurationInDays { get; set; }
        public int BugsCount { get; set; }
        public bool MadeDeadline { get; set; }
    }
}
