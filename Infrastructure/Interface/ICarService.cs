using Domain;
namespace Infrastructure.Interface;

public interface ICarService
{
    Task<int> Create(Car car);
    Task<List<Car>> GetAll();
    Task<Car?> GetById(int id);
    Task<int> Update(Car car);
    Task<int> Delete(int id);
}