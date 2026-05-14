
using Domain;

namespace Infrastructure.Interface;

public interface ICustomerService
{
    Task<int> Create(Customer customer);
    Task<List<Customer>> GetAll();
    Task<Customer?> GetById(int id);
    Task<int> Update(Customer customer);
    Task<int> Delete(int id);
}