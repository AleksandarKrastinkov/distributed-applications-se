using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockNewsletter.Models
{
    public class WatchList
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Stock Symbol is required")]
        [StringLength(10, ErrorMessage = "Stock Symbol cannot be longer than 10 characters")]
        public string StockSymbol { get; set; } = string.Empty;

        public string? Notes { get; set; }
        
        public DateTime DateAdded { get; set; } = DateTime.Now;

        // Navigation property - make nullable to avoid validation issues
        public virtual User? User { get; set; }
    }
} 