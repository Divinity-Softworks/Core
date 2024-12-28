namespace Microsoft.IdentityModel.JsonWebTokens;

public static class JsonWebTokenHandlerExtensions {
    /// <summary>
    /// Checks if a JWT token is expired.
    /// </summary>
    /// <param name="jwtHandler">The <see cref="JsonWebTokenHandler"/> instance.</param>
    /// <param name="token">The JWT token as a string.</param>
    /// <returns>True if the token is expired; otherwise, false.</returns>
    /// <exception cref="ArgumentException">Thrown when the token does not appear to be in a proper JWT format or does not contain an expiration claim.</exception>
    public static bool IsTokenExpired(this JsonWebTokenHandler jwtHandler, string token) {
        // Check if the token is in a valid JWT format
        if (!jwtHandler.CanReadToken(token))
            throw new ArgumentException("The token does not appear to be in a proper JWT format.");

        // Read the token
        JsonWebToken jsonWebToken = new (token);

        // Extract the expiration claim
        System.Security.Claims.Claim claim = jsonWebToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Exp)
            ?? throw new ArgumentException("The token does not contain an expiration claim.");

        // Convert expiration time from seconds since epoch to DateTime
        long exp = long.Parse(claim.Value);
        DateTime expirationDate = DateTimeOffset.FromUnixTimeSeconds(exp).UtcDateTime;

        // Compare with the current time
        return expirationDate < DateTime.UtcNow;
    }
}
