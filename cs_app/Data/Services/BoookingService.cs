using cs_app.Data.DTOs;
using cs_app.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace cs_app.Data.Services
{
    public class BookingService
    {
        private EducationContext _context;

        public BookingService(EducationContext context)
        {
            _context = context;
        }


        //метод создания отеля
        public async Task<Booking?> AddBooking(BookingDTO booking)
        {
            Booking nbooking = new Booking
            {
                Hotel = booking.Hotel,
                Room = booking.Room,
                Id = booking.Id,
                CarName = booking.CarName,
                GuideName = booking.GuideName,
            };
            if (booking.Avias.Any())
            {
                nbooking.Avias = _context.Avias.ToList().IntersectBy(booking.Avias, avias => avias.Id).ToList();
            }


            var result = _context.Bookings.Add(nbooking);
            await _context.SaveChangesAsync();
            return await Task.FromResult(result.Entity);
        }

        //Метод получения отеля
        public async Task<List<Booking>> GetBookings()
        {
            var result = await _context.Bookings.Include(a => a.Passengers).Include(b => b.Avias).ToListAsync();
            return await Task.FromResult(result);
        }

        //Метод получения отеля по ID
        public async Task<Booking?> GetBooking(int id)
        {
            var result = _context.Bookings.Include(a => a.Passengers).Include(b => b.Avias)
                .FirstOrDefault(booking => booking.Id == id);

            return await Task.FromResult(result);
        }

        //Метод получения неполной информации об отеле
        public async Task<List<IncompleteBookingDTO>> GetBookingsIncomplete()
        {
            var bookings = await _context.Bookings.ToListAsync();
            List<IncompleteBookingDTO> result = new List<IncompleteBookingDTO>();
            bookings.ForEach(au => result.Add(new IncompleteBookingDTO
            {
                Id = au.Id,
                Hotel = au.Hotel
            }));
            return await Task.FromResult(result);
        }

        //Метод обновления информации об отеле
        public async Task<Booking?> UpdateBooking(int id, BookingDTO updatedBooking)
        {
            var booking = await _context.Bookings.Include(booking => booking.Passengers).Include(b => b.Avias)
                .FirstOrDefaultAsync(au => au.Id == id);
            if (booking != null)
            {
                booking.Hotel = updatedBooking.Hotel;
                booking.Room = updatedBooking.Room;
                booking.CarName = updatedBooking.CarName;
                booking.GuideName = updatedBooking.GuideName;
                if (updatedBooking.Avias.Any())
                {
                    booking.Avias = _context.Avias.ToList().IntersectBy(updatedBooking.Avias, avia => avia.Id).ToList();
                }

                if (updatedBooking.Passengers.Any())
                {
                    booking.Passengers = _context.Passengers.ToList()
                        .IntersectBy(updatedBooking.Passengers, passenger => passenger.Id).ToList();
                }

                _context.Bookings.Update(booking);
                _context.Entry(booking).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return booking;
            }

            return null;
        }

        //метод удаления отеля
        public async Task<bool> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(au => au.Id == id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}