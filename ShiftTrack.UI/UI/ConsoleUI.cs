using Spectre.Console;

namespace ShiftTrack.UI.UI;

public class ConsoleUI
{
    public void ShowHeader()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new FigletText("ShiftTrack").Color(Color.Cyan1));
        AnsiConsole.MarkupLine("[grey]Manage employees, track shifts, and stay on schedule.[/]");
        AnsiConsole.WriteLine();
    }

    public void Pause()
    {
        AnsiConsole.MarkupLine("\n[grey italic]Press any key to continue...[/]");
        Console.ReadKey(true);
    }

    public void ShowSuccess(string message) =>
        AnsiConsole.MarkupLine($"[green]✓ {message}[/]");

    public void ShowError(string message) =>
        AnsiConsole.MarkupLine($"[red]✗ {message}[/]");

    public void ShowInfo(string message) =>
        AnsiConsole.MarkupLine($"[aqua]i {message}[/]");

    public void ShowGoodbye()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new FigletText("Goodbye!").Color(Color.Cyan1));
        AnsiConsole.MarkupLine("[aqua]Thanks for using ShiftTrack![/]");
    }
}