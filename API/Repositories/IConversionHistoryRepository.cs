using CurrencyConverter.Models;


namespace CurrencyConverterApi.Interfaces
{
    public interface IConversionHistoryRepository
    {
        Task<IEnumerable<ConversionHistory>> GetAllAsync();
        Task<ConversionHistory?> GetByIdAsync(int id);
        Task<ConversionHistory> AddAsync(ConversionHistory history);
        Task<bool> UpdateAsync(ConversionHistory history);
        Task<bool> DeleteAsync(int id);
    }
}
