using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.ModelDTO;

public class AddProductWareHouse
{
    [Required]
    [Range(1,int.MaxValue, ErrorMessage = "Id of product must be at least 1")]
    public int IdProduct { get; set; }
    [Required]
    [Range(1,int.MaxValue, ErrorMessage = "Id of warehouse must be at least 1")]
    public int IdWarehouse { get; set; }
    [Required]
    public int Amount { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
}