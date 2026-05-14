using Dapper;
using Domain;
using Infrastructure.Date;
using Infrastructure.Interface;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Service;

public class RentalService : IRentalService
{
    private readonly DateContext _context;
    private readonly ILogger<RentalService> _logger;

    public RentalService(DateContext context, ILogger<RentalService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<int> Create(Rental rental)
    {
        _logger.LogInformation("Creating rental for customerId={customerId}", rental.CustomerId);

        try
        {
            using var conn = _context.GetConnection();


            if (rental.TotalCost <= 0)
            {
                Console.WriteLine("TotalCost must be greater than 0");
            }

            var sql = @"insert into Rentals
                        (CarId, CustomerId, StartDate, EndDate, TotalCost)
                        values
                        (@CarId, @CustomerId, @StartDate, @EndDate, @TotalCost)";

            return await conn.ExecuteAsync(sql, rental);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating rental");
            throw;
        }
    }

    public async Task<List<Rental>> GetAll()
    {
        _logger.LogInformation("Getting all rentals");

        try
        {
            using var conn = _context.GetConnection();

            var sql = "select * from Rentals";

            var rentals = (await conn.QueryAsync<Rental>(sql)).ToList();

            return rentals;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all rentals");
            throw;
        }
    }

    public async Task<List<Rental>> GetByCustomerId(int customerId)
    {
        _logger.LogInformation("Getting rentals by customerId={customerId}", customerId);

        try
        {
            using var conn = _context.GetConnection();

            var sql = "select * from Rentals where CustomerId = @CustomerId";

             var rnt = (await conn.QueryAsync<Rental>(sql, new { CustomerId = customerId })).ToList();

            return rnt;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting rentals by customerId={customerId}", customerId);
            throw;
        }
    }

    public async Task<int> Update(Rental rental)
    {
        _logger.LogInformation("Updating rental id={id}", rental.RentalId);

        try
        {
            using var conn = _context.GetConnection();

            if (rental.StartDate >= rental.EndDate)
            {
                Console.WriteLine("StartDate must be less than EndDate");
            }

            var sql = @"update Rentals
                        set CarId = @CarId,
                            CustomerId = @CustomerId,
                            StartDate = @StartDate,
                            EndDate = @EndDate,
                            TotalCost = @TotalCost
                        where RentalId = @RentalId";

            var rnt = await conn.ExecuteAsync(sql, rental);

            return rnt;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating rental id={id}", rental.RentalId);
            throw;
        }
    }
}