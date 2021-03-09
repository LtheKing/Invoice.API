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

        public InvoicePropertyModel GetAllProps(ref string msg)
        {
            var response = new InvoicePropertyModel();

            var queryCustomer = string.Format(@"SELECT * FROM KLBCustomer");
            var msgCustomer = string.Empty;
            var dtCustomers = ExecuteQueryWithParam(queryCustomer, new List<SqlParameter>(), ref msgCustomer);
            var customers = DataTableHelper.DataTableToList<CustomerModel>(dtCustomers);

            if (msgCustomer.Length > 0) {
                msg = msgCustomer;
                return response;
            }

            var querypo = string.Format(@"SELECT * FROM KLBCustomerPO");
            var msgpo = string.Empty;
            var dtPos = ExecuteQueryWithParam(querypo, new List<SqlParameter>(), ref msgpo);
            var pos = DataTableHelper.DataTableToList<CustomerPOModel>(dtPos);

            if (msgpo.Length > 0)
            {
                msg = msgpo;
                return response;
            }

            var queryCurrency = string.Format(@"SELECT * FROM KLBCurrency");
            var msgCurrency = string.Empty;
            var dtCurrencys = ExecuteQueryWithParam(queryCurrency, new List<SqlParameter>(), ref msgCurrency);
            var currencies = DataTableHelper.DataTableToList<CurrencyModel>(dtCurrencys);

            if (msgCurrency.Length > 0)
            {
                msg = msgCurrency;
                return response;
            }

            var queryLanguage = string.Format(@"SELECT * FROM KLBLanguage");
            var msgLanguage = string.Empty;
            var dtLanguages = ExecuteQueryWithParam(queryLanguage, new List<SqlParameter>(), ref msgLanguage);
            var language = DataTableHelper.DataTableToList<LanguageModel>(dtLanguages);

            if (msgLanguage.Length > 0)
            {
                msg = msgLanguage;
                return response;
            }

            var queryNoInvoice = string.Format(@"
                DECLARE 
                @newInvoiceNo VARCHAR(10),
                @oldInvoiceNo VARCHAR(10),
                @num VARCHAR(6),
                @seq INT;

                BEGIN TRY

                    SET @oldInvoiceNo = (select TOP 1 NoInvoice FROM KLBInvoice ORDER BY NoInvoice DESC);
                    SET @num = (select SUBSTRING(@oldInvoiceNo, 5, len(@oldInvoiceNo)))
                    SET @seq = (select cast(@num as int)) + 1
                    
                        set @newInvoiceNo = CASE WHEN (LEN(@seq) > 0) and (LEN(@seq) < 2) THEN 'INV-00' + CAST(@seq AS VARCHAR(5))
                                            WHEN (LEN(@seq) > 1) and (LEN(@seq) < 3) THEN 'INV-0' + CAST(@seq AS VARCHAR(5))
                                            WHEN (LEN(@seq) > 2) and (LEN(@seq) < 4) THEN 'INV-' + CAST(@seq AS VARCHAR(5))
                                            END 
                        SELECT @newInvoiceNo [NoInvoice]
                END TRY

                BEGIN CATCH
                    SELECT @@ERROR
                END CATCH   
                ");
            var noInvoice = ExecuteScalar(queryNoInvoice);

            response.Customers = customers;
            response.CustomerPOs = pos;
            response.Currencies = currencies;
            response.Language = language;
            response.NoInvoice = noInvoice;

            return response;
        }

        public bool InsertInvoice (InvoiceModel data, ref string msg) 
        {
            var queryInvoiceDetail = string.Empty;

            foreach (var item in data.InvoiceDetail)
            {
                queryInvoiceDetail += string.Format(@"

                    INSERT INTO KLBInvoiceDetail(InvoiceID, ContentName, Quantity, Rate, Amount)
                                                VALUES(@newId, '{0}', {1}, {2}, {3})

                ", item.ContentName, item.Quantity, item.Rate, item.Amount);
            }

            var param = new List<SqlParameter>()
            {
                new SqlParameter() { ParameterName = "@NoInvoice", Value = data.NoInvoice},
                new SqlParameter() { ParameterName = "@Sender", Value = data.Sender},
                new SqlParameter() { ParameterName = "@Customer", Value = data.Customer},
                new SqlParameter() { ParameterName = "@CustomerAddress", Value = data.CustomerAddress},
                new SqlParameter() { ParameterName = "@Date", Value = data.Date},
                new SqlParameter() { ParameterName = "@DueDate", Value = data.DueDate},
                new SqlParameter() { ParameterName = "@PONumber", Value = data.PONumber},
                new SqlParameter() { ParameterName = "@SubTotal", Value = data.SubTotal},
                new SqlParameter() { ParameterName = "@DiscountName", Value = data.DiscountName},
                new SqlParameter() { ParameterName = "@DiscountPersentation", Value = data.DiscountPersentation},
                new SqlParameter() { ParameterName = "@Discount", Value = data.Discount},
                new SqlParameter() { ParameterName = "@Total", Value = data.Total},

            };
            var query = string.Format(@"
                DECLARE @newId INT
                BEGIN TRY
                INSERT INTO KLBInvoice (NoInvoice, Sender, Customer, CustomerAddress,
						Date, DueDate, PONumber, SubTotal, DiscountName,
						DiscountPersentation, Discount, Total)
                VALUES (@NoInvoice, @Sender, @Customer, @CustomerAddress,
                        @Date, @DueDate, @PONumber, @SubTotal, @DiscountName,
                        @DiscountPersentation, @Discount, @Total) 

                        SET @newId = @@IDENTITY
                {0}

                SELECT 'COMPLETE'
                END TRY

                BEGIN CATCH
                    SELECT @@ERROR
                END CATCH
            ", queryInvoiceDetail);

            var result = ExecuteQueryWithParam(query, param, ref msg);

            if (msg.Length > 0) {
                return false;
            }

            return true;
        }
    }
}
