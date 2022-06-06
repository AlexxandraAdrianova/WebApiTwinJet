using Blazor.Data.Models;

namespace Blazor.Services
{
    public interface IBookingProvider //интерфейс, описывающий получение данных по отелям
    {
        Task<List<Booking>> GetAll();
        Task<Booking> GetOne(int id);
        Task<bool> Add(Booking avia);
        Task<Booking> Edit(Booking avia);
        Task<bool> Remove(int id);

    }
}