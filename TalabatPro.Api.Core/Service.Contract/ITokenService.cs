using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatPro.Api.Core.Entities.IdentityModule;

namespace TalabatPro.Api.Core.Service.Contract
{
    public interface ITokenService
    {
        Task<TokenResponse> CreateTokenAsync(AppUser user,UserManager<AppUser>userManager);
    }
}
