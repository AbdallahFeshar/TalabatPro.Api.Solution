using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TalabatPro.Api.Core.Entities.IdentityModule;
using TalabatPro.Api.Core.Service.Contract;
using TalabatPro.Api.DTOS;
using TalabatPro.Api.Repository.IdentityData;

namespace TalabatPro.Api.Controllers
{
 
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser>signInManager,ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDto dto)
        {
           
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is null)
                return Unauthorized();
         
            var result =await _signInManager.CheckPasswordSignInAsync(user,dto.Password,false);
            if (!result.Succeeded)
                return Unauthorized(); 
          

            var tokenResponse = await _tokenService.CreateTokenAsync(user, _userManager);


            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                AccessToken = tokenResponse.AccessToken,
                RefreshToken = tokenResponse.RefreshToken
            });
        }
      
        
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is not null)
                return BadRequest(new { message = "User with this email already exists." });


            var newUser = new AppUser()
            {
                DisplayName = dto.DispalayName,
                Email = dto.Email,
                UserName = dto.Email.Split('@')[0],
                PhoneNumber = dto.PhoneNumber
            };
            var result = await _userManager.CreateAsync(newUser, dto.Password);
            if (!result.Succeeded)
                return BadRequest();
            // Generate token
            var tokenResponse = await _tokenService.CreateTokenAsync(user, _userManager);
            
            return Ok(new UserDto()
            {
                DisplayName = newUser.DisplayName,
                Email = newUser.Email,
                AccessToken = tokenResponse.AccessToken,
                RefreshToken = tokenResponse.RefreshToken
            });

        }
       


    }
}
