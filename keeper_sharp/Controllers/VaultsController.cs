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
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vs;

        public VaultsController(VaultsService vs)
        {
            _vs = vs;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Vault>> Create([FromBody] Vault data)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                data.CreatorId = user.Id;
                data.Creator = user;

                Vault vault = _vs.Create(data);
                return Ok(vault);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Vault> GetById(int id)
        {
            try
            {
                Vault vault = _vs.GetById(id);
                return Ok(vault);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Vault>> Edit([FromBody] Vault updateData, int id)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();
                updateData.Id = id;
                updateData.CreatorId = user.Id;
                Vault vault = _vs.Edit(updateData);
                return Ok(vault);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Remove(int id)
        {
            try
            {
                Account user = await HttpContext.GetUserInfoAsync<Account>();

                _vs.Remove(id, user.Id);
                return Ok("deleted");
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}