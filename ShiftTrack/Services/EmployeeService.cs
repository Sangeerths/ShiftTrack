using Microsoft.EntityFrameworkCore;
using ShiftTrack.Data;
using ShiftTrack.Models;

namespace ShiftTrack.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly ShiftDbContext _dbContext;

        public EmployeeService(ShiftDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _dbContext.Employees
                .ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _dbContext.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> UpdateEmployeeAsync(int id, Employee employee)
        {
            var existingEmployee = await _dbContext.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (existingEmployee == null)
            {
                return false;
            }

            existingEmployee.EmployeeId = employee.EmployeeId;
            existingEmployee.Name = employee.Name;
            existingEmployee.UpdatedAt = employee.UpdatedAt;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _dbContext.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
            {
                return false;
            }

            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
