public class ConversionHistoryDto
{
    public int? UserId { get; set; }
    public string FromCurrency { get; set; } = null!;
    public string ToCurrency { get; set; } = null!;
    public decimal Amount { get; set; }
    public decimal ConvertedAmount { get; set; }
    public decimal RateUsed { get; set; }
}
