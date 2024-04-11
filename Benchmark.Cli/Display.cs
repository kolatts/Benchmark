namespace Benchmark.Cli;

public static class Display
{
    public static Color BrandColor { get; } = new(0, 108, 247);

    public static Style BrandStyle { get; } = new(BrandColor);

    public static Color WarningColor { get; } = Color.Yellow;

    public static Style WarningStyle { get; } = new(WarningColor, null, Decoration.Bold);

    public static Color ErrorColor { get; } = Color.Red;
    public static Style ErrorStyle { get; } = new(ErrorColor);

    public static Progress SimpleColumns(this Progress progress)
    {
        return progress.Columns(new SpinnerColumn(Spinner.Known.Aesthetic) { Style = BrandStyle }, new TaskDescriptionColumn());
    }
    public static Progress PercentageColumns(this Progress progress)
    {
        return progress.Columns(new SpinnerColumn(Spinner.Known.Arc), new TaskDescriptionColumn(), new ProgressBarColumn(), new PercentageColumn() { Style = BrandStyle });
    }

    public static ProgressTask Complete(this ProgressTask progressTask)
    {
        progressTask.Value = progressTask.MaxValue;
        return progressTask;
    }

    public static void LogInformation(string text)
    {
        AnsiConsole.Write(new Text(text, BrandStyle));
        AnsiConsole.WriteLine();
    }
    public static void LogError(string text)
    {
        AnsiConsole.Write(new Text(text, ErrorStyle));
        AnsiConsole.WriteLine();
    }

    public static void LogError(Exception ex, string text)
    {
        AnsiConsole.Write(new Text("Error: ", ErrorStyle));
        AnsiConsole.Write(new Text(ex.Message, ErrorStyle));
        AnsiConsole.Write(new Text(" - ", BrandStyle));
        AnsiConsole.Write(new Text(ex.Message, BrandStyle));
        AnsiConsole.WriteLine();
    }
    public static void LogWarning(string text)
    {
        AnsiConsole.Write(new Text(text, WarningStyle));
        AnsiConsole.WriteLine();
    }

}