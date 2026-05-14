using Domain;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;


[ApiController]
[Route("api/rentals")]
public class RentalController : ControllerBase
{
    private readonly IRentalService _rentalService;

    public RentalController(IRentalService rentalService)
    {
        _rentalService = rentalService;
    }

    
    [HttpGet]
    public async Task<List<Rental>> GetAll()
    {
        return await _rentalService.GetAll();
    }

  
    [HttpGet("customer/{customerId}")]
    public async Task<List<Rental>> GetByCustomerId(int customerId)
    {
        return await _rentalService.GetByCustomerId(customerId);
    }

   
    [HttpPost]
    public async Task<int> Create(Rental rental)
    {
        return await _rentalService.Create(rental);
    }

  
    [HttpPut]
    public async Task<int> Update(Rental rental)
    {
        return await _rentalService.Update(rental);
    }
}