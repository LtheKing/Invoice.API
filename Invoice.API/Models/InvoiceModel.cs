using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.API.Models
{
    public class InvoiceModel
    {
        public int ID { get; set; }
        public string NoInvoice { get; set; }
        public string Sender { get; set; }
        public string Customer { get; set; }
        public string CustomerAddress { get; set; }
        public Nullable<DateTime> Date { get; set; }
        public Nullable<DateTime> DueDate { get; set; }
        public string PONumber { get; set; }
        public decimal SubTotal { get; set; }
        public string DiscountName { get; set; }
        public int DiscountPersentation { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
    }
}
