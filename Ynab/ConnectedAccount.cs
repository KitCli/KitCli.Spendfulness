using Ynab.Clients;
using Ynab.Responses.Accounts;

namespace Ynab;

public class ConnectedAccount(AccountsClient client, AccountResponse response) : Account(response)
{
    private readonly AccountsClient _client = client;
}