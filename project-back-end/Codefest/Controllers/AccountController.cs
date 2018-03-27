using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;
using Codefest.Models.Account;

namespace Codefest.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            try
            {
                // Logic with Entity Framework to get all available accounts.

                var accounts = new[] 
                {
                    new AccountModel
                    {
                        Name = "Juadissimo"
                    }
                };

                    
                // Example return statement:

                return Ok(new
                {
                    success = true,
                    content = accounts,
                    message = ""
                });
            }
            catch (Exception exception)
            {
                Log.Logger.Information("Something went wrong");

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
