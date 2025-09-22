using CurrencyConverter.Models;
using System.Text.Json.Serialization;

public partial class Currency
{
    public string CurrencyCode { get; set; } = null!;
    public string CurrencyName { get; set; } = null!;
    public string? Symbol { get; set; }
    public bool? IsActive { get; set; }

    [JsonIgnore] // 👈 prevents loop
    public virtual ICollection<ConversionHistory> ConversionHistoryFromCurrencyNavigations { get; set; } = new List<ConversionHistory>();

    [JsonIgnore] // 👈 prevents loop
    public virtual ICollection<ConversionHistory> ConversionHistoryToCurrencyNavigations { get; set; } = new List<ConversionHistory>();

    [JsonIgnore] // 👈 prevents loop
    public virtual ICollection<ExchangeRate> ExchangeRateBaseCurrencyNavigations { get; set; } = new List<ExchangeRate>();

    [JsonIgnore] // 👈 prevents loop
    public virtual ICollection<ExchangeRate> ExchangeRateTargetCurrencyNavigations { get; set; } = new List<ExchangeRate>();
}
