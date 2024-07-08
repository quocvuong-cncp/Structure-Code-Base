using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contract.Abstractions.Message;
using Domain.Contract.Services.Identity;

namespace Domain.Application.Usecases.V1.Queries.Identity;
public class RefreshTokenQuery: IQuery<Reponse.LoginResponse>
{
    public string AccessToken { set; get; }
    public string RefreshToken { set; get; }
    public DateTime RefreshTokenExpire { set; get; }
}
