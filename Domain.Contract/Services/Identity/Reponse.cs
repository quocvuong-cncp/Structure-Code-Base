using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contract.Services.Identity;
public static class Reponse
{
    public record LoginResponse(string AccessToken, string RefreshToken, DateTime RefreshTokenExpire);
}
