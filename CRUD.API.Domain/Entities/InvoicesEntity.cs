using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.API.Domain.Entities
{
   

    public class InvoicesEntity
    {
        public int IdInvoice { get; set; }
        public DateTime IssuedDate { get; set; }
        public int IdClient { get; set; }
        public string Service { get; set; }
        public float Total { get; set; }
        public string invoiceStatus { get; set; }
        public float Balance { get; set; }
        public DateTime DueDate { get; set; }
        public int TotalRows { get; set; }
    }
}
