using CurrencyConverter.Models;
using CurrencyConverterApi.Interfaces;
using Microsoft.EntityFrameworkCore;

public class ExchangeRateRepository : IExchangeRateRepository
{
    private readonly CurrencyConverterContext _context;

    public ExchangeRateRepository(CurrencyConverterContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ExchangeRate>> GetAllAsync() =>
        await _context.ExchangeRates.Include(e => e.BaseCurrencyNavigation)
                                    .Include(e => e.TargetCurrencyNavigation)
                                    .ToListAsync();

    public async Task<ExchangeRate?> GetByIdAsync(int id) =>
        await _context.ExchangeRates.Include(e => e.BaseCurrencyNavigation)
                                    .Include(e => e.TargetCurrencyNavigation)
                                    .FirstOrDefaultAsync(e => e.RateId == id);

    public async Task<ExchangeRate?> GetRateAsync(string baseCurrency, string targetCurrency) =>
        await _context.ExchangeRates.Include(e => e.BaseCurrencyNavigation)
                                    .Include(e => e.TargetCurrencyNavigation)
                                    .FirstOrDefaultAsync(e => e.BaseCurrency == baseCurrency && e.TargetCurrency == targetCurrency);

    public async Task<ExchangeRate?> GetByCurrenciesAsync(string baseCurrency, string targetCurrency) =>
        await _context.ExchangeRates.FirstOrDefaultAsync(e => e.BaseCurrency == baseCurrency && e.TargetCurrency == targetCurrency);

    public async Task<bool> CurrencyExistsAsync(string currencyCode) =>
        await _context.Currencies.AnyAsync(c => c.CurrencyCode == currencyCode);

    public async Task<ExchangeRate> AddAsync(ExchangeRate rate)
    {
        _context.ExchangeRates.Add(rate);
        await _context.SaveChangesAsync();
        return rate;
    }

    public async Task<bool> UpdateAsync(ExchangeRate rate)
    {
        var existing = await _context.ExchangeRates.FindAsync(rate.RateId);
        if (existing == null) return false;

        existing.Rate = rate.Rate;
        existing.LastUpdated = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.ExchangeRates.FindAsync(id);
        if (existing == null) return false;

        _context.ExchangeRates.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }
}
