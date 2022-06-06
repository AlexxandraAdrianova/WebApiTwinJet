using cs_app.Data.DTOs;
using cs_app.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace cs_app.Data.Services
{
    public class AviaService
    {
        private EducationContext _context;

        public AviaService(EducationContext context)
        {
            _context = context;
        }


        //метод создания отеля
        public async Task<Avia?> AddAvia(AviaDTO avia)
        {
            Avia navia = new Avia
            {
                Id = avia.Id,
                Dest = avia.Dest,
                Place = avia.Place,
                NumPass = avia.NumPass,
                Flight = avia.Flight,
                Country = avia.Country,
                Price = avia.Price,
            };
            if (avia.Passengers.Any())
            {
                navia.Passengers = _context.Passengers.ToList()
                    .IntersectBy(avia.Passengers, affiliation => affiliation.Id)
                    .ToList();
            }

            var result = _context.Avias.Add(navia);
            await _context.SaveChangesAsync();
            return await Task.FromResult(result.Entity);
        }

        //Метод получения отеля
        public async Task<List<Avia>> GetAvias()
        {
            var result = await _context.Avias.Include(a => a.Passengers).Include(b => b.Bookings).ToListAsync();
            return await Task.FromResult(result);
        }

        //Метод получения отеля по ID
        public async Task<Avia?> GetAvia(long id)
        {
            var result = _context.Avias.Include(a => a.Passengers).Include(b => b.Bookings)
                .FirstOrDefault(avia => avia.Id == id);

            return await Task.FromResult(result);
        }

        //Метод получения неполной информации об отеле
        public async Task<List<IncompleteAviaDTO>> GetAviasIncomplete()
        {
            var avias = await _context.Avias.ToListAsync();
            List<IncompleteAviaDTO> result = new List<IncompleteAviaDTO>();
            avias.ForEach(au => result.Add(new IncompleteAviaDTO
            {
                Id = au.Id,
                Country = au.Country,
                Price = au.Price,
            }));
            return await Task.FromResult(result);
        }

        //Метод обновления информации об отеле
        public async Task<Avia?> UpdateAvia(long id, AviaDTO updatedAvia)
        {
            var avia = await _context.Avias.Include(avia => avia.Passengers).Include(b => b.Bookings)
                .FirstOrDefaultAsync(au => au.Id == id);
            if (avia != null)
            {
                avia.Country = updatedAvia.Country;
                avia.Price = updatedAvia.Price;
                avia.Place = updatedAvia.Place;
                avia.Dest = updatedAvia.Dest;
                avia.NumPass = updatedAvia.NumPass;
                avia.Flight = updatedAvia.Flight;
                if (updatedAvia.Passengers.Any())
                {
                    avia.Passengers = _context.Passengers.ToList()
                        .IntersectBy(updatedAvia.Passengers, passenger => passenger.Id).ToList();
                }

                if (updatedAvia.Bookings.Any())
                {
                    avia.Bookings = _context.Bookings.ToList().IntersectBy(updatedAvia.Bookings, bookings => bookings.Id)
                        .ToList();
                }

                _context.Avias.Update(avia);
                _context.Entry(avia).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return avia;
            }

            return null;
        }

        //метод удаления отеля
        public async Task<bool> DeleteAvia(long id)
        {
            var avia = await _context.Avias.FirstOrDefaultAsync(au => au.Id == id);
            if (avia != null)
            {
                _context.Avias.Remove(avia);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}