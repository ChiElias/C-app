
using System.Security.Claims;

namespace API.Extensions;

public static class ClaimsPrincipalExtensions {
  public static string? GetUsername(this ClaimsPrincipal user) => user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}