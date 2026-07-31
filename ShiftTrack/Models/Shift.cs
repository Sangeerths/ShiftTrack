namespace ShiftTrack.Models;

public class Shift
{
    public int ShiftId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime ClockInTime { get; set; }
    public DateTime? ClockOutTime { get; set; }
    public string Notes { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
