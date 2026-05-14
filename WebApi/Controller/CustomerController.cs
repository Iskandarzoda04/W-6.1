using Domain;
using Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller;


[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

  
    [HttpGet]
    public async Task<List<Customer>> GetAll()
    {
        return await _customerService.GetAll();
    }

  
    [HttpGet("{id}")]
    public async Task<Customer?> GetById(int id)
    {
        return await _customerService.GetById(id);
    }

   
    [HttpPost]
    public async Task<int> Create(Customer customer)
    {
        return await _customerService.Create(customer);
    }

  
    [HttpPut]
    public async Task<int> Update(Customer customer)
    {
        return await _customerService.Update(customer);
    }

    [HttpDelete("{id}")]
    public async Task<int> Delete(int id)
    {
        return await _customerService.Delete(id);
    }
}
