using System.Globalization;
using Cli.Commands.Abstractions;
using Cli.Commands.Abstractions.Outcomes;
using Cli.Instructions.Abstractions;
using CsvHelper;
using Spendfulness.Database;
using Ynab;

namespace SpendfulnessCli.Commands.Exports.YnabCalibration;

public record YnabCalibrationCliCommand : CliCommand
{
}

public class YnabCalibrationCliCommandGenerator : ICliCommandGenerator<YnabCalibrationCliCommand>
{
    public CliCommand Generate(CliInstruction instruction)
    {
        return new YnabCalibrationCliCommand();
    }
}

public class YnabCalibrationCliCommandHandler : ICliCommandHandler<YnabCalibrationCliCommand>
{
    private readonly SpendfulnessBudgetClient _spendfulnessBudgetClient;

    public YnabCalibrationCliCommandHandler(SpendfulnessBudgetClient spendfulnessBudgetClient)
    {
        _spendfulnessBudgetClient = spendfulnessBudgetClient;
    }

    public async Task<CliCommandOutcome> Handle(YnabCalibrationCliCommand request, CancellationToken cancellationToken)
    {
        var budget = await _spendfulnessBudgetClient.GetDefaultBudget();

        var transactions = await budget.GetTransactions();

        var yearToDate = DateTime.UtcNow.AddYears(-1);

        var transactionsYearToDate = transactions
            .Where(t => t.Occured >= yearToDate)
            .ToList();

        var categoryGroups = await budget.GetCategoryGroups();

        var csvRows = new List<YnabCalibrationCsvRow>();

        foreach (var categoryGroup in categoryGroups)
        {
            foreach (var category in categoryGroup.Categories)
            {
                var transactionsForCategory = transactionsYearToDate
                    .Where(t => t.CategoryId == category.Id)
                    .ToList();

                var totalAmount = transactionsForCategory.Sum(t => t.Amount);

                var csvRow = new YnabCalibrationCsvRow
                {
                    CategoryName = category.Name,
                    Thing = "TODO",
                    // TODO: Determine Need vs Want
                    Neccessity = "Need",
                    Fixed = false, // TODO: Determine if fixed
                    WeeklyCost = totalAmount / 52,
                    FortnightlyCost = totalAmount / 26,
                    MonthlyCost = totalAmount / 12,
                    QuarterlyCost = totalAmount / 4,
                    BiannualCost = totalAmount / 2,
                    AnnualCost = totalAmount,
                    ThreeYearlyCost = totalAmount / 3,
                };

                csvRows.Add(csvRow);
            }
        }
        
        var profileDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var ynabCalibrationPath = $"{profileDirectoryPath}//ynab_calibration.csv";
        
        using var writer = new StreamWriter(ynabCalibrationPath);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        await csv.WriteRecordsAsync(csvRows, cancellationToken);

        return new CliCommandOutputOutcome("Ynab Calibration Exported");
    }
}

public record YnabCalibrationCsvRow
{
    // TOOD: Might be able to use a converter and jsut give this the object.
    public string CategoryName { get; set; }
    public string Thing { get; set; }
    public string Neccessity { get; set; }
    public bool Fixed { get; set; }
    public decimal WeeklyCost { get; set; }
    public decimal FortnightlyCost { get; set; }
    public decimal MonthlyCost { get; set; }
    public decimal QuarterlyCost { get; set; }
    public decimal BiannualCost { get; set; }
    public decimal AnnualCost { get; set; }
    public decimal ThreeYearlyCost { get; set; }
}