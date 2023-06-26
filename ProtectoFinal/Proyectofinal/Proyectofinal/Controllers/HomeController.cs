using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyectofinal.Models;
using Proyectofinal.Models.ViewModels;
using System.Diagnostics;
using System.Globalization;


namespace Proyectofinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProyectoFinalDemeterContext _dbcontext;

        public HomeController(ProyectoFinalDemeterContext context)
        {
            _dbcontext = context;
        }

        
        public IActionResult resumenVenta()
        {
            DateTime FechaInicio = DateTime.Now;
            FechaInicio = FechaInicio.AddDays(-5);
            var Lista = (from data in _dbcontext.Ventas.ToList()
                         group data by data.Fecha into gr
                         select new VMVenta
                         {
                             fecha = gr.Key?.ToString("dd/MM/yyyy"), // Convertir la fecha a string
                             cantidad = gr.Count(),

                         });

            return Ok(Lista);

        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}