using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StockNewsletter.Models
{
    public class CompanyEarnings
    {
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }
        
        [JsonPropertyName("quarterlyEarnings")]
        public List<QuarterlyEarning>? QuarterlyEarnings { get; set; }
        
        [JsonPropertyName("annualEarnings")]
        public List<AnnualEarning>? AnnualEarnings { get; set; }
    }

    public class QuarterlyEarning
    {
        [JsonPropertyName("fiscalDateEnding")]
        public string? FiscalDateEnding { get; set; }
        
        [JsonPropertyName("reportedEPS")]
        public string? ReportedEPS { get; set; }
        
        [JsonPropertyName("estimatedEPS")]
        public string? EstimatedEPS { get; set; }
        
        [JsonPropertyName("surprise")]
        public string? Surprise { get; set; }
        
        [JsonPropertyName("surprisePercentage")]
        public string? SurprisePercentage { get; set; }
    }

    public class AnnualEarning
    {
        [JsonPropertyName("fiscalDateEnding")]
        public string? FiscalDateEnding { get; set; }
        
        [JsonPropertyName("reportedEPS")]
        public string? ReportedEPS { get; set; }
    }
} 