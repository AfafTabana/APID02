using APID02.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;


namespace APID02.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        
        [HttpPost]
        public IActionResult Login(LoginDto _login)
        {
            if (_login.username == "afaf" && _login.password == "123")
            {
                var userdata = new List<Claim>();
                userdata.Add(new Claim("Username", _login.username));
                userdata.Add(new Claim(ClaimTypes.HomePhone, "04821708"));
                userdata.Add(new Claim(ClaimTypes.Country, "Egypt"));

                var key = "welcome to my sercert key Afaf Tabana";
                var secertkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                var signingcer = new SigningCredentials(secertkey, SecurityAlgorithms.HmacSha256);

                var Token = new JwtSecurityToken(
                    claims: userdata,
                    expires : DateTime.Now.AddDays(1),
                    signingCredentials: signingcer

                    ) ;

                var SendedToken = new JwtSecurityTokenHandler().WriteToken(Token); ;


                return Ok(SendedToken);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult getall()
        {
            return Ok("valid");
        }
        
    }
}
