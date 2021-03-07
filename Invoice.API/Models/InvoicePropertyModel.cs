using System.Collections.Generic;

namespace Invoice.API.Models
{
    public class InvoicePropertyModel
    {
        public string NoInvoice { get; set; }
        public List<CustomerModel> Customers { get; set; }
        public List<CustomerPOModel> CustomerPOs { get; set; }
        public List<CurrencyModel> Currencies { get; set; }
        public List<LanguageModel> Language { get; set; }
    }

    public class CustomerModel {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string LogoURl { get; set; }
    }

    public class CustomerPOModel {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public string PONumber { get; set; }
        public string Description { get; set; }
    }

    public class CurrencyModel {
        public int ID { get; set; }
        public string Currency { get; set; }
    }

    public class LanguageModel
    {
        public int ID { get; set; }
        public string Language { get; set; }
    }

}