using System.Net.Http;
using System.Net.Http.Json;
using TestDevCom.Web.Models;
using TestDevCom.Web.Services;

public class AnnouncementService : IAnnouncementService
{
    private readonly HttpClient _client;

    public AnnouncementService(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("TestDevComAPI");
    }

    public async Task<IEnumerable<Announcement>> GetAllAsync()
    {
        return await _client.GetFromJsonAsync<List<Announcement>>("api/Announcement") ?? new();
    }

    public async Task<IEnumerable<Announcement>> GetAllAsync(string? category = null, string? subCategory = null)
    {
        var query = new List<string>();

        if (!string.IsNullOrEmpty(category))
            query.Add($"category={Uri.EscapeDataString(category)}");

        if (!string.IsNullOrEmpty(subCategory))
            query.Add($"subCategory={Uri.EscapeDataString(subCategory)}");

        string queryString = query.Count > 0 ? "?" + string.Join("&", query) : "";

        var response = await _client.GetAsync($"api/Announcement{queryString}");

        if (!response.IsSuccessStatusCode)
            return new List<Announcement>();

        return await response.Content.ReadFromJsonAsync<List<Announcement>>() ?? new();
    }


    public async Task<Announcement?> GetByIdAsync(int id)
    {
        var response = await _client.GetAsync($"api/Announcement/{id}");
        if (!response.IsSuccessStatusCode) return null;

        return await response.Content.ReadFromJsonAsync<Announcement>();
    }

    public async Task<bool> CreateAsync(Announcement model)
    {
        var response = await _client.PostAsJsonAsync("api/Announcement", model);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAsync(Announcement model)
    {
        Console.WriteLine($"BaseAddress: {_client.BaseAddress}");
        Console.WriteLine($"Final URI: {_client.BaseAddress}api/Announcement/{model.Id}");

        var response = await _client.PutAsJsonAsync($"api/Announcement/{model.Id}", model);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _client.DeleteAsync($"api/Announcement/{id}");
        return response.IsSuccessStatusCode;
    }
}
