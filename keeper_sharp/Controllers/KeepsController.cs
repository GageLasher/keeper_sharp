using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keeper_sharp.Models;
using keeper_sharp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keeper_sharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _ks;

        public KeepsController(KeepsService ks)
        {
            _ks = ks;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Keep>> Create([FromBody] Keep data)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                data.CreatorId = user.Id;
                Keep keep = _ks.Create(data);
                return Ok(keep);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}