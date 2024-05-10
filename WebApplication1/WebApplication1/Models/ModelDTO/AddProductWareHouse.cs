using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.ModelDTO;

public class AddProductWareHouse
{
    [Required]
    public int IdOrder { get; set; }
    [Required]
    public int IdWarehouse { get; set; }
    [Required]
    public int Amount { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
}