using System.ComponentModel.DataAnnotations;

namespace YnabCli.Database.SpendingSamples;

public class SpendingSampleGroup
{
    public Guid Id { get; set; }
    
    public DateTime Created { get; set; }

    [MaxLength(50)]
    public required string YnabTransactionDerivedFromId { get; set; }

    public ICollection<SpendingSample> Samples { get; set; } = [];
}