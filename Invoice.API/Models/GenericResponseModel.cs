using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.API.Models
{
    public class GenericResponseModel<T>
    {
        public T Value { get; set; }
        public string ErrorMessage { get; set; }
        public string WarningMessage { get; set; }
        public bool Status { get; set; } = false;
    }
}
