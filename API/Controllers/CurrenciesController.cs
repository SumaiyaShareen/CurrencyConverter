using CurrencyConverter.Models;
using CurrencyConverterApi.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyRepository _repo;

        public CurrencyController(ICurrencyRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpGet("{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var result = await _repo.GetByCodeAsync(code);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Currency currency)
        {
            var existing = await _repo.GetByCodeAsync(currency.CurrencyCode);
            if (existing != null)
                return Conflict($"Currency with code '{currency.CurrencyCode}' already exists.");

            var added = await _repo.AddAsync(currency);
            return CreatedAtAction(nameof(GetByCode), new { code = added.CurrencyCode }, added);
        }


        [HttpPut("{code}")]
        public async Task<IActionResult> Update(string code, Currency currency)
        {
            if (code != currency.CurrencyCode) return BadRequest();
            var success = await _repo.UpdateAsync(currency);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            var success = await _repo.DeleteAsync(code);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
