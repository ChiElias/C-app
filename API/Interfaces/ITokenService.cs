using Company.ClassLibrary1;

namespace API.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}