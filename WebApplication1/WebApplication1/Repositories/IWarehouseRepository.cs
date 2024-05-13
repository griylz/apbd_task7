using WebApplication1.Models;
using WebApplication1.Models.ModelDTO;

namespace WebApplication1.Repositories;

public interface IWarehouseRepository
{
    Task<bool> CheckProduct(ProductWarehouse productWarehouse);
    Task<bool> CheckWarehouse(int idWarehouse);
    Task<bool> GetOrder(ProductWarehouse productWarehouse);
    Task<bool> CheckOrder(int idOrder);
    Task<bool> UpdateFulfilledData(int idOrder);
    Task<int> AddProductWarehouse(ProductWarehouse productWarehouse);

}