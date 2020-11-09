using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Podler.Models;
using Podler.Repositories;

namespace Podler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenresRepository _genresRepository;

        public GenresController(IGenresRepository genresRepository)
        {
            _genresRepository = genresRepository;
        }            

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var genres = await _genresRepository.GetAllAsync();

            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var genre = await _genresRepository.GetAsync(id);

            if (genre != null)
                return Ok(genre);
            
            return NotFound("Genero nao encontrado.");
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] Genre genre)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Error verifique os campos e tente novamente.");

                var genreDb = await _genresRepository.GetByTitleAsync(genre.Title);

                if (genreDb != null)
                    return BadRequest("Ja existe um genero com esse nome.");

                await _genresRepository.IncludeAsync(genre);

                var url = Url.Action("GetByIdAsync", new { id =  genre.Id});
                return Created(url, genre);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromForm] Genre genre)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Error verifique os campos e tente novamente.");

                var genreDb = await _genresRepository.GetAsync(genre.Id);

                if (genreDb == null)
                    return NotFound("Genero nao encontrado.");

                genreDb.Update(genre);

                await _genresRepository.UpdateAsync(genreDb);

                return Ok("Genero alterado com sucesso.");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var genreDb = await _genresRepository.GetAsync(id);

            if (genreDb == null)
                return NotFound("Genero nao encontrado.");

            await _genresRepository.RemoveAsync(genreDb);

            return NoContent();
        }
    }
}
