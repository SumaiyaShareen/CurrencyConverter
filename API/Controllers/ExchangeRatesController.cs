using CurrencyConverter.DTO;
using CurrencyConverter.Models;
using CurrencyConverterApi.DTOs;
using CurrencyConverterApi.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRateRepository _repo;

        public ExchangeRateController(IExchangeRateRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("rate")]
        public async Task<IActionResult> GetRate([FromQuery] string baseCurrency, [FromQuery] string targetCurrency)
        {
            var rate = await _repo.GetRateAsync(baseCurrency, targetCurrency);
            if (rate == null) return NotFound();
            return Ok(rate);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ExchangeRateDto dto)
        {
            if (dto.BaseCurrency == dto.TargetCurrency)
                return BadRequest("Base and target currencies cannot be the same.");

            var baseExists = await _repo.CurrencyExistsAsync(dto.BaseCurrency);
            var targetExists = await _repo.CurrencyExistsAsync(dto.TargetCurrency);
            if (!baseExists || !targetExists)
                return BadRequest("One or both currencies do not exist.");

            var existing = await _repo.GetByCurrenciesAsync(dto.BaseCurrency, dto.TargetCurrency);
            if (existing != null)
                return Conflict($"Exchange rate from {dto.BaseCurrency} to {dto.TargetCurrency} already exists.");

            var rate = new ExchangeRate
            {
                BaseCurrency = dto.BaseCurrency,
                TargetCurrency = dto.TargetCurrency,
                Rate = dto.Rate,
                LastUpdated = DateTime.UtcNow
            };

            await _repo.AddAsync(rate);

            return CreatedAtAction(nameof(GetById), new { id = rate.RateId }, rate);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ExchangeRateUpdateDto dto)
        {
            if (id != dto.RateId) return BadRequest();

            // Get the existing rate from DB
            var existingRate = await _repo.GetByIdAsync(id);
            if (existingRate == null) return NotFound();

            // Update the fields
            existingRate.BaseCurrency = dto.BaseCurrency;
            existingRate.TargetCurrency = dto.TargetCurrency;
            existingRate.Rate = dto.Rate;
            existingRate.LastUpdated = DateTime.UtcNow;

            var success = await _repo.UpdateAsync(existingRate);
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
