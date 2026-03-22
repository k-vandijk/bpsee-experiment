using System.ComponentModel.DataAnnotations.Schema;

namespace BPSEE.Experiment.Domain.Entities;

public class OrderLine : BaseEntity
{
    [ForeignKey(nameof(Order))]
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;

    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal => Quantity * UnitPrice;
}