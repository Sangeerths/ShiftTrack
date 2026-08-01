using ShiftTrack.UI.DTO.Employee;
using System.Net.Http.Json;

namespace ShiftTrack.UI.Services;

public class EmployeeApiService
{
    private readonly HttpClient _client;
    private readonly ValidationService _validationService;

    public EmployeeApiService()
    {
        _client = new HttpClient();
        _validationService = new ValidationService();
    }

    public async Task<EmployeeDto[]> GetAllAsync()
    {
        var employees = await _client.GetFromJsonAsync<EmployeeDto[]>("api/Employee");
        return employees ?? Array.Empty<EmployeeDto>();
    }

    public async Task<EmployeeDto?> GetByIdAsync(int id)
    {
        _validationService.ValidateId(id);
        var response = await _client.GetAsync($"api/Employee/{id}");
        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<EmployeeDto>();
    }

    public async Task<bool> CreateAsync(CreateEmployeeDto dto)
    {
        _validationService.ValidateName(dto.EmployeeName);

        var payload = new { EmployeeName = dto.EmployeeName.Trim() };
        var response = await _client.PostAsJsonAsync("api/Employee", payload);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateAsync(UpdateEmployeeDto dto)
    {
        _validationService.ValidateId(dto.EmployeeId);
        _validationService.ValidateName(dto.EmployeeName);

        var payload = new { EmployeeId = dto.EmployeeId, EmployeeName = dto.EmployeeName.Trim(), UpdatedAt = DateTime.UtcNow };
        var response = await _client.PutAsJsonAsync($"api/Employee/{dto.EmployeeId}", payload);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        _validationService.ValidateId(id);
        var response = await _client.DeleteAsync($"api/Employee/{id}");
        return response.IsSuccessStatusCode;
    }
}
