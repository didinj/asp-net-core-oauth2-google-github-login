using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

public class RoleClaimsTransformer : IClaimsTransformation
{
    private readonly UserManager<IdentityUser> _userManager;

    public RoleClaimsTransformer(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var identity = (ClaimsIdentity)principal.Identity!;
        var user = await _userManager.GetUserAsync(principal);

        if (user != null)
        {
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                if (!identity.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == role))
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                }
            }
        }

        return principal;
    }
}
