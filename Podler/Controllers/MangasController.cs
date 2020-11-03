using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Podler.Models.Extensions;
using Podler.Models.Mangas;
using Podler.Repositories;

namespace Podler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MangasController : ControllerBase
    {
        private readonly ILogger<MangasController> _logger;

        private readonly IMangasRepository _mangaRepository;
        private readonly IConfiguration _configuration;

        private int MaxImageLength { get => _configuration.GetValue<int>("Storage:MaxImageLength"); }

        public MangasController(ILogger<MangasController> logger,
                                IMangasRepository mangaRepository,
                                IConfiguration configuration)
		{
		    _logger = logger;
            _mangaRepository = mangaRepository;
            _configuration = configuration;
		}

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var mangas = await _mangaRepository.GetAllAsync();

            return Ok(mangas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var manga = await _mangaRepository.GetAsync(id);

            if (manga != null)
            {
                return Ok(manga.ToApi());
            }

            return NotFound("Manga nao encontrado.");
        }
        
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] MangaUpload manga)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Error verifique os campos e tente novamente.");

                var mangaDb = await _mangaRepository.GetByTitleAsync(manga.Title);

                if (mangaDb != null)
                    return BadRequest("Ja existe um manga com esse nome!");

                if (manga.Cover != null && manga.Cover.Length > MaxImageLength)
                    return BadRequest("A imagem deve ser menor que 5MB.");

                mangaDb = await _mangaRepository.IncludeMangaAsync(manga);

                var url = Url.Action("GetById", new { id = mangaDb.Id });

                return Created(url, mangaDb);
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromForm] MangaUpload manga)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Error verifique os campos e tente novamente.");

                var mangaDb = await _mangaRepository.GetAsync(manga.Id);

                if (mangaDb == null)
                    return BadRequest("Manga nao encontrado.");

                if (manga.Cover != null && manga.Cover.Length > MaxImageLength)
                    return BadRequest($"A imagem deve ser menor que 5MB.");

                await _mangaRepository.UpdateMangaAsync(manga, mangaDb);

                return Ok("Manga alterado com sucesso.");
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var mangaDb = await _mangaRepository.GetAsync(id);

            if (mangaDb != null)
            {
                await _mangaRepository.RemoveAsync(mangaDb);

                return NoContent();
            }

            return BadRequest("Manga não encontrado.");
        }
    }
}
