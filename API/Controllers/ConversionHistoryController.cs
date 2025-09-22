using CurrencyConverter.Models;
using CurrencyConverterApi.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversionHistoryController : ControllerBase
    {
        private readonly IConversionHistoryRepository _repo;

        public ConversionHistoryController(IConversionHistoryRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostConversionHistory([FromBody] ConversionHistoryDto dto)
        {
            var history = new ConversionHistory
            {
                UserId = dto.UserId,
                FromCurrency = dto.FromCurrency,
                ToCurrency = dto.ToCurrency,
                Amount = dto.Amount,
                ConvertedAmount = dto.ConvertedAmount,
                RateUsed = dto.RateUsed,
                ConversionDate = DateTime.UtcNow
            };

            // Save using repository
            var created = await _repo.AddAsync(history);

            return CreatedAtAction(nameof(GetById), new { id = created.HistoryId }, created);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ConversionHistory history)
        {
            if (id != history.HistoryId) return BadRequest();
            var success = await _repo.UpdateAsync(history);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _repo.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
