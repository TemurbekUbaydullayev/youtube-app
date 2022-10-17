using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YouTube.WebApi.Domain.Commons;

namespace YouTube.WebApi.Domain.Entities;

public class Video : Auditable
{
    [MinLength(1), MaxLength(50)]
    public string Name { get; set; } = null!;
    public long UserId { get; set; }
    public string VideoPath { get; set; } = null!;
    public double VideoSize { get; set; }


    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
}
