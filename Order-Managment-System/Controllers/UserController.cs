using Core.Models.IdentityServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Order_Managment_System.Dtos;
using Order_Managment_System.ErrorResponse;

namespace Order_Managment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthService authService;
        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IAuthService authService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.authService = authService;
           
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = new AppUser
            {
               UserName = registerDto.Email.Split('@')[0],
                Email = registerDto.Email,
                PhoneNumber = registerDto.Phone
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
           
            if (result.Succeeded && registerDto.Password == registerDto.ConfirmPassword)
            {
                return new UserDto
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    Token = await authService.CreateTokenAsync(user,_userManager)
                };
            }
            return BadRequest(new ApiException(500,"Failed to Register Account...Please Try Again"));
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized(new ApiException(401 , "This Account is Not Found"));
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (result.Succeeded)
            {
                return new UserDto
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    Token = await authService.CreateTokenAsync(user, _userManager)
                };
            }
            return Unauthorized(new ApiException(401 ,"Invalid Email Or Password....Please Check Ur Account "));
        }

    }
}
