namespace ZigitBackend.DTOs
{
    /// <summary>
    /// Login result model
    /// </summary>
    public record LoginResultDTO(string Token, PersonalDetailsDTO PersonalDetails);

}
