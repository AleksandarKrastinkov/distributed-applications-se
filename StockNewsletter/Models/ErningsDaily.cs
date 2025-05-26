using System;

namespace StockNewsletter.Models
{
    public class EarningsDaily
    {
        public string? Symbol { get; set; }
        public string? Name { get; set; }
        public DateTime? ReportDate { get; set; }
        public string? FiscalDateEnding { get; set; }
        public string? Estimate { get; set; }
        public string? Currency { get; set; }
    }
} 