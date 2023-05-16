using Semifinals.Utils.GatewayFramework.Authentication;

namespace Semifinals.Api.Utils.Authentication;

public class SemifinalsAuthenticator : Authenticator
{
    public SemifinalsAuthenticator(
        HttpRequest req,
        bool requiresAuthorizationHeader = false,
        int requiresPermissions = 0
        )
    : base(
        req,
        requiresAuthorizationHeader,
        requiresPermissions) { }

    public override bool Authenticate()
    {
        // TODO: Implement auth API and setup correct authentication

        bool inDevEnvironment = Environment.GetEnvironmentVariable("Development") != null;
        return inDevEnvironment;
    }
    
    public override async Task<bool> Authorize()
    {
        // TODO: Implement auth API and setup correct authorization

        bool inDevEnvironment = Environment.GetEnvironmentVariable("Development") != null;
        return await Task.Run(() => inDevEnvironment);
    }
}