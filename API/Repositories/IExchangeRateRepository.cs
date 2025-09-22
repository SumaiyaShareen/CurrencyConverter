using CurrencyConverter.Models;

namespace CurrencyConverterApi.Interfaces
{
    public interface IExchangeRateRepository
    {
        Task<IEnumerable<ExchangeRate>> GetAllAsync();
        Task<ExchangeRate?> GetByIdAsync(int id);
        Task<ExchangeRate?> GetRateAsync(string baseCurrency, string targetCurrency);
        Task<ExchangeRate?> GetByCurrenciesAsync(string baseCurrency, string targetCurrency); // NEW
        Task<bool> CurrencyExistsAsync(string currencyCode); // NEW
        Task<ExchangeRate> AddAsync(ExchangeRate rate);
        Task<bool> UpdateAsync(ExchangeRate rate);
        Task<bool> DeleteAsync(int id);
    }
}
