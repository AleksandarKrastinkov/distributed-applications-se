using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockNewsletter.Data;
using StockNewsletter.Models;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using StockNewsletter.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace StockNewsletter.Controllers
{
    [Authorize]
    public class WatchlistController : Controller
    {
        private readonly AppDbContext _context;
        private readonly AlphaVantageService _alphaVantageService;
        private readonly IMemoryCache _cache;
        private readonly ILogger<WatchlistController> _logger;
        
        private const string STOCK_OPTIONS_CACHE_KEY = "stock_options_dropdown";
        private readonly TimeSpan STOCK_OPTIONS_CACHE_DURATION = TimeSpan.FromDays(7);

        public WatchlistController(AppDbContext context, AlphaVantageService alphaVantageService, IMemoryCache cache, ILogger<WatchlistController> logger)
        {
            _context = context;
            _alphaVantageService = alphaVantageService;
            _cache = cache;
            _logger = logger;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (string.IsNullOrEmpty(userIdClaim))
            {
                // For testing, return a default user ID
                return 1;
            }
            
            return int.Parse(userIdClaim);
        }

        private async Task<List<SelectListItem>> GetStockOptionsAsync()
        {
            // Check cache first
            if (_cache.TryGetValue(STOCK_OPTIONS_CACHE_KEY, out List<SelectListItem> cachedOptions))
            {
                _logger.LogInformation("Using cached stock options ({Count} options)", cachedOptions.Count);
                return cachedOptions;
            }

            _logger.LogInformation("Generating fresh stock options from API");
            
            try
            {
                var earnings = await _alphaVantageService.GetEarningsCalendarAsync();
                
                if (earnings == null || !earnings.Any())
                {
                    _logger.LogWarning("No earnings data from API, using fallback stocks");
                    var fallbackOptions = GetFallbackStockOptions();
                    _cache.Set(STOCK_OPTIONS_CACHE_KEY, fallbackOptions, TimeSpan.FromHours(1));
                    return fallbackOptions;
                }

                var stockOptions = earnings
                    .Where(e => !string.IsNullOrEmpty(e.Symbol) && !string.IsNullOrEmpty(e.Name))
                    .GroupBy(e => e.Symbol)
                    .Select(g => g.First())
                    .OrderBy(e => e.Symbol)
                    .Select(e => new SelectListItem
                    {
                        Value = e.Symbol,
                        Text = $"{e.Symbol} - {e.Name}"
                    })
                    .ToList();

                stockOptions.Insert(0, new SelectListItem
                {
                    Value = "",
                    Text = "-- Select a Stock --"
                });

                _logger.LogInformation("Successfully created {Count} stock options", stockOptions.Count);
                
                // Cache the successful result
                _cache.Set(STOCK_OPTIONS_CACHE_KEY, stockOptions, STOCK_OPTIONS_CACHE_DURATION);
                
                return stockOptions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetStockOptionsAsync");
                
                var fallbackOptions = GetFallbackStockOptions("-- API Error - Using Fallback Stocks --");
                _cache.Set(STOCK_OPTIONS_CACHE_KEY, fallbackOptions, TimeSpan.FromHours(1));
                return fallbackOptions;
            }
        }
        
        private List<SelectListItem> GetFallbackStockOptions(string headerText = "-- Select a Stock --")
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "", Text = headerText },
                
                // Major Tech Stocks
                new SelectListItem { Value = "AAPL", Text = "AAPL - Apple Inc." },
                new SelectListItem { Value = "MSFT", Text = "MSFT - Microsoft Corporation" },
                new SelectListItem { Value = "GOOGL", Text = "GOOGL - Alphabet Inc. Class A" },
                new SelectListItem { Value = "GOOG", Text = "GOOG - Alphabet Inc. Class C" },
                new SelectListItem { Value = "AMZN", Text = "AMZN - Amazon.com Inc." },
                new SelectListItem { Value = "TSLA", Text = "TSLA - Tesla Inc." },
                new SelectListItem { Value = "NVDA", Text = "NVDA - NVIDIA Corporation" },
                new SelectListItem { Value = "META", Text = "META - Meta Platforms Inc." },
                new SelectListItem { Value = "NFLX", Text = "NFLX - Netflix Inc." },
                new SelectListItem { Value = "AMD", Text = "AMD - Advanced Micro Devices Inc." },
                new SelectListItem { Value = "INTC", Text = "INTC - Intel Corporation" },
                new SelectListItem { Value = "CRM", Text = "CRM - Salesforce Inc." },
                new SelectListItem { Value = "ORCL", Text = "ORCL - Oracle Corporation" },
                new SelectListItem { Value = "IBM", Text = "IBM - International Business Machines" },
                new SelectListItem { Value = "CSCO", Text = "CSCO - Cisco Systems Inc." },
                new SelectListItem { Value = "ADBE", Text = "ADBE - Adobe Inc." },
                
                // Financial Stocks
                new SelectListItem { Value = "JPM", Text = "JPM - JPMorgan Chase & Co." },
                new SelectListItem { Value = "BAC", Text = "BAC - Bank of America Corp." },
                new SelectListItem { Value = "V", Text = "V - Visa Inc." },
                new SelectListItem { Value = "MA", Text = "MA - Mastercard Inc." },
                
                // Healthcare & Pharma
                new SelectListItem { Value = "JNJ", Text = "JNJ - Johnson & Johnson" },
                new SelectListItem { Value = "PFE", Text = "PFE - Pfizer Inc." },
                new SelectListItem { Value = "UNH", Text = "UNH - UnitedHealth Group Inc." },
                
                // Consumer & Retail
                new SelectListItem { Value = "WMT", Text = "WMT - Walmart Inc." },
                new SelectListItem { Value = "HD", Text = "HD - Home Depot Inc." },
                new SelectListItem { Value = "KO", Text = "KO - Coca-Cola Company" },
                new SelectListItem { Value = "PEP", Text = "PEP - PepsiCo Inc." },
                
                // Energy
                new SelectListItem { Value = "XOM", Text = "XOM - Exxon Mobil Corporation" },
                new SelectListItem { Value = "CVX", Text = "CVX - Chevron Corporation" },
                
                // ETFs
                new SelectListItem { Value = "SPY", Text = "SPY - SPDR S&P 500 ETF Trust" },
                new SelectListItem { Value = "QQQ", Text = "QQQ - Invesco QQQ Trust" }
            };
        }

        public async Task<IActionResult> Index()
        {
            var currentUserId = GetCurrentUserId();
            var userWatchLists = await _context.WatchLists
                .Where(w => w.UserId == currentUserId)
                .ToListAsync();
            return View(userWatchLists);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUserId = GetCurrentUserId();
            var watchList = await _context.WatchLists
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == currentUserId);
            
            if (watchList == null)
            {
                return NotFound();
            }

            return View(watchList);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.StockOptions = await GetStockOptionsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StockSymbol,Notes")] WatchList watchList)
        {
            if (ModelState.IsValid)
            {
                watchList.DateAdded = DateTime.Now;
                watchList.UserId = GetCurrentUserId();
                
                _context.Add(watchList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.StockOptions = await GetStockOptionsAsync();
            return View(watchList);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUserId = GetCurrentUserId();
            var watchList = await _context.WatchLists
                .FirstOrDefaultAsync(w => w.Id == id && w.UserId == currentUserId);
            
            if (watchList == null)
            {
                return NotFound();
            }

            ViewBag.StockOptions = await GetStockOptionsAsync();
            return View(watchList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,StockSymbol,Notes,DateAdded")] WatchList watchList)
        {
            if (id != watchList.Id)
            {
                return NotFound();
            }

            var currentUserId = GetCurrentUserId();
            if (watchList.UserId != currentUserId)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(watchList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WatchListExists(watchList.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.StockOptions = await GetStockOptionsAsync();
            return View(watchList);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUserId = GetCurrentUserId();
            var watchList = await _context.WatchLists
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == currentUserId);
            
            if (watchList == null)
            {
                return NotFound();
            }

            return View(watchList);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var currentUserId = GetCurrentUserId();
            var watchList = await _context.WatchLists
                .FirstOrDefaultAsync(w => w.Id == id && w.UserId == currentUserId);
            
            if (watchList != null)
            {
                _context.WatchLists.Remove(watchList);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool WatchListExists(int id)
        {
            var currentUserId = GetCurrentUserId();
            return _context.WatchLists.Any(e => e.Id == id && e.UserId == currentUserId);
        }
    }
}
