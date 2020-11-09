using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Podler.Models.Chapters;
using Podler.Repositories;

namespace Podler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChaptersController : ControllerBase
    {
        private readonly IChaptersRepository _chaptersRepository;
        private readonly IMangasRepository _mangasRepository;
        private readonly IConfiguration _configuration;

        private int MaxImageLength { get => _configuration.GetValue<int>("Storage:MaxImageLength"); }

        public ChaptersController(IChaptersRepository chaptersRepository,
                                  IMangasRepository mangasRepository,
                                  IConfiguration configuration)
        {
            _chaptersRepository = chaptersRepository;
            _mangasRepository = mangasRepository;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var chapters = await _chaptersRepository.GetAllAsync();

            return Ok(chapters);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var chapter = await _chaptersRepository.GetAsync(id);

            if (chapter == null)
                return NotFound("Capitulo nao encontrado.");

            return Ok(chapter);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] ChapterUpload chapterUpload)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Error Verifique os campos e tente novamente.");

                if (chapterUpload.Pages.Count < 1)
                    return BadRequest("O capitulo deve ter pelo menos uma imagem.");

                var imagesIsValid = chapterUpload.Pages.All(p => p.Length < MaxImageLength);

                if(!imagesIsValid)
                    return BadRequest("As imagens devem ser menores que 5MB");
                
                var mangaDb = await _mangasRepository.GetAsync(chapterUpload.MangaId);

                if (mangaDb == null)
                    return NotFound("Manga nao foi encontrado para adicionar o capitulo.");

                var chapterDb = await _chaptersRepository.IncludeChapterAsync(chapterUpload, mangaDb);

                var url = Url.Action("GetByIdAsync", new { id = chapterDb.Id });

                return Created(url, chapterDb);
            }
            catch(Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e}");
            }
        }
    }
}
