using WebApplication1.Models;
using WebApplication1.Models.ModelDTO;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IProductWarehouseRepository _productWarehouseRepository;

    public WarehouseService(IProductWarehouseRepository productWarehouseRepository)
    {
        _productWarehouseRepository = productWarehouseRepository;
    }


    public int AddProductWarehouse(AddProductWareHouse addProductWareHouse)
    {
        throw new NotImplementedException();
    }

    public ProductWarehouse DtoToModel(AddProductWareHouse addProductWareHouse)
    {
        throw new NotImplementedException();
    }
}