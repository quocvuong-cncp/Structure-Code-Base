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
using Domain.Domain.Abstractions.Interface.UnitofWorks;

namespace Domain.Application.Usecases.V1.Queries.Identity;
public class LoginQueryHandler : IQueryHandler<LoginQuery, Reponse.LoginResponse>
{
    private readonly IUnitofWorkEF _unitofWorkEF;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ICacheService _cacheService;

    public LoginQueryHandler(IUnitofWorkEF unitofWorkEF, IJwtTokenService jwtTokenService, ICacheService cacheService)
    {
        _unitofWorkEF = unitofWorkEF;
        _jwtTokenService = jwtTokenService;
        _cacheService = cacheService;
    }
    public async Task<Result<Reponse.LoginResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        //check user
        //check valid pass, user
        var user = await _unitofWorkEF.ProductRepository.FindByIdAsync(Guid.NewGuid());
        IEnumerable<Claim> claims= new List<Claim>();
        var token = _jwtTokenService.GenerateAccessToken(claims);
        var resfreshToken = _jwtTokenService.GenerateRefreshToken();
        var response = new Reponse.LoginResponse(token, resfreshToken, DateTime.Now.AddHours(3));
        Result<Reponse.LoginResponse> result = new(true, null, response);
        await _cacheService.SetAsync<Reponse.LoginResponse>("giang", response);
        return result;

    }
}
