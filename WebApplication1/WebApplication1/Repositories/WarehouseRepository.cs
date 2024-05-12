using Microsoft.Data.SqlClient;
using WebApplication1.Models;
using WebApplication1.Models.ModelDTO;

namespace WebApplication1.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly IConfiguration _configuration;

    public WarehouseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public async Task<bool> CheckProduct(int idProduct)
    {
        try
        {

            await using var connection = new SqlConnection(_configuration.GetConnectionString("2019SBD"));
            await connection.OpenAsync();
            await using var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"SELECT 1 FROM Product WHERE IdProduct = @IdProduct";
            command.Parameters.AddWithValue("@idProduct", idProduct);
            var result = await command.ExecuteScalarAsync();
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public async Task<bool> CheckWarehouse(int idWarehouse)
    {
        try
        {

            await using var connection = new SqlConnection(_configuration.GetConnectionString("2019SBD"));
            await connection.OpenAsync();
            await using var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"SELECT 1 FROM Warehouse WHERE IdWarehouse = @IdWarehouse";
            command.Parameters.AddWithValue("@IdWarehouse", idWarehouse);
            var result = await command.ExecuteScalarAsync();
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public async Task<bool> GetOrder(ProductWarehouse productWarehouse)
    {
        try
        {

            await using var connection = new SqlConnection(_configuration.GetConnectionString("2019SBD"));
            await connection.OpenAsync();
            await using var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"SELECT IdOrder FROM [Order] WHERE IdProduct = @IdProduct AND Amount = @Amount";
            command.Parameters.AddWithValue("@IdProduct",productWarehouse.IdProduct);
            command.Parameters.AddWithValue("@Amount",productWarehouse.Amount);
            var result = await command.ExecuteScalarAsync();
            if (result != null)
            {
                productWarehouse.IdOrder = (int)result;
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public async Task<bool> CheckOrder(int idOrder)
    {
        try
        {

            await using var connection = new SqlConnection(_configuration.GetConnectionString("2019SBD"));
            await connection.OpenAsync();
            await using var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"SELECT 1 FROM Product_Warehouse WHERE IdOrder = @IdOrder";
            command.Parameters.AddWithValue("@IdOrder",idOrder); 
            var result = await command.ExecuteScalarAsync();
            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public async Task<bool> UpdateFulfilledData(DateTime currentDateTime)
    {
        throw new NotImplementedException();
    }

    public async Task<int> AddProductWarehouse(ProductWarehouse productWarehouse)
    {
        throw new NotImplementedException();
    }
}