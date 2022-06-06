using Blazor.Data.Models;

namespace Blazor.Services
{
    public interface IAviaProvider //интерфейс, описывающий получение данных по билетам
    {
        Task<List<Avia>> GetAll();
        Task<Avia> GetOne(int id);
        Task<bool> Add(Avia avia);
        Task<Avia> Edit(Avia avia);
        Task<bool> Remove(int id);
    }
}
