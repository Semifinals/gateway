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

        return _CheckTemporaryAuthKey();
    }
    
    public override async Task<bool> Authorize()
    {
        // TODO: Implement auth API and setup correct authorization

        return await Task.Run(() => _CheckTemporaryAuthKey());
    }

    private bool _CheckTemporaryAuthKey()
    {
        string? authHeader = Request.Headers["Authorization"].FirstOrDefault(defaultValue: null);

        if (authHeader == null)
            return false;

        if (authHeader != Environment.GetEnvironmentVariable("TemporaryAuthKey"))
            return false;

        return true;
    }
}