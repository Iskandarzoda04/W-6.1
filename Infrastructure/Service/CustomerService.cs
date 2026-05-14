using Dapper;
using Domain;
using Infrastructure.Date;
using Infrastructure.Interface;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class CustomerService : ICustomerService
{
    private readonly DateContext _context;
    private readonly ILogger<CustomerService> _logger;

    public CustomerService(DateContext context, ILogger<CustomerService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<int> Create(Customer customer)
    {
        _logger.LogInformation("Creating customer: fullName={fullName}", customer.FullName);

        try
        {
         
            using var conn = _context.GetConnection();

            var sql = @"insert into Customers (FullName, Phone, Email)
                        values (@FullName, @Phone, @Email)";

            var cs = await conn.ExecuteAsync(sql, customer);

            return cs;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating customer: fullName={fullName}", customer.FullName);
            throw;
        }
    }

    public async Task<List<Customer>> GetAll()
    {
        _logger.LogInformation("Getting all customers");

        try
        {
            using var conn = _context.GetConnection();

            var sql = "select * from Customers order by CustomerId";

            var cs = (await conn.QueryAsync<Customer>(sql)).ToList();

            return cs;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all customers");
            throw;
        }
    }

    public async Task<Customer?> GetById(int id)
    {
        _logger.LogInformation("Getting customer by id: id={id}", id);

        try
        {
          

            using var conn = _context.GetConnection();

            var sql = "select * from Customers where CustomerId = @Id";

            var cs = await conn.QueryFirstOrDefaultAsync<Customer>(sql, new { Id = id });
            return cs;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting customer by id: id={id}", id);
            throw;
        }
    }

    public async Task<int> Update(Customer customer)
    {
        _logger.LogInformation("Updating customer: id={id}", customer.CustomerId);

        try
        {

            using var conn = _context.GetConnection();

            var sql = @"update Customers
                        set FullName = @FullName,
                            Phone = @Phone,
                            Email = @Email
                        where CustomerId = @CustomerId";

            var cs = await conn.ExecuteAsync(sql, customer);
            return cs;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating customer: id={id}", customer.CustomerId);
            throw;
        }
    }

    public async Task<int> Delete(int id)
    {
        _logger.LogInformation("Deleting customer: id={id}", id);

        try
        {

            using var conn = _context.GetConnection();

            var sql = "delete from Customers where CustomerId = @Id";

            var cs = await conn.ExecuteAsync(sql, new { Id = id });
            return cs;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting customer: id={id}", id);
            throw;
        }
    }
}