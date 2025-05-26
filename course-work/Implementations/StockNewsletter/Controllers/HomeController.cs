using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StockNewsletter.Models;
using StockNewsletter.Data;

namespace StockNewsletter.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult StockSearch(string symbol)
    {
        _logger.LogInformation("🔍 StockSearch requested for symbol: {Symbol}", symbol ?? "null");
        
        if (string.IsNullOrWhiteSpace(symbol))
        {
            _logger.LogWarning("⚠️ StockSearch called with empty symbol, redirecting to home");
            TempData["SearchError"] = "Please enter a stock symbol or company name to search.";
            return RedirectToAction(nameof(Index));
        }

        // Clean up the symbol (remove extra spaces, convert to uppercase)
        symbol = symbol.Trim().ToUpperInvariant();
        
        _logger.LogInformation("📡 Redirecting to HistoricalEarnings for symbol: {Symbol}", symbol);
        
        // Redirect to the Historical Earnings page with the symbol
        return RedirectToAction("HistoricalEarnings", "Earnings", new { symbol = symbol });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
