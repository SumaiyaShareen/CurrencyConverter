using CurrencyConverter.Models;

using CurrencyConverterApi.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace CurrencyConverterApi.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly CurrencyConverterContext _context;

        public CurrencyRepository(CurrencyConverterContext context) => _context = context;

        public async Task<IEnumerable<Currency>> GetAllAsync() => await _context.Currencies.ToListAsync();

        public async Task<Currency?> GetByCodeAsync(string code) => await _context.Currencies.FindAsync(code);

        public async Task<Currency> AddAsync(Currency currency)
        {
            _context.Currencies.Add(currency);
            await _context.SaveChangesAsync();
            return currency;
        }

        public async Task<bool> UpdateAsync(Currency currency)
        {
            var existing = await _context.Currencies.FindAsync(currency.CurrencyCode);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(currency);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(string code)
        {
            var record = await _context.Currencies.FindAsync(code);
            if (record == null) return false;

            _context.Currencies.Remove(record);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
