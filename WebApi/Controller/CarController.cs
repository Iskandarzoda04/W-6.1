using Domain;
using Infrastructure.Interface;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controller;


[ApiController]
[Route("api/cars")]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;

    public CarController(ICarService carService)
    {
        _carService = carService;
    }

   
    [HttpGet]
    public async Task<List<Car>> GetAll()
    {
        return await _carService.GetAll();
    }

   
    [HttpGet("{id}")]
    public async Task<Car?> GetById(int id)
    {
        return await _carService.GetById(id);
    }

    [HttpPost]
    public async Task<int> Create(Car car)
    {
        return await _carService.Create(car);
    }

  
    [HttpPut]
    public async Task<int> Update(Car car)
    {
        return await _carService.Update(car);
    }

  
    [HttpDelete("{id}")]
    public async Task<int> Delete(int id)
    {
        return await _carService.Delete(id);
    }
}