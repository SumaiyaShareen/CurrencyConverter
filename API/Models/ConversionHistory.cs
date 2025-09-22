using System;
using System.Collections.Generic;

namespace CurrencyConverter.Models;

public partial class ConversionHistory
{
    public int HistoryId { get; set; }

    public int? UserId { get; set; }

    public string FromCurrency { get; set; } = null!;

    public string ToCurrency { get; set; } = null!;

    public decimal Amount { get; set; }

    public decimal ConvertedAmount { get; set; }

    public decimal RateUsed { get; set; }

    public DateTime? ConversionDate { get; set; }

    public virtual Currency FromCurrencyNavigation { get; set; } = null!;

    public virtual Currency ToCurrencyNavigation { get; set; } = null!;
}
