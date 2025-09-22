using System;
using System.Collections.Generic;

namespace CurrencyConverter.Models;

public partial class ExchangeRate
{
    public int RateId { get; set; }

    public string BaseCurrency { get; set; } = null!;

    public string TargetCurrency { get; set; } = null!;

    public decimal Rate { get; set; }

    public DateTime? LastUpdated { get; set; }
    
    public virtual Currency BaseCurrencyNavigation { get; set; } = null!;

    public virtual Currency TargetCurrencyNavigation { get; set; } = null!;
}
