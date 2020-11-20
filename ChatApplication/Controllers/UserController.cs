using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApplicationServiceLayer;
using ChatModelLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace ChatApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService userService;   

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }


        // POST api/UserDetails
        [HttpPost]
        public ActionResult<Boolean> Post([FromBody] UserDetails userDetails)
        {
            bool result= userService.AddUser(userDetails);
            if (result)
            {
                return this.Ok(new Response()
                {
                    StateCode =HttpStatusCode.OK,
                    Message = "Registered Successfully"
                });
            }
            else
            {
                return this.BadRequest(new Response()
                {
                    StateCode = HttpStatusCode.BadRequest,
                    Message = "Failed To Registered"
                });

            }

            
        }

        [HttpPost("/login")]
        public ActionResult<String> Login([FromBody] Login login) {
            string result=userService.UserLogin(login);
            if (result.Length>2)
            {
                return this.Ok(new Response()
                {
                    StateCode = HttpStatusCode.OK,
                    Message = "Login Successfully",
                    Data = result
                });
            }
            else
            {
                return this.BadRequest(new Response()
                {
                    StateCode = HttpStatusCode.BadRequest,
                    Message = "Login To Registered"
                });

            }
        }

    }
}
