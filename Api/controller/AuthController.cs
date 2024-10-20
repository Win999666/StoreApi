using Api.Common;
using Api.Data;
using Api.Model;
using Api.ModelDto;
using Api.Servise;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;


namespace Api.controller
{
    public class AuthController : StoreController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JwtTokenGenerator jwtTokenGenerator;
        public AuthController(AppDbContext dbContext,
               UserManager<AppUser> userManager,
               RoleManager<IdentityRole> roleManager,
               JwtTokenGenerator jwtTokenGenerator)
               : base(dbContext)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterReqestDto registerReqestDto)
        {
            if (registerReqestDto == null)
            {
                return BadRequest(new ResponceServer
                {
                    IsSucsesful = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = { "некорректная модель запроса" }
                });
            }
            var userFromDb = await dbContext
                .Users
                .FirstOrDefaultAsync(u => u.UserName.ToLower() == registerReqestDto.UserName.ToLower());
            if (userFromDb != null)
            {
                return BadRequest(new ResponceServer
                {
                    IsSucsesful = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = { "такой пользователь есть" }
                });
            }
            var newAppUser = new AppUser
            {
                UserName = registerReqestDto.UserName,
                Email = registerReqestDto.Email,
                NormalizedEmail = registerReqestDto.Email.ToUpper(),
                //FirstName = registerReqestDto.UserName
            };
            var result = await userManager.CreateAsync(newAppUser, registerReqestDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new ResponceServer
                {
                    IsSucsesful = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = { "ошибка регистрации" }
                });
            }
            var newRoleAppUser = registerReqestDto.Role.Equals(
                SharedData.Roles.Admin,
                StringComparison.OrdinalIgnoreCase)
                ? SharedData.Roles.Admin
                : SharedData.Roles.Consimer;
            await userManager.AddToRoleAsync(newAppUser, newRoleAppUser);

            return Ok(new ResponceServer
            {
                StatusCode = HttpStatusCode.OK,
                Result = "регистрация завершена"
      
            });
        }

        [HttpPost]
        public async Task<ActionResult<ResponceServer>> Login([FromBody] LoginReqestDto loginReqestDto)
        {
            var UserFromDb = await dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == loginReqestDto.Email.ToLower());


            if (UserFromDb == null || !await userManager.CheckPasswordAsync(UserFromDb, loginReqestDto.
                Password))
            {
                return BadRequest(new ResponceServer
                {
                    IsSucsesful = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessages = { "такого пользователя нет" }
                });
            }
            var roles = await userManager.GetRolesAsync(UserFromDb);
            var token = jwtTokenGenerator.GenerateJwtToken(UserFromDb, roles);

            return Ok(new ResponceServer
            {
                StatusCode = HttpStatusCode.OK,
                Result = new LoginResponceDto
                {
                    Email = UserFromDb.Email,
                    Token = token
                }
            });
        }
    }
    //    [HttpPost]
    //    public async Task<ActionResult<ResponceServer>> Login(
    //          [FromBody] LoginReqestDto loginRequestDto
    //      )
    //    {
    //        var userFromDb = await dbContext
    //            .Users
    //            .FirstOrDefaultAsync(u => u.Email.ToLower() ==
    //                loginRequestDto.Email.ToLower());

    //        if (userFromDb == null
    //           || !await userManager.CheckPasswordAsync(
    //                userFromDb, loginRequestDto.Password))
    //        {
    //            return BadRequest(new ResponceServer
    //            {
    //                IsSucsesful = false,
    //                StatusCode = HttpStatusCode.BadRequest,
    //                ErrorMessages = { "Такого пользователя нет" }
    //            });
    //        }

    //        var roles = await userManager.GetRolesAsync(userFromDb);
    //        var token = jwtTokenGenerator.GenerateJwtToken(userFromDb, roles);

    //        return Ok(new ResponceServer
    //        {
    //            StatusCode = HttpStatusCode.OK,
    //            Result = new LoginResponceDto
    //            {
    //                Email = userFromDb.Email,
    //                Token = token,
    //            }
    //        });
    //    }
    //}
}


