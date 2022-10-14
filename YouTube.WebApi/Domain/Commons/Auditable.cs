namespace YouTube.WebApi.Domain.Commons;

public class Auditable
{
    public uint Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
