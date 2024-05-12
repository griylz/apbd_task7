using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.ModelDTO;
using WebApplication1.Services;

namespace WebApplication1.Controllers;
[ApiController]
[Route("api/[controller]")]
public class WareHouseController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;

    public WareHouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpPost]
    public async Task<IActionResult> AddProductWarehouse([FromBody] AddProductWareHouse addProductWareHouse)
    {
        try
        {
            return Ok(await _warehouseService.AddProductWarehouse(addProductWareHouse));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
}