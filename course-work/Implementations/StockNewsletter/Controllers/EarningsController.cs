using Microsoft.AspNetCore.Mvc;
using StockNewsletter.Services;
using StockNewsletter.Models;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace StockNewsletter.Controllers
{
    [Authorize]
    public class EarningsController : Controller
    {
        private readonly AlphaVantageService _alphaVantageService;
        private readonly ILogger<EarningsController> _logger;

        public EarningsController(AlphaVantageService alphaVantageService, ILogger<EarningsController> logger)
        {
            _alphaVantageService = alphaVantageService;
            _logger = logger;
        }

        public async Task<IActionResult> EarningsCalendar(string symbol)
        {
            var earnings = await _alphaVantageService.GetEarningsCalendarAsync();
            
            if (!string.IsNullOrEmpty(symbol))
            {
                earnings = earnings
                    .Where(e =>
                        (!string.IsNullOrEmpty(e.Symbol) && e.Symbol.Contains(symbol, StringComparison.OrdinalIgnoreCase)) ||
                        (!string.IsNullOrEmpty(e.Name) && e.Name.Contains(symbol, StringComparison.OrdinalIgnoreCase))
                    )
                    .ToList();
            }
            
            return View(earnings);
        }

        public async Task<IActionResult> HistoricalEarnings(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                _logger.LogWarning("HistoricalEarnings called with empty symbol");
                return RedirectToAction(nameof(EarningsCalendar));
            }

            try
            {
                var earnings = await _alphaVantageService.GetHistoricalEarningsAsync(symbol);
                
                if (earnings == null)
                {
                    // Create fallback model when API returns null
                    earnings = new CompanyEarnings
                    {
                        Symbol = symbol,
                        QuarterlyEarnings = new List<QuarterlyEarning>(),
                        AnnualEarnings = new List<AnnualEarning>()
                    };
                }
                
                return View(earnings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in HistoricalEarnings for symbol {Symbol}", symbol);
                
                // Create fallback model on exception
                var fallbackEarnings = new CompanyEarnings
                {
                    Symbol = symbol,
                    QuarterlyEarnings = new List<QuarterlyEarning>(),
                    AnnualEarnings = new List<AnnualEarning>()
                };
                
                return View(fallbackEarnings);
            }
        }
    }
} 