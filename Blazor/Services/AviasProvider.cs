using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Blazor.Data.Models;
using Blazor.Services;

namespace Blazor.Services
{
    public class AviasProvider : IAviaProvider
    {
        private HttpClient _client;
        public AviasProvider(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<Avia>> GetAll()
        {
            return await _client.GetFromJsonAsync<List<Avia>>("/api/avia");
        }

        public async Task<Avia> GetOne(int id)
        {
            return await _client.GetFromJsonAsync<Avia>($"/api/avia/{id}");
        }

        public async Task<bool> Add(Avia avia)
        {
            string data = JsonConvert.SerializeObject(avia);
            StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var responce = await _client.PostAsync($"/api/avia", httpContent);
            return await Task.FromResult(responce.IsSuccessStatusCode);
        }

        public async Task<Avia> Edit(Avia avia)
        {
            string data = JsonConvert.SerializeObject(avia);
            StringContent httpContent = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var responce = await _client.PutAsync($"/api/avia", httpContent);
            Avia item1 = JsonConvert.DeserializeObject<Avia>(responce.Content.ReadAsStringAsync().Result);
            return await Task.FromResult(item1);
        }

        public async Task<bool> Remove(int id)
        {
            var delete = await _client.DeleteAsync($"/api/avia/${id}");

            return await Task.FromResult(delete.IsSuccessStatusCode);
        }

    }
}

