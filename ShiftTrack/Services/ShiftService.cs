using Microsoft.EntityFrameworkCore;
using ShiftTrack.Data;
using ShiftTrack.Models;

namespace ShiftTrack.Services;

public class ShiftService: IShiftService
{
    private readonly ShiftDbContext _dbContext;

    public ShiftService(ShiftDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Shift>> GetAllShiftsAsync()
    {
        return await _dbContext.Shifts
            .Include(s => s.Employee)
            .ToListAsync();
    }

    public async Task<Shift> GetShiftByIdAsync(int id)
    {
        return await _dbContext.Shifts
            .Include(s => s.Employee)
            .FirstOrDefaultAsync(s => s.ShiftId == id);
    }

    public async Task <Shift> CreateShiftAsync(Shift shift)
    {
          _dbContext.Shifts.Add(shift);
          await _dbContext.SaveChangesAsync();
          return shift;
    }

    public async Task<bool> UpdateShiftAsync(int id, Shift shift)
    {
        var existingShift = await _dbContext.Shifts
            .FirstOrDefaultAsync(s => s.ShiftId == id);

        if (existingShift == null)
        {
            return false;
        }

        existingShift.EmployeeId = shift.EmployeeId;
        existingShift.ClockInTime = shift.ClockInTime;
        existingShift.ClockOutTime = shift.ClockOutTime;
        existingShift.Notes = shift.Notes;
        existingShift.Status = shift.Status;
        existingShift.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteShiftAsync(int id)
    {
        var shift = await _dbContext.Shifts
            .FirstOrDefaultAsync(s => s.ShiftId == id);

        if (shift == null)
        {
            return false;
        }

        _dbContext.Shifts.Remove(shift);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}
