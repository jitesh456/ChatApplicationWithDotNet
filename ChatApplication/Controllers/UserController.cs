using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApplicationServiceLayer;
using ChatModelLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
        public void Post([FromBody] UserDetails userDetails)
        {
            userService.AddUser(userDetails);
        }

    }
}
