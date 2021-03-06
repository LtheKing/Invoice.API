using Invoice.API.Helper;
using Invoice.API.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.API.DataAccess
{
    public class InvoiceDA : BaseDA
    {
        public List<InvoiceModel> GetAll(ref string  msg)
        {
            var response = new List<InvoiceModel>();
            var query = string.Format(@"SELECT * FROM KLBInvoice");
            var result = ExecuteQueryWithParam(query, new List<SqlParameter>(), ref msg);
            if (result.Rows.Count > 0)
            {
                response = DataTableHelper.DataTableToList<InvoiceModel>(result);
            }

            return response;
        }
    }
}
