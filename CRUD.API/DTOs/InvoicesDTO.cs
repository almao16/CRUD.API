using System;

namespace CRUD.API.DTOs
{
    public class InvoicesDTO
    {
        public int IdInvoice { get; set; }
        public DateTime IssuedDate { get; set; }
        public int IdClient { get; set; }
        public string Service { get; set; }
        public float Total { get; set; }
        public string InvoiceStatus { get; set; }
        public float Balance { get; set; }
        public DateTime DueDate { get; set; }
    }
}
