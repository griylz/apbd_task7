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


    public async Task<bool> CheckProduct(ProductWarehouse productWarehouse)
    {
        try
        {

            await using var connection = new SqlConnection(_configuration.GetConnectionString("2019SBD"));
            await connection.OpenAsync();
            await using var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"SELECT Price FROM Product WHERE IdProduct = @IdProduct";
            command.Parameters.AddWithValue("@idProduct", productWarehouse.IdProduct);
            var result = await command.ExecuteScalarAsync();
            if (result != DBNull.Value && result != null)
            {
                decimal price = (decimal)result;
                productWarehouse.Price = price * productWarehouse.Amount;
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

    public async Task<bool> UpdateFulfilledData(int idOrder)
    {
        try
        {

            await using var connection = new SqlConnection(_configuration.GetConnectionString("2019SBD"));
            await connection.OpenAsync();
            await using var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"UPDATE [Order] SET FulfilledAt = @FulfilledAt WHERE IdOrder=@IdOrder";
            command.Parameters.AddWithValue("@FulfilledAt", DateTime.UtcNow);
            command.Parameters.AddWithValue("@IdOrder",idOrder); 
            var result = await command.ExecuteNonQueryAsync();
            if (result > 0)
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

    public async Task<int> AddProductWarehouse(ProductWarehouse productWarehouse)
    {
        try
        {

            await using var connection = new SqlConnection(_configuration.GetConnectionString("2019SBD"));
            await connection.OpenAsync();
            await using var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = @"INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) VALUES (@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, @CreatedAt);SELECT SCOPE_IDENTITY();";
            command.Parameters.AddWithValue("@IdWarehouse",productWarehouse.IdWarehouse);
            command.Parameters.AddWithValue("@IdProduct",productWarehouse.IdProduct);
            command.Parameters.AddWithValue("@IdOrder",productWarehouse.IdOrder);
            command.Parameters.AddWithValue("@Amount",productWarehouse.Amount);
            command.Parameters.AddWithValue("@Price",productWarehouse.Price);
            command.Parameters.AddWithValue("@CreatedAt",productWarehouse.CreatedAt);

            var res = await command.ExecuteScalarAsync();
            if (res != null)
            {
                return Convert.ToInt32(res);
            }
            else
            {
                Console.WriteLine("sas");
                return 0;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }   
    }
}