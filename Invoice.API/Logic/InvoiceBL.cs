﻿using Invoice.API.DataAccess;
using Invoice.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.API.Logic
{
    public class InvoiceBL
    {
        public GenericResponseModel<List<InvoiceModel>> GetAll() 
        {
            var response = new GenericResponseModel<List<InvoiceModel>>();

            try
            {
                string msg = string.Empty;
                var da = new InvoiceDA();
                response.Value = da.GetAll(ref msg);

                if (msg.Length > 0)
                {
                    response.ErrorMessage = "Failed Get Data";
                    response.WarningMessage = msg;
                    return response;
                }

                response.Status = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.ToString();
            }

            return response;
        }

        public GenericResponseModel<InvoicePropertyModel> GetAllProps() 
        {
            var response = new GenericResponseModel<InvoicePropertyModel>();

            try
            {
                string msg = string.Empty;
                var da = new InvoiceDA();
                response.Value = da.GetAllProps(ref msg);

                if (msg.Length > 0)
                {
                    response.ErrorMessage = "Failed Get Data";
                    response.WarningMessage = msg;
                    return response;
                }

                response.Status = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.ToString();
            }

            return response;
        }
    }
}
