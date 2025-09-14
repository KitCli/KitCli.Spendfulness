namespace Ynab.Http;

public record YnabHttpError(
    string Id,
    string Name,
    string Detail
);