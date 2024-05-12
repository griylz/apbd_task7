using WebApplication1.Models;
using WebApplication1.Models.ModelDTO;

namespace WebApplication1.Services;

public interface IWarehouseService
{
    public Task<int> AddProductWarehouse(AddProductWareHouse addProductWareHouse);
    public ProductWarehouse DtoToModel(AddProductWareHouse addProductWareHouse);
}