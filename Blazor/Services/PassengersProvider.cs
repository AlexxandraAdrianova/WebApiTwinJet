using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Blazor.Data.Models;
using Blazor.Services;

namespace Blazor.Services
{
    public class PassengersProvider :IPassengerProvider
    {
        private HttpClient _client;
        public PassengersProvider(HttpClient passenger)
        {
            _client = passenger;
        }

        public async Task<List<Passenger>> GetAll()
        {
            return await _client.GetFromJsonAsync<List<Passenger>>("/api/passenger");
        }

        public async Task<Passenger> GetOne(int id)
        {
            return await _client.GetFromJsonAsync<Passenger>($"/api/passenger/{id}");
        }

        public async Task<bool> Add(Passenger avia)
        {
            string data = JsonConvert.SerializeObject(avia);
            StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var responce = await _client.PostAsync($"/api/passenger", httpContent);
            return await Task.FromResult(responce.IsSuccessStatusCode);
        }

        public async Task<Passenger> Edit(Passenger avia)
        {
            string data = JsonConvert.SerializeObject(avia);
            StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var responce = await _client.PutAsync($"/api/passenger", httpContent);
            Passenger passenger = JsonConvert.DeserializeObject<Passenger>(responce.Content.ReadAsStringAsync().Result);
            return await Task.FromResult(passenger);
        }

        public async Task<bool> Remove(int id)
        {
            var delete = await _client.DeleteAsync($"/api/passenger/${id}");

            return await Task.FromResult(delete.IsSuccessStatusCode);
        }

    }
}

