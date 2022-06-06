using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Blazor.Data.Models;
using Blazor.Services;

namespace Blazor.Services
{
    public class BookingsProvider : IBookingProvider
    {
        private HttpClient _client;
        public BookingsProvider(HttpClient passenger)
        {
            _client = passenger;
        }

        public async Task<List<Booking>> GetAll()
        {
            return await _client.GetFromJsonAsync<List<Booking>>("/api/booking");
        }

        public async Task<Booking> GetOne(int id)
        {
            return await _client.GetFromJsonAsync<Booking>($"/api/booking/{id}");
        }

        public async Task<bool> Add(Booking avia)
        {
            string data = JsonConvert.SerializeObject(avia);
            StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var responce = await _client.PostAsync($"/api/booking", httpContent);
            return await Task.FromResult(responce.IsSuccessStatusCode);
        }

        public async Task<Booking> Edit(Booking avia)
        {
            string data = JsonConvert.SerializeObject(avia);
            StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var responce = await _client.PutAsync($"/api/booking", httpContent);
            Booking booking = JsonConvert.DeserializeObject<Booking>(responce.Content.ReadAsStringAsync().Result);
            return await Task.FromResult(booking);
        }

        public async Task<bool> Remove(int id)
        {
            var delete = await _client.DeleteAsync($"/api/booking/${id}");

            return await Task.FromResult(delete.IsSuccessStatusCode);
        }

    }
}