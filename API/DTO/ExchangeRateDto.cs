namespace CurrencyConverter.DTO
{
    public class ExchangeRateDto
    {
        
            public string BaseCurrency { get; set; } = null!;
            public string TargetCurrency { get; set; } = null!;
            public decimal Rate { get; set; }
        
    }
}
