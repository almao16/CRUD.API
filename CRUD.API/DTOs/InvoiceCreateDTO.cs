using System;
using System.ComponentModel.DataAnnotations;

namespace CRUD.API.DTOs
{

    public class InvoiceCreateDTO
    {
        [Required]
        public DateTime IssuedDate { get; set; }
        [Required]
        public int IdInvoice { get; set; }
        [Required]
        public string Service { get; set; }
        [Required]
        public float Total { get; set; }
        [Required]
        public string InvoiceStatus { get; set; }
        [Required]
        public float Balance { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
    }
}
