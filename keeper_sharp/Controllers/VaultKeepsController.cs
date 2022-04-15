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
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vks;

        public VaultKeepsController(VaultKeepsService vks)
        {
            _vks = vks;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep data)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                data.CreatorId = user.Id;
                VaultKeep vaultKeep = _vks.Create(data);
                return Ok(vaultKeep);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}