using System.ComponentModel.DataAnnotations.Schema;

namespace BPSEE.Experiment.Domain.Entities;

public class Order : BaseEntity
{
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;

    public virtual List<OrderLine> OrderLines { get; set; } = new();
}