using WebApplication1.Models;
using WebApplication1.Models.ModelDTO;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _warehouseRepository;

    public WarehouseService(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }


    public async Task<int> AddProductWarehouse(AddProductWareHouse addProductWareHouse)
    {
        var productWarehouse = DtoToModel(addProductWareHouse);
        if (!await _warehouseRepository.CheckProduct(productWarehouse))
        {
            throw new KeyNotFoundException($"Product with give Id - {productWarehouse.IdProduct} doesn't exist");
        }

        if (!await _warehouseRepository.CheckWarehouse(productWarehouse.IdWarehouse))
        {
            throw new KeyNotFoundException($"Warehouse with give Id - {productWarehouse.IdWarehouse} doesn't exist");
        }
        
        if (!await _warehouseRepository.GetOrder(productWarehouse))
        {
            throw new KeyNotFoundException(
                $"Order with given product Id - {productWarehouse.IdWarehouse} and amount - {productWarehouse.Amount} doesn't exist");
        }
        if (await _warehouseRepository.CheckOrder(productWarehouse.IdOrder))
        {
            throw new ArgumentException(
                $"We already have a value in the Product_Warehouse table with give Id Order - {productWarehouse.IdOrder}");
        }

        await _warehouseRepository.UpdateFulfilledData(productWarehouse.IdOrder);
        var res =  await _warehouseRepository.AddProductWarehouse(productWarehouse);
        return res;
    }

    public ProductWarehouse DtoToModel(AddProductWareHouse addProductWareHouse)
    {
        return new ProductWarehouse()
        {
            Amount = addProductWareHouse.Amount,
            IdProduct = addProductWareHouse.IdProduct,
            IdWarehouse = addProductWareHouse.IdWarehouse,
            CreatedAt = addProductWareHouse.CreatedAt
        };
    }
}