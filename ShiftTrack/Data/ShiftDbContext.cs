using Microsoft.EntityFrameworkCore;
using ShiftTrack.Models;

namespace ShiftTrack.Data
{
    public class ShiftDbContext: DbContext
    {
        public ShiftDbContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<Shift> Shifts { get; set; }
    }
}
