using ShiftTrack.UI.DTO.Employee;
using ShiftTrack.UI.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShiftTrack.UI.UI
{
    public class EmployeeMenu
    {
        private readonly EmployeeApiService _employeeApiService;
        private readonly ConsoleUI _ui;

        public EmployeeMenu()
        {
            _employeeApiService = new EmployeeApiService();
            _ui = new ConsoleUI();
        }

        public async Task ListEmployeesAsync()
        {
            var employees = await AnsiConsole.Status()
                .StartAsync("Fetching employees...", async _ => await _employeeApiService.GetAllAsync());

            var table = new Table().Border(TableBorder.Rounded);
            table.AddColumn("ID");
            table.AddColumn("Name");

            foreach (var e in employees)
            {
                table.AddRow(e.EmployeeId.ToString(), Markup.Escape(e.EmployeeName));
            }

            AnsiConsole.Write(table);
        }

        public async Task GetEmployeeByIdAsync()
        {
            var id = AnsiConsole.Ask<int>("Employee [green]ID[/]:");

            var employee = await AnsiConsole.Status()
                .StartAsync("Looking up employee...", async _ => await _employeeApiService.GetByIdAsync(id));

            if (employee == null)
            {
                _ui.ShowError("Employee not found.");
                return;
            }

            var table = new Table().Border(TableBorder.Rounded);
            table.AddColumn("ID");
            table.AddColumn("Name");
            table.AddRow(employee.EmployeeId.ToString(), Markup.Escape(employee.EmployeeName));
            AnsiConsole.Write(table);
        }

        public async Task CreateEmployeeAsync()
        {
            CreateEmployeeDto employee = new CreateEmployeeDto
            {
                EmployeeName = AnsiConsole.Ask<string>("Employee [green]name[/]:")
            };

            var success = await AnsiConsole.Status()
                .StartAsync("Creating employee...", async _ => await _employeeApiService.CreateAsync(employee));

            if (success)
                _ui.ShowSuccess("Employee created.");
            else
                _ui.ShowError("Failed to create employee.");
        }

        public async Task UpdateEmployeeAsync()
        {
            UpdateEmployeeDto employee = new UpdateEmployeeDto
            {
                EmployeeId = AnsiConsole.Ask<int>("Employee [green]ID[/]:"),
                EmployeeName = AnsiConsole.Ask<string>("New [green]name[/]:")
            };

            var success = await AnsiConsole.Status()
                .StartAsync("Updating employee...", async _ => await _employeeApiService.UpdateAsync(employee));

            if (success)
                _ui.ShowSuccess("Employee updated.");
            else
                _ui.ShowError("Failed to update employee.");
        }

    }
}
