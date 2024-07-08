using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Application.Abstractions;
using Domain.Contract.Abstractions.Message;
using Domain.Contract.Abstractions.Shared;
using Domain.Contract.Services.Identity;

namespace Domain.Application.Usecases.V1.Queries.Identity;
public class RefreshTokenHandler : IQueryHandler<RefreshTokenQuery, Reponse.LoginResponse>
{
    private readonly ICacheService _cacheService;
    private readonly IJwtTokenService _jwtTokenService;

    public RefreshTokenHandler(ICacheService cacheService, IJwtTokenService jwtTokenService)
    {
        _cacheService = cacheService;
        _jwtTokenService = jwtTokenService;
    }
    public async Task<Result<Reponse.LoginResponse>> Handle(RefreshTokenQuery request, CancellationToken cancellationToken)
    {
        var claimsPrincipal = _jwtTokenService.GetPrincipalFromExpiredToken(request.AccessToken);

        var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
        if (email == null)  throw new Exception("Not Vallid");
        var response = await _cacheService.GetAsync<Reponse.LoginResponse>(email);
        if (response == null)  throw new Exception("Not Vallid");
        if (response.RefreshToken != request.RefreshToken || response.RefreshTokenExpire > DateTime.Now)  throw new Exception("Not Vallid");
        var newAccessToken = _jwtTokenService.GenerateAccessToken(claimsPrincipal.Claims);
        var newRefreshToken = _jwtTokenService.GenerateRefreshToken();
        var newReponse = new Reponse.LoginResponse(newAccessToken, newRefreshToken, DateTime.Now.AddMinutes(5));
        await _cacheService.SetAsync<Reponse.LoginResponse>(email, newReponse);
        return new Result<Reponse.LoginResponse>(true, null, newReponse);
    }
}
