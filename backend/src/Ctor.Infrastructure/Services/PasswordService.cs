using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Interfaces;

namespace Ctor.Infrastructure.Services;
public class PasswordService : IPasswordService
{
    public string GeneratePassword()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        int length = RandomLength();

        var random = new Random();
        var randomString = new string(Enumerable.Repeat(chars, length)
                                                .Select(s => s[random.Next(s.Length)]).ToArray());
        return randomString;
    }

    private int RandomLength()
    {
        var random = new Random();
        return random.Next(7, 15);
    }
}
