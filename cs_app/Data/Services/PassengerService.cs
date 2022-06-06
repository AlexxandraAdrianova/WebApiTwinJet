using cs_app.Data;
using cs_app.Data.DTOs;
using cs_app.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace cs_app.Data.Services
{
    public class PassengerService
    {
        private EducationContext _context;
        public PassengerService(EducationContext context)
        {
            _context = context;
        }


        //метод создания клиента
        public async Task<Passenger?> AddPassenger(PassengerDTO passenger)
        {
            Passenger npassenger = new Passenger()
            {
                Id = passenger.Id,
                Name = passenger.Name,
                Surname = passenger.Surname,
                SecName = passenger.SecName,
                Age = passenger.Age,
                Doc = passenger.Doc
            };
            if (passenger.Bookings.Any())
            {
                npassenger.Bookings = _context.Bookings.ToList().IntersectBy(passenger.Avias, order => order.Id).ToList();
            }
            var result = _context.Passengers.Add(npassenger);
            await _context.SaveChangesAsync();
            return await Task.FromResult(result.Entity);
        }
        //Метод получения клиента
        public async Task<List<Passenger>> GetPassengers()
        {
            var result = await _context.Passengers.Include(a => a.Bookings).Include(b => b.Avias).ToListAsync();
            return await Task.FromResult(result);
        }
        //Метод получения клиента по ID
        public async Task<Passenger?> GetPassengers(int id)
        {

            var result = _context.Passengers.Include(a => a.Bookings).
                                        Include(b => b.Avias).
                                        FirstOrDefault(passenger => passenger.Id == id);

            return await Task.FromResult(result);
        }
        //Метод получения неполной информации об отеле
        public async Task<List<IncompletePassengerDTO>> GetPassengersIncomplete()
        {
            var passengers = await _context.Passengers.ToListAsync();
            List<IncompletePassengerDTO> result = new List<IncompletePassengerDTO>();
            passengers.ForEach(au => result.Add(new IncompletePassengerDTO
            {
                Id = au.Id,
                Name = au.Name,
                Surname = au.Surname
            }));
            return await Task.FromResult(result);
        }
        //Метод обновления информации об отеле
        public async Task<Passenger?> UpdatePassenger(int id, PassengerDTO updatedPassenger)
        {
            var passenger = await _context.Passengers.Include(passenger => passenger.Bookings).
                                              Include(b => b.Avias).
                                              FirstOrDefaultAsync(au => au.Id == id);
            if (passenger != null)
            {
                passenger.Name = updatedPassenger.Name;
                passenger.Surname = updatedPassenger.Surname;
                passenger.SecName = updatedPassenger.SecName;
                passenger.Age = updatedPassenger.Age;
                passenger.Doc = updatedPassenger.Doc;
                if (updatedPassenger.Bookings.Any())
                {
                    passenger.Bookings = _context.Bookings.ToList().IntersectBy(updatedPassenger.Bookings, orders => orders.Id).ToList();
                }
                if (updatedPassenger.Avias.Any())
                {
                    passenger.Avias = _context.Avias.ToList().IntersectBy(updatedPassenger.Avias, avia => avia.Id).ToList();
                }
                _context.Passengers.Update(passenger);
                _context.Entry(passenger).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return passenger;
            }
            return null;
        }
        //метод удаления отеля
        public async Task<bool> DeletePassenger(int id)
        {
            var passenger = await _context.Passengers.FirstOrDefaultAsync(au => au.Id == id);
            if (passenger != null)
            {
                _context.Passengers.Remove(passenger);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
