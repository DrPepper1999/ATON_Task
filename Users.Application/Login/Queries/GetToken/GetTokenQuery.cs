using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Application.Login.Queries.GetToken
{
    public class GetTokenQuery : IRequest<TokenVm>
    {
        public string Login { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string JwtKey { get; set; } = String.Empty;
        public string JwtIssuer { get; set; } = String.Empty;
        public string JwtAudience { get; set; } = String.Empty;
    }
}
