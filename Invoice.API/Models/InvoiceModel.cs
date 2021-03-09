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
        public string SubTotal { get; set; }
        public string DiscountName { get; set; }
        public int DiscountPersentation { get; set; }
        public string Discount { get; set; }
        public string Total { get; set; }
        public List<InvoiceDetail> InvoiceDetail { get; set; }
    }

    public class InvoiceDetail {
        public int InvoiceID { get; set; }
        public string ContentName { get; set; }
        public string Quantity { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
    }
}
