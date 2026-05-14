using Dapper;
using Domain;
using Infrastructure.Interface;
using Infrastructure.Date;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class CarService : ICarService
{
    private readonly DateContext _context;
    private readonly ILogger<CarService> _logger;

    public CarService(DateContext context, ILogger<CarService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<int> Create(Car car)
    {
       _logger.LogInformation("Creating a new car: model={model}, manufacturer={manufacturer}" , car.Model, car.Manufacturer);
         
         try
        {
             using var conn = _context.GetConnection();

     if (string.IsNullOrWhiteSpace(car.Model))

        Console.WriteLine("Model is required");

    if (string.IsNullOrWhiteSpace(car.Manufacturer))
        Console.WriteLine("Manufacturer is required");

    if (car.Year < 1990 || car.Year > DateTime.Now.Year)

       Console.WriteLine("Invalid year");

    if (car.PricePerDay <= 0)
            
     Console.WriteLine("Price per day must be greater than 0");

            

        var sql = @"insert into Cars (Model, Manufacturer, Year, PricePerDay)
                    values (@Model, @Manufacturer, @Year, @PricePerDay)";

        return await conn.ExecuteAsync(sql, car);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating a new car: model={model}, manufacturer={manufacturer}", car.Model, car.Manufacturer);
            throw;
        }

    }


       
    

 
    public async Task<List<Car>> GetAll()
    {
        _logger.LogInformation("Retrieving all cars");

        try
        {
        using var conn = _context.GetConnection();

        var sql = "select * from Cars";

        var cr =(await conn.QueryAsync<Car>(sql)).ToList();
        return cr;


        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all cars");
            throw;
        }
      
    }

    public async Task<Car?> GetById(int id)
    {
        _logger.LogInformation("Retrieving car by id: id={id}", id);

        try

        {
         using var conn = _context.GetConnection();


        var sql = "select * from Cars where CarId = @Id";

        var cr = await conn.QueryFirstOrDefaultAsync<Car>(sql, new { Id = id });  
        return cr;

        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error retrieving car by id: id={id}", id);
            throw;
        }
   
    }

  
    public async Task<int> Update(Car car)
    {
        _logger.LogInformation("Updating car: id={id}", car.CarId);

        try
        {
            using var conn = _context.GetConnection();

            var sql = @"update Cars
                        set Model = @Model,
                            Manufacturer = @Manufacturer,
                            Year = @Year,
                        PricePerDay = @PricePerDay
                    where CarId = @CarId";

            return await conn.ExecuteAsync(sql, car);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating car: id={id}", car.CarId);
            throw;
        }
    }

    public async Task<int> Delete(int id)
    {
        _logger.LogInformation("Deleting car: id={id}", id);

        try
        {
            using var conn = _context.GetConnection();


        var sql = "delete from Cars where CarId = @Id";

        var cr = await conn.ExecuteAsync(sql, new { Id = id });
        return cr;
        } 
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error deleting car; id={id}", id);
            throw;
        }
    }
}



