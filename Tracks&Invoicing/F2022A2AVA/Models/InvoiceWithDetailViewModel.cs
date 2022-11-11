using F2022A2AVA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F2022A2AVA.Models
{
    public class InvoiceWithDetailViewModel : InvoiceBaseViewModel
    {
        public InvoiceWithDetailViewModel()
        {
            InvoiceLines = new HashSet<InvoiceLineWithDetailViewModel>();
        }
        public string CustomerFirstName { get; set; }

        public string CustomerLastName { get; set; }

        public string CustomerState { get; set; }

        public string CustomerCountry { get; set; }

        public string CustomerEmployeeFirstName { get; set; }

        public string CustomerEmployeeLastName { get; set; }

        public IEnumerable<InvoiceLineWithDetailViewModel> InvoiceLines { get; set; }
    }
}