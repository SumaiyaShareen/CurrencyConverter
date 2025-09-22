using CurrencyConverter.Models;


namespace CurrencyConverterApi.Interfaces
{
    public interface ICurrencyRepository
    {
        Task<IEnumerable<Currency>> GetAllAsync();
        Task<Currency?> GetByCodeAsync(string code);
        Task<Currency> AddAsync(Currency currency);
        Task<bool> UpdateAsync(Currency currency);
        Task<bool> DeleteAsync(string code);
    }
}
