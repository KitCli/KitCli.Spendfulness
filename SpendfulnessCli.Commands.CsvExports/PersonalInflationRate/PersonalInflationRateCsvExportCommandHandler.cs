using Cli.Commands.Abstractions.Handlers;
using Cli.Commands.Abstractions.Outcomes;

namespace SpendfulnessCli.Commands.CsvExports.PersonalInflationRate;

public class PersonalInflationRateCsvExportCommandHandler : ICliCommandHandler<PersonalInflationRateCsvExportCommand>
{
    public Task<CliCommandOutcome[]> Handle(PersonalInflationRateCsvExportCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}