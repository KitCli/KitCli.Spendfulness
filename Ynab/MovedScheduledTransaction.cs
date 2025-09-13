namespace Ynab;

public record MovedScheduledTransaction(
    string Id,
    Guid AccountId
);