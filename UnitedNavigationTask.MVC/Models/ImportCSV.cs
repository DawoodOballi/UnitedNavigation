using UnitedNavigationTask.MVC.Dtos;

namespace UnitedNavigationTask.MVC.Models
{
    public class ImportCSV
    {
        public IFormFile? FormFile { get; set; }
        public IEnumerable<CsvDto>? Records { get; set; }
    }
}
