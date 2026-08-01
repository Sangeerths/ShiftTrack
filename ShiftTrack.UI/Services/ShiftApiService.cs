using ShiftTrack.Models;
using ShiftTrack.UI.DTO.Shifts;
using System.Net.Http.Json;
namespace ShiftTrack.UI.Services;

public class ShiftApiService
{
    private readonly HttpClient _client;
    private readonly ValidationService _validationService;

    public ShiftApiService()
    {
        _client = new HttpClient();
        _validationService = new ValidationService();
    }


    public async Task<ShiftDto[]> GetAllAsync()
    {
        var shifts = await _client.GetFromJsonAsync<ShiftDto[]>("api/Shift");
        return shifts ?? Array.Empty<ShiftDto>();
    }

    public async Task<ShiftDto?> GetByIdAsync(int id)
    {
        _validationService.ValidateId(id);

        var response = await _client.GetAsync($"api/Shift/{id}");
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<ShiftDto>();
    }

    public async Task<bool> CreateAsync(int employeeId, string notes)
    {
        _validationService.ValidateId(employeeId);
        _validationService.ValidateNotes(notes);

        var payload = new
        {
            EmployeeId = employeeId,
            ClockInTime = DateTime.UtcNow,
            ClockOutTime = (DateTime?)null,
            Notes = notes.Trim(),
            Status = ShiftStatus.InProgress,
            CreatedAt = DateTime.UtcNow
        };

        var response = await _client.PostAsJsonAsync("api/Shift", payload);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAsync(int id, ShiftStatus? status, string? notes, DateTime? clockOutTime = null)
    {
        _validationService.ValidateId(id);

        if (status == null && notes == null && clockOutTime == null)
            throw new ArgumentException("Nothing to update — provide a status, notes, or clock-out time.");

        if (status.HasValue && !Enum.IsDefined(status.Value))
            throw new ArgumentException("Invalid shift status.");

        if (notes != null)
            _validationService.ValidateNotes(notes);

        var payload = new
        {
            ShiftId = id,
            Status = status,
            Notes = notes?.Trim(),
            ClockOutTime = clockOutTime
        };

        var response = await _client.PutAsJsonAsync($"api/Shift/{id}", payload);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        _validationService.ValidateId(id);
        var response = await _client.DeleteAsync($"api/Shift/{id}");
        return response.IsSuccessStatusCode;
    }
}
