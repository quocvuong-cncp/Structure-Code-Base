using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contract.Abstractions.Message;
using Domain.Contract.Services.Identity;
using Domain.Contract.Services.Identity;

namespace Domain.Application.Usecases.V1.Queries.Identity;
public class LoginQuery: IQuery<Reponse.LoginResponse>
{
    public string Email { get; set; }
    public string PassWord { get; set; }
}
