using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace CareConnect.Service.Helpers;

public static class ValidationHelper
{
    public static bool IsPasswordHard(string password)
    {
        // Check if the password is null
        if (password is null) return false;

        // Check if the password is at least 8 characters long
        if (password.Length < 8) return false;

        // Check if the password contains at least one uppercase letter
        if (!password.Any(char.IsUpper)) return false;

        // Check if the password contains at least one lowercase letter
        if (!password.Any(char.IsLower)) return false;

        // Check if the password contains at least one digit
        if (!password.Any(char.IsDigit)) return false;

        // Check if the password contains at least one special character
        if (!password.Any(c => !char.IsLetterOrDigit(c))) return false;

        return true;
    }

    public static bool IsEmailValid(string email)
    {
        if (email is null) return false;

        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, pattern);
    }

    public static bool IsFileValid(IFormFile file)
    {
        if (file is null) return false;

        return true;
    }
}
