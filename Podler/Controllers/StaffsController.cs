using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Podler.Models;
using Podler.Repositories;

namespace Podler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffsController : ControllerBase
    {
        private readonly IStaffsRepository _staffRepository;

        public StaffsController(IStaffsRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var staffs = await _staffRepository.GetAllAsync();

            return Ok(staffs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var staff = await _staffRepository.GetAsync(id);

            if (staff == null)
                return NotFound("Staff nao encontrado.");

            return Ok(staff);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] Staff staff)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Error Verifique os campos e tente novamente");

                var staffDb = await _staffRepository.GetByTitleAsync(staff.Title);

                if (staffDb != null)
                    return BadRequest("Ja exite um staff com esse nome.");

                await _staffRepository.IncludeAsync(staff);

                var url = Url.Action("GetByIdAsync", new { id = staff.Id });
                
                return Created(url, staff);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromForm] Staff staff)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("Error Verifique os campos e tente novamente");

                var staffDb = await _staffRepository.GetAsync(staff.Id);

                if (staffDb == null)
                    return NotFound("Staff nao encontrado.");

                staffDb.Update(staff);

                await _staffRepository.UpdateAsync(staffDb);

                return Ok("Staff alterado com sucesso.");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal Server Error: {e}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var staffDb = await _staffRepository.GetAsync(id);

            if (staffDb == null)
                return NotFound("Staff nao encontrado");

            await _staffRepository.RemoveAsync(staffDb);

            return NoContent();
        }

    }
}
