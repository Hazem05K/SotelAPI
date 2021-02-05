using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SoTelAPI.Authentication;
using SoTelAPI.Email;
using Microsoft.AspNetCore.Authorization;
using System.Collections;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DigiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class User : Controller
    {

        private ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        public readonly IPasswordHasher<IdentityUser> _passwordHasher;
        private SendMail mail = new SendMail();

        public User(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {

            this.userManager = userManager;
            this.roleManager = roleManager;
            _dbContext = dbContext;

            _configuration = configuration;
        }


        [HttpGet]
        [Route("all-user")]
        public IEnumerable HandleGetAllUsers()
        {

            IEnumerable<ApplicationUser> users = _dbContext.Users.ToList();

            return users;
        }

        [HttpPost]
        [Route("add-user")]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {

            var userExists = await userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

            var confirmationLink = Url.Action("ConfirmEmail", "Authenticate",
                new { userId = user.Id, token = token }, Request.Scheme);


            mail.NewUser(confirmationLink);
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [Route("delete/{id}")]
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {

            var user = await userManager.FindByIdAsync(id);
            int count = await _dbContext.Database.ExecuteSqlCommandAsync($"DELETE FROM [Post] where UserId = {id}");

            if (user == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User not found" });

            }
            else
            {
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return Ok(new Response { Status = "Success", Message = "User deleted !" });
                }

               
                return (IActionResult) result.Errors;
            }
        }
    }
}
