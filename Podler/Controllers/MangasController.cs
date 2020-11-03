using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Podler.Models.Mangas;
using Podler.Repositories;

namespace Podler.Controllers
{
    [Route("api/[controller]")]
    public class MangasController : ControllerBase
    {
        private readonly ILogger<MangasController> _logger;

        private readonly IMangasRepository _mangaRepository;

		public MangasController(ILogger<MangasController> logger, IMangasRepository mangaRepository)
		{
		    _logger = logger;
            _mangaRepository = mangaRepository;
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
                return Ok(manga);
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

                if (manga.Cover.Length > 5242880)
                    return BadRequest("A imagem deve ser menor que 5MB.");

                mangaDb = await _mangaRepository.SaveMangaAsync(manga);

                var url = Url.Action("GetById", new { id = mangaDb.Id });

                return Created(url, mangaDb);
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e}");
            }
        }
    }
}
