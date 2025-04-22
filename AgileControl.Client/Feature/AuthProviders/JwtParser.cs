using System.Security.Claims;
using System.Text.Json;

namespace AgileControl.Client.Feature.AuthProviders;

public static class JwtParser
{
    public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];

        var jsonBytes = ParseBase64WithoutPadding(payload);

        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        if (keyValuePairs != null)
        {
            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
        }

        return claims;
    }

    // Метод для извлечения конкретного claim по ключу.
    public static string GetClaimValue(string jwt, string claimType)
    {
        var claims = ParseClaimsFromJwt(jwt);
        var claim = claims.FirstOrDefault(c => c.Type == claimType);
        return claim?.Value ?? string.Empty;
    }

    // Метод для получения email пользователя из токена.
    public static string GetUserEmail(string jwt)
    {
        return GetClaimValue(jwt, ClaimTypes.Email);
    }

    // Метод для получения UserId из токена.
    public static string GetUserId(string jwt)
    {
        return GetClaimValue(jwt, ClaimTypes.NameIdentifier);
    }

    // Метод для парсинга Base64 без паддинга.
    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}