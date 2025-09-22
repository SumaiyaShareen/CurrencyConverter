using CurrencyConverter.Models;
using CurrencyConverterApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverterApi.Repositories
{
    public class ConversionHistoryRepository : IConversionHistoryRepository
    {
        private readonly CurrencyConverterContext _context;

        public ConversionHistoryRepository(CurrencyConverterContext context) => _context = context;

        public async Task<IEnumerable<ConversionHistory>> GetAllAsync() =>
    await _context.ConversionHistories
        .Include(h => h.FromCurrencyNavigation)
        .Include(h => h.ToCurrencyNavigation)
        .AsNoTracking()
        .ToListAsync();

        public async Task<ConversionHistory?> GetByIdAsync(int id) =>
            await _context.ConversionHistories
                .Include(h => h.FromCurrencyNavigation)
                .Include(h => h.ToCurrencyNavigation)
                .FirstOrDefaultAsync(h => h.HistoryId == id);


        public async Task<ConversionHistory> AddAsync(ConversionHistory history)
        {
            _context.ConversionHistories.Add(history);
            await _context.SaveChangesAsync();
            return history;
        }

        public async Task<bool> UpdateAsync(ConversionHistory history)
        {
            var existing = await _context.ConversionHistories.FindAsync(history.HistoryId);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(history);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var record = await _context.ConversionHistories.FindAsync(id);
            if (record == null) return false;

            _context.ConversionHistories.Remove(record);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
