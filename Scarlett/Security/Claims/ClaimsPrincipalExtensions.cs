namespace Scarlett.Security.Claims
{
    using System;
    using System.Security.Claims;

    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            var nameIdentifier = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(nameIdentifier, out Guid id))
            {
                throw new Exception("User ID not found");
            }

            return id;
        }
    }
}