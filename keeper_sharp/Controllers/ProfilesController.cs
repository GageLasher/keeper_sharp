using System.Collections.Generic;
using keeper_sharp.Models;
using keeper_sharp.Services;
using Microsoft.AspNetCore.Mvc;

namespace keeper_sharp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _ps;
        private readonly KeepsService _ks;
        private readonly VaultsService _vs;

        public ProfilesController(ProfilesService ps, KeepsService ks, VaultsService vs)
        {
            _ps = ps;
            _ks = ks;
            _vs = vs;
        }

        [HttpGet("{id}")]
        public ActionResult<Account> GetById(string id)
        {
            try
            {
                Account profile = _ps.GetById(id);
                return Ok(profile);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}/keeps")]
        public ActionResult<List<Keep>> GetKeepsByProfileId(string id)
        {
            try
            {
                List<Keep> keeps = _ks.GetKeepsByProfileId(id);
                return Ok(keeps);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/vaults")]
        public ActionResult<List<Vault>> GetVaultsByProfileId(string id)
        {
            try
            {
                List<Vault> vaults = _vs.GetVaultsByProfileId(id);
                return Ok(vaults);
            }
            catch (System.Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
