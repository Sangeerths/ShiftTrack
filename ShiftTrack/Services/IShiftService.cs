using ShiftTrack.Models;

namespace ShiftTrack.Services
{
    public interface IShiftService
    {
        Task<List<Shift>> GetAllShiftsAsync();
        Task<Shift?> GetShiftByIdAsync(int id);
        Task<Shift> CreateShiftAsync(Shift shift);
        Task<bool> UpdateShiftAsync(int id, Shift shift);
        Task<bool> DeleteShiftAsync(int id);
    }
}
