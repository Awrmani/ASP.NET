using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace F2022A2AVA.Controllers
{
    public class InvoicesController : Controller
    {
        private Manager m = new Manager();
        // GET: Invoice
        public ActionResult Index()
        {
            var invoices = m.InvoiceGetAll();
            return View(invoices);
        }

        // GET: Invoice/Details/5
        public ActionResult Details(int? id)
        {
            var invoice = m.InvoiceGetByIdWithDetail(id.GetValueOrDefault());
            if (invoice == null)
                return HttpNotFound();
            return View(invoice);
        }
    }
}
