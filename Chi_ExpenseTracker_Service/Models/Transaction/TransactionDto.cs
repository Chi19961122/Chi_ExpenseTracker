using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chi_ExpenseTracker_Service.Models.Transaction
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }

        public int UserId { get; set; }

        public int CategoryId { get; set; }

        public decimal Amount { get; set; }

        public string? Description { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
