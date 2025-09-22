
        namespace CurrencyConverterApi.DTOs
    {
        public class ExchangeRateUpdateDto
        {
            public int RateId { get; set; }
            public string BaseCurrency { get; set; } = null!;
            public string TargetCurrency { get; set; } = null!;
            public decimal Rate { get; set; }
        }
    }



