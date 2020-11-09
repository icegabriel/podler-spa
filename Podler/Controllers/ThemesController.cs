using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Podler.Models;
using Podler.Repositories;

namespace Podler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThemesController : ControllerBase
    {
        private readonly IThemesRepository _themesRepository;

        public ThemesController(IThemesRepository themesRepository)
        {
            _themesRepository = themesRepository;
        }            

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var themes = await _themesRepository.GetAllAsync();

            return Ok(themes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var theme = await _themesRepository.GetAsync(id);

            if (theme != null)
                return Ok(theme);
            
            return NotFound("Tema nao encontrado.");
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] Theme theme)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Error verifique os campos e tente novamente.");

                var themeDb = await _themesRepository.GetByTitleAsync(theme.Title);

                if (themeDb != null)
                    return BadRequest("Ja existe um tema com esse nome.");

                await _themesRepository.IncludeAsync(theme);

                var url = Url.Action("GetByIdAsync", new { id =  theme.Id});

                return Created(url, theme);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromForm] Theme theme)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Error verifique os campos e tente novamente.");

                var themeDb = await _themesRepository.GetAsync(theme.Id);

                if (themeDb == null)
                    return NotFound("Tema nao encontrado.");

                themeDb.Update(theme);

                await _themesRepository.UpdateAsync(themeDb);

                return Ok("Tema alterado com sucesso.");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var themeDb = await _themesRepository.GetAsync(id);

            if (themeDb == null)
                return NotFound("Tema nao encontrado.");

            await _themesRepository.RemoveAsync(themeDb);

            return NoContent();
        }
    }
}
