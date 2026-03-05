using DTOs;
using Entities;

namespace Service
{
    public interface IPasswordService
    {
        Password CheckPasswordStrong(Password password);
        Password CheckPasswordStrong(string password);
        bool IsPasswordStrong(string password, int minStrength = 2);
    }
}