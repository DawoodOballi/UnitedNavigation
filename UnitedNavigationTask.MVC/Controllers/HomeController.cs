using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using UnitedNavigationTask.MVC.Dtos;
using UnitedNavigationTask.MVC.Handlers;
using UnitedNavigationTask.MVC.Models;

namespace UnitedNavigationTask.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Import(ImportCSV model)
        {
            if(model.FormFile == null || !ModelState.IsValid)
            {
                return View("Error");
            }

            var filePath = FileHandler.SaveFile(model.FormFile, "/Uploads/csv");

            var isValidExtension = FileHandler.isFileExtensionValid(model.FormFile, out var errors);
            if(!isValidExtension)
            {
                foreach (var error in errors)
                    ModelState.AddModelError("", error);
                System.IO.File.Delete(filePath);
                return View(model);
            }

            var fieldsToPrint = FileHandler.ValidateFileFields(filePath, out errors);
            if(errors.Count > 0)
            {
                foreach (var error in errors)
                    ModelState.AddModelError("", error);
                System.IO.File.Delete(filePath);
                return View(model);
            }
            model.Records = fieldsToPrint;
            return View("Index", model);
        }

        [HttpPost]
        public IActionResult ExportToXML(ImportCSV model)
        {
            if(model == null || !ModelState.IsValid)
            {
                return RedirectToAction("Error");
            }

            // get the latest file that was upload to export
            // It hasnt been made clear on the structure of the XML. Couldnt complete this in time.
            var directory = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\Uploads\\csv\\");
            var filePath = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First().ToString();
            var source = System.IO.File.ReadAllLines(filePath).ToList();
            XElement ord = new XElement("Root",
                from str in source
                let fields = str.Split(',')
                group str by fields[0] into groupedOrders
                select new XElement("Orders", new XAttribute("OrderID", groupedOrders.Key))
            );
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] IEnumerable<CsvDto> csvDtos)
        {
            if (csvDtos.Count() <= 0 || !ModelState.IsValid)
            {
                return RedirectToAction("Error");
            }

            //// get the latest file that was upload to export
            //// It hasnt been made clear on the structure of the XML so I assume that the root will be 'Orders'
            var httpClient = new HttpClient();
            var baseUrl = "https://devaccountsnv.serveftp.net:65001/api";
            var apiHandler = new ApiHandler(csvDtos, httpClient, baseUrl);
            var token = await apiHandler.GetJWTToken("/Jwt");
            var responseIds = await apiHandler.PlaceOrderPost(token, "/PurchaseOrder/Save");
            // Here I intended to return the response Ids to ajax success call and print them out.
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}