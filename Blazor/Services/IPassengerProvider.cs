using Blazor.Data.Models;

namespace Blazor.Services
{
    public interface IPassengerProvider //интерфейс, описывающий получение данных по клиентам
    {
        Task<List<Passenger>> GetAll();
        Task<Passenger> GetOne(int id);
        Task<bool> Add(Passenger item);
        Task<Passenger> Edit(Passenger item);
        Task<bool> Remove(int id);
    }
}
