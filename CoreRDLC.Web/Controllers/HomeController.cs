using AspNetCore.Reporting;
using CoreRDLC.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoreRDLC.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Print()
        {
            var dt = new DataTable();
            dt = GetEmpleados();
            string mimetype = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\ReportEmpleados.rdlc";

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("prm", "RDLC Report");
            LocalReport localReport = new LocalReport(path);

            localReport.AddDataSource("dsEmpleados", dt);
            
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimetype);
            return File(result.MainStream, "application/pdf");
        }

        public DataTable GetEmpleados()
        {
            var dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Departamento");
            dt.Columns.Add("Cargo");

            DataRow row;
            for (int i = 100; i < 150; i++)
            {
                row = dt.NewRow();
                row["Id"] = i;
                row["Nombre"] = "Empleado " + i;
                row["Departamento"] = "Sistemas de Información";
                row["Cargo"] = "Desarrollador";
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
