using CarManufacturers.Data;
using CarManufacturers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Collections;

namespace CarManufacturers.Controllers___Copia
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly ModelDbContext _context;
        public ModelsController(ModelDbContext context) => _context = context;

        //listamos todos los modelos
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<Model>> Get_Models()
            => await _context.Models.ToListAsync();

        //listamos un modelo por ID
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Model), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetModelByID(int id)
        {
            var model = await _context.Models.FindAsync(id);
            return model == null ? NotFound() : Ok(model);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateModel(Model model)
        {
            await _context.Models.AddAsync(model);
            await _context.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateModel(int id, Model model)
        {
            if (id != model.Id) return BadRequest();
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteModel(int id)
        {
            var model = await _context.Models.FindAsync(id);
            if (model == null) return NotFound();

            _context.Models.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
