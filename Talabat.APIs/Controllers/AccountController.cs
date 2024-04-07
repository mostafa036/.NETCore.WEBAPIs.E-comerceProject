using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Extenstions;
using Talabat.APIs.Services;
using Talabat.Core.Entites.Identity;

namespace Talabat.APIs.Controllers
{

    public class AccountController : BaseAPIsController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenServices _tokenServices;
        private readonly IMapper _mapper;

        public AccountController( UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenServices tokenServices,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenServices = tokenServices;
            _mapper = mapper;
        }
        
        [HttpPost("login")]  
        public async Task<ActionResult<UserDto>> login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized(new ApiResponese(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponese(401));

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenServices.CreatedToken(user,_userManager),
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDtos registerDtos)
        {
            if(CheackEmailExists(registerDtos.Email).Result.Value)
             return BadRequest(new ApiValidtionErrorResponse() { Errors = new[] {" This Email is already  Exists "}});

            var user = new AppUser()
            {
                DisplayName = registerDtos.DisplayName,
                Email = registerDtos.Email,
                PhoneNumber = registerDtos.PhoneNumber,
                UserName = registerDtos.Email.Split("@")[0]
            };
            var result = await _userManager.CreateAsync(user , registerDtos.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponese(400));
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenServices.CreatedToken(user,_userManager),
            });
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetcurentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenServices.CreatedToken(user, _userManager)
            });
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto updateAddress)
        {
            var address = _mapper.Map<AddressDto , Address>(updateAddress);

            var appUser = await _userManager.finduserwithaddressbeemailasync(User);

            appUser.Address = address;

            var result = await _userManager.UpdateAsync(appUser);

            if (!result.Succeeded) return BadRequest(new  ApiResponese(400 , "error update address "));

            return Ok(_mapper.Map<Address , AddressDto>(appUser.Address));
        }

        [Authorize, HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetAddress()
        {
            var appUser = await _userManager.finduserwithaddressbeemailasync(User);
            //var email = User.FindFirstValue(ClaimTypes.Email);
            //var appUser = await _userManager.FindByEmailAsync(email);
            //if (appUser == null) return BadRequest(new ApiResponese(400)
            return Ok(_mapper.Map<Address, AddressDto>(appUser.Address));
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheackEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;

        }
    }
}
