using Domain;

namespace Infrastructure.Interface;

public interface IRentalService
{
    Task<int> Create(Rental rental);
    Task<List<Rental>> GetAll();
    Task<List<Rental>> GetByCustomerId(int customerId);
    Task<int>  Update(Rental rental);
}