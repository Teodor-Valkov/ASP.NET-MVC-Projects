using LocalPub.Models;
using System.Security.Principal;

public static class PrincipalExtensions
{
    public static int GetUserId(this IPrincipal principal)
    {
        ClientModel client = principal as ClientModel;

        if (client != null)
        {
            return client.Id;
        }

        return 0;
    }
}