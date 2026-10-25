using System;
using System.ComponentModel.DataAnnotations;

namespace WebNetProject386.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the expense title.")]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a category.")]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter the amount.")]
        [Range(0.01, 1000000, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Please select the expense date.")]
        [DataType(DataType.Date)]
        public DateTime ExpenseDate { get; set; } = DateTime.Today;

        [StringLength(300)]
        public string? Description { get; set; }
    }
}