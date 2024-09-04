using System.Text.Json;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisAsPrimaryDatabase.Data;
using RedisAsPrimaryDatabase.Dtos;
using RedisAsPrimaryDatabase.Models;
using StackExchange.Redis;

namespace RedisAsPrimaryDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController(IPlatformRepositry platformRepositry) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(CreatePlatfromDto dto)
        {
            var platform = dto.Adapt<Platfrom>();
            await platformRepositry.Create(platform);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allPlatForms = await platformRepositry.GetAll();
            var result = allPlatForms.Select(i => i.Adapt<PlatformDto>()).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var platForm = await platformRepositry.GetById(id);
            if (platForm is null)
            {
                return NotFound();
            }
            return Ok(platForm.Adapt<PlatformDto>());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await platformRepositry.Delete(id);
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> Put(UpdatePlatformDto dto)
        {
            var platform = platformRepositry.GetById(dto.Id);
            if (platform is null)
            {
                return NotFound();
            }
            await platformRepositry.Update(new Platfrom()
            {
                Id = dto.Id,
                Name = dto.Name
            });
            return Ok();
        }
    }
}
