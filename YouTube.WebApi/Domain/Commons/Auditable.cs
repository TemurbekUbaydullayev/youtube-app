using System.Text.Json.Serialization;

namespace YouTube.WebApi.Domain.Commons;

public class Auditable
{
    public long Id { get; set; }

    [JsonIgnore]
    public bool IsActive { get; set; } = true;

    [JsonIgnore]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [JsonIgnore]
    public DateTime UpdatedAt { get; set; } = DateTime.Now
}
