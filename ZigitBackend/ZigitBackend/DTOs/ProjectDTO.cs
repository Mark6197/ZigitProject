
namespace ZigitBackend.DTOs
{
    public record ProjectDTO(Guid ProjectGuid, string Name, int Score, int DurationInDays, int BugsCount, bool MadeDeadline);
}